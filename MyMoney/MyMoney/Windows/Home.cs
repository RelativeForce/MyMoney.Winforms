using System;
using System.Reflection;
using System.Windows.Forms;
using MyMoney.Core;
using MyMoney.File;
using MyMoney.Properties;
using MyMoney.Windows.Components;
using MyMoney.Windows.Components.DBFileDialogs;

namespace MyMoney.Windows
{

    public sealed partial class Home : Form, IView
    {

        #region components

        private readonly GraphHandler _plotter;

        private readonly TransactionViewer _viewer;

        private AddTransactionWindow _addTransactionWindow;

        private MonthlyAllowanceChanger _monthlyAllowanceChanger;

        private readonly ToolTipHandler _toolTipHandler;

        #endregion components

        #region fields

        private readonly IDataController _controller;

        private readonly FileStoreManager _fileStore;

        private DateTime _highlightedMonth;

        private string _previousValue = "";

        private bool _updated;

        #endregion fields

        public Home(IDataController controller, FileStoreManager fileStore)
        {
            _controller = controller;
            _fileStore = fileStore;

            _controller.AddView(this);

            InitializeComponent();

            _highlightedMonth = DateTime.Today;
            _plotter = new GraphHandler(monthPlot, controller);

            _viewer = new TransactionViewer(
                new[] {
                    new TransactionView(date1, description1, amount1, delete1),
                    new TransactionView(date2, description2, amount2, delete2),
                    new TransactionView(date3, description3, amount3, delete3)
                }, scrollBar, _highlightedMonth, controller);

            _toolTipHandler = new ToolTipHandler();

            DisableOperationControls();

            Text = Assembly.GetExecutingAssembly().GetName().Name + " [" + Assembly.GetExecutingAssembly().GetName().Version + "]";

            if (!fileStore.Read()) return;

            controller.Connect();
            controller.Load();
            EnableOperationControls();

        }

        public void RefreshView()
        {

            _viewer.display();

            _plotter.draw(_highlightedMonth);

        }

        #region form_event_handlers

        #region general

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
                _viewer.display();
            }
            else if (e.KeyCode == Keys.Up && scrollBar.Enabled && scrollBar.Value > 0)
            {
                // Scroll up the list of Transactions.
                scrollBar.Value--;
                _viewer.display();
            }

