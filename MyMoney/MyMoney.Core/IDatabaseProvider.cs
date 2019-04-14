namespace MyMoney.Core
{
    public interface IDatabaseProvider
    {
        IDatabase NewInstance { get; }
    }
}
