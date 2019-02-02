namespace MyMoney.Core
{
    public interface IView
    {
        void RefreshView();

        void Notify(Type type, Priority priority, string message);
    }
}
