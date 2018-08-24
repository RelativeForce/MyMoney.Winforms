using System;
using System.Windows.Forms;
using MyMoney.Controllers;
using MyMoney.Model.Database;
using MyMoney.Model.Table;
using MyMoney.Windows.Components;

namespace MyMoney.Windows
{

    public partial class AddTransactionWindow : Form, IView
    {
        private bool moneyOut;

        private readonly IDataController controller;

        private ToolTipHandler toolTipHandler = new ToolTipHandler();
 
        public AddTransactionWindow(IDataController controller)
        {
            this.controller = controller;

            controller.AddView(this);

            moneyOut = true;

            InitializeComponent();
        }

        private void AddTransactionWindow_Load(object sender, EventArgs e)
        {
            // Set the date to today's date.
            date.Value = DateTime.Now;

            date.MaxDate = DateTime.Today;

        }

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
                    var ttm = new ToolTipModel("Error", "Amounts must be positive and greater than zero.", (amountTextBox));                         
                    toolTipHandler.Draw(ttm);
                }
                else if (descriptionTextBox.Text.Equals(""))
                {
                    var ttm = new ToolTipModel("Error", "Please input a description.", descriptionTextBox);
                    toolTipHandler.Draw(ttm);
                }
                else if (descriptionTextBox.Text.Length > CashFlowModel.DESCRIPTION_LENGTH)
                {
                    var ttm = new ToolTipModel("Error", "The description is too long. Please shorten it.", descriptionTextBox);
                    toolTipHandler.Draw(ttm);
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
                var ttm = new ToolTipModel("Error", "Please input a valid monetary value.", amountTextBox);
                toolTipHandler.Draw(ttm);
            }

            // If all the fields are valid.
            if (valid)
            {
                // transfer data to main form
                transferData();

                Console.WriteLine("Data Sent");

                // Close this window
                Close();

            }

        }

        private void transferData()
        {

            double amount = double.Parse(amountTextBox.Text);

            if (moneyOut)
            {
                amount *= -1;
            }

            Row newRow = new Row();

            newRow.addColoumn(CashFlowModel.DATE_COLOUMN, date.Value.ToShortDateString());
            newRow.addColoumn(CashFlowModel.DESCRIPTION_COLOUMN, cleanDescription());
            newRow.addColoumn(CashFlowModel.AMOUNT_COLOUMN, amount.ToString());
            newRow.addColoumn(CashFlowModel.TRANSACTION_ID_COLOUMN, "" + controller.GetAvalaibleTransactionID());

            controller.Add(newRow, CashFlowModel.TABLE_NAME);

            controller.RefreshViews();

        }

        private string cleanDescription()
        {

            string description = descriptionTextBox.Text;

            description = description.Replace("'", "");

            return description;
        }

        private void AddTransactionWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            controller.RemoveView(this);
            Dispose();
        }

        private void showToggleToolTip(object sender, EventArgs e)
        {
           var ttm = new ToolTipModel("Toggling Income/Outcome", "Click here to toggle whether the transaction is Income or Outcome.", (sender as Button));
         toolTipHandler.Draw(ttm);
        }

        private void showAmountToolTip(object sender, EventArgs e)
        {
           var ttm = new ToolTipModel("Input Amount", "Input the amount of money in the transaction.", amountTextBox);
           toolTipHandler.Draw(ttm);
        }

        private void showDescriptionToolTip(object sender, EventArgs e)
        {
           var ttm = new ToolTipModel("Input Description", "Input a descrition for the transaction. Less than 50 characters.", descriptionTextBox);
           toolTipHandler.Draw(ttm);
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

        public void RefreshView()
        {
            // Do Nothing
        }
    }
}
