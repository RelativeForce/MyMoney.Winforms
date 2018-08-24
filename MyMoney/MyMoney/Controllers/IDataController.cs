using System;
using MyMoney.Model.Table;
using MyMoney.Windows;

namespace MyMoney.Controllers
{
    public interface IDataController
    {

        void Create();

        void Load();

        void Clear();

        Row[] GetRows(Predicate<Row> check, string tableName);

        void Add(Row row, string tableName);

        int NumberOfRows(Predicate<Row> check, string tableName);

        int GetAvalaibleTransactionID();

        void Update(Row row, string tableName, string updatedCol);

        void Connect();

        void Disconnect();

        void SetStartDate(DateTime startDate);

        void AddView(IView view);

        void RemoveView(IView view);

        void RefreshViews();

        void Remove(Row row, string tableName);

        double GetMonthlyAllowance(DateTime date);
    }
}
