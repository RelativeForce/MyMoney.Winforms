using System.Windows.Forms;
using MyMoney.Core.Model;

namespace MyMoney.Windows.Components
{

    public class TransactionView
    {

        public readonly RichTextBox date;

        public readonly RichTextBox description;

        public readonly RichTextBox amount;

        public readonly Button delete;

        public int transactionId;

        public TransactionView(RichTextBox date, RichTextBox description, RichTextBox amount, Button delete)
        {
            this.delete = delete;
            this.date = date;
            this.description = description;
            this.amount = amount;
            transactionId = -1;

        }

        public void SetView(Transaction transaction)
        {
            // Set the row to be the same as the specified row.
            this.transactionId = transaction?.Id ?? -1;

            // If the row is null then all the fields should be empty
            if (transaction != null)
            {
                // Set the delete button as enabled
                UpdateButton(delete, true);

                // Update all the values in the text boxes to the ones specified in the paramter row.
                UpdateBox(date, transaction.Date.ToShortDateString());
                UpdateBox(description, transaction.Description);
                UpdateBox(amount, transaction.Amount.ToString());
            }
            else
            {
                // Set the delete button as disabled
                UpdateButton(delete, false);

                // Set all the boxes as empty and disabled
                UpdateBox(date, "");
                UpdateBox(description, "");
                UpdateBox(amount, "");
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
            date.Enabled = true;
            amount.Enabled = true;
            delete.Enabled = true;
            description.Enabled = true;
        }

        public void Disable()
        {
            date.Enabled = false;
            amount.Enabled = false;
            delete.Enabled = false;
            description.Enabled = false;
        }

    }
}
