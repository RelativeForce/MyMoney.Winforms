using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MyMoney.Controllers;
using MyMoney.Core;
using MyMoney.Core.Controllers;
using MyMoney.Core.Database;
using MyMoney.Core.Table;
using MyMoney.Database;
using MyMoney.File;
using MyMoney.Windows;

namespace MyMoney
{
    static class Program
    {
        [STAThread]
        static void Main()
        {

            ICashFlowQueryService cfqs = new SqlCashFlowQueryService();
            IBudgetQueryService bqs = new SqlBudgetQueryService();

            IDatabaseService sql = new SqlDatabaseService();

            var tables = new List<ITableController>
            {
                new BudgetController(bqs, sql),
                new CashFlowController(cfqs, sql)
            };

            IDataController controller = new DataController(sql, tables);

            var fileStore = new FileStoreManager();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Home(controller, fileStore));
        }
    }
}
