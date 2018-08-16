// Framework Imports
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Reflection;
// Finance_Handler Imports
using MyMoney.Data_Storage;
using MyMoney.Windows.Data_Transfer;
using MyMoney.Windows.User_Display;
using MyMoney.Database;

namespace MyMoney.Windows
{
    /// <summary>
    /// The background functionality for the Main form.
    /// </summary>
    public partial class Main_Form : Form, Buffered
    {

        /// <summary>
        /// Stores a refernece to the singleton of <see cref="SQLHandler"/>.
        /// </summary>
        private SQLHandler SQL = SQLHandler.getInstance();

        /// <summary>
        /// Handles all the behaviours of the graph on this form
        /// </summary>
        private GraphHandler plotter;

        /// <summary>
        /// Handles all the behaviours of the transaction view components on the form.
        /// </summary>
        private TransactionViewer viewer;

        /// <summary>
        /// Allows data to being transafered between another form and this to cause 
        /// changes in the components on this form.
        /// </summary>
        public System.ComponentModel.BackgroundWorker buffer;

        /// <summary>
        /// The instance of the <see cref="AddTransactionWindow"/> that is 
        /// displayed by the main form. Only one can be displayed at a time.
        /// </summary>
        private AddTransactionWindow addTransactionWindow;

        /// <summary>
        /// The instance of the <see cref="MonthlyAllowanceChanger"/> that is 
        /// displayed by the main form. Only one may be active at any given time.
        /// </summary>
        private MonthlyAllowanceChanger monthlyAllowanceChanger;

        /// <summary>
        /// Stores the <see cref="DateTime"/> that denoted the month displayed on the main form.
        /// </summary>
        private DateTime highlightedMonth;

        /// <summary>
        /// Holds the value of the view field that is currently being modifed by the user.
        /// </summary>
        private string previousValue = "";

        /// <summary>
        /// Holds wherther the user successfully updated the transaction. If not 
        /// the <see cref="previousValue"/> will replace what ever the user changed 
        /// the filed to when the field is no longer selected.
        /// </summary>
        private Boolean updated = false;

        /// <summary>
        /// Handles all the toolTips behaviours on this form.
        /// </summary>
        private ToolTipHandler toolTipHandler;

        /// <summary>
        /// Constructs the main form.
        /// </summary>
        public Main_Form()
        {

            InitializeComponent();

            this.buffer = new System.ComponentModel.BackgroundWorker();
            this.buffer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.parsePacket);

            this.highlightedMonth = DateTime.Today;
            this.plotter = new GraphHandler(monthPlot);

            this.viewer = new TransactionViewer(
                new TransactionView[] { 
                    new TransactionView(date1, description1, amount1, delete1), 
                    new TransactionView(date2, description2, amount2, delete2),
                    new TransactionView(date3, description3, amount3, delete3),
                }, scrollBar, highlightedMonth);

            this.toolTipHandler = new ToolTipHandler();

            disableOperationControls();

            this.Text = Assembly.GetExecutingAssembly().GetName().Name + " [" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + "]";

            checkStorageFile();
        }

