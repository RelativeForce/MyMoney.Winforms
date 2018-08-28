using System;
using System.Linq;
using MyMoney.Core.Database;
using MyMoney.Core.Model;
using MyMoney.Core.Table;
using MyMoney.Database;

namespace MyMoney.Core.Controllers
{
    public class BudgetController : ITableController
    {
        private readonly IBudgetQueryService _queryService;
        private readonly IDatabaseService _databaseService;
        private readonly Table.Table _rawTable;

        public BudgetController(IBudgetQueryService queryService, IDatabaseService databaseService)
        {
            _queryService = queryService;
            _databaseService = databaseService;
            _rawTable = new Table.Table(BudgetModel.Coloumns());
        }

        public void Create()
        {
            _databaseService.Execute(_queryService.Create());
        }

        public void Add(Row row)
        {

            _rawTable.addRow(row);

            var monthCode = row.getValue(BudgetModel.MONTH_COLOUMN);
            var amount = row.getValue(BudgetModel.AMOUNT_COLOUMN);

            _databaseService.Execute(_queryService.Add(monthCode, amount));

        }

        public void Update(Row row, string updatedCol)
        {

            if (BudgetModel.AMOUNT_COLOUMN.Equals(updatedCol)) throw new ArgumentException("Invalid updated coloumn.");

            var allowance = _rawTable.getRow(BudgetModel.MONTH_COLOUMN, row.getValue(BudgetModel.MONTH_COLOUMN));

            allowance.updateColoumn(BudgetModel.AMOUNT_COLOUMN, row.getValue(BudgetModel.AMOUNT_COLOUMN));

            var monthCode = row.getValue(BudgetModel.MONTH_COLOUMN);
            var newAmount = row.getValue(BudgetModel.AMOUNT_COLOUMN);

            _databaseService.Execute(_queryService.Update(monthCode, newAmount));

        }

        public void Populate()
        {
            _databaseService.PopulateTable(_queryService.Populate(), this);
        }

        public bool IsValidRow(Row row)
        {
            return _rawTable.check(row);
        }

        public Row[] GetRows(Predicate<Row> check)
        {
            return _rawTable.getRows().Where(check.Invoke).ToArray();

        }

        public void Clear()
        {
            _rawTable.clear();
        }

        public string GetTableName()
        {
            return BudgetModel.TABLE_NAME;
        }

        public void Delete(Row row)
        {
            _rawTable.remove(row);

            var monthCode = row.getValue(BudgetModel.MONTH_COLOUMN);

            _databaseService.Execute(_queryService.Delete(monthCode));

        }

        public void AddRow(Row row)
        {
            _rawTable.addRow(row);
        }
    }
}
