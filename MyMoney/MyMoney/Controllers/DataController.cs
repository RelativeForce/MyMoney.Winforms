using System;
using System.Collections.Generic;
using MyMoney.Controllers.TableControllers;
using MyMoney.File;
using MyMoney.Model.Database;
using MyMoney.Model.Table;

namespace MyMoney.Controllers
{

    public class DataController : IDataController
    {

        private readonly List<ITableController> tables;

        private readonly ISQLController sql;

        private DataController(ISQLController sql) {

            this.sql = sql;

            tables = new List<ITableController>
            {
                new BudgetController(),
                new CashFlowController()
            };

        }

        public void Create()
        {
            sql.TryCreateDBFile();

            tables.ForEach(table => sql.Execute(table.GetCreationQuery()));
        }

        public void Load() {

            Clear();

            Console.WriteLine("Internal Storge Cleared");

            tables.ForEach(table => sql.PopulateTable(table.GetPopulateQuery(), table));

            Console.WriteLine("Tables Loaded");

        }

        public void Clear() {

            tables.ForEach(table => table.Clear());

        }

        public Row[] GetRows(Predicate<Row> check, string tableName) {

            ITableController table = GetTable(tableName);

            if (table == null) throw new ArgumentException("Invalid table name.");

            return table.GetRows(check);

        }

        public void Add(Row row, string tableName) {

            ITableController table = GetTable(tableName);

            if (table == null) throw new ArgumentException("Invalid table name.");

            string addQuery = table.GetAddQuery(row);

            sql.Execute(addQuery);


        }

        public int NumberOfRows(Predicate<Row> check, string tableName)
        {
            return GetRows(check, tableName).Length;
        }

        public int GetAvalaibleTransactionID()
        {
            string MAX_COLOUMN = "Maximum";

            // Hold the highest transaction id in the CashFlowController table plus one.
            int id = sql.GetValueInt(
                "SELECT MAX(" + CashFlowModel.TRANSACTION_ID_COLOUMN + ") AS " + MAX_COLOUMN
                    + " FROM " + CashFlowModel.TABLE_NAME + ";", MAX_COLOUMN) + 1;

            return id;

        }

        public void Update(Row row, string tableName, string updatedCol) {

            ITableController table = GetTable(tableName);

            if (table == null) throw new ArgumentException("Invalid table name.");

            string updateQuery = table.GetUpdateQuery(row, updatedCol);

            sql.Execute(updateQuery);

        }

        private ITableController GetTable(string tableName) {
            return tables.Find(table => table.GetTableName().Equals(tableName));
        }
    }

 }
