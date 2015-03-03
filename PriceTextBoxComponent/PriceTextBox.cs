using System;
using System.Drawing;
using System.Windows.Forms;

namespace PriceTextBoxComponent
{
    public partial class PriceTextBox : TextBox
    {
        private Color _plainColor = Color.Black;
        public Color PlainColor
        {
            get { return _plainColor; }
            set { _plainColor = value; }
        }

        private Color _warningColor = Color.Red;
        public Color WarningColor
        {
            get { return _warningColor; }
            set { _warningColor = value; }
        }

        public PriceTextBox()
        {
            InitializeComponent();
        }

        // Restricts the entry of characters to digits and controlling keystrokes
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
            base.OnTextChanged(e);

            if (String.IsNullOrEmpty(Text))
            {
                return;
            }

            int price;
            if (!Int32.TryParse(Text, out price))
            {
                ForeColor = _warningColor;
                return;
            }

            if (price > 500)
            {
                ForeColor = _warningColor;
                return;
            }

            ForeColor = _plainColor;
        }
    }
}
