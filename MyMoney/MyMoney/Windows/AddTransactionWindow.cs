using System;
using System.Windows.Forms;
using MyMoney.Core;
using MyMoney.Core.Model;
using MyMoney.Windows.Components;

namespace MyMoney.Windows
{

    public partial class AddTransactionWindow : Form, IView
    {
        private bool moneyOut;

        private ToolTipHandler toolTipHandler = new ToolTipHandler();

        private readonly IController controller;

        public AddTransactionWindow(IController controller)
        {
            this.controller = controller;

            controller.AddView(this);

            moneyOut = true;

            InitializeComponent();
        }

        public void RefreshView()
        {
            // Do Nothing
        }

        public void Notify(Core.Type type, Priority priority, string message)
        {
            // TODO: Add functionality for dislpaying messages on AddTransactionWindow
        }

        private void AddTransactionWindow_Load(object sender, EventArgs e)
        {
            // Set the date to today's date.
            date.Value = DateTime.Now;

            date.MaxDate = DateTime.Today;

        }

        private void Submit_Click(object sender, EventArgs e)
        {

            double amount = 0;

            // If this value remaines fales then a feild was invalid.
            bool valid = false;

            try
            {
                // Parse the amount as a double
                amount = double.Parse(amountTextBox.Text);

                // Check that the description is not empty but is also less than the maximum length of the description.
                if (amount <= 0)
                {
                    var ttm = new ToolTipModel("Error", "Amounts must be positive and greater than zero.", (amountTextBox));
                    toolTipHandler.Draw(ttm);
                }
                else if (descriptionTextBox.Equals(""))
                {
                    var ttm = new ToolTipModel("Error", "Please input a description.", descriptionTextBox);
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
               
                if (moneyOut)
                {
                    amount *= -1;
                }

                using (var model = controller.Database())
                {

                    var transaction = new Transaction(date.Value, CleanDescription(), amount);

                    model.Transactions.Add(transaction);

                }

                controller.RefreshViews();

                Console.WriteLine("Data Sent");

                // Close this window
                Close();

            }

        }

        private string CleanDescription()
        {

            string description = descriptionTextBox.Text;

            description = description.Replace("'", "");

            return description;
        }

        private void AddTransactionWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            controller.RemoveView(this);
            controller.RefreshViews();
            Dispose();
        }

        private void ShowToggleToolTip(object sender, EventArgs e)
        {
            var ttm = new ToolTipModel("Toggling Income/Outcome", "Click here to toggle whether the transaction is Income or Outcome.", (sender as Button));
            toolTipHandler.Draw(ttm);
        }

        private void ShowAmountToolTip(object sender, EventArgs e)
        {
            var ttm = new ToolTipModel("Input Amount", "Input the amount of money in the transaction.", amountTextBox);
            toolTipHandler.Draw(ttm);
        }

        private void ShowDescriptionToolTip(object sender, EventArgs e)
        {
            var ttm = new ToolTipModel("Input Description", "Input a descrition for the transaction. Less than 50 characters.", descriptionTextBox);
            toolTipHandler.Draw(ttm);
        }

        private void ToggleFlip(object sender, EventArgs e)
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
