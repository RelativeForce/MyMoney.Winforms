using System;
using System.Windows.Forms;
using MyMoney.Core;
using MyMoney.Core.Model;
using MyMoney.Properties;
using Type = MyMoney.Core.Type;

namespace MyMoney.Windows.Components
{
    public partial class TransactionView : UserControl
    {
        public int? TransactionId { get; private set; }
        public TransactionViewGroup ParentView { set; private get; }
        public IController Controller { set; private get; }

        public TransactionView()
        {
            InitializeComponent();
            ParentView = null;
            Controller = null;
            TransactionId = null;

            amount.ParentView = this;
            description.ParentView = this;
            date.ParentView = this;
        }

        public void SetView(Transaction transaction)
        {
            // Set the row to be the same as the specified row.
            this.TransactionId = transaction?.Id;

            // If the row is null then all the fields should be empty
            if (transaction != null)
            {
                // Set the delete button as enabled
                delete.Enabled = true;

                // Update all the values in the text boxes to the ones specified in the paramter row.
                date.SetText(transaction.Date.ToShortDateString());
                description.SetText(transaction.Description);
                amount.SetText(transaction.Amount.ToString());
            }
            else
            {
                Clear();
            }
        }

        public void Clear() {
            // Set the delete button as disabled
            delete.Enabled = false;

            // Set all the boxes as empty and disabled
            date.ClearAndDisable();
            description.ClearAndDisable();
            amount.ClearAndDisable();
        }

        private void Delete() {

            using (var model = Controller.Database())
            {

                var transaction = model.Transactions.Find(t => t.Id.Equals(TransactionId));

                var success = model.Transactions.Delete(transaction);

                if (success)
                {
                    Clear();
                    Controller.RefreshViews();
                }
                else
                {
                    Controller.NotifyViews(
                        Type.Error,
                        Priority.High,
                        "Failed to delete transaction!"
                    );
                }
            }

        }

        private void Delete_Btn_Click(object sender, EventArgs e)
        {
            // Confirm that the transaction should be deleted.
            var dialogResult = MessageBox.Show(Resources.DeleteTransactionWarningBody, Resources.DeleteTransactionWarningTitle, MessageBoxButtons.YesNo);

            // If the dialog returns yes then delete the transaction.
            if (dialogResult != DialogResult.Yes) return;

            Delete();
            Controller.RefreshViews();
        }

        public void SaveChanges()
        {
            // If nothing has changed then skip
            if (!date.HasChanged && !description.HasChanged && !amount.HasChanged) return;

            using (var model = Controller.Database())
            {
                var transaction = model.Transactions.Find(t => t.Id.Equals(TransactionId));

                try
                {
                    if (date.HasChanged) {
                        transaction.Date = DateTime.Parse(date.Text);
                    }

                    if (description.HasChanged) {
                        transaction.Description = description.Text;
                    }

                    if (amount.HasChanged) {
                        transaction.Amount = double.Parse(amount.Text);
                    }

                    model.SaveChanges();

                    date.ChangesSaved();
                    description.ChangesSaved();
                    amount.ChangesSaved();
                }
                catch (Exception ex)
                {

                    Controller.NotifyViews(
                        Type.Error,
                        Priority.High,
                        $"Error while saving changes - {ex.Message}"
                    );
                }
            }
        }

        private void description_Load(object sender, EventArgs e)
        {

        }
    }
}
