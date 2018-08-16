using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyMoney.Windows.Data_Transfer;
using MyMoney.Windows.User_Display;
using MyMoney.Data_Storage;
using MyMoney.Database;

namespace MyMoney.Windows
{
    public partial class MonthlyAllowanceChanger : Form, Buffered
    {

        /// <summary>
        /// <see cref="Buffered"/> destination of the <see cref="Packet"/>s from this window. This cannot be <code>null</code>. 
        /// </summary>
        private Buffered dataDestination;

        /// <summary>
        /// Handles all the toolTips behaviours on this form.
        /// </summary>
        private ToolTipHandler toolTipHandler;

        /// <summary>
        /// Constructs an new <see cref="MonthlyAllowanceChanger"/> using a DateTime 
        /// that specifies the inital month the window will display.
        /// </summary>
        /// <param name="dataDestination">A reference to the object the data from this will be sent to.</param>
        /// <param name="date">The inital month the window will display.</param>
        public MonthlyAllowanceChanger(Buffered dataDestination, DateTime date)
        {
            // Check argumants
            if (dataDestination == null)
            {
                throw new NullReferenceException("Must be a valid Buffered window.");
            }

            // Initalise fields and components
            this.dataDestination = dataDestination;

            InitializeComponent();

            this.toolTipHandler = new ToolTipHandler();

            month.Value = date;

            month.MaxDate = System.DateTime.Today;

            updateBudget(date);

        }

        /// <summary>
        /// This is unused as this window is always a child of <see cref="Main_Form"/>
        /// </summary>
        /// <param name="data"></param>
        public void recieve(Packet data)
        {
            // Do nothing as this is a child of the Main Window
        }

        /// <summary>
        /// Handles the user clicking the submit button on the <seealso cref="MonthlyAllowanceChanger"/>.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void submit_Click(object sender, EventArgs e)
        {

            try
            {

                // If the user has inputted a monthly allowance that is different to the current one.
                if (!oldAllowanceBox.Text.Equals(newAllowanceBox.Text))
                {
                    // If the user has not left the new monthly allowance text box empty.
                    if (!(newAllowanceBox.Text.Equals(string.Empty)))
                    {
                        // Parse the new monthly allowance as a double value.
                        double newBudget = Double.Parse(newAllowanceBox.Text);

                        // If the monthly allowance is greater than zero.
                        if (newBudget > 0)
                        {

                            // Create a row that will be added to the budget table.
                            Row newRow = new Row();

                            // Add the monthly allowance an the month it is assigned to.
                            newRow.addColoumn(Budget.MONTH_COLOUMN, (month.Value.Month < 10 ? "0" + month.Value.Month : "" + month.Value.Month) + "" + month.Value.Year);
                            newRow.addColoumn(Budget.AMOUNT_COLOUMN, "" + newBudget);

                            // Add the row to the packet as data.
                            Packet data = new Packet(this, dataDestination, newRow);

                            // Send the data to the main form and close this window.
                            dataDestination.recieve(data);
                            this.Dispose();
                        }
                        else
                        {
                            // Feedback to user.
                            toolTipHandler.draw(
                                "Error",
                                "New Monthly allowance must be greater than zero.",
                                newAllowanceBox
                                );
                        }
                    }
                    else
                    {
                        // Feedback to user.
                        toolTipHandler.draw(
                        "Error",
                        "No new Allowance specified. Please enter a new allowance or 'Cancel'.",
                        newAllowanceBox
                        );

                    }
                }
                else
                {
                    // Feedback to user.
                    toolTipHandler.draw(
                        "Error",
                        "Allowance is unchanged. Please enter a new allowance or 'Cancel'.",
                        newAllowanceBox
                        );


                }
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

        /// <summary>
        /// Handles the user clicking the cancel button which results in closing the form.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// Updates the current budget displayed to reflect the date selected in the date time picker.
        /// </summary>
        /// <param name="date">Date that denotes the budget.</param>
        private void updateBudget(DateTime date)
        {
            // Converts the date into a month code. MM format
            string monthCode = (date.Month < 10 ? "0" + date.Month : "" + date.Month) + "" + date.Year;

            // Retrieves the value of that month budget from the database.
            double currentValue = Budget.getInstance().getBudget(monthCode);

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

        /// <summary>
        /// Recommend a value for the user based on there previous transactions.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void recommendButton_Click(object sender, EventArgs e)
        {
            //TODO: Recommend a allowance based on a saving goal or rate of spendature.
        }

        /// <summary>
        /// When the value in the month date time picker is changed update the budget to reflect the newly specified month.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void month_ValueChanged(object sender, EventArgs e)
        {
            updateBudget(month.Value);
        }

        /// <summary>
        /// Closes the form when the form is closed.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void MonthlyAllowanceChanger_FormClosed(object sender, FormClosedEventArgs e)
        {
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
    }
}
