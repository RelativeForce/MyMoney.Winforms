using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MyMoney.Controllers;
using MyMoney.Controllers.TableControllers;
using MyMoney.File;
using MyMoney.Windows;

namespace MyMoney
{
    static class Program
    {
        [STAThread]
        static void Main()
        {

            ISQLController sql = new SQLController();

            List<ITableController> tables = new List<ITableController>
            {
                new BudgetController(),
                new CashFlowController()
            };

            IDataController controller = new DataController(sql, tables);

            FileStoreManager fileStore = new FileStoreManager();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Home(controller, fileStore));
        }
    }
}
