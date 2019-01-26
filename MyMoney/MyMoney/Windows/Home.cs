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

        private AddTransactionWindow _addTransactionWindow;

        private MonthlyAllowanceChanger _monthlyAllowanceChanger;

        private readonly ToolTipHandler _toolTipHandler;

        #endregion components

        #region fields

        private readonly FileStoreManager _fileStore;

        private DateTime _highlightedMonth;

        private readonly IController _controller;

        #endregion fields

        public Home(FileStoreManager fileStore, IController controller)
        {
            _fileStore = fileStore;
            _controller = controller;
            InitializeComponent();

            _highlightedMonth = DateTime.Today;
            _plotter = new GraphHandler(monthPlot, controller);

            viewer.Controller = controller;

            _toolTipHandler = new ToolTipHandler();

            controller.AddView(this);
        }

        public void RefreshView()
        {

            viewer.Display();

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
            else if (e.KeyCode == Keys.Down)
            {
                viewer.ScrollDown();
            }
            else if (e.KeyCode == Keys.Up)
            {
                viewer.ScrollUp();
            }

            e.Handled = true;
        }

        private void LoadForm(object sender, EventArgs e)
        {

            DisableOperationControls();

            Text = Assembly.GetExecutingAssembly().GetName().Name + " [" + Assembly.GetExecutingAssembly().GetName().Version + "]";

            var filePath = _fileStore.Read();

            if (System.IO.File.Exists(filePath))
            {
                _controller.ChangeDatabaseFile(filePath);

                EnableOperationControls();
            }
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
            viewer.Display();
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
            viewer.Enabled = false;
            addTransactionButton.Enabled = false;
            changeMonthlyAllowanceButtton.Enabled = false;
            rightButton.Enabled = false;
            leftButton.Enabled = false;
        }

        private void EnableOperationControls()
        {
            changeMonthlyAllowanceButtton.Enabled = true;
            addTransactionButton.Enabled = true;
            leftButton.Enabled = true;
            viewer.Enabled = true;
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
            viewer.Display(_highlightedMonth);

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

        public void Notify(Core.Type type, Priority priority, string message)
        {
            // TODO: show error
        }
    }
}

