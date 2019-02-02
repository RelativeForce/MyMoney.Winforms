namespace MyMoney.Core
{
    public interface IController
    {
        void AddView(IView view);

        IDatabase Database();

        void RefreshViews();

        void RemoveView(IView view);

        void ChangeDatabaseFile(string dbPath);

        bool IsDatabaseConnected { get; }

        void NotifyViews(Type type, Priority priority, string message);
    }
}
