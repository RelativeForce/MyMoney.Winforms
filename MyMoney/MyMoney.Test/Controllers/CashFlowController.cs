using System;
using System.Linq;
using System.Text.RegularExpressions;
using MyMoney.Core.Database;
using MyMoney.Core.Model;
using MyMoney.Core.Table;
using MyMoney.Database;

namespace MyMoney.Core.Controllers
{

    public class CashFlowController : ITableController
    {
        private readonly ICashFlowQueryService _queryService;
        private readonly IDatabaseService _databaseService;

        private readonly Table.Table _rawTable;

        public CashFlowController(ICashFlowQueryService queryService, IDatabaseService databaseService)
        {
            _queryService = queryService;
            _databaseService = databaseService;
            _rawTable = new Table.Table(CashFlowModel.Coloumns());
        }

        public void Populate()
        {

            var endDate = CashFlowModel.StartDate.AddMonths(1);

            _databaseService.PopulateTable(_queryService.SelectAllBetween(CashFlowModel.StartDate, endDate), this);

        }

        public void Add(Row row)
        {

            if (row == null) throw new ArgumentNullException(nameof(row) + " cannot be null.");

            var description = row.GetValue(CashFlowModel.DESCRIPTION_COLOUMN);
            var amount = row.GetValue(CashFlowModel.AMOUNT_COLOUMN);
            var date = row.GetValue(CashFlowModel.DATE_COLOUMN);
            var id = row.GetValue(CashFlowModel.TRANSACTION_ID_COLOUMN);

            var rows = _rawTable.GetRows();

            if (rows.Length <= 0)
                _rawTable.InsertRow(0, row);
            else
                InsertRow(row, rows);

            _databaseService.Execute(_queryService.Add(id, date, amount, description));

        }

        public void Clear()
        {
            _rawTable.Clear();
        }

        public string GetTableName()
        {
            return CashFlowModel.TABLE_NAME;
        }

        public void Create()
        {
            _databaseService.Execute(_queryService.Create());
        }

        public Row[] GetRows(Predicate<Row> check)
        {
            return _rawTable.GetRows().Where(check.Invoke).ToArray();
        }

        public void Delete(Row row)
        {
            _rawTable.Remove(row);

            var id = row.GetValue(CashFlowModel.TRANSACTION_ID_COLOUMN);

            _databaseService.Execute(_queryService.Delete(id));

        }

        public void Update(Row row, string updatedCol)
        {

            var newValue = row.GetValue(updatedCol);
            var id = row.GetValue(CashFlowModel.TRANSACTION_ID_COLOUMN);

            CheckNewValue(updatedCol, newValue);

            foreach (var currentRow in _rawTable.GetRows())
            {
                if (!currentRow.GetValue(CashFlowModel.TRANSACTION_ID_COLOUMN).Equals(id)) continue;

                currentRow.UpdateColoumn(updatedCol, newValue);
                break;
            }

            _databaseService.Execute(_queryService.Update(id, newValue, updatedCol));


        }

        public bool IsValidRow(Row row)
        {

            return _rawTable.Check(row);

        }

        private void CheckDate(string value)
        {

            // Check the input string is in the correct date format of dd/mm/yyyy
            var regex = new Regex(@"[0-9][0-9]/[0-9][0-9]/[0-9][0-9][0-9][0-9]");
            var match = regex.Match(value);

            // If it is not then throw an exception with an appropriote message.
            if (!match.Success)
            {
                throw new Exception("Date not in correct format.");
            }

        }

        private void CheckDescription(string value)
        {

            // If the description is to long thhrow an exception with an appropriote error message.
            if (value.Length > CashFlowModel.DESCRIPTION_LENGTH)
            {
                throw new ArgumentException($"Description may only be {CashFlowModel.DESCRIPTION_LENGTH} characters long.");
            }

        }

        private void CheckAmount(string value)
        {
            // If the value parses as a double value it is valid.
            if (!double.TryParse(value, out var amount))
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
            DateTime paramDate = DateTime.Parse(row.GetValue(CashFlowModel.DATE_COLOUMN));

            int first = 0;
            int last = rows.Length - 1;
            int mid = last / 2;
            bool found = false;

            while (!found && first < last)
            {

                mid = (first + last) / 2;

                DateTime rowDate = DateTime.Parse(rows[mid].GetValue(CashFlowModel.DATE_COLOUMN));

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

            DateTime lastRowDate = DateTime.Parse(rows[mid].GetValue(CashFlowModel.DATE_COLOUMN));

            if (lastRowDate > paramDate && !found)
            {
                _rawTable.InsertRow(mid + 1, row);
            }
            else
            {
                _rawTable.InsertRow(mid, row);
            }
        }

        public void AddRow(Row row)
        {
            _rawTable.AddRow(row);
        }
    }
}
