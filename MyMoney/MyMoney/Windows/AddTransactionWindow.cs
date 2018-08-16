using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MyMoney.Windows.Data_Transfer;
using MyMoney.Database;
using MyMoney.Data_Storage;
using MyMoney.Windows.User_Display;

namespace MyMoney.Windows
{
    /// <summary>
    /// This window is a form that allows the user to input the deatils of a new transtaction.
    /// </summary>
    public partial class AddTransactionWindow : Form, Buffered
    {
        private bool moneyOut;

        /// <summary>
        /// <see cref="Buffered"/> destination of the <see cref="Packet"/>s from this window. This cannot be <code>null</code>. 
        /// </summary>
        private Buffered dataDestination;

        private ToolTipHandler toolTipHandler = new ToolTipHandler();

        /// <summary>
        /// Constructs a new <see cref="AddTransactionWindow"/>
        /// </summary>
        /// <param name="dataDestination">
        /// <see cref="Buffered"/> destination of the <see cref="Packet"/>s from this window. This cannot be <code>null</code>.
        /// </param>
        public AddTransactionWindow(Buffered dataDestination)
        {
            // Check argumants
            if (dataDestination == null)
            {
                throw new NullReferenceException("Must be a valid Buffered window.");
            }

            this.moneyOut = true;

            // Initalise fields and components
            this.dataDestination = dataDestination;
            InitializeComponent();
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
        /// Executes when the window opens, sets the value of <see cref="date"/> to the current time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTransactionWindow_Load(object sender, EventArgs e)
        {
            // Set the date to today's date.
            date.Value = System.DateTime.Now;

            date.MaxDate = System.DateTime.Today;
        }

        /// <summary>
        /// Checks the validity of all the information inputted by the user into the window 
        /// and if it is valid that it is sent back to <see cref="Main_Form"/> to be processed. 
        /// Otherwise a alert is displayed on the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submit_Click(object sender, EventArgs e)
        {

            double amount = 0;

            // If this value remaines fales then a feild was invalid.
            Boolean valid = false;

            try
            {
                // Parse the amount as a double
                amount = Double.Parse(amountTextBox.Text);

                // Check that the description is not empty but is also less than the maximum length of the description.
                if (amount <= 0)
                {
                    toolTipHandler.draw("Error", "Amounts must be positive and greater than zero.", (amountTextBox));
                }
                else if (descriptionTextBox.Text.Equals(""))
                {
                    toolTipHandler.draw("Error", "Please input a description.", descriptionTextBox);
                }
                else if (descriptionTextBox.Text.Length > CashFlow.DESCRIPTION_LENGTH)
                {
                    toolTipHandler.draw("Error", "The description is too long. Please shorten it.", descriptionTextBox);
                }
                else
                {
                    valid = true;
                }

            }
            catch (Exception ex)
            {
                // Must not be a valid double value.
                Console.WriteLine(ex);
                toolTipHandler.draw("Error", "Please input a valid monetary value.", amountTextBox);
            }

            // If all the fields are valid.
            if (valid)
            {
                // transfer data to main form
                transferData();

                Console.WriteLine("Data Sent");

                // Close this window
                this.Close();

            }

        }

        /// <summary>
        /// Parse fields into data that can be sent to <see cref="Main_Form"/>.
        /// </summary>
        private void transferData()
        {

            // Create a new table row to hold the data
            Row newRow = new Row();

            // Add the date to the row
            newRow.addColoumn(CashFlow.DATE_COLOUMN, date.Value.ToShortDateString());

            // Add the description to the row
            newRow.addColoumn(CashFlow.DESCRIPTION_COLOUMN, cleanDescription());

            double amount = Double.Parse(amountTextBox.Text);

            if (moneyOut)
            {
                amount *= -1;
            }

            // Add the amount to the row
            newRow.addColoumn(CashFlow.AMOUNT_COLOUMN, amount.ToString());

            //Add the transaction id to the row
            newRow.addColoumn(CashFlow.TRANSACTION_ID_COLOUMN, "" + CashFlow.getInstance().getAvalaibleTransactionID());

            // Create the packet to be transfered to the new window.
            Packet data = new Packet(this, dataDestination, newRow);

            // Pass the packet to the windows buffer
            dataDestination.recieve(data);

        }

        /// <summary>
        /// Sanitises the Description field to be used in the SQL database.
        /// </summary>
        /// <returns>Sanaties description.</returns>
        private string cleanDescription()
        {

            string description = descriptionTextBox.Text;

            description = description.Replace("'", "");

            return description;
        }

        private void AddTransactionWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void showToggleToolTip(object sender, EventArgs e)
        {
            toolTipHandler.draw("Toggling Income/Outcome", "Click here to toggle whether the transaction is Income or Outcome.", (sender as Button));
        }

        private void showAmountToolTip(object sender, EventArgs e)
        {
            toolTipHandler.draw("Input Amount", "Input the amount of money in the transaction.", amountTextBox);
        }

        private void showDescriptionToolTip(object sender, EventArgs e)
        {
            toolTipHandler.draw("Input Description","Input a descrition for the transaction. Less than 50 characters.",descriptionTextBox);
        }

        private void toggleFlip(object sender, EventArgs e)
        {
            if (toggleButton.Text.Equals(">"))
            {
                toggleButton.Text = "<";
                moneyOut = false;
            }
            else if (toggleButton.Text.Equals("<"))
            {
                toggleButton.Text = ">";
                moneyOut = true;
            }
        }
    }
}
