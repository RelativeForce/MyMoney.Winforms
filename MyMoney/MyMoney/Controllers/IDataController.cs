using MyMoney.Model.Table;
using System;

namespace MyMoney.Controllers
{
    interface IDataController
    {

        void Create();

        void Load();

        void Clear();

        Row[] GetRows(Predicate<Row> check, string tableName);

        void Add(Row row, string tableName);

        int NumberOfRows(Predicate<Row> check, string tableName);

        int GetAvalaibleTransactionID();

        void Update(Row row, string tableName, string updatedCol);


    }
}
