using MyMoney.File;
using MyMoney.Model.Table;
using MyMoney.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyMoney.Controllers.TableControllers
{

    public class CashFlowController : ITableController
    {

        private DateTime StartDate
        {
            set { StartDate = value; }
            get { return StartDate; }
        }

        private Table rawTable;

        public CashFlowController()
        {

            rawTable = new Table(CashFlowModel.Coloumns());

            StartDate = DateTime.Now;
        }

        public string GetPopulateQuery()
        {

            //TODO: Add the abillity for the repeat transactions to be added to the table even if the orignial transaction occured before the current month.

            DateTime endDate = StartDate.AddMonths(1);

            string endMonthString = endDate.ToShortDateString();

            string startDateString = StartDate.ToShortDateString();

            // Puts the date in the yyyy-mm-dd
            string startMonth = startDateString[6] + "" + startDateString[7] + "" + startDateString[8] + "" + startDateString[9]
                + "-" + startDateString[3] + "" + startDateString[4]
                + "-" + "01";

            string endMonth = endMonthString[6] + "" + endMonthString[7] + "" + endMonthString[8] + "" + endMonthString[9]
                + "-" + endMonthString[3] + "" + endMonthString[4]
                + "-" + "01";

            return "SELECT * FROM CashFlow"
                + " WHERE " + CashFlowModel.DATE_COLOUMN
                + " BETWEEN '" + startMonth + "' AND '" + endMonth + "'"
                + " ORDER BY " + CashFlowModel.DATE_COLOUMN + " DESC;";


        }

        public string GetAddQuery(Row row)
        {

            if (row == null) throw new ArgumentNullException("Row cannot be null.");

            string sql = GetAddQueryString(row);

            Row[] rows = rawTable.getRows();

            if (rows.Length <= 0)
            {
                rawTable.insertRow(0, row);
                return sql;
            }

            InsertRow(row, rows);

            return sql;

        }

        public void Clear()
        {
            rawTable.clear();
        }

        public string GetTableName()
        {
            return CashFlowModel.TABLE_NAME;
        }

        public string GetCreationQuery()
        {

            string SQL = "CREATE TABLE " + CashFlowModel.TABLE_NAME + "("
                + CashFlowModel.TRANSACTION_ID_COLOUMN + " NUMBER(10,0) UNIQUE, "
                + CashFlowModel.DESCRIPTION_COLOUMN + " VARCHAR(" + CashFlowModel.DESCRIPTION_LENGTH + ") NOT NULL, "
                + CashFlowModel.DATE_COLOUMN + " DATE NOT NULL, "
                + CashFlowModel.AMOUNT_COLOUMN + " NUMBER(5,2) NOT NULL, "
                + "PRIMARY KEY (" + CashFlowModel.TRANSACTION_ID_COLOUMN + ")"
                + ");";

            return SQL;

        }

        public Row[] GetRows(Predicate<Row> check)
        {

            List<Row> validRows = new List<Row>();

            foreach (Row row in rawTable.getRows())
            {
 
                if (check.Invoke(row))
                {
                    validRows.Add(row);
                }
            }


            return validRows.ToArray();

        }

        public string GetDeleteQuery(Row row)
        {
            rawTable.remove(row);

            return "DELETE FROM " + CashFlowModel.TABLE_NAME + " WHERE " + CashFlowModel.TRANSACTION_ID_COLOUMN + " = " + row.getValue(CashFlowModel.TRANSACTION_ID_COLOUMN);

        }

        public string GetUpdateQuery(Row row, string updatedCol)
        {

            string newValue = row.getValue(updatedCol);
            string id = row.getValue(CashFlowModel.TRANSACTION_ID_COLOUMN);

            CheckNewValue(updatedCol, newValue);

            foreach (Row currentRow in rawTable.getRows())
            {
                if (currentRow.getValue(CashFlowModel.TRANSACTION_ID_COLOUMN).Equals(id))
                {
                    currentRow.updateColoumn(updatedCol, newValue);
                    break;
                }
            }

            return "UPDATE " + CashFlowModel.TABLE_NAME + " SET " + updatedCol
                + " = " + (updatedCol.Equals(CashFlowModel.AMOUNT_COLOUMN) ? newValue : "'" + newValue + "'")
                + " WHERE " + CashFlowModel.TRANSACTION_ID_COLOUMN + " = " + id + ";";


        }

        public bool IsValidRow(Row row) {

            return rawTable.check(row);

        }

        private void CheckDate(string value)
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

        private void CheckDescription(string value)
        {

            // If the description is to long thhrow an exception with an appropriote error message.
            if (value.Length > 50)
            {
                throw new ArgumentException("Description may only be 50 characters long.");
            }

        }

        private void CheckAmount(string value)
        {
            // If the value parses as a double value it is valid.
            if (!double.TryParse(value, out double amount))
            {
                throw new Exception("Not a valid number.");
            }


        }

        private void CheckNewValue(string updatedCol, string newValue)
        {

            switch (updatedCol)
            {
                case CashFlowModel.DATE_COLOUMN:
                    CheckDate(newValue);
                    break;
                case CashFlowModel.DESCRIPTION_COLOUMN:
                    CheckDescription(newValue);
                    break;
                case CashFlowModel.AMOUNT_COLOUMN:
                    CheckAmount(newValue);
                    break;
                default:
                    throw new ArgumentException("The updatedCol is not a attribute of " + CashFlowModel.TABLE_NAME);

            }
        }

        private void InsertRow(Row row, Row[] rows)
        {
            DateTime paramDate = DateTime.Parse(row.getValue(CashFlowModel.DATE_COLOUMN));

            int first = 0;
            int last = rows.Length - 1;
            int mid = last / 2;
            bool found = false;

            while (!found && first < last)
            {

                mid = (first + last) / 2;

                DateTime rowDate = DateTime.Parse(rows[mid].getValue(CashFlowModel.DATE_COLOUMN));

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

            DateTime lastRowDate = DateTime.Parse(rows[mid].getValue(CashFlowModel.DATE_COLOUMN));

            if (lastRowDate > paramDate && !found)
            {
                rawTable.insertRow(mid + 1, row);
            }
            else
            {
                rawTable.insertRow(mid, row);
            }
        }

        private string GetAddQueryString(Row row)
        {
            string description = row.getValue(CashFlowModel.DESCRIPTION_COLOUMN);

            double amount = double.Parse(row.getValue(CashFlowModel.AMOUNT_COLOUMN));

            string date = row.getValue(CashFlowModel.DATE_COLOUMN);

            int id = int.Parse(row.getValue(CashFlowModel.TRANSACTION_ID_COLOUMN));

            // Puts the date in the yyyy-mm-dd
            string formattedDate = date[6] + "" + date[7] + "" + date[8] + "" + date[9]
                + "-" + date[3] + "" + date[4]
                + "-" + date[0] + "" + date[1];

            return "INSERT INTO CashFlow VALUES ('" + id
                 + "', '" + description
                 + "', '" + formattedDate
                 + "', " + amount + ")";
        }


    }
}
