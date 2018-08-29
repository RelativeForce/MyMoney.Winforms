using System.Collections.Generic;
using MyMoney.Core.Database;
using MyMoney.Core.Table;

namespace MyMoney.Database
{
    public interface IDatabaseService
    {

        void Execute(Command command);

        void Connect(string filePath);

        int GetValueInt(Command command, string coloumnName);

        void PopulateTable(Command command, ITableController table);

        bool CheckColoumnTitles(Command command, IReadOnlyList<string> columns);

        void TryCreateDatabaseFile();

        void Disconnect();
    }
}
