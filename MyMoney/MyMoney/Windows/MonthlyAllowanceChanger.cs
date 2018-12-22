using System;
using System.Windows.Forms;
using MyMoney.Core;
using MyMoney.Core.Model;
using MyMoney.Windows.Components;

namespace MyMoney.Windows
{
    public partial class MonthlyAllowanceChanger : Form, IView
    {
        private readonly IController controller;

        private ToolTipHandler toolTipHandler;

        public MonthlyAllowanceChanger(IController controller, DateTime date)
        {
            this.controller = controller;

            controller.AddView(this);

            InitializeComponent();

            toolTipHandler = new ToolTipHandler();

            month.Value = date;

            month.MaxDate = DateTime.Today;

            updateBudget(date);

        }

        private void submit_Click(object sender, EventArgs e)
        {

            try
            {

                // If the user has inputted a monthly allowance that is different to the current one.
                if (oldAllowanceBox.Text.Equals(newAllowanceBox.Text))
                {
                    var ttm = new ToolTipModel(
                        "Error",
                        "Allowance is unchanged. Please enter a new allowance or 'Cancel'.",
                        newAllowanceBox
                    );

                    // Feedback to user.
                    toolTipHandler.Draw(ttm);

                    return;
                }

                // If the user has not left the new monthly allowance text box empty.
                if ((newAllowanceBox.Text.Equals(string.Empty)))
                {
                    var ttm = new ToolTipModel(
                        "Error",
                        "No new Allowance specified. Please enter a new allowance or 'Cancel'.",
                        newAllowanceBox
                     );

                    // Feedback to user.
                    toolTipHandler.Draw(ttm);

                    return;

                }
                // Parse the new monthly allowance as a double value.
                double newBudget = double.Parse(newAllowanceBox.Text);

                // If the monthly allowance is greater than zero.
                if (newBudget <= 0)
                {
                    var ttm = new ToolTipModel(
                        "Error",
                         "New Monthly allowance must be greater than zero.",
                         newAllowanceBox
                    
                    );

                    // Feedback to user.
                    toolTipHandler.Draw(ttm);
                    return;
                }

                using (var model = controller.Database()) {

                    var existingBudget = model.Budgets.Find(month.Value);

                    if (existingBudget == null)
                    {
                        var budget = new Budget(month.Value, newBudget);
                        model.Budgets.Add(budget);
                    }
                    else {
                        existingBudget.Amount = newBudget;
                        model.SaveChanges();

                    }
                }

                controller.RefreshViews();

                Dispose();


            }
            catch (Exception ex)
            {
                var ttm = new ToolTipModel("Error", ex.Message, newAllowanceBox);
                // Feedback to user.
                toolTipHandler.Draw(ttm);

            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void updateBudget(DateTime date)
        {

            using (var model = controller.Database()) {

                var budget = model.Budgets.Find(date);

                // If there actually is no value for that month display a dash. Otherwise display the value.
                if (budget == null)
                {
                    oldAllowanceBox.Text = "-";
                }
                else
                {
                    oldAllowanceBox.Text = budget.Amount.ToString();
                }
            }
        }

        private void recommendButton_Click(object sender, EventArgs e)
        {
            //TODO: Recommend a allowance based on a saving goal or rate of spendature.
        }

        private void month_ValueChanged(object sender, EventArgs e)
        {
            updateBudget(month.Value);
        }


        private void MonthlyAllowanceChanger_FormClosed(object sender, FormClosedEventArgs e)
        {
            controller.RemoveView(this);

            Dispose();
        }

        private void showAmountToolTip(object sender, EventArgs e)
        {
            var ttm = new ToolTipModel("Input Allowance", "Input a new monthly allowance for the selected month.", newAllowanceBox);
            toolTipHandler.Draw(ttm);
        }

        private void showRecommendToolTip(object sender, EventArgs e)
        {
            var ttm = new ToolTipModel("Recommend", "Unavailable", recommendButton);
            toolTipHandler.Draw(ttm);
        }

        public void RefreshView()
        {
            updateBudget(month.Value);
        }
    }
}
