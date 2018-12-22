using MyMoney.Core;
using System.Collections.Generic;

namespace MyMoney
{
    public class Controller : IController
    {

        private readonly List<IView> views;

        public Controller() {
            views = new List<IView>();
        }

        public bool IsDatabaseConnected { get => Infrastructure.Database.IsDatabaseConnected; }


        public void AddView(IView view)
        {
            if(!views.Contains(view)) views.Add(view);
        }

        public void ChangeDatabaseFile(string dbPath)
        {
            Infrastructure.Database.SetConnection(dbPath);
            RefreshViews();
        }

        public void CreateDatabaseFile(string dbPath)
        {
            System.IO.File.Create(dbPath);
            ChangeDatabaseFile(dbPath);
        }

        public IDatabase Database()
        {
            return new Infrastructure.Database();
        }

        public void RefreshViews()
        {
            views.ForEach(v => v.RefreshView());
        }

        public void RemoveView(IView view)
        {
            views.Remove(view);
        }
    }
}
