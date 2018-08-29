using System;

namespace MyMoney.Core.Table
{
    public interface ITableController
    {

        void Create();

        void Add(Row newRow);

        void Update(Row row, string updatedCol);

        void Populate();

        void Delete(Row row);

        bool IsValidRow(Row row);

        void Clear();

        string GetTableName();

        Row[] GetRows(Predicate<Row> check);

        void AddRow(Row row);

    }
}