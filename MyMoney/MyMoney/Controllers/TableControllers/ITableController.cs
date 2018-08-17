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

        string GetDeleteQuery(Row row);

        bool IsValidRow(Row row);

        void Clear();

        string GetTableName();

        Row[] GetRows(Predicate<Row> check);

        void Add(Row row);

    }
}