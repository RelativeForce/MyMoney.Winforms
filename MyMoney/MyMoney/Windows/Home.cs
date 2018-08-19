// Framework Imports
using System;
using System.Windows.Forms;
using System.Reflection;
// MyMoney Imports
using MyMoney.Windows.Components;
using MyMoney.Controllers;
using MyMoney.File;

namespace MyMoney.Windows
{

    public partial class Home : Form, IView
    {

        #region components

        private GraphHandler plotter;

        private TransactionViewer viewer;

        private AddTransactionWindow addTransactionWindow;

        private MonthlyAllowanceChanger monthlyAllowanceChanger;

        private ToolTipHandler toolTipHandler;

        #endregion components

        #region fields

        private readonly IDataController controller;

        private readonly FileStoreManager fileStore;

        private DateTime highlightedMonth;

        private string previousValue = "";

        private bool updated = false;

        #endregion fields

        public Home(IDataController controller, FileStoreManager fileStore)
        {
            this.controller = controller;
            this.fileStore = fileStore;

            controller.AddView(this);

            InitializeComponent();

            this.highlightedMonth = DateTime.Today;
            this.plotter = new GraphHandler(monthPlot, controller);

            this.viewer = new TransactionViewer(
                new TransactionView[] {
                    new TransactionView(date1, description1, amount1, delete1),
                    new TransactionView(date2, description2, amount2, delete2),
                    new TransactionView(date3, description3, amount3, delete3),
                }, scrollBar, highlightedMonth, controller);

            this.toolTipHandler = new ToolTipHandler();

            DisableOperationControls();

            this.Text = Assembly.GetExecutingAssembly().GetName().Name + " [" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + "]";


            if (fileStore.Read())
            {
                controller.Connect();
                controller.Load();
                EnableOperationControls();
            }

        }

        #region form_event_handlers

        private void KeyPressed(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Right && rightButton.Enabled)
            {
                // Move to next month
                NextMonth(null, null);
            }
            else if (e.KeyCode == Keys.Left && leftButton.Enabled)
            {
                // Move to previou month
                PreviousMonth(null, null);
            }
            else if (e.KeyCode == Keys.Down && scrollBar.Enabled && scrollBar.Value < scrollBar.Maximum)
            {
                // Scroll down the list of Transactions.
                scrollBar.Value++;
                viewer.display();
            }
            else if (e.KeyCode == Keys.Up && scrollBar.Enabled && scrollBar.Value > 0)
            {
                // Scroll up the list of Transactions.
                scrollBar.Value--;
                viewer.display();
            }

            e.Handled = true;
        }

        private void LoadForm(object sender, EventArgs e)
        {

            viewer.display();

            plotter.draw();

        }

        private void ScrollTransactions(object sender, EventArgs e)
        {
            // Rerender the viewer.
            viewer.display();
        }

        private void AddTransaction(object sender, EventArgs e)
        {
            // If there is now AddTranactionWindow open then open one.
            if (addTransactionWindow == null || addTransactionWindow.IsDisposed)
            {
                addTransactionWindow = new AddTransactionWindow(controller);
                addTransactionWindow.Show();
            }
        }

        private void MainClosed(object sender, FormClosedEventArgs e)
        {
            controller.Disconnect();

            controller.RemoveView(this);

        }

        private void DeleteTransaction(object sender, EventArgs e)
        {
            // Confirm that the transaction should be deleted.
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this transaction?", "Delete Transatcion?", MessageBoxButtons.YesNo);

            // If the dialog returns yes then delete the transaction.
            if (dialogResult == DialogResult.Yes)
            {
                viewer.deleteTransaction(sender as Button);
                plotter.draw();
            }
        }

        private void NextMonth(object sender, EventArgs e)
        {
            highlightedMonth = highlightedMonth.AddMonths(1);
            LoadMonth();
        }

        private void PreviousMonth(object sender, EventArgs e)
        {
            // Deincrement the month.
            highlightedMonth = highlightedMonth.AddMonths(-1);
            LoadMonth();
        }

        private void DisplayUpdateTransactionToolTip(object sender, EventArgs e)
        {
            // Display a tool tip to help users understand how to update transactions.
            toolTipHandler.draw("Updating Transactions", "Press ENTER to save your changes.", (sender as RichTextBox));
        }

        private void BeginUpdate(object sender, EventArgs e)
        {
            previousValue = (sender as RichTextBox).Text;
        }

        private void EndUpdate(object sender, EventArgs e)
        {
            if (!updated)
            {
                (sender as RichTextBox).Text = previousValue;
            }
            else
            {
                updated = false;
            }
        }

        private void UpdateField(object sender, KeyEventArgs e)
        {
            // If the user pressed enter.
            if (e.KeyCode == Keys.Enter)
            {
                // If the user has changed the contents of the text box.
                if (!(sender as RichTextBox).Text.Equals(previousValue))
                {
                    // Attempt to update the transaction and store the result.
                    string result = viewer.updateTransaction(sender as RichTextBox);

                    // If the result is an empty string then the update was successful.
                    if (result.Equals(""))
                    {
                        updated = true;
                        previousValue = (sender as RichTextBox).Text;

                        DisplaySuccessUpdatedFieldToolTip(sender as RichTextBox);
                    }
                    else
                    {
                        DisplayFailUpdatedFieldToolTip(sender as RichTextBox, result);
                    }

                }
                // Define the ENTER as handled so a new line is not created in the text box.
                e.Handled = true;
            }

        }

