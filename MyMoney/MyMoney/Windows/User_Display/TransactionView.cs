using MyMoney.Model.Database;
using MyMoney.Model.Table;
using System.Windows.Forms;

namespace MyMoney.Windows.User_Display
{
    /// <summary>
    /// An immutable object that encapsultes the components used to display a transaction on the main form.
    /// </summary>
    public class TransactionView
    {

        /// <summary>
        /// The text box that will hold the date of the transaction.
        /// </summary>
        public RichTextBox date;

        /// <summary>
        /// The text box that will hold the description of the transaction.
        /// </summary>
        public RichTextBox description;

        /// <summary>
        /// The text box that will hold the amount of currency that 
        /// was gained or lost through this transaction.
        /// </summary>
        public RichTextBox amount;

        /// <summary>
        /// The button that will be used to delete this transaction.
        /// </summary>
        public Button delete;

        /// <summary>
        /// Stores the data that will be displayed in this transacation view.
        /// </summary>
        public Row row;

        /// <summary>
        /// Constructs a new transaction view.
        /// </summary>
        /// <param name="date">
        /// The text box that will hold the date of the transaction.
        /// </param>
        /// <param name="description">
        /// The text box that will hold the description of the transaction.
        /// </param>
        /// <param name="amount">
        /// The text box that will hold the amount of currency that 
        /// was gained or lost through this transaction.
        /// </param>
        /// <param name="delete">
        /// The button that will be used to delete this transaction.
        /// </param>
        /// </param>
        /// <param name="data">
        /// The data that will be displayed in this transacation view.
        /// </param>
        public TransactionView(RichTextBox date, RichTextBox description, RichTextBox amount, Button delete)
        {
            this.delete = delete;
            this.date = date;
            this.description = description;
            this.amount = amount;
            this.row = null;

        }

        /// <summary>
        /// Displays a specified <see cref="Row"/> in this <see cref="TransactionView"/>.
        /// </summary>
        /// <param name="row">The <see cref="Row"/> to be displayed.</param>
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

        /// <summary>
        /// Updates the enabled state of a specified button to a specified bool state. 
        /// This allows thread safe modification of the button.
        /// </summary>
        /// <param name="button">The button to be modified.</param>
        /// <param name="enabled">The new enabled state of the button.</param>
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

        /// <summary>
        /// Updates the contents of a specified RichTextBox to a specified bool state. 
        /// This allows thread safe modification of the RichTextBox. If the new contents 
        /// are empty then the RichTextBox is disabled and vice versa if it is not.
        /// </summary>
        /// <param name="box">The RichTextBox to be modified.</param>
        /// <param name="item">The new contents of the RichTextBox.</param>
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

        /// <summary>
        /// Enables all the controls in the view.
        /// </summary>
        public void enable()
        {
            date.Enabled = true;
            amount.Enabled = true;
            delete.Enabled = true;
            description.Enabled = true;
        }

        /// <summary>
        /// Disables all the contols in the view.
        /// </summary>
        public void disable()
        {
            date.Enabled = false;
            amount.Enabled = false;
            delete.Enabled = false;
            description.Enabled = false;
        }

    }
}
