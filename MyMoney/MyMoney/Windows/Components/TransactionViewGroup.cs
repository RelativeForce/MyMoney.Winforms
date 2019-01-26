using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MyMoney.Core;

namespace MyMoney.Windows.Components
{
    public partial class TransactionViewGroup : UserControl
    {

        public IController Controller {
            private get => controller;
            set {
                controller = value;
                views.ForEach(v => v.Controller = value);
            }
        }

        private IController controller;

        private readonly List<TransactionView> views;
        private DateTime displayMonth;

        public TransactionViewGroup()
        {
            controller = null;
            InitializeComponent();

            views = new List<TransactionView>
            {
                transactionView1,
                transactionView2,
                transactionView3
            };

            displayMonth = DateTime.Now;

            views.ForEach(v => v.ParentView = this );
        }

        public void Display(DateTime month)
        {

            displayMonth = month;
            ChangeScrollBarValue(0);
            Display();

        }

        public void Display()
        {

            // If there is no month to display clear all the views
            if (displayMonth == null)
            {
                views.ForEach(v => v.Clear());
                return;
            }

            // Holds the number of views 
            int numberOfViews = views.Count;

            var startOfMonth = new DateTime(displayMonth.Year, displayMonth.Month, 1);
            var endOfMonth = new DateTime(displayMonth.Year, displayMonth.Month, 1).AddMonths(1).AddDays(-1);

            using (var model = Controller.Database())
            {

                var transactions = model.Transactions.Where(startOfMonth, endOfMonth).ToArray();

                // Holds the number of transations for the month specified by the date time picker.
                int numberOfTransactions = transactions.Length;

                if (numberOfTransactions > 0)
                {
                    scrollBar.Enabled = true;

                    if (scrollBar.Value >= numberOfTransactions)
                    {
                        ChangeScrollBarValue(numberOfTransactions - 1);
                    }

                    if (numberOfTransactions > numberOfViews)
                    {
                        ChangeScrollBarMaxValue(numberOfTransactions - numberOfViews);
                    }
                    else
                    {
                        ChangeScrollBarMaxValue(0);
                    }
                }

                // Iterates through the views
                for (int viewIndex = 0; viewIndex < views.Count; viewIndex++)
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

        public void Clear()
        {
            views.ForEach(t => t.Clear());
            scrollBar.Enabled = false;
        }

        public void ScrollDown() {

            if (!scrollBar.Enabled || scrollBar.Value >= scrollBar.Maximum) return;

            scrollBar.Value++;
            Display();
        }

        public void ScrollUp()
        {
            if (!scrollBar.Enabled || scrollBar.Value == 0) return;

            scrollBar.Value--;
            Display();
        }

        private void ChangeScrollBarValue(int newValue)
        {
            scrollBar.Value = newValue;
        }

        private void ChangeScrollBarMaxValue(int newValue)
        {
            scrollBar.Maximum = newValue;
        }

        private void ScrollBarValueChanged(object sender, EventArgs e)
        {
            Display();
        }
    }
}
