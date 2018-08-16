using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMoney.Data_Storage;

namespace MyMoney.Database
{
    public class Repeat : Table
    {
        public static Repeat INSTANCE = new Repeat();

        public const string START_DATE_COLOUMN = "Start_Date";

        public const string END_DATE_COLOUMN = "End_Date";

        public const string TABLE_NAME = "Repeat";

        public Repeat()
            : base(new string[]{
                CashFlow.TRANSACTION_ID_COLOUMN,
                START_DATE_COLOUMN,
                END_DATE_COLOUMN
            })
        { }

        // TODO: Add support for repeated transactions. This include an update to Cashflow to allow modification to multiple elements of the intenal table at onnce.
         
        /// <summary>
        /// Loads the Repeat table from the database file.
        /// </summary>
        /// <param name="startDate">Unused.</param>
        public override void loadFromSource(DateTime startDate)
        {
            // TODO: Load the Repeat table into internal storage.
        }


        /// <summary>
        /// Creates the Repeat table in the local sql database using the <see cref="SQLHandler"/>.
        /// </summary>
        public override void create()
        {
            // Holds the sql command to create the Repeat table.
            string SQL = "CREATE TABLE " + TABLE_NAME + "("
                + CashFlow.TRANSACTION_ID_COLOUMN + " NUMBER(10,0) UNIQUE, "
                + START_DATE_COLOUMN + " DATE NOT NULL, "
                + END_DATE_COLOUMN + " DATE NOT NULL, "
                + "PRIMARY KEY (" + CashFlow.TRANSACTION_ID_COLOUMN + "),"
                + "FOREIGN KEY (" + CashFlow.TRANSACTION_ID_COLOUMN + ") REFERENCES " + CashFlow.TABLE_NAME + "(" + CashFlow.TRANSACTION_ID_COLOUMN + ")"
                + ");";

            // Executes the command
            SQLHandler.getInstance().executeNONQuery(SQL);

        }
    }
}
