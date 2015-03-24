using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using OrderManagementService;

namespace OrderManagement
{
    public partial class ManagementOrdersForm : Form
    {
        private IOrderManagementService _ordersService;
        private DataGridViewComboBoxColumn _statusBoxColumn;

        public ManagementOrdersForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Get orders from remoting server
            var converterType = typeof (IOrderManagementService);
            _ordersService =
                (IOrderManagementService)
                    Activator.GetObject(converterType, "http://localhost:8001/OrderManagementService.rem");
            orderBindingSource.DataSource = _ordersService.GetOrders();

            // Build data grid view
            var grid = orderDataGridView;
            // Add Id column
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                Name = "Id",
                HeaderText = "#",
                Width = 30,
                ReadOnly = true
            });
            // Add Status column
            _statusBoxColumn = new DataGridViewComboBoxColumn
            {
                DataPropertyName = "Status",
                Name = "Status",
                HeaderText = "Status",
                Width = 100,
                Items = {"Prepared", "Placed", "Awaiting items", "Being packed", "Dispatched", "Delivered"}
            };
            grid.Columns.Add(_statusBoxColumn);
            // Add Created column
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CreatedDateTime",
                Name = "CreatedDateTime",
                HeaderText = "Created",
                Width = 120,
                ReadOnly = true
            });
            // Add Price Total column
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "OrderTotal",
                Name = "OrderTotal",
                HeaderText = "Total (£)",
                Width = 80,
                ReadOnly = true
            });

            grid.EditingControlShowing += dataGridView1_EditingControlShowing;
        }

        // https://msdn.microsoft.com/en-us/library/system.windows.forms.datagridviewcomboboxeditingcontrol.aspx
        private void dataGridView1_EditingControlShowing(object sender,
            DataGridViewEditingControlShowingEventArgs e)
        {
            var combo = e.Control as ComboBox;
            if (combo != null)
            {
                // Remove an existing event-handler, if present, to avoid  
                // adding multiple handlers when the editing control is reused.
                combo.SelectionChangeCommitted -= comboBox_SelectionChangeCommitted;

                // Add the event handler. 
                combo.SelectionChangeCommitted += comboBox_SelectionChangeCommitted;
            }
        }

        private void comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateLabel("Saving...", Color.DarkSalmon);
            // Get info about order
            var currentcell = orderDataGridView.CurrentCellAddress;
            var status = (string) ((ComboBox) sender).SelectedItem;
            int id = (int) orderDataGridView.Rows[currentcell.Y].Cells[0].Value;

            // Update order status
            _ordersService.UpdateOrder(id, status);
            UpdateLabel("Saved", Color.ForestGreen, true);
        }

        private void UpdateLabel(string text, Color color, bool flash = false)
        {
            Invoke((MethodInvoker)delegate
            {
                this.toolStripStatusLabel1.Text = text;
                this.toolStripStatusLabel1.ForeColor = color;
                this.statusStrip1.Refresh();

                Thread.Sleep(150);
                if (flash)
                {
                    this.toolStripStatusLabel1.Text = "";
                    this.statusStrip1.Refresh();
                }
            });
        }
    }
}