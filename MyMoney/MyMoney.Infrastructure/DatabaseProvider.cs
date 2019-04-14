using MyMoney.Core;

namespace MyMoney.Infrastructure
{
    public sealed class DatabaseProvider : IDatabaseProvider
    {
        public IDatabase NewInstance => new Database();
    }
}
