using System.Windows.Forms;
using MyMoney.Core.Model;
using MyMoney.Core.Table;


namespace MyMoney.Windows.Components
{

    public class TransactionView
    {

        public readonly RichTextBox Date;

        public readonly RichTextBox Description;

        public readonly RichTextBox Amount;

        public readonly Button Delete;

        public Row Row;

        public TransactionView(RichTextBox date, RichTextBox description, RichTextBox amount, Button delete)
        {
            Delete = delete;
            Date = date;
            Description = description;
            Amount = amount;
            Row = null;

        }

        public void SetView(Row row)
        {
            // Set the row to be the same as the specified row.
            Row = row;

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
                UpdateButton(Delete, true);

                // Update all the values in the text boxes to the ones specified in the paramter row.
                UpdateBox(this.Date, date);
                UpdateBox(Description, row.getValue(CashFlowModel.DESCRIPTION_COLOUMN));
                UpdateBox(Amount, row.getValue(CashFlowModel.AMOUNT_COLOUMN));
            }
            else
            {
                // Set the delete button as disabled
                UpdateButton(Delete, false);

                // Set all the boxes as empty and disabled
                UpdateBox(Date, "");
                UpdateBox(Description, "");
                UpdateBox(Amount, "");


            }
        }

        private void UpdateButton(Button button, bool enabled)
        {
            if (button.InvokeRequired)
            {
                button.BeginInvoke((MethodInvoker)delegate
                {
                    button.Enabled = enabled;
                });
            }
            else
            {
                button.Enabled = enabled;
            }
        }

        private void UpdateBox(RichTextBox box, string item)
        {

            if (box.InvokeRequired)
            {
                box.BeginInvoke((MethodInvoker)delegate
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

        public void Enable()
        {
            Date.Enabled = true;
            Amount.Enabled = true;
            Delete.Enabled = true;
            Description.Enabled = true;
        }

        public void Disable()
        {
            Date.Enabled = false;
            Amount.Enabled = false;
            Delete.Enabled = false;
            Description.Enabled = false;
        }

    }
}
