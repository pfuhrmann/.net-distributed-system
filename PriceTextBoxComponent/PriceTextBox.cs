using System;
using System.Drawing;
using System.Windows.Forms;

namespace PriceTextBoxComponent
{
    public partial class PriceTextBox : TextBox
    {
        private Color _plainColor = Color.Black;
        private Color _warningColor = Color.Red;

        public PriceTextBox()
        {
            InitializeComponent();

            ForeColor = _plainColor;
        }

        public Color WarningColor
        {
            get { return _warningColor; }
            set { _warningColor = value; }
        }

        public Color PlainColor
        {
            get { return _plainColor; }
            set { _plainColor = value; }
        }

        // Restricts the entry of characters to digits (including hex), the negative sign, 
        // the decimal point, and editing keystrokes (backspace). 
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (Char.IsDigit(e.KeyChar))
            {
                var value = Double.Parse(e.KeyChar.ToString());

                if (value > 500.0)
                {
                    ForeColor = _warningColor;
                }
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
