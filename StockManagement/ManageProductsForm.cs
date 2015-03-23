﻿using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DataModel;

namespace StockManagement
{
    public partial class ManageProductsForm : Form
    {
        private DbModel _context;

        public ManageProductsForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Initialize DB context
            _context = DbModel.Create();
            _context.Products.Include(m => m.Stocks).Load();
            _context.Warehouses.Load();

            productBindingSource.DataSource =
                _context.Products.Local.ToBindingList();

            // Add Price column
            var priceColumn = new DataGridViewTextBoxColumn();
            var cell = new DataGridViewPriceTextBoxCell();
            priceColumn.DataPropertyName = "Price";
            priceColumn.CellTemplate = cell;
            priceColumn.Name = "Price";
            priceColumn.HeaderText = "Price (£)";
            priceColumn.Width = 80;
            var grid = productDataGridView;
            grid.Columns.Insert(2, priceColumn);

            // Modify Warehouse column in Stocks grid
            var warehouseColumn = DataGridViewComboBoxColumn;
            warehouseColumn.DataPropertyName = "WarehouseId";
            warehouseColumn.DisplayMember = "Name";
            warehouseColumn.ValueMember = "Id";
            InitializeWarehouseDataSource();
        }

        private void ManageProductsForm_Shown(object sender, EventArgs e)
        {
            // Attach events to Products grid view
            productDataGridView.CellValueChanged +=
                generalDataGridView_CellValueChanged;
            productDataGridView.CellFormatting +=
                productDataGridView_CellFormatting;
            productDataGridView.CellValidating +=
                productDataGridView_CellValidating;

            // Attach events to Stocks grid view
            stocksDataGridView.CellValueChanged +=
                generalDataGridView_CellValueChanged;
            stocksDataGridView.CellValidating +=
                stocksDataGridView_CellValidating;
        }

        private void productBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            Validate();

            // Removing disconected entities
            foreach (var stock in _context.Stocks.Local.ToList())
            {
                if (stock.Product == null)
                {
                    _context.Stocks.Remove(stock);
                }

                if (stock.Warehouse == null)
                {
                    _context.Stocks.Remove(stock);
                }
            }

            // Removing placeholder warehouse
            var warehouse = _context.Warehouses.Find(0);
            _context.Warehouses.Remove(warehouse);
            // Save the changes to the database.
            _context.SaveChanges();
            // Reentering placeholder warehouse
            InitializeWarehouseDataSource();

            // Refresh the controls to show the values
            // that were generated by the database.
            stocksDataGridView.Refresh();
            productDataGridView.Refresh();

            // Update status label
            UpdateLabel("Saved!", Color.ForestGreen, true);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // Reset label
            toolStripLabel2.Text = "";

            var text = toolStripTextBox1.Text;
            var filterType = toolStripComboBox1.SelectedIndex;
            switch (filterType)
            {
                // Filter by Name
                case 0:
                    productBindingSource.DataSource =
                        _context.Products.Local
                            .ToBindingList().Where(p => p.Name.Contains(text));
                    break;
                // Filter by ID
                case 1:
                    // No ID provided, return all results
                    if (String.IsNullOrEmpty(text))
                    {
                        productBindingSource.DataSource =
                            _context.Products.Local.ToBindingList();
                        break;
                    }

                    int id;
                    if (Int32.TryParse(text, out id))
                    {
                        productBindingSource.DataSource =
                            _context.Products.Find(id);
                        break;
                    }

                    toolStripLabel2.Text = "ID value must be numeric";
                    break;
            }
        }

        private void generalDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Change in state occured, show it in label
            UpdateLabel("State updated, please save your changes", Color.DarkOrange);
        }

        private void productDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex < 0)
            {
                return;
            }

            // Change Price to red if value is higher than 500
            if (productDataGridView.Columns[e.ColumnIndex].Name == "Price" && e.Value != null)
            {
                var cell = productDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                var value = Double.Parse(e.Value.ToString());
                if (value > 500)
                {
                    cell.Style.ForeColor = Color.Red;
                    return;
                }

                cell.Style.ForeColor = Color.Black;
            }
        }

        private void productDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var errorText = "";

            // Validate value not empty
            if (String.IsNullOrEmpty(e.FormattedValue.ToString()))
            {
                var productId = Int32.Parse(productDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
                if (productId != 0)
                {
                    errorText = "Value cannot be empty";
                    e.Cancel = true;
                }
            }

            // Validate the Weight entry
            if (productDataGridView.Columns[e.ColumnIndex].Name == "Weight")
            {
                int num;
                if (!Int32.TryParse(e.FormattedValue.ToString(), out num))
                {
                    errorText = "Weight value must be numeric";
                    e.Cancel = true;
                }
            }

            // Validate the Box Items entry
            if (productDataGridView.Columns[e.ColumnIndex].Name == "BoxItemsAmount")
            {
                int num;
                if (!Int32.TryParse(e.FormattedValue.ToString(), out num))
                {
                    errorText = "Box Items value must be numeric";
                    e.Cancel = true;
                }
            }

            // Handle validation error
            if (!String.IsNullOrEmpty(errorText))
            {
                productDataGridView.Rows[e.RowIndex].ErrorText = errorText;
                return;
            }
            productDataGridView.Rows[e.RowIndex].ErrorText = "";
        }

        private void stocksDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // Validate value not empty
            if (String.IsNullOrEmpty(e.FormattedValue.ToString()))
            {
                stocksDataGridView.Rows[e.RowIndex].ErrorText = "Value cannot be empty";
                e.Cancel = true;
                return;
            }

            // Validate Quantity entry
            if (stocksDataGridView.Columns[e.ColumnIndex].Name == "Quantity")
            {
                int num;
                if (!Int32.TryParse(e.FormattedValue.ToString(), out num))
                {
                    stocksDataGridView.Rows[e.RowIndex].ErrorText = "Quantity value must be numeric";
                    e.Cancel = true;
                    return;
                }
                stocksDataGridView.Rows[e.RowIndex].ErrorText = "";
            }

            productDataGridView.Refresh();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _context.Dispose();
        }

        private void UpdateLabel(string text, Color color, bool flash = false)
        {
            Invoke((MethodInvoker) delegate
            {
                this.toolStripStatusLabel1.Text = text;
                this.toolStripStatusLabel1.ForeColor = color;
                this.statusStrip1.Refresh();

                if (flash)
                {
                    Thread.Sleep(1000);
                    this.toolStripStatusLabel1.Text = "";
                    this.statusStrip1.Refresh();
                }
            });
        }

        private void InitializeWarehouseDataSource()
        {
            var data = _context.Warehouses.Local.ToBindingList();
            data.Insert(0, new Warehouse {Id = 0, Name = "Select warehouse"});
            var warehouseColumn = DataGridViewComboBoxColumn;
            warehouseColumn.DataSource = data;
        }
    }
}