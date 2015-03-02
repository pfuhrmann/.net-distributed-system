using System.Windows.Forms;
using PriceTextBoxComponent;

namespace StockManagement
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    [
        ComVisible(true),
        ClassInterface(ClassInterfaceType.AutoDispatch)
    ]
    public class DataGridViewPriceTextBoxEditingControl : PriceTextBox, IDataGridViewEditingControl
    {
        private static readonly DataGridViewContentAlignment anyTop = DataGridViewContentAlignment.TopLeft | DataGridViewContentAlignment.TopCenter | DataGridViewContentAlignment.TopRight;
        private static readonly DataGridViewContentAlignment anyRight = DataGridViewContentAlignment.TopRight | DataGridViewContentAlignment.MiddleRight | DataGridViewContentAlignment.BottomRight;
        private static readonly DataGridViewContentAlignment anyCenter = DataGridViewContentAlignment.TopCenter | DataGridViewContentAlignment.MiddleCenter | DataGridViewContentAlignment.BottomCenter;

        private DataGridView dataGridView;
        private bool valueChanged;
        private bool repositionOnValueChange;
        private int rowIndex;

        public DataGridViewPriceTextBoxEditingControl()
            : base()
        {
            this.TabStop = false;
        }

        public virtual DataGridView EditingControlDataGridView
        {
            get
            {
                return this.dataGridView;
            }
            set
            {
                this.dataGridView = value;
            }
        }

        public virtual object EditingControlFormattedValue
        {
            get
            {
                return GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting);
            }
            set
            {
                this.Text = (string)value;
            }
        }

        public virtual int EditingControlRowIndex
        {
            get
            {
                return this.rowIndex;
            }
            set
            {
                this.rowIndex = value;
            }
        }

        public virtual bool EditingControlValueChanged
        {
            get
            {
                return this.valueChanged;
            }
            set
            {
                this.valueChanged = value;
            }
        }

        public virtual Cursor EditingPanelCursor
        {
            get
            {
                return Cursors.Default;
            }
        }

        public virtual bool RepositionEditingControlOnValueChange
        {
            get
            {
                return this.repositionOnValueChange;
            }
        }

        public virtual void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            if (dataGridViewCellStyle.BackColor.A < 255)
            {
                // Our TextBox does not support transparent back colors
                Color opaqueBackColor = Color.FromArgb(255, dataGridViewCellStyle.BackColor);
                this.BackColor = opaqueBackColor;
                this.dataGridView.EditingPanel.BackColor = opaqueBackColor;
            }
            else
            {
                this.BackColor = dataGridViewCellStyle.BackColor;
            }
            this.ForeColor = dataGridViewCellStyle.ForeColor;
            if (dataGridViewCellStyle.WrapMode == DataGridViewTriState.True)
            {
                this.WordWrap = true;
            }
            this.TextAlign = TranslateAlignment(dataGridViewCellStyle.Alignment);
            this.repositionOnValueChange = (dataGridViewCellStyle.WrapMode == DataGridViewTriState.True && (dataGridViewCellStyle.Alignment & anyTop) == 0);
        }

        public virtual bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Right:
                    // If the end of the selection is at the end of the string
                    // let the DataGridView treat the key message
                    if ((this.RightToLeft == RightToLeft.No && !(this.SelectionLength == 0 && this.SelectionStart == this.Text.Length)) ||
                        (this.RightToLeft == RightToLeft.Yes && !(this.SelectionLength == 0 && this.SelectionStart == 0)))
                    {
                        return true;
                    }
                    break;

                case Keys.Left:
                    // If the end of the selection is at the begining of the string
                    // or if the entire text is selected and we did not start editing
                    // send this character to the dataGridView, else process the key event
                    if ((this.RightToLeft == RightToLeft.No && !(this.SelectionLength == 0 && this.SelectionStart == 0)) ||
                        (this.RightToLeft == RightToLeft.Yes && !(this.SelectionLength == 0 && this.SelectionStart == this.Text.Length)))
                    {
                        return true;
                    }
                    break;

                case Keys.Down:
                    // If the end of the selection is on the last line of the text then 
                    // send this character to the dataGridView, else process the key event
                    int end = this.SelectionStart + this.SelectionLength;
                    if (this.Text.IndexOf("\r\n", end) != -1)
                    {
                        return true;
                    }
                    break;

                case Keys.Up:
                    // If the end of the selection is on the first line of the text then 
                    // send this character to the dataGridView, else process the key event
                    if (!(this.Text.IndexOf("\r\n") < 0 || this.SelectionStart + this.SelectionLength < this.Text.IndexOf("\r\n")))
                    {
                        return true;
                    }
                    break;

                case Keys.Home:
                case Keys.End:
                    if (this.SelectionLength != this.Text.Length)
                    {
                        return true;
                    }
                    break;

                case Keys.Prior:
                case Keys.Next:
                    if (this.valueChanged)
                    {
                        return true;
                    }
                    break;

                case Keys.Delete:
                    if (this.SelectionLength > 0 ||
                        this.SelectionStart < this.Text.Length)
                    {
                        return true;
                    }
                    break;

                case Keys.Enter:
                    if ((keyData & (Keys.Control | Keys.Shift | Keys.Alt)) == Keys.Shift && this.Multiline && this.AcceptsReturn)
                    {
                        return true;
                    }
                    break;
            }
            return !dataGridViewWantsInputKey;
        }

        public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return this.Text;
        }

        public virtual void PrepareEditingControlForEdit(bool selectAll)
        {
            if (selectAll)
            {
                SelectAll();
            }
            else
            {
                // Do not select all the text, but
                // position the caret at the end of the text
                this.SelectionStart = this.Text.Length;
            }
        }

        private void NotifyDataGridViewOfValueChange()
        {
            this.valueChanged = true;
            this.dataGridView.NotifyCurrentCellDirty(true);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            // Let the DataGridView know about the value change
            NotifyDataGridViewOfValueChange();
        }


        private static HorizontalAlignment TranslateAlignment(DataGridViewContentAlignment align)
        {
            if ((align & anyRight) != 0)
            {
                return HorizontalAlignment.Right;
            }
            else if ((align & anyCenter) != 0)
            {
                return HorizontalAlignment.Center;
            }
            else
            {
                return HorizontalAlignment.Left;
            }
        }
    }
}

