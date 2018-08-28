using System;
using MyMoney.Core.Database;
using MyMoney.Core.Model;

namespace MyMoney.Database
{
   public class SqlCashFlowQueryService : ICashFlowQueryService
   {

      private const string DateFormat = "{0}{1}{2}{3}-{4}{5}-{6}{7}";

      public Command SelectAllBetween(DateTime start, DateTime end)
      {
         var endMonthString = end.ToShortDateString();

         var startDateString = start.ToShortDateString();
         
         var startMonth = string.Format(DateFormat,
            startDateString[6],
            startDateString[7],
            startDateString[8],
            startDateString[9],
            startDateString[3],
            startDateString[4],
            "0", "1");

         var endMonth = string.Format(DateFormat,
            endMonthString[6],
            endMonthString[7],
            endMonthString[8],
            endMonthString[9],
            endMonthString[3],
            endMonthString[4],
            "0", "1");

         var queryText =
            $"SELECT * FROM {CashFlowModel.TABLE_NAME} WHERE {CashFlowModel.DATE_COLOUMN} BETWEEN '{startMonth}' AND '{endMonth}' ORDER BY {CashFlowModel.DATE_COLOUMN} DESC;";

         return new Command(queryText);
      }

      public Command Delete(string id)
      {
         var commandText = $"DELETE FROM {CashFlowModel.TABLE_NAME} WHERE {CashFlowModel.TRANSACTION_ID_COLOUMN} = {id}";

         return new Command(commandText);
      }

      public Command Update(string id, string newValue, string updatedCol)
      {

         var command =
            $"UPDATE {CashFlowModel.TABLE_NAME} SET {updatedCol} = {newValue} WHERE {CashFlowModel.TRANSACTION_ID_COLOUMN} = {id};";

         return new Command(command);

      }

      public Command Add(string id, string date, string amount, string description)
      {

         var formattedDate = string.Format(DateFormat,
            date[6],
            date[7],
            date[8],
            date[9],
            date[3],
            date[4],
            date[0],
            date[1]);

         var commandText = $"INSERT INTO CashFlow VALUES ('{id}', '{description}', '{formattedDate}', {amount})";

         return new Command(commandText);

      }

      public Command Create()
      {
         var commandText =
            $"CREATE TABLE {CashFlowModel.TABLE_NAME}({CashFlowModel.TRANSACTION_ID_COLOUMN} NUMBER(10,0) UNIQUE, {CashFlowModel.DESCRIPTION_COLOUMN} VARCHAR({CashFlowModel.DESCRIPTION_LENGTH}) NOT NULL, {CashFlowModel.DATE_COLOUMN} DATE NOT NULL, {CashFlowModel.AMOUNT_COLOUMN} NUMBER(5,2) NOT NULL, PRIMARY KEY ({CashFlowModel.TRANSACTION_ID_COLOUMN}));";

         return new Command(commandText);
      }
   }
}
