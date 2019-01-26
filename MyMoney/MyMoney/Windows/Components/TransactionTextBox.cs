using System;
using System.Windows.Forms;

namespace MyMoney.Windows.Components
{

    public partial class TransactionTextBox : UserControl
    {
        public bool HasChanged { get; set; }
        public new string Text
        {
            get => textBox.Text;
            private set => textBox.Text = value;
        }

        public new bool Enabled
        {
            get => textBox.Enabled;
            set => textBox.Enabled = value;
        }

        public TransactionView ParentView { private get; set; }

        private string PreviousValue { get; set; }

        public TransactionTextBox()
        {
            InitializeComponent();

            PreviousValue = "";
        }

        public void Revert()
        {
            HasChanged = false;

            if (textBox.InvokeRequired)
            {
                textBox.Invoke((MethodInvoker)delegate
                {
                    textBox.Text = PreviousValue;
                });
            }
            else
            {
                textBox.Text = PreviousValue;
            }
        }

        public void SetText(string text)
        {

            if (textBox.InvokeRequired)
            {
                textBox.Invoke((MethodInvoker)delegate
                {
                    textBox.Text = text;
                    textBox.Enabled = true;
                });
            }
            else
            {
                textBox.Text = text;
                textBox.Enabled = true;
            }

            PreviousValue = text;
            HasChanged = false;

            textBox.Refresh();
        }

        public void ChangesSaved()
        {
            PreviousValue = Text;
            HasChanged = false;
        }

        public void ClearAndDisable()
        {

            if (textBox.InvokeRequired)
            {
                textBox.Invoke((MethodInvoker)delegate
                {
                    textBox.Enabled = false;
                    textBox.Text = "";
                });
            }
            else
            {
                textBox.Enabled = false;
                textBox.Text = "";
            }

            PreviousValue = "";
            HasChanged = false;
        }

        private void SetValueChanged(object sender, EventArgs e)
        {
            HasChanged = true;
        }

        private void ProcessKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Return))
            {
                // Remove new line
                Text = Text.TrimEnd('\n');

                ParentView?.SaveChanges();

                e.Handled = true;
            }
        }
    }
}
