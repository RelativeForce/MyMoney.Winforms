using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MyMoney.Data_Storage;

namespace MyMoney.Database
{
    /// <summary>
    /// The internal representation of the CashFlow table from the local sql database.
    /// </summary>
    public class CashFlow : Table
    {
        private static CashFlow instance = new CashFlow();

        /// <summary>
        /// The coloumn title for the attribute that stores the date of a transation.
        /// </summary>
        public const string DATE_COLOUMN = "Transaction_Date";

        /// <summary>
        /// The coloumn title of the attribute that stores the description of a transaction.
        /// </summary>
        public const string DESCRIPTION_COLOUMN = "Description";

        /// <summary>
        /// The coloumn title of the attribute that stores the primaery key identification number of a transaction.
        /// </summary>
        public const string TRANSACTION_ID_COLOUMN = "Transaction_ID";

        /// <summary>
        /// The coloumn title of the attribute of that stores the magnitude of the transaction.
        /// </summary>
        public const string AMOUNT_COLOUMN = "Amount";

        /// <summary>
        /// The table name.
        /// </summary>
        public const string TABLE_NAME = "CashFlow";

        /// <summary>
        /// The maximum number of characters allowed in the description attribute. 
        /// </summary>
        public const int DESCRIPTION_LENGTH = 50;


        private CashFlow()
            : base(new string[] { 
                DESCRIPTION_COLOUMN, 
                TRANSACTION_ID_COLOUMN, 
                AMOUNT_COLOUMN, 
                DATE_COLOUMN })
        {


        }

        /// <summary>
        /// Loads the CashFlow table from the database file.
        /// </summary>
        /// <param name="startDate">The start of the month that is to be loaded.</param>
        public override void loadFromSource(DateTime startDate)
        {

            //TODO: Add the abillity for the repeat transactions to be added to the table even if the orignial transaction occured before the current month.

            DateTime endDate = startDate.AddMonths(1);

            string endMonthString = endDate.ToShortDateString();

            string startDateString = startDate.ToShortDateString();

            // Puts the date in the yyyy-mm-dd
            string startMonth = startDateString[6] + "" + startDateString[7] + "" + startDateString[8] + "" + startDateString[9]
                + "-" + startDateString[3] + "" + startDateString[4]
                + "-" + "01";

            string endMonth = endMonthString[6] + "" + endMonthString[7] + "" + endMonthString[8] + "" + endMonthString[9]
                + "-" + endMonthString[3] + "" + endMonthString[4]
                + "-" + "01";

            SQLDatabase.getInstance().executeQuery("SELECT * FROM CashFlow"
                + " WHERE " + DATE_COLOUMN
                + " BETWEEN '" + startMonth + "' AND '" + endMonth + "'"
                + " ORDER BY " + DATE_COLOUMN + " DESC;", out this.rawTable);

            Console.WriteLine("CashFlow Loaded " + startMonth + " " + endMonth);
        }

        /// <summary>
        /// Returns the singleton instance of the CashFlow table.
        /// </summary>
        /// <returns></returns>
        public static CashFlow getInstance()
        {
            return instance;
        }

        /// <summary>
        /// Adds a <see cref="Row"/> to the CashFlow table.
        /// </summary>
        /// <param name="row">The <see cref="Row"/> to be added.</param>
        public new void addRow(Row row)
        {

            if (row == null)
            {
                throw new ArgumentNullException("Row cannot be null.");
            }

            Row[] rows = getRows();

            if (rows.Length > 0)
            {
                DateTime paramDate = DateTime.Parse(row.getValue(DATE_COLOUMN));

                int first = 0;
                int last = rows.Length - 1;
                int mid = last / 2;
                bool found = false;

                while (!found && first < last)
                {

                    mid = (first + last) / 2;

                    DateTime rowDate = DateTime.Parse(rows[mid].getValue(DATE_COLOUMN));

                    Console.Out.WriteLine("Rows[" + mid + "] : " + rowDate.ToShortDateString() + "");

                    // If the current row's date is before the parameter rows date.
                    if (rowDate > paramDate)
                    {
                        first = mid + 1;
                    }
                    // If the current row's date is after the parameter rows date.
                    else if (rowDate < paramDate)
                    {
                        last = mid - 1;
                    }
                    // If the current row's date is the same the parameter rows date.
                    else
                    {
                        found = true;
                    }
                }

                DateTime lastRowDate = DateTime.Parse(rows[mid].getValue(DATE_COLOUMN));

                if (lastRowDate > paramDate && !found)
                {
                    insertRow(mid + 1, row);
                }
                else
                {
                    insertRow(mid, row);
                }
            }
            else
            {

                insertRow(0, row);

            }


            string description = row.getValue(CashFlow.DESCRIPTION_COLOUMN);

            double amount = Double.Parse(row.getValue(CashFlow.AMOUNT_COLOUMN));

            string date = row.getValue(CashFlow.DATE_COLOUMN);

            int id = int.Parse(row.getValue(CashFlow.TRANSACTION_ID_COLOUMN));

            // Puts the date in the yyyy-mm-dd
            string formattedDate = date[6] + "" + date[7] + "" + date[8] + "" + date[9]
                + "-" + date[3] + "" + date[4]
                + "-" + date[0] + "" + date[1];

            SQLDatabase.getInstance().executeNONQuery(
                "INSERT INTO CashFlow VALUES ('" + id
                + "', '" + description
                + "', '" + formattedDate
                + "', " + amount + ")"
                );



        }

