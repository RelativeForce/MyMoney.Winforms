using System.Collections.Generic;
using MyMoney.Controllers.TableControllers;

namespace MyMoney.Controllers
{
    public interface ISQLController
    {

        void Execute(string SQL);

        void Connect(string filePath);

        int GetValueInt(string sql, string coloumnName);

        void PopulateTable(string sql, ITableController table);

        bool CheckColoumnTitles(string SQL, IReadOnlyList<string> columns);

        void TryCreateDBFile();

        void Disconnect();
    }
}
