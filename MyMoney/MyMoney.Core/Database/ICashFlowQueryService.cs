using System;

namespace MyMoney.Core.Database
{
    public interface ICashFlowQueryService
    {

        Command SelectAllBetween(DateTime start, DateTime end);

        Command Delete(string id);

        Command Update(string id, string newValue, string updatedCol);

        Command Add(string id, string date, string amount, string description);

        Command Create();

    }
}
