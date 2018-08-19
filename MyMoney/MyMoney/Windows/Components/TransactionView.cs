using MyMoney.Model.Database;
using MyMoney.Model.Table;
using System.Windows.Forms;

namespace MyMoney.Windows.Components
{

    public class TransactionView
    {

        public RichTextBox date;

        public RichTextBox description;

        public RichTextBox amount;

        public Button delete;

        public Row row;

        public TransactionView(RichTextBox date, RichTextBox description, RichTextBox amount, Button delete)
        {
            this.delete = delete;
            this.date = date;
            this.description = description;
            this.amount = amount;
            this.row = null;

        }

        public void setView(Row row)
        {
            // Set the row to be the same as the specified row.
            this.row = row;

            // If the row is null then all the fields should be empty
            if (row != null)
            {

                // Stores the date of the transaction temporarily so that it may be check to be the correct length.
                string date = row.getValue(CashFlowModel.DATE_COLOUMN);
                if (date.Length > 10)
                {
                    date = date.Remove(10);
                }

                // Set the delete button as enabled
                updateButton(this.delete, true);

                // Update all the values in the text boxes to the ones specified in the paramter row.
                updateBox(this.date, date);
                updateBox(this.description, row.getValue(CashFlowModel.DESCRIPTION_COLOUMN));
                updateBox(this.amount, row.getValue(CashFlowModel.AMOUNT_COLOUMN));
            }
            else
            {
                // Set the delete button as disabled
                updateButton(this.delete, false);

                // Set all the boxes as empty and disabled
                updateBox(this.date, "");
                updateBox(this.description, "");
                updateBox(this.amount, "");


            }
        }

        private void updateButton(Button button, bool enabled)
        {
            if (button.InvokeRequired)
            {
                button.BeginInvoke((MethodInvoker)delegate()
                {
                    button.Enabled = enabled;
                });
            }
            else
            {
                button.Enabled = enabled;
            }
        }

        private void updateBox(RichTextBox box, string item)
        {

            if (box.InvokeRequired)
            {
                box.BeginInvoke((MethodInvoker)delegate()
                {
                    box.Text = item;

                    if (item.Equals(""))
                    {
                        box.Enabled = false;
                    }
                    else
                    {
                        box.Enabled = true;
                    }
                });
            }
            else
            {
                box.Text = item;

                if (item.Equals(""))
                {
                    box.Enabled = false;
                }
                else
                {
                    box.Enabled = true;
                }
            }
        }

        public void enable()
        {
            date.Enabled = true;
            amount.Enabled = true;
            delete.Enabled = true;
            description.Enabled = true;
        }

        public void disable()
        {
            date.Enabled = false;
            amount.Enabled = false;
            delete.Enabled = false;
            description.Enabled = false;
        }

    }
}
