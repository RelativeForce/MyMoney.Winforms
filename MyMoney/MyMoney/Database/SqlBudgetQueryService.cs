using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMoney.Core.Database;
using static MyMoney.Core.Model.BudgetModel;

namespace MyMoney.Database
{
   public class SqlBudgetQueryService : IBudgetQueryService
   {
      public Command Create()
      {
         var commandText = $"CREATE TABLE {TABLE_NAME} ({MONTH_COLOUMN} VARCHAR(6) UNIQUE, {AMOUNT_COLOUMN} NUMBER(5,2) NOT NULL, PRIMARY KEY ({MONTH_COLOUMN}));";

         return new Command(commandText);
      }

      public Command Populate()
      {
         var commandText = $"SELECT * FROM {TABLE_NAME};";

         return new Command(commandText);
      }

      public Command Delete(string monthCode)
      {
         var commandText = $"DELETE FROM {TABLE_NAME} WHERE {MONTH_COLOUMN} = {monthCode};";

         return new Command(commandText);
      }

      public Command Update(string monthCode, string newAmount)
      {
         var commandText = $"UPDATE {TABLE_NAME} SET {AMOUNT_COLOUMN} = {newAmount} WHERE {MONTH_COLOUMN} = '{monthCode}';";

         return new Command(commandText);
      }

      public Command Add(string monthCode, string amount)
      {
         var commandText = $"INSERT INTO {TABLE_NAME} VALUES ('{monthCode}', {amount});";

         return new Command(commandText);
      }
   }
}
