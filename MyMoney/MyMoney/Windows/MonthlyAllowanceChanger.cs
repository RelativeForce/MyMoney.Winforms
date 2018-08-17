using System;
using System.Windows.Forms;
using MyMoney.Controllers;
using MyMoney.Model.Database;
using MyMoney.Model.Table;
using MyMoney.Windows.User_Display;

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

            this.toolTipHandler = new ToolTipHandler();

            month.Value = date;

            month.MaxDate = System.DateTime.Today;

            updateBudget(date);

        }

        private void submit_Click(object sender, EventArgs e)
        {

            try
            {

                // If the user has inputted a monthly allowance that is different to the current one.
                if (oldAllowanceBox.Text.Equals(newAllowanceBox.Text))
                {

                    // Feedback to user.
                    toolTipHandler.draw(
                        "Error",
                        "Allowance is unchanged. Please enter a new allowance or 'Cancel'.",
                        newAllowanceBox
                        );

                    return;
                }

                // If the user has not left the new monthly allowance text box empty.
                if ((newAllowanceBox.Text.Equals(string.Empty)))
                {

                    // Feedback to user.
                    toolTipHandler.draw(
                    "Error",
                    "No new Allowance specified. Please enter a new allowance or 'Cancel'.",
                    newAllowanceBox
                    );

                    return;

                }
                // Parse the new monthly allowance as a double value.
                double newBudget = Double.Parse(newAllowanceBox.Text);

                // If the monthly allowance is greater than zero.
                if (newBudget <= 0)
                {
                    // Feedback to user.
                    toolTipHandler.draw(
                        "Error",
                        "New Monthly allowance must be greater than zero.",
                        newAllowanceBox
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
                
                this.Dispose();


            }
            catch (Exception ex)
            {
                // Feedback to user.
                toolTipHandler.draw(
                        "Error",
                        ex.Message,
                        newAllowanceBox
                        );

            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
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

            this.Dispose();
        }

        /// <summary>
        /// Displays the tool tip over the amount text box.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void showAmountToolTip(object sender, EventArgs e)
        {
            toolTipHandler.draw("Input Allowance", "Input a new monthly allowance for the selected month.", newAllowanceBox);
        }

        /// <summary>
        /// Displays the recommend tool tip over the recomend button.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void showRecommendToolTip(object sender, EventArgs e)
        {
            toolTipHandler.draw("Recommend", "Unavailable", recommendButton);
        }

        public void RefreshView()
        {
            // Do nothing
        }
    }
}
