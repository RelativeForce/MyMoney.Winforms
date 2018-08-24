using System;
using System.Collections.Generic;
using MyMoney.Model.Database;
using MyMoney.Model.Table;

namespace MyMoney.Controllers.TableControllers
{
    public class BudgetController : ITableController
    {
        private Table rawTable;

        public BudgetController()
        {
            rawTable = new Table(BudgetModel.Coloumns());
        }

        public string GetCreationQuery()
        {
            string SQL = "CREATE TABLE " + BudgetModel.TABLE_NAME + "("
                    + BudgetModel.MONTH_COLOUMN + " VARCHAR(6) UNIQUE, "
                    + BudgetModel.AMOUNT_COLOUMN + " NUMBER(5,2) NOT NULL, "
                    + "PRIMARY KEY (" + BudgetModel.MONTH_COLOUMN + ")"
                    + ");";

            return SQL;
        }

        public string GetAddQuery(Row row)
        {

            rawTable.addRow(row);

            return "INSERT INTO " + BudgetModel.TABLE_NAME
                + " VALUES ('" + row.getValue(BudgetModel.MONTH_COLOUMN)
                + "', " + row.getValue(BudgetModel.AMOUNT_COLOUMN) + ");";

        }

        public string GetUpdateQuery(Row row, string updatedCol)
        {

            if (BudgetModel.AMOUNT_COLOUMN.Equals(updatedCol)) throw new ArgumentException("Invalid updated coloumn.");

            Row allowance = rawTable.getRow(BudgetModel.MONTH_COLOUMN, row.getValue(BudgetModel.MONTH_COLOUMN));

            allowance.updateColoumn(BudgetModel.AMOUNT_COLOUMN, row.getValue(BudgetModel.AMOUNT_COLOUMN));

            return "UPDATE " + BudgetModel.TABLE_NAME
                + " SET " + BudgetModel.AMOUNT_COLOUMN + " = " + row.getValue(BudgetModel.AMOUNT_COLOUMN)
                + " WHERE " + BudgetModel.MONTH_COLOUMN + " = '" + row.getValue(BudgetModel.MONTH_COLOUMN) + "';"; ;

        }

        public string GetPopulateQuery()
        {
            return "SELECT * FROM " + BudgetModel.TABLE_NAME + ";";
        }

        public bool IsValidRow(Row row)
        {
            return rawTable.check(row);
        }

        public Row[] GetRows(Predicate<Row> check)
        {

            List<Row> validRows = new List<Row>();

            foreach (Row row in rawTable.getRows())
                if (check.Invoke(row))
                    validRows.Add(row);

            return validRows.ToArray();

        }

        public void Clear()
        {
            rawTable.clear();
        }

        public string GetTableName()
        {
            return BudgetModel.TABLE_NAME;
        }

        public string GetDeleteQuery(Row row)
        {
            rawTable.remove(row);

            return "DELETE FROM " + BudgetModel.TABLE_NAME + " WHERE " + BudgetModel.MONTH_COLOUMN + " = " + row.getValue(BudgetModel.MONTH_COLOUMN);

        }

        public void Add(Row row)
        {
            rawTable.addRow(row);
        }
    }
}
