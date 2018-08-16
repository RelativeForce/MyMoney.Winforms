using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMoney.Data_Storage;
namespace MyMoney.Database
{
    public class Budget : Table
    {

        /// <summary>
        /// The singleton global instance of the Budget Table.
        /// </summary>
        private static Budget INSTANCE = new Budget();

        /// <summary>
        /// The title of the month coloumn.
        /// </summary>
        public const string MONTH_COLOUMN = "Month";

        /// <summary>
        /// The title of the Amount Coloum.
        /// </summary>
        public const string AMOUNT_COLOUMN = "Amount";

        /// <summary>
        /// The title of the Budget Table.
        /// </summary>
        public const string TABLE_NAME = "Budget";

        /// <summary>
        /// Constructs an new Budget Table.
        /// </summary>
        private Budget()
            : base(new string[] { MONTH_COLOUMN, AMOUNT_COLOUMN })
        {

        }

        /// <summary>
        /// Retrieves the instance of the Budget singleton.
        /// </summary>
        /// <returns>Budget Table</returns>
        public static Budget getInstance()
        {
            return INSTANCE;
        }

        /// <summary>
        /// Imports the contents of the Budget table from the database file.
        /// </summary>
        public override void loadFromSource(DateTime date)
        {
            SQLHandler.getInstance().executeQuery("SELECT * FROM " + TABLE_NAME + ";", out this.rawTable);
            Console.WriteLine("Budget Loaded");
        }

        /// <summary>
        /// Creates the Budget table in the database file.
        /// </summary>
        public override void create()
        {

            string SQL = "CREATE TABLE " + TABLE_NAME + "("
                    + MONTH_COLOUMN + " VARCHAR(6) UNIQUE, "
                    + AMOUNT_COLOUMN + " NUMBER(5,2) NOT NULL, "
                    + "PRIMARY KEY (" + MONTH_COLOUMN + ")"
                    + ");";

            // Executes the command
            SQLHandler.getInstance().executeNONQuery(SQL);

        }

        /// <summary>
        /// Adds a row to the Budget table. This overloads 
        /// the <see cref="Table#AddRow"/> as rows in Budget require 
        /// aditional checks.
        /// </summary>
        /// <param name="row">Row to be added.</param>
        public new void addRow(Row row)
        {

            // If the budget for the month specified in the row is all ready present in the table.
            if (!getBudget(row.getValue(MONTH_COLOUMN)).Equals(Double.NaN))
            {

                // Retrieve the row in the table with the same month code as the specified row. 
                // Then change the value assigned to the amount coloumn in that row to reflect 
                // the new value in the specified row. 
                Row allowance = base.getRow(MONTH_COLOUMN, row.getValue(MONTH_COLOUMN));

                allowance.updateColoumn(AMOUNT_COLOUMN, row.getValue(AMOUNT_COLOUMN));

                SQLHandler.getInstance().executeNONQuery(
                    "UPDATE " + TABLE_NAME
                    + " SET " + AMOUNT_COLOUMN + " = " + row.getValue(AMOUNT_COLOUMN)
                    + " WHERE " + MONTH_COLOUMN + " = '" + row.getValue(MONTH_COLOUMN) + "';"
                    );


            }
            else
            {
                // Add the row to the table
                base.addRow(row);

                // Add the entry to the SQL database.
                SQLHandler.getInstance().executeNONQuery(
                    "INSERT INTO " + TABLE_NAME
                    + " VALUES ('" + row.getValue(MONTH_COLOUMN)
                    + "', " + row.getValue(AMOUNT_COLOUMN) + ");");
            }
        }

        /// <summary>
        /// Gets the budget of a certian month. 
        /// </summary>
        /// <param name="date">
        /// Month string that denotes the date.
        /// </param>
        /// <returns>
        /// The budget of that month. Return NaN if the specified 
        /// month doesn't have a budget.
        /// </returns>
        public double getBudget(string date)
        {
            // The inital value of the budget.
            double budget = Double.NaN;

            try
            {
                // Iterate through all the months with assigned budgets.
                foreach (Row row in getRows())
                {

                    // If the current row has the same month string as the parameter row.
                    if (row.getValue(MONTH_COLOUMN).Equals(date))
                    {
                        return Double.Parse(row.getValue(AMOUNT_COLOUMN)); 
                    }
                }
            }
            catch (Exception e)
            {
                // Feedback exception.
                Console.Out.WriteLine(e.Message);
            }

            return budget;
        }
    
    }
}