        /// <summary>
        /// Triggered when the user presses a key on the main form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void keyPressed(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Right && rightButton.Enabled)
            {
                // Move to next month
                nextMonth(null, null);
            }
            else if (e.KeyCode == Keys.Left && leftButton.Enabled)
            {
                // Move to previou month
                previousMonth(null, null);
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

        /// <summary>
        /// Checks if the storage file exists and that it contains 
        /// the absolute file path of a database file. If so 
        /// <see cref="DatabaseHandler#FILE_PATH"/> is assigned the 
        /// stored database path. 
        /// </summary>
        private void checkStorageFile()
        {
            // If there is no current database file open in program.
            if (DatabaseHandler.FILE_PATH.Equals("undefined"))
            {
                // If the storage file does not exist in the local storage.
                if (!System.IO.File.Exists(DatabaseHandler.STORAGE_FILE))
                {
                    // Feedback and create the file.
                    Console.Out.WriteLine("Storage Created");
                    System.IO.File.Create(DatabaseHandler.STORAGE_FILE);
                }
                else
                {
                    // Feedback and read the contents of the storage file.
                    Console.Out.WriteLine("Storage Located");
                    string storageFileText = System.IO.File.ReadAllText(DatabaseHandler.STORAGE_FILE);

                    // Check that the file specified in the storage exists.
                    if (System.IO.File.Exists(storageFileText))
                    {
                        // Feedback and set the specified file as the file to be loaded.
                        Console.Out.WriteLine("Stored File Loaded");
                        DatabaseHandler.FILE_PATH = storageFileText;

                        // Connect to the database file and enable the controls on the main form.
                        SQL.connect();
                        enableOperationControls();
                    }
                    else
                    {
                        // The file is wiped because the contents are invalid.
                        Console.Out.WriteLine("Stored File Invalid, Wiping Storage File.");
                        System.IO.File.WriteAllText(DatabaseHandler.STORAGE_FILE, "");
                    }
                }
            }
        }

        /// <summary>
        /// Occurs when the Main Form is initally loaded.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void loadForm(object sender, EventArgs e)
        {

            viewer.display();

            plotter.draw();

        }

        /// <summary>
        /// Triggered when the user uses the <see cref="scrollBar"/> this 
        /// method causes the <see cref="veiwer"/> to be rerended with 
        /// <see cref="TransactionView"/>s based on the position of the scroll bar.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void scroll(object sender, EventArgs e)
        {
            // Rerender the viewer.
            viewer.display();
        }

        /// <summary>
        /// Opens the <see cref="AddTransactionWindow"/>.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void addTransaction(object sender, EventArgs e)
        {
            // If there is now AddTranactionWindow open then open one.
            if (addTransactionWindow == null || addTransactionWindow.IsDisposed)
            {
                addTransactionWindow = new AddTransactionWindow(this);
                addTransactionWindow.Show();
            }
        }

        /// <summary>
        /// Occurs when the Main Form is closed.
        /// </summary>
        /// <param name="sender">unused.</param>
        /// <param name="e">Unused.</param>
        private void formClosed(object sender, FormClosedEventArgs e)
        {
            SQL.terminate();
        }

        /// <summary>
        /// Used to revive <see cref="Packet"/>s from other Forms.
        /// </summary>
        /// <param name="packet">The data to be transfered.</param>
        public void recieve(Packet packet)
        {
            // Pass the paket to the buffer to allow the thread safe parsing of the Packet.
            buffer.RunWorkerAsync(packet);
        }

        /// <summary>
        /// Parses packets from other forms.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Packet of data</param>
        private void parsePacket(object sender, DoWorkEventArgs e)
        {
            // Cast to Packet.
            Packet packet = e.Argument as Packet;

            try
            {

                Console.WriteLine("Data Recieved");

                if (packet.source is AddTransactionWindow)
                {

                    // Cast the packet date as a Row and add that row to the CashFlow Table.
                    CashFlow.getInstance().addRow(packet.data as Row);

                    viewer.display();

                    plotter.draw(highlightedMonth);

                }
                else if (packet.source is MonthlyAllowanceChanger)
                {

                    Budget.getInstance().addRow(packet.data as Row);

                    plotter.draw(highlightedMonth);

                }
                Console.WriteLine("Successful Data Transfer");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed Data Transfer");
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Occurs when a delete button is pressed. Causes a transaction to be deleted.
        /// </summary>
        /// <param name="sender">The button that was pressed.</param>
        /// <param name="e"Unused.></param>
        private void deleteTransaction(object sender, EventArgs e)
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

        /// <summary>
        /// Occurs when the value in <see cref="highlightMonth"/> is changed and 
        /// redraws the plaoter and display based on the new date value.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void nextMonth(object sender, EventArgs e)
        {
            highlightedMonth = highlightedMonth.AddMonths(1);
            loadMonth();
        }

        /// <summary>
        /// Load the month that is currently specifed by the <see cref="highlightedMonth"/>.
        /// </summary>
        private void loadMonth()
        {

            // Import new month
            DatabaseHandler.getInstance().load(highlightedMonth);

            updateView();
        }

        /// <summary>
        /// Check the months stored in the database to see if the 
        /// <see cref="rightButton"/> and <see cref="leftButton"/> 
        /// should be enabled or disable.
        /// </summary>
        private void updateView()
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

        /// <summary>
        /// Changes the currently selected month ot the previous month.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void previousMonth(object sender, EventArgs e)
        {
            // Deincrement the month.
            highlightedMonth = highlightedMonth.AddMonths(-1);
            loadMonth();
        }

        /// <summary>
        /// Displays the update transaction tool tip. Occurs when the user hovers over a transaction field.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void displayUpdateTransactionToolTip(object sender, EventArgs e)
        {
            // Display a tool tip to help users understand how to update transactions.
            toolTipHandler.draw("Updating Transactions", "Press ENTER to save your changes.", (sender as RichTextBox));
        }

        /// <summary>
        /// Saves the focused text box's contents in <see cref="previousValue"/>.
        /// </summary>
        /// <param name="sender">Focused Ricg=h text box.</param>
        /// <param name="e">Unused.</param>
        private void beginUpdate(object sender, EventArgs e)
        {
            previousValue = (sender as RichTextBox).Text;
        }

        /// <summary>
        /// Checks if the focused text box was updated or not.
        /// </summary>
        /// <param name="sender">Focused text box.</param>
        /// <param name="e">Unused.</param>
        private void endUpdate(object sender, EventArgs e)
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

        /// <summary>
        /// Updates a transaction field. Occurs when a user types in a field.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Key presses in a RichTextBox</param>
        private void updateField(object sender, KeyEventArgs e)
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

                        displaySuccessUpdatedFieldToolTip(sender as RichTextBox);
                    }
                    else
                    {
                        displayFailUpdatedFieldToolTip(sender as RichTextBox, result);
                    }

                }
                // Define the ENTER as handled so a new line is not created in the text box.
                e.Handled = true;
            }

        }

        /// <summary>
        /// Shows a transaction update failure tool tip.
        /// </summary>
        /// <param name="sender">Text box to display the tool tip on.</param>
        /// <param name="errorMessage">Message to be displayed.</param>
        private void displayFailUpdatedFieldToolTip(RichTextBox sender, string errorMessage)
        {
            toolTipHandler.draw("Failure", errorMessage, sender);
        }

        /// <summary>
        /// Shows a sucsessful transaction update tool tip. 
        /// </summary>
        /// <param name="sender">Text box to display the tool tip on.</param>
        private void displaySuccessUpdatedFieldToolTip(RichTextBox sender)
        {
            toolTipHandler.draw("Success", "Your changes have been saved.", sender);
        }

        /// <summary>
        /// Triggered by clicking the <see cref="changeMonthlyAllowanceButton"/>. 
        /// Opens a <see cref="MonthlyAllowanceChanger"/> if one is not currently open.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void changeMonthlyAllowance(object sender, EventArgs e)
        {
            // If there is no current MonthlyAllowanceChanger active then open a new one.
            if (monthlyAllowanceChanger == null || monthlyAllowanceChanger.IsDisposed)
            {
                this.monthlyAllowanceChanger = new MonthlyAllowanceChanger(this, highlightedMonth);
                this.monthlyAllowanceChanger.Show();
            }
        }

        /// <summary>
        /// Creates a open file dialog. The user will then select a new database file and it is opened.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void importDBFile(object sender, EventArgs e)
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
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // IF the file specified in the open file €dialog is 
                // NOT the same as the current database.
                if (!DatabaseHandler.FILE_PATH.Equals(dialog.FileName))
                {
                    // Clear the internal storage.
                    DatabaseHandler.getInstance().clear();

                    // Assign the specified file as the new database file.
                    DatabaseHandler.FILE_PATH = dialog.FileName;

                    // Connect to the new database.
                    SQL.connect();

                    // Load the new database into internal storage.
                    DatabaseHandler.getInstance().load(highlightedMonth);

                    // Enable the view controls.
                    enableOperationControls();

                }

            }
        }

        /// <summary>
        /// Creates a save file dialog. The user will choose the name and location of the database file.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void createDBFile(object sender, EventArgs e)
        {

            // Holds save file dialog.
            System.Windows.Forms.SaveFileDialog dialog = new System.Windows.Forms.SaveFileDialog();

            // Set the attributes of the save file dialog.
            dialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialog.Title = "Choice where to save the new Database File";
            dialog.Filter = "sqlite files (*.sqlite)|*.sqlite";
            dialog.DefaultExt = "sqlite";

            // If the user clicks ok on the save file dialog.
            if (dialog.ShowDialog() == DialogResult.OK)
            {

                // If the specified file path is different to that in internal storage.
                if (!DatabaseHandler.FILE_PATH.Equals(dialog.FileName))
                {
                    // Assign the new file path.
                    DatabaseHandler.FILE_PATH = dialog.FileName;

                    // Clear the internal database.
                    DatabaseHandler.getInstance().clear();

                    // Create the database structure in the new file.
                    DatabaseHandler.getInstance().create();

                    // Connect to new database.
                    SQL.connect();

                    // Load databasde into internal memory.
                    DatabaseHandler.getInstance().load(highlightedMonth);

                    enableOperationControls();
                }

            }


        }

        /// <summary>
        /// Disables all the controls associated with viewing transactions.
        /// </summary>
        private void disableOperationControls()
        {

            viewer.disable();
            addTransactionButton.Enabled = false;
            changeMonthlyAllowanceButtton.Enabled = false;
            rightButton.Enabled = false;
            leftButton.Enabled = false;
            scrollBar.Enabled = false;

        }

        /// <summary>
        /// Enables all the controls associated with viewing transactions.
        /// </summary>
        private void enableOperationControls()
        {

            viewer.enable();
            viewer.display();
            plotter.draw();
            changeMonthlyAllowanceButtton.Enabled = true;
            addTransactionButton.Enabled = true;
            leftButton.Enabled = true;
            scrollBar.Enabled = true; ;

        }

        /// <summary>
        /// Opens the about window.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void openAboutWindow(object sender, EventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }

        /// <summary>
        /// Creates a open file dialog. The user will then select a new database file, 
        /// it is updateed to the current schema and it is opened. 
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void updateDatabaseFile(object sender, EventArgs e)
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
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // IF the file specified in the open file €dialog is 
                // NOT the same as the current database.
                if (!DatabaseHandler.FILE_PATH.Equals(dialog.FileName))
                {
                    // Clear the internal storage.
                    DatabaseHandler.getInstance().clear();

                    // Assign the specified file as the new database file.
                    DatabaseHandler.FILE_PATH = dialog.FileName;

                    // Connect to the new database.
                    SQL.connect();

                    // Update the database file.
                    SQL.updateDB();

                    // Load the new database into internal storage.
                    DatabaseHandler.getInstance().load(highlightedMonth);

                    // Enable the view controls.
                    enableOperationControls();

                }

            };

        }

    }
}

