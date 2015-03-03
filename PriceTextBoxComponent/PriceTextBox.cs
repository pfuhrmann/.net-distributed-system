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

            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            // Call the base OnTextChanged method. 
            base.OnTextChanged(e);

            if (!String.IsNullOrEmpty(Text))
            {
                try
                {
                    var value = Double.Parse(Text);

                    if (value > 500.0)
                    {
                        ForeColor = _warningColor;
                    }
                    else
                    {
                        ForeColor = _plainColor;
                    }
                }
                catch (Exception)
                {
                    ForeColor = _warningColor;
                }
            }
        }
    }
}