        private void ChangeMonthlyAllowance(object sender, EventArgs e)
        {
            // If there is no current MonthlyAllowanceChanger active then open a new one.
            if (monthlyAllowanceChanger == null || monthlyAllowanceChanger.IsDisposed)
            {
                this.monthlyAllowanceChanger = new MonthlyAllowanceChanger(controller, highlightedMonth);
                this.monthlyAllowanceChanger.Show();
            }
        }

        private void ImportDBFile(object sender, EventArgs e)
        {
            // Holds a new Open file dialog
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();

            // Assign the attributes of the dialog.
            dialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); ;
            dialog.Multiselect = false;
            dialog.Title = "Import Database File";
            dialog.DefaultExt = "sqlite";
            dialog.Filter = "sqlite files (*.sqlite)|*.sqlite";
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;

            if (dialog.ShowDialog() != DialogResult.OK) return;
            if (FileStoreManager.DB_FILE_PATH.Equals(dialog.FileName)) return;

            FileStoreManager.DB_FILE_PATH = dialog.FileName;

            fileStore.ClearFileStore();
            fileStore.Store();

            controller.Connect();
            controller.SetStartDate(highlightedMonth);
            controller.Load();

            EnableOperationControls();

        }

        private void CreateDBFile(object sender, EventArgs e)
        {

            // Holds save file dialog.
            System.Windows.Forms.SaveFileDialog dialog = new System.Windows.Forms.SaveFileDialog();

            // Set the attributes of the save file dialog.
            dialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialog.Title = "Choice where to save the new Database File";
            dialog.Filter = "sqlite files (*.sqlite)|*.sqlite";
            dialog.DefaultExt = "sqlite";

            if (dialog.ShowDialog() != DialogResult.OK) return;

            if (FileStoreManager.DB_FILE_PATH.Equals(dialog.FileName)) return;

            FileStoreManager.DB_FILE_PATH = dialog.FileName;
            fileStore.ClearFileStore();
            fileStore.Store();

            controller.Create();
            controller.Connect();
            controller.SetStartDate(highlightedMonth);
            controller.Load();

            EnableOperationControls();


        }

        private void OpenAboutWindow(object sender, EventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }

        private void UpdateDatabaseFile(object sender, EventArgs e)
        {

            // Holds a new Open file dialog
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();

            // Assign the attributes of the dialog.
            dialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); ;
            dialog.Multiselect = false;
            dialog.Title = "Import Database File";
            dialog.DefaultExt = "sqlite";
            dialog.Filter = "sqlite files (*.sqlite)|*.sqlite";
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;

            // If the user clicks ok in the dialog.
            if (dialog.ShowDialog() != DialogResult.OK) return;

            // IF the file specified in the open file €dialog is 
            // NOT the same as the current database.
            if (FileStoreManager.DB_FILE_PATH.Equals(dialog.FileName)) return;

            FileStoreManager.DB_FILE_PATH = dialog.FileName;

            fileStore.ClearFileStore();
            fileStore.Store();

            controller.Connect();

            // Update the database file.
            // controller.updateDB();

            controller.SetStartDate(highlightedMonth);
            controller.Load();

            // Enable the view controls.
            EnableOperationControls();

        }

        #endregion form_event_handlers

        private void LoadMonth()
        {

            controller.SetStartDate(highlightedMonth);
            controller.Load();

            UpdateView();
        }

        private void UpdateView()
        {
            // If month currently being displayed is the present month 
            // then disable the rightButton. Otherwise enable it.
            if (highlightedMonth.Date <= DateTime.Today.AddMonths(-1))
            {
                rightButton.Enabled = true;
            }
            else
            {
                rightButton.Enabled = false;
            }

            // If the month currently being displayed is above the mimimum 
            // value of the DateTime data type enable the leftButton. Otherwise 
            // enable it.
            if (highlightedMonth.Date >= DateTime.MinValue.AddMonths(1))
            {

                leftButton.Enabled = true;
            }
            else
            {
                leftButton.Enabled = false;
            }

            // Redraw the graph and display the tranactions of that month.
            plotter.draw(highlightedMonth);
            viewer.display(highlightedMonth);

        }

        private void DisplayFailUpdatedFieldToolTip(RichTextBox sender, string errorMessage)
        {
            toolTipHandler.draw("Failure", errorMessage, sender);
        }

        private void DisplaySuccessUpdatedFieldToolTip(RichTextBox sender)
        {
            toolTipHandler.draw("Success", "Your changes have been saved.", sender);
        }

        private void DisableOperationControls()
        {

            viewer.disable();
            addTransactionButton.Enabled = false;
            changeMonthlyAllowanceButtton.Enabled = false;
            rightButton.Enabled = false;
            leftButton.Enabled = false;
            scrollBar.Enabled = false;

        }

        private void EnableOperationControls()
        {

            viewer.enable();
            viewer.display();
            plotter.draw();
            changeMonthlyAllowanceButtton.Enabled = true;
            addTransactionButton.Enabled = true;
            leftButton.Enabled = true;
            scrollBar.Enabled = true; ;

        }

        public void RefreshView()
        {

            viewer.display();

            plotter.draw(highlightedMonth);

        }
    }
}

