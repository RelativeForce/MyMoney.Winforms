using System;
using System.Collections.Generic;
using MyMoney.File;
using MyMoney.Core;
using MyMoney.Core.Database;
using MyMoney.Core.Model;
using MyMoney.Core.Table;
using MyMoney.Database;

namespace MyMoney.Controllers
{

   public class DataController : IDataController
    {
        private readonly HashSet<IView> _views;

        private readonly List<ITableController> _tables;

        private readonly IDatabaseService _databaseService;

        public DataController(IDatabaseService databaseService, List<ITableController> tables) {

            this._databaseService = databaseService;
            this._tables = tables;
            _views = new HashSet<IView>();
                
        }

        public void Create()
        {
            _databaseService.TryCreateDatabaseFile();

            _tables.ForEach(table => table.Create());
        }

        public void Load() {

            Clear();

            Console.WriteLine("Internal Storge Cleared");

            _tables.ForEach(table => table.Populate());

            Console.WriteLine("Tables Loaded");

        }

        public void Clear() {

            _tables.ForEach(table => table.Clear());

        }

        public Row[] GetRows(Predicate<Row> check, string tableName) {

            var table = GetTable(tableName);

            if (table == null) throw new ArgumentException("Invalid table name.");

            return table.GetRows(check);

        }

        public void Add(Row row, string tableName) {

            var table = GetTable(tableName);

            if (table == null) throw new ArgumentException("Invalid table name.");

            table.Add(row);
        }

        public int NumberOfRows(Predicate<Row> check, string tableName)
        {
            return GetRows(check, tableName).Length;
        }

       public int GetAvalaibleTransactionID()
       {
          const string maxColoumn = "Maximum";

          var command = new Command(
             $"SELECT MAX({CashFlowModel.TRANSACTION_ID_COLOUMN}) AS {maxColoumn} FROM {CashFlowModel.TABLE_NAME};"
          );

          // The highest transaction id in the CashFlowController table plus one.
          return _databaseService.GetValueInt(command, maxColoumn) + 1;

        }

        public void Update(Row row, string tableName, string updatedCol) {

            var table = GetTable(tableName);

            if (table == null) throw new ArgumentException("Invalid table name.");

            table.Update(row, updatedCol); 
        }

        public void Remove(Row row, string tableName) {

            var table = GetTable(tableName);

            if (table == null) throw new ArgumentException("Invalid table name.");

            table.Delete(row);

        }

        private ITableController GetTable(string tableName) {
            return _tables.Find(table => table.GetTableName().Equals(tableName));
        }

        public void Connect()
        {
            _databaseService.Connect(FileStoreManager.DB_FILE_PATH);
            Console.WriteLine("Connected to - " + FileStoreManager.DB_FILE_PATH);
        }

        public void Disconnect()
        {
            _databaseService.Disconnect();
        }

        public void SetStartDate(DateTime startDate) {
            CashFlowModel.StartDate = startDate;
        }

        public void AddView(IView view)
        {
            _views.Add(view);
        }

        public void RemoveView(IView view)
        {
            _views.Remove(view);
        }

        public void RefreshViews() {
            foreach (IView view in _views) view.RefreshView();
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
