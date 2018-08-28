using System;
using System.Windows.Forms;
using MyMoney.Core;
using MyMoney.Core.Model;
using MyMoney.Core.Table;
using MyMoney.Windows.Components;

namespace MyMoney.Windows
{
    public partial class MonthlyAllowanceChanger : Form, IView
    {
        private readonly IDataController controller;

        private ToolTipHandler toolTipHandler;

        public MonthlyAllowanceChanger(IDataController controller, DateTime date)
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
                    var ttm = new ToolTipModel("Error",
                         "Allowance is unchanged. Please enter a new allowance or 'Cancel'.",
                         newAllowanceBox);
                    // Feedback to user.
                    toolTipHandler.Draw(ttm

                             );

                    return;
                }

                // If the user has not left the new monthly allowance text box empty.
                if ((newAllowanceBox.Text.Equals(string.Empty)))
                {
                    var ttm = new ToolTipModel("Error",
                     "No new Allowance specified. Please enter a new allowance or 'Cancel'.",
                     newAllowanceBox);
                    // Feedback to user.
                    toolTipHandler.Draw(
                             ttm
                         );

                    return;

                }
                // Parse the new monthly allowance as a double value.
                double newBudget = Double.Parse(newAllowanceBox.Text);

                // If the monthly allowance is greater than zero.
                if (newBudget <= 0)
                {
                    var ttm = new ToolTipModel("Error",
                         "New Monthly allowance must be greater than zero.",
                         newAllowanceBox);
                    // Feedback to user.
                    toolTipHandler.Draw(
                                    ttm
                             );
                    return;
                }

                // Create a row that will be added to the budget table.
                Row newRow = new Row();

                // Add the monthly allowance an the month it is assigned to.
                newRow.addColoumn(BudgetModel.MONTH_COLOUMN, (month.Value.Month < 10 ? "0" + month.Value.Month : "" + month.Value.Month) + "" + month.Value.Year);
                newRow.addColoumn(BudgetModel.AMOUNT_COLOUMN, "" + newBudget);

                controller.Add(newRow, BudgetModel.TABLE_NAME);

                controller.RefreshViews();

                Dispose();


            }
            catch (Exception ex)
            {
                var ttm = new ToolTipModel("Error",
                         ex.Message,
                         newAllowanceBox);
                // Feedback to user.
                toolTipHandler.Draw(
                               ttm
                            );

            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void updateBudget(DateTime date)
        {

            double currentValue = controller.GetMonthlyAllowance(date);

            // If there actually is no value for that month display a dash. Otherwise display the value.
            if (currentValue.Equals(Double.NaN))
            {
                oldAllowanceBox.Text = "-";
            }
            else
            {
                oldAllowanceBox.Text = "" + currentValue;
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

        /// <summary>
        /// Displays the tool tip over the amount text box.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void showAmountToolTip(object sender, EventArgs e)
        {
            var ttm = new ToolTipModel("Input Allowance", "Input a new monthly allowance for the selected month.", newAllowanceBox);
            toolTipHandler.Draw(ttm);
        }

        /// <summary>
        /// Displays the recommend tool tip over the recomend button.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void showRecommendToolTip(object sender, EventArgs e)
        {
            var ttm = new ToolTipModel("Recommend", "Unavailable", recommendButton);
            toolTipHandler.Draw(ttm);
        }

        public void RefreshView()
        {
            // Do nothing
        }
    }
}
