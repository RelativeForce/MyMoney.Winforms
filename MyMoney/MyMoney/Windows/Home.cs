using System;
using System.Reflection;
using System.Windows.Forms;
using MyMoney.Core;
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

        private readonly FileStoreManager _fileStore;

        private DateTime _highlightedMonth;

        private string _previousValue = "";

        private bool _updated;

        private readonly IController _controller;

        #endregion fields

        public Home(FileStoreManager fileStore, IController controller)
        {
            _fileStore = fileStore;
            _controller = controller;
            InitializeComponent();

            _highlightedMonth = DateTime.Today;
            _plotter = new GraphHandler(monthPlot, controller);

            var transactionViews = new[] {
                new TransactionView(date1, description1, amount1, delete1),
                new TransactionView(date2, description2, amount2, delete2),
                new TransactionView(date3, description3, amount3, delete3)
            };

            _viewer = new TransactionViewer(controller, transactionViews, scrollBar, _highlightedMonth);

            _toolTipHandler = new ToolTipHandler();

            controller.AddView(this);

            DisableOperationControls();

            Text = Assembly.GetExecutingAssembly().GetName().Name + " [" + Assembly.GetExecutingAssembly().GetName().Version + "]";

            var filePath = _fileStore.Read();

            if (System.IO.File.Exists(filePath))
            {
                _controller.ChangeDatabaseFile(filePath);

                EnableOperationControls();
            }
        }

        public void RefreshView()
        {

            _viewer.Display();

            _plotter.Draw(_highlightedMonth);

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
                _viewer.Display();
            }
            else if (e.KeyCode == Keys.Up && scrollBar.Enabled && scrollBar.Value > 0)
            {
                // Scroll up the list of Transactions.
                scrollBar.Value--;
                _viewer.Display();
            }

            e.Handled = true;
        }

        private void LoadForm(object sender, EventArgs e)
        {

            if (!_controller.IsDatabaseConnected) return;

            _viewer.Display();

            _plotter.Draw();

        }

        private void MainClosed(object sender, FormClosedEventArgs e)
        {
            _controller.RemoveView(this);
        }

        #endregion general

        #region transactions

        private void ScrollTransactions(object sender, EventArgs e)
        {
            // Rerender the viewer.
            _viewer.Display();
        }

        private void DeleteTransaction(object sender, EventArgs e)
        {
            // Confirm that the transaction should be deleted.
            var dialogResult = MessageBox.Show(Resources.DeleteTransactionWarningBody, Resources.DeleteTransactionWarningTitle, MessageBoxButtons.YesNo);

            // If the dialog returns yes then delete the transaction.
            if (dialogResult != DialogResult.Yes) return;

            _viewer.DeleteTransaction(sender as Button);
            _plotter.Draw();
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

            var textBox = (RichTextBox)sender;

            // If the user has changed the contents of the text box.
            if (textBox.Text.Equals(_previousValue)) return;

            // Attempt to update the transaction and store the result.
            var result = _viewer.UpdateTransaction(textBox);

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

            ((RichTextBox)sender).Text = _previousValue;
            _previousValue = "";
        }

        #endregion transactions

        #region db_file

        private void ImportDBFile(object sender, EventArgs e)
        {

            DBLoadFileDialog dialog = new DBLoadFileDialog(filePath =>
            {
                _controller.ChangeDatabaseFile(filePath);
                _fileStore.Clear();
                _fileStore.Save(filePath);

                EnableOperationControls();
            });

            dialog.Show();

        }

        private void CreateDBFile(object sender, EventArgs e)
        {

            DBCreateFileDialog dialog = new DBCreateFileDialog(filePath =>
            {
                _controller.ChangeDatabaseFile(filePath);
                _fileStore.Clear();
                _fileStore.Save(filePath);

                EnableOperationControls();

            });

            dialog.Show();

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
            _viewer.Disable();
            addTransactionButton.Enabled = false;
            changeMonthlyAllowanceButtton.Enabled = false;
            rightButton.Enabled = false;
            leftButton.Enabled = false;
            scrollBar.Enabled = false;
        }

        private void EnableOperationControls()
        {
            _viewer.Enable();
            _viewer.Display();
            _plotter.Draw();
            changeMonthlyAllowanceButtton.Enabled = true;
            addTransactionButton.Enabled = true;
            leftButton.Enabled = true;
            scrollBar.Enabled = true; ;
        }

        #endregion operation_controls

        private void LoadMonth()
        {
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
            _plotter.Draw(_highlightedMonth);
            _viewer.Display(_highlightedMonth);

        }

        private void DisplayFailUpdatedFieldToolTip(RichTextBox sender, string errorMessage)
        {
            var ttm = new ToolTipModel("Failure", errorMessage, sender);

            _toolTipHandler.Draw(ttm);
        }

        private void DisplaySuccessUpdatedFieldToolTip(RichTextBox sender)
        {
            var ttm = new ToolTipModel("Success", "Your changes have been saved.", sender);
            _toolTipHandler.Draw(ttm);
        }

    }
}

