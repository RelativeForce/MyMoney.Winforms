using System;
using System.Windows.Forms;
using MyMoney.Core;
using System.Linq;

namespace MyMoney.Windows.Components
{

    public class TransactionViewer
    {
        private readonly IController controller;
        private readonly TransactionView[] views;
        private readonly ScrollBar scrollBar;

        private DateTime date;

        public TransactionViewer(IController controller, TransactionView[] views, ScrollBar scrollBar, DateTime DTP)
        {
            this.controller = controller;
            this.views = views;
            this.date = DTP;
            this.scrollBar = scrollBar;
        }

        public void Display(DateTime month)
        {

            date = month;
            ScollBar_changeValue(0);
            Display();

        }

        public void Display()
        {

            // Holds the number of views 
            int numberOfViews = views.Length;

            var startOfMonth = new DateTime(date.Year, date.Month, 1);
            var endOfMonth = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);

            using (var model = controller.Database())
            {

                var transactions = model.Transactions.Where(startOfMonth, endOfMonth).ToArray();

                // Holds the number of transations for the month specified by the date time picker.
                int numberOfTransactions = transactions.Length;

                if (numberOfTransactions != 0)
                {
                    if (scrollBar.Value >= numberOfTransactions)
                    {
                        ScollBar_changeValue(numberOfTransactions - 1);
                    }


                    if (numberOfTransactions > numberOfViews)
                    {
                        ScrollBar_changeMaxValue(numberOfTransactions - numberOfViews);
                    }
                    else
                    {
                        ScrollBar_changeMaxValue(0);
                    }


                }

                // Iterates through the views
                for (int viewIndex = 0; viewIndex < views.Length; viewIndex++)
                {

                    // If there is a transaction avalible for the current view. Otherwise
                    // set the view to display nothing.
                    if (numberOfTransactions > viewIndex)
                    {
                        views[viewIndex].SetView(transactions[scrollBar.Value + viewIndex]);
                    }
                    else
                    {
                        views[viewIndex].SetView(null);
                    }
                }
            }
        }

        public void DeleteTransaction(Button sender)
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

                    using (var model = controller.Database())
                    {

                        var transaction = model.Transactions.Find(t => t.Id.Equals(toDelete.transactionId));

                        var success = model.Transactions.Delete(transaction);

                        if (success)
                        {
                            // As a transaction is being removed move the viewer up the list of transactions.
                            if (scrollBar.Value > 0) scrollBar.Value--;

                            Display();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete transaction!");
                        }
                    }
                }

                Display();
            }
        }

        public string UpdateTransaction(RichTextBox box)
        {
            // If the parameter text box is not a member of any of the 
            // views then this error message is returned.
            string result = "This text box is not a transaction view.";

            using (var model = controller.Database())
            {

                // Iterate through all the views in this transaction viewer.
                foreach (TransactionView view in views)
                {
                    var transaction = model.Transactions.Find(t => t.Id.Equals(view.transactionId));

                    try
                    {

                        if (box.Equals(view.date))
                        {
                            transaction.Date = DateTime.Parse(box.Text);
                            result = "";
                        }

                        else if (box.Equals(view.description))
                        {
                            // Parse box as description
                            transaction.Description = box.Text;
                            result = "";
                        }
                        else if (box.Equals(view.amount))
                        {
                            // Parse box as amount
                            transaction.Amount = double.Parse(box.Text);
                            result = "";
                        }

                        model.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        result = ex.Message;
                    }
                }
            }

            return result;
        }

        private void EnableButton(Button button, Boolean state)
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

        private void ScollBar_changeValue(int newValue)
        {

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

        private void ScrollBar_changeMaxValue(int newValue)
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

        public void Enable()
        {

            foreach (TransactionView view in views)
            {
                view.Enable();
            }

        }

        public void Disable()
        {

            foreach (TransactionView view in views)
            {
                view.Disable();
            }

        }
    }
}
