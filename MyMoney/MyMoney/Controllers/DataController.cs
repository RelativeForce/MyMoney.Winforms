using System;
using System.Collections.Generic;
using MyMoney.Controllers.TableControllers;
using MyMoney.File;
using MyMoney.Model.Database;
using MyMoney.Model.Table;
using MyMoney.Windows;

namespace MyMoney.Controllers
{

    public class DataController : IDataController
    {
        private readonly HashSet<IView> views;

        private readonly List<ITableController> tables;

        private readonly ISQLController sql;

        public DataController(ISQLController sql, List<ITableController> tables) {

            this.sql = sql;
            this.tables = tables;
            this.views = new HashSet<IView>();
                
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

        public void Remove(Row row, string tableName) {

            ITableController table = GetTable(tableName);

            if (table == null) throw new ArgumentException("Invalid table name.");

            string deleteQuery = table.GetDeleteQuery(row);

            sql.Execute(deleteQuery);

        }

        private ITableController GetTable(string tableName) {
            return tables.Find(table => table.GetTableName().Equals(tableName));
        }

        public void Connect()
        {
            sql.Connect(FileStoreManager.DB_FILE_PATH);
        }

        public void Disconnect()
        {
            sql.Disconnect();
        }

        public void SetStartDate(DateTime startDate) {
            CashFlowModel.StartDate = startDate;
        }

        public void AddView(IView view)
        {
            views.Add(view);
        }

        public void RemoveView(IView view)
        {
            views.Remove(view);
        }

        public void RefreshViews() {
            foreach (IView view in views) view.RefreshView();
        }

        public double GetMonthlyAllowance(DateTime date)
        {
            string monthCode = (date.Month < 10 ? "0" + date.Month : "" + date.Month) + "" + date.Year;

            bool sameMonthCode(Row row) => monthCode.Equals(row.getValue(BudgetModel.MONTH_COLOUMN));

            Row[] results = GetRows(sameMonthCode, BudgetModel.TABLE_NAME);

            if (results.Length == 0) {
                return 200;
            }

            string currentValueStr = results[0].getValue(BudgetModel.AMOUNT_COLOUMN);

            return double.Parse(currentValueStr);
        }
    }
 }