            e.Handled = true;
        }

        private void LoadForm(object sender, EventArgs e)
        {

            _viewer.display();

            _plotter.draw();

        }

        private void MainClosed(object sender, FormClosedEventArgs e)
        {
            _controller.Disconnect();

            _controller.RemoveView(this);

        }

        #endregion general

        #region transactions

        private void ScrollTransactions(object sender, EventArgs e)
        {
            // Rerender the viewer.
            _viewer.display();
        }

        private void DeleteTransaction(object sender, EventArgs e)
        {
            // Confirm that the transaction should be deleted.
            var dialogResult = MessageBox.Show(Resources.DeleteTransactionWarningBody, Resources.DeleteTransactionWarningTitle, MessageBoxButtons.YesNo);

            // If the dialog returns yes then delete the transaction.
            if (dialogResult != DialogResult.Yes) return;

            _viewer.deleteTransaction(sender as Button);
            _plotter.draw();
        }

        private void DisplayUpdateTransactionToolTip(object sender, EventArgs e)
        {

             var ttm = new ToolTipModel(
                "Updating Transactions", 
                "Press ENTER to save your changes.", 
                (sender as RichTextBox)
             );

            _toolTipHandler.Draw(ttm);
        }

        private void ChangeTransactionField(object sender, KeyEventArgs e)
        {

            // If the user pressed enter.
           if (e.KeyCode != Keys.Enter) return;

           // Define the ENTER as handled so a new line is not created in the text box.
           e.Handled = true;

           var textBox = (RichTextBox) sender;

           // If the user has changed the contents of the text box.
           if (textBox.Text.Equals(_previousValue)) return;

           // Attempt to update the transaction and store the result.
           var result = _viewer.updateTransaction(textBox);

           // If the result is an empty string then the update was successful.
           if (result.Equals(""))
           {
              _updated = true;
              _previousValue = textBox.Text;

              DisplaySuccessUpdatedFieldToolTip(textBox);
           }
           else
           {
              DisplayFailUpdatedFieldToolTip(textBox, result);
           }



        }

        private void CachePreviousValue(object sender, EventArgs e)
        {
            _previousValue = (sender as RichTextBox)?.Text;
        }

        private void RevertToPreviousValue(object sender, EventArgs e)
        {
           if (_updated) return;

           ((RichTextBox) sender).Text = _previousValue;
           _previousValue = "";
        }

        #endregion transactions

        #region db_file

        private void ImportDBFile(object sender, EventArgs e)
        {

            DBLoadFileDialog dialog = new DBLoadFileDialog(fileName => {

                if (FileStoreManager.DB_FILE_PATH.Equals(fileName)) return;

                FileStoreManager.DB_FILE_PATH = fileName;

                _fileStore.ClearFileStore();
                _fileStore.Store();

                _controller.Connect();
                _controller.SetStartDate(_highlightedMonth);
                _controller.Load();

                EnableOperationControls();

            });

            dialog.Show();

        }

        private void CreateDBFile(object sender, EventArgs e)
        {

            DBCreateFileDialog dialog = new DBCreateFileDialog(fileName => {

                if (FileStoreManager.DB_FILE_PATH.Equals(fileName)) return;

                FileStoreManager.DB_FILE_PATH = fileName;
                _fileStore.ClearFileStore();
                _fileStore.Store();

                _controller.Create();
                _controller.Connect();
                _controller.SetStartDate(_highlightedMonth);
                _controller.Load();

                EnableOperationControls();

            });

            dialog.Show();

        }

        private void UpdateDBFile(object sender, EventArgs e)
        {

            // Holds a new Open file dialog
            OpenFileDialog dialog = new OpenFileDialog();

            // Assign the attributes of the dialog.
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); ;
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

            _fileStore.ClearFileStore();
            _fileStore.Store();

            _controller.Connect();

            // Update the database file.
            // controller.updateDB();

            _controller.SetStartDate(_highlightedMonth);
            _controller.Load();

            // Enable the view controls.
            EnableOperationControls();

        }

        #endregion db_file

        #region open_window

        private void AddTransaction(object sender, EventArgs e)
        {
            // If there is now AddTranactionWindow open then open one.
            if (_addTransactionWindow == null || _addTransactionWindow.IsDisposed)
            {
                _addTransactionWindow = new AddTransactionWindow(_controller);
                _addTransactionWindow.Show();
            }
        }

        private void ChangeMonthlyAllowance(object sender, EventArgs e)
        {
            // If there is no current MonthlyAllowanceChanger active then open a new one.
            if (_monthlyAllowanceChanger == null || _monthlyAllowanceChanger.IsDisposed)
            {
                _monthlyAllowanceChanger = new MonthlyAllowanceChanger(_controller, _highlightedMonth);
                _monthlyAllowanceChanger.Show();
            }
        }

        private void OpenAboutWindow(object sender, EventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }

        #endregion open_window

        #region change_month

        private void NextMonth(object sender, EventArgs e)
        {
            _highlightedMonth = _highlightedMonth.AddMonths(1);
            LoadMonth();
        }

        private void PreviousMonth(object sender, EventArgs e)
        {
            // Deincrement the month.
            _highlightedMonth = _highlightedMonth.AddMonths(-1);
            LoadMonth();
        }

        #endregion chnage_month

        #endregion form_event_handlers

        #region operation_controls

        private void DisableOperationControls()
        {
            _viewer.disable();
            addTransactionButton.Enabled = false;
            changeMonthlyAllowanceButtton.Enabled = false;
            rightButton.Enabled = false;
            leftButton.Enabled = false;
            scrollBar.Enabled = false;
        }

        private void EnableOperationControls()
        {
            _viewer.enable();
            _viewer.display();
            _plotter.draw();
            changeMonthlyAllowanceButtton.Enabled = true;
            addTransactionButton.Enabled = true;
            leftButton.Enabled = true;
            scrollBar.Enabled = true; ;
        }

        #endregion operation_controls

        private void LoadMonth()
        {

            _controller.SetStartDate(_highlightedMonth);
            _controller.Load();

            UpdateView();
        }

        private void UpdateView()
        {
            // If month currently being displayed is the present month 
            // then disable the rightButton. Otherwise enable it.
            if (_highlightedMonth.Date <= DateTime.Today.AddMonths(-1))
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
            if (_highlightedMonth.Date >= DateTime.MinValue.AddMonths(1))
            {

                leftButton.Enabled = true;
            }
            else
            {
                leftButton.Enabled = false;
            }

            // Redraw the graph and display the tranactions of that month.
            _plotter.draw(_highlightedMonth);
            _viewer.display(_highlightedMonth);

        }

        private void DisplayFailUpdatedFieldToolTip(RichTextBox sender, string errorMessage)
        {
           var ttm = new ToolTipModel("Failure", errorMessage, sender);
 
            _toolTipHandler.Draw(ttm);
        }

        private void DisplaySuccessUpdatedFieldToolTip(RichTextBox sender)
        {
            var ttm =  new ToolTipModel("Success", "Your changes have been saved.", sender);
            _toolTipHandler.Draw(ttm);
        }

    }
}

