using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyMoney.Database;
using MyMoney.Data_Storage;

namespace MyMoney.Windows.User_Display
{
    /// <summary>
    /// Encapsulates the behaviours of viewing transactions on the 
    /// <see cref="Main_Form"/>. Each transaction on the <see cref="Main_Form"/> 
    /// is displayed using a <see cref="TransactionView"/>, these store referneces 
    /// to the control objects that display the contents of the transaction to the user.
    /// </summary>
    public class TransactionViewer
    {
        /// <summary>
        /// The views that are present on screen.
        /// </summary>
        private TransactionView[] views;

        /// <summary>
        /// The date time picker that denotes which months transaction
        /// should be displayed in the viewer.
        /// </summary>
        private DateTime DTP;

        private ScrollBar scrollBar;

        /// <summary>
        /// Constructs a new <see cref="TransactionViewer"/>.
        /// </summary>
        /// <param name="views">The views that are present on screen.</param>
        /// <param name="upButton">The button that navigates up the list of transactions.</param>
        /// <param name="downButton">The button that navigates down the list of transactions.</param>
        /// <param name="DTP">The date time picker that denotes which months transaction
        /// should be displayed in the viewer.</param>
        public TransactionViewer(TransactionView[] views, ScrollBar scrollBar, DateTime DTP)
        {
            this.views = views;
            this.DTP = DTP;
            this.scrollBar = scrollBar;
        }

        public void display(DateTime month)
        {

            DTP = month;
            scollBar_changeValue(0);
            display();

        }

        /// <summary>
        /// Updates the viewer with the transactions based on the 
        /// index and the month selected in the date time picker.
        /// </summary>
        public void display()
        {

            // Holds the number of views 
            int numberOfViews = views.Length;

            // Holds the transactions of the month specified by the date time picker.
            Row[] cashFlowRows = CashFlow.getInstance().getRows(DTP);

            // Holds the number of transations for the month specified by the date time picker.
            int numberOfTransactions = cashFlowRows.Length;

            if (numberOfTransactions != 0)
            {
                if (scrollBar.Value >= numberOfTransactions)
                {
                    scollBar_changeValue(numberOfTransactions - 1);
                }


                if (numberOfTransactions > numberOfViews)
                {
                    scrollBar_changeMaxValue(numberOfTransactions - numberOfViews);
                }
                else
                {
                    scrollBar_changeMaxValue(0);
                }


            }



            // Iterates through the views
            for (int viewIndex = 0; viewIndex < views.Length; viewIndex++)
            {

                // If there is a transaction avalible for the current view. Otherwise
                // set the view to display nothing.
                if (numberOfTransactions > viewIndex)
                {
                    views[viewIndex].setView(cashFlowRows[scrollBar.Value + viewIndex]);
                }
                else
                {
                    views[viewIndex].setView(null);
                }
            }

        }

        /// <summary>
        /// Removes a transaction specified by the button that is assigned to a view.
        /// </summary>
        /// <param name="sender">The delete button in a view.</param>
        public void deleteTransaction(Button sender)
        {
            // The transaction the contains the transaction to be removed.
            TransactionView toDelete = null;

            // Iterates through all the views.
            foreach (TransactionView view in views)
            {
                // If the parameter button is the same as the button assigned to the cureent view.
                if (view.delete.Equals(sender))
                {
                    toDelete = view;
                    break;
                }
            }

            // If the view assigned to the parameter button was found.
            if (toDelete != null)
            {

                string date = toDelete.date.Text;
                string description = toDelete.description.Text;
                string amount = toDelete.amount.Text;

                // If the view is populated then deleted it from the CashFlowController table.
                if (!date.Equals("") && !description.Equals("") && !amount.Equals(""))
                {

                    CashFlow.getInstance().deleteFrom(int.Parse(toDelete.row.getValue(CashFlow.TRANSACTION_ID_COLOUMN)));

                    // As a transaction is being removed move the viewer up the list of transactions.
                    if (scrollBar.Value > 0) scrollBar.Value--;

                    display();
                }
            }
        }

        /// <summary>
        /// Updates a transaction field value based on a specified rich text box.
        /// </summary>
        /// <param name="box">The rich text box with the updated value inside.</param>
        /// <returns></returns>
        public string updateTransaction(RichTextBox box)
        {
            // If the parameter text box is not a member of any of the 
            // views then this error message is returned.
            string result = "This text box is not a transaction view.";

            // Iterate through all the views in this transaction viewer.
            foreach (TransactionView view in views)
            {
                try
                {
                    int id = int.Parse(view.row.getValue(CashFlow.TRANSACTION_ID_COLOUMN));

                    if (box.Equals(view.date))
                    {
                        // Parse box as date
                        CashFlow.getInstance().updateRow(id, CashFlow.DATE_COLOUMN, box.Text);
                        result = "";

                    }
                    else if (box.Equals(view.description))
                    {
                        // Parse box as description
                        CashFlow.getInstance().updateRow(id, CashFlow.DESCRIPTION_COLOUMN, box.Text);
                        result = "";

                    }
                    else if (box.Equals(view.amount))
                    {
                        // Parse box as amount
                        CashFlow.getInstance().updateRow(id, CashFlow.AMOUNT_COLOUMN, box.Text);
                        result = "";
                    }

                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
            }

            return result;
        }

        /// <summary>
        /// Updates the enables state of a button. This is a thread safe way to modify the state of a button.
        /// </summary>
        /// <param name="button">Button to be modified.</param>
        /// <param name="state">New state of the button.</param>
        private void enableButton(Button button, Boolean state)
        {

            if (button.InvokeRequired)
            {
                button.BeginInvoke((MethodInvoker)delegate()
                {
                    button.Enabled = state;
                });
            }
            else
            {
                button.Enabled = state;
            }

        }

        /// <summary>
        /// Changes the value of the scroll bar.
        /// </summary>
        /// <param name="newValue">New value of the scroll bar.</param>
        private void scollBar_changeValue(int newValue) {

            // Check if the scroll bar reqires invoke.
            if (scrollBar.InvokeRequired)
            {
                scrollBar.BeginInvoke((MethodInvoker)delegate()
                {
                    // Assign new value.
                    scrollBar.Value = newValue;
                });
            }
            else
            {
                // Assign new value.
                scrollBar.Value = newValue;
            }
        
        }

        /// <summary>
        /// Change the max value of the scroll bar.
        /// </summary>
        /// <param name="newValue">New max value of the scroll bar.</param>
        private void scrollBar_changeMaxValue(int newValue)
        {

            if (scrollBar.InvokeRequired)
            {
                scrollBar.BeginInvoke((MethodInvoker)delegate()
                {
                    scrollBar.Maximum = newValue;
                });
            }
            else
            {
                scrollBar.Maximum = newValue;
            }
        }

        /// <summary>
        /// Enables all the veiws in the viewer.
        /// </summary>
        public void enable()
        {

            foreach (TransactionView view in views)
            {
                view.enable();
            }

        }

        /// <summary>
        /// Disables all the views in the viewer.
        /// </summary>
        public void disable()
        {

            foreach (TransactionView view in views)
            {
                view.disable();
            }

        }
    }
}
