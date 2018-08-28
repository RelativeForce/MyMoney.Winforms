using System;
using System.Windows.Forms;
using MyMoney.Core;
using MyMoney.Core.Model;
using MyMoney.Core.Table;

namespace MyMoney.Windows.Components
{

   public class TransactionViewer
    {

        private TransactionView[] views;

        private DateTime DTP;

        private ScrollBar scrollBar;

        private IDataController controller;

        public TransactionViewer(TransactionView[] views, ScrollBar scrollBar, DateTime DTP, IDataController controller)
        {
            this.views = views;
            this.DTP = DTP;
            this.scrollBar = scrollBar;
            this.controller = controller;
        }

        public void display(DateTime month)
        {

            DTP = month;
            scollBar_changeValue(0);
            display();

        }

        public void display()
        {

            // Holds the number of views 
            int numberOfViews = views.Length;

            var startOfMonth = new DateTime(DTP.Year, DTP.Month, 1);
            var endOfMonth = new DateTime(DTP.Year, DTP.Month, 1).AddMonths(1).AddDays(-1);

            // Get all the transactions of the current month
            Row[] cashFlowRows = controller.GetRows(row => {

                DateTime rowDateTime = DateTime.Parse(row.getValue(CashFlowModel.DATE_COLOUMN));

                return DateTime.Compare(rowDateTime, startOfMonth) >= 0 && DateTime.Compare(rowDateTime, endOfMonth) <= 0;

            }, CashFlowModel.TABLE_NAME);


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

                    controller.Remove(toDelete.row, CashFlowModel.TABLE_NAME);

                    // As a transaction is being removed move the viewer up the list of transactions.
                    if (scrollBar.Value > 0) scrollBar.Value--;

                    display();
                }
            }
        }

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
                    int id = int.Parse(view.row.getValue(CashFlowModel.TRANSACTION_ID_COLOUMN));

                    if (box.Equals(view.date))
                    {

                        view.row.updateColoumn(CashFlowModel.DATE_COLOUMN, box.Text);
                        controller.Update(view.row, CashFlowModel.TABLE_NAME, CashFlowModel.DATE_COLOUMN);
                        
                        result = "";

                    }
                    else if (box.Equals(view.description))
                    {
                        // Parse box as description
                        view.row.updateColoumn(CashFlowModel.DESCRIPTION_COLOUMN, box.Text);
                        controller.Update(view.row, CashFlowModel.TABLE_NAME, CashFlowModel.DESCRIPTION_COLOUMN);
                        result = "";

                    }
                    else if (box.Equals(view.amount))
                    {
                        // Parse box as amount
                        view.row.updateColoumn(CashFlowModel.AMOUNT_COLOUMN, box.Text);
                        controller.Update(view.row, CashFlowModel.TABLE_NAME, CashFlowModel.AMOUNT_COLOUMN);
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

        private void enableButton(Button button, Boolean state)
        {

            if (button.InvokeRequired)
            {
                button.BeginInvoke((MethodInvoker)delegate
                {
                    button.Enabled = state;
                });
            }
            else
            {
                button.Enabled = state;
            }

        }

        private void scollBar_changeValue(int newValue) {

            // Check if the scroll bar reqires invoke.
            if (scrollBar.InvokeRequired)
            {
                scrollBar.BeginInvoke((MethodInvoker)delegate
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

        private void scrollBar_changeMaxValue(int newValue)
        {

            if (scrollBar.InvokeRequired)
            {
                scrollBar.BeginInvoke((MethodInvoker)delegate
                {
                    scrollBar.Maximum = newValue;
                });
            }
            else
            {
                scrollBar.Maximum = newValue;
            }
        }

        public void enable()
        {

            foreach (TransactionView view in views)
            {
                view.enable();
            }

        }

        public void disable()
        {

            foreach (TransactionView view in views)
            {
                view.disable();
            }

        }
    }
}
