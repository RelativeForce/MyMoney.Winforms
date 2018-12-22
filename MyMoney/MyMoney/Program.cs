using System;
using System.Windows.Forms;
using MyMoney.Core;
using MyMoney.Windows;

namespace MyMoney
{
    static class Startup
    {
        [STAThread]
        static void Main()
        {
            var fileStore = new FileStoreManager();

            IController controller = new Controller();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Home(fileStore, controller));
        }
    }
}
