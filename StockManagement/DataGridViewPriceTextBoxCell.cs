using System;
using System.Windows.Forms;


namespace StockManagement
{
    public class DataGridViewPriceTextBoxCell : DataGridViewTextBoxCell
    {
        // Type of this cell's editing control
        private static Type defaultEditType = typeof(DataGridViewPriceTextBoxEditingControl);

        public override Type EditType
        {
            get
            {
                return defaultEditType;
            }
        }
    }
}
