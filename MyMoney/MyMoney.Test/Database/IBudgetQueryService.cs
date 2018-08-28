namespace MyMoney.Core.Database
{
   public interface IBudgetQueryService
   {

      Command Create();

      Command Populate();

      Command Delete(string monthCode);

      Command Update(string monthCode, string newAmount);

      Command Add(string monthCode, string amount);

   }
}
