using System;
using MyMoney.Model.Table;

namespace MyMoney.Controllers.TableControllers
{
    public interface ITableController
    {

        string GetCreationQuery();

        string GetAddQuery(Row newRow);

        string GetUpdateQuery(Row row, string updatedCol);

        string GetPopulateQuery();

        bool IsValidRow(Row row);

        void Clear();

        string GetTableName();

        string GetDeleteQuery(Row row);

        Row[] GetRows(Predicate<Row> check);
        void Add(Row newRow);
    }
}