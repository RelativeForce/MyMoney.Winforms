using MyMoney.Controllers.TableControllers;
using System.Collections.Generic;

namespace MyMoney.Controllers
{
    public interface ISQLController
    {

        void Execute(string SQL);

        void Connect();

        int GetValueInt(string sql, string coloumnName);

        void PopulateTable(string sql, ITableController table);

        bool CheckColoumnTitles(string SQL, IReadOnlyList<string> columns);

        void TryCreateDBFile();

        void Disconnect();
    }
}