        /// <summary>
        /// Retrieves the lowest transaction id that will be unique in the CashFlow table.
        /// </summary>
        /// <returns><code>int</code> transaction id</returns>
        public int getAvalaibleTransactionID()
        {
            // Holds the name of the variable from the aggregate function.
            string MAX_COLOUMN = "Maximum";

            // Hold the highest transaction id in the CashFlow table plus one.
            int id = SQLDatabase.getInstance().executeQuery("SELECT MAX(" + CashFlow.TRANSACTION_ID_COLOUMN
                    + ") AS " + MAX_COLOUMN
                    + " FROM " + CashFlow.TABLE_NAME + ";", MAX_COLOUMN) + 1;

            return id;

        }

        /// <summary>
        /// Filters out the Rows of the a cash flow table based on a specified month. 
        /// </summary>
        /// <param name="month">The month that rows will be returned for.</param>
        /// <returns></returns>
        public Row[] getRows(DateTime month)
        {

            // Stores the rows from the correct month.
            List<Row> validRows = new List<Row>();

            // Stores the number of the month using two digits.
            String monthString = (month.Month < 10 ? "0" + month.Month : "" + month.Month) + "" + month.Year;

            // Iterates through all the rows in the CashFlow table
            foreach (Row row in base.getRows())
            {
                // The date of the row.
                string date = row.getValue(CashFlow.DATE_COLOUMN);

                string rowMonthString = "" + date[3] + "" + date[4] + "" + date[6] + "" + date[7] + "" + date[8] + "" + date[9];

                // If the month specified and the rows month are the same 
                // then add the current row to the list of valid rows.
                if (monthString.Equals(rowMonthString))
                {
                    validRows.Add(row);
                }
            }


            return validRows.ToArray<Row>();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public int numberOfRows(DateTime month)
        {

            int numberOfRows = 0;

            // Stores the number of the month using two digits.
            String monthString = month.Month < 10 ? "0" + month.Month : "" + month.Month;

            // Iterates through all the rows in the CashFlow table
            foreach (Row row in getRows())
            {
                // The date of the row.
                string date = row.getValue(CashFlow.DATE_COLOUMN);


                if (date.Length > 0)
                {
                    // If the month specified and the rows month are the same 
                    // then add the current row to the list of valid rows.
                    if (monthString.Equals("" + date[3] + "" + date[4]))
                    {
                        numberOfRows++;
                    }
                }
            }


            return numberOfRows;

        }

        /// <summary>
        /// Creates the CashFlow table in the local sql database using the <see cref="SQLDatabase"/>.
        /// </summary>
        public override void create()
        {
            // Holds the sql command to create the CashFlow table.
            string SQL = "CREATE TABLE " + TABLE_NAME + "("
                + TRANSACTION_ID_COLOUMN + " NUMBER(10,0) UNIQUE, "
                + DESCRIPTION_COLOUMN + " VARCHAR(" + DESCRIPTION_LENGTH + ") NOT NULL, "
                + DATE_COLOUMN + " DATE NOT NULL, "
                + AMOUNT_COLOUMN + " NUMBER(5,2) NOT NULL, "
                + "PRIMARY KEY (" + TRANSACTION_ID_COLOUMN + ")"
                + ");";

            // Executes the command
            SQLDatabase.getInstance().executeNONQuery(SQL);

        }

        /// <summary>
        /// Removes a transaction from the table based on the transaction id.
        /// </summary>
        /// <param name="id">The id of the transaction to be removed.</param>
        public void deleteFrom(int id)
        {


            // Construct the query that will remove the transaction from the table in the database.
            string SQL = "DELETE FROM " + TABLE_NAME + " WHERE " + CashFlow.TRANSACTION_ID_COLOUMN + " = " + id;

            // Execute the query that will remove the transaction from the database.
            SQLDatabase.getInstance().executeNONQuery(SQL);

            // Holds the row from the CashFlow table to be removed.
            Row toDelete = null;

            // Iterate through the internal cashflow table to see if the row must be deleted 
            // internally also.
            foreach (Row row in getRows())
            {

                // If the attrabutes of the current row equal those specified in the parameters 
                // then assaign it to be removed. 
                if (id == int.Parse(row.getValue(TRANSACTION_ID_COLOUMN)))
                {
                    toDelete = row;
                }

            }

            // If there is a row to be removed then remove it.
            if (toDelete != null)
            {
                remove(toDelete);
            }
        }

        /// <summary>
        /// Checks if a specified value is in the correct date format to be inserted into 
        /// the CashFlow Table. This method will throw an expection if the value is not valid.
        /// </summary>
        /// <param name="value">Value to be checked.</param>
        private void checkDate(string value)
        {

            // Check the input string is in the correct date format of dd/mm/yyyy
            Regex regex = new Regex(@"[0-9][0-9]/[0-9][0-9]/[0-9][0-9][0-9][0-9]");
            Match match = regex.Match(value);

            // If it is not then throw an exception with an appropriote message.
            if (!match.Success)
            {
                throw new Exception("Date not in correct format.");
            }

        }

        /// <summary>
        /// Checks if a specified value is in the correct description format to be inserted into 
        /// the CashFlow Table. This method will throw an expection if the value is not valid.
        /// </summary>
        /// <param name="value">Value to be checked.</param>
        private void checkDescription(string value)
        {

            // If the description is to long thhrow an exception with an appropriote error message.
            if (value.Length > 50)
            {
                throw new ArgumentException("Description may only be 50 characters long.");
            }

        }

        /// <summary>
        /// Checks if a specified value is in the amount format to be inserted into 
        /// the CashFlow Table. This method will throw an expection if the value is not valid.
        /// </summary>
        /// <param name="value">Value to be checked.</param>
        private void checkAmount(string value)
        {
            // If the value parses as a double value it is valid.
            double amount;
            if (!Double.TryParse(value, out amount))
            {
                throw new Exception("Not a valid number.");
            }


        }

        /// <summary>
        /// Updates a coloumn value in a specified row in the SQL CashFlow table and internal CashFlow table.
        /// </summary>
        /// <param name="id">The row to be modified.</param>
        /// <param name="updatedCol">The column in that row to be updated.</param>
        /// <param name="newValue">The new value for that row.</param>
        public void updateRow(int id, string updatedCol, string newValue)
        {
            // Check if the coloumn to be updated is valid.
            switch (updatedCol)
            {
                case CashFlow.DATE_COLOUMN:
                    checkDate(newValue);
                    break;
                case CashFlow.DESCRIPTION_COLOUMN:
                    checkDescription(newValue);
                    break;
                case CashFlow.AMOUNT_COLOUMN:
                    checkAmount(newValue);
                    break;
                default:
                    throw new ArgumentException("The updatedCol is not a attribute of CashFlow");

            }
            // Exectute the update on the SQL database.
            SQLDatabase.getInstance().executeNONQuery(
                "UPDATE " + TABLE_NAME + " SET " + updatedCol
                + " = " + (updatedCol.Equals(AMOUNT_COLOUMN) ? newValue : "'" + newValue + "'")
                + " WHERE " + TRANSACTION_ID_COLOUMN + " = " + id + ";"
                );

            // Itterate through all the row in the internal CashFlow table.
            foreach (Row row in getRows())
            {

                // If both the rows have the same ID then update the row.
                if (row.getValue(CashFlow.TRANSACTION_ID_COLOUMN).Equals("" + id))
                {
                    row.updateColoumn(updatedCol, newValue);
                    break;
                }
            }
        }

        private class Max : Table
        {

            private const string MAX_COLOUMN = "Maximum";

            public Max()
                : base(new string[]{
            MAX_COLOUMN
            })
            {
                loadFromSource(DateTime.Today);
            }

            public override void create()
            {

            }

            public override void loadFromSource(DateTime startDate)
            {

                SQLDatabase.getInstance().executeQuery("SELECT MAX(" + CashFlow.TRANSACTION_ID_COLOUMN
                    + ") AS " + MAX_COLOUMN
                    + " FROM " + CashFlow.TABLE_NAME + ";", out this.rawTable);

            }

            public int getMax()
            {

                Row row = this.rawTable[0];

                return Int32.Parse(row.getValue(MAX_COLOUMN));
            }

        }
    }
}
