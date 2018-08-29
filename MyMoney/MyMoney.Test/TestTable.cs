﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyMoney.Core.Table;

namespace MyMoney.Test
{

   [TestClass]
   public class TestTable
   {
      private const string ValidColoumn1 = "test1";
      private const string ValidColoumn2 = "test2";
      private const string ValidColoumn3 = "test3";
      private const string ValidColoumn4 = "test4";

      private static readonly string[] ValidColoumns = { ValidColoumn1, ValidColoumn2, ValidColoumn3, ValidColoumn4 };

      #region Construct

      [TestMethod]
      public void TestConstructionWithValidColoumns()
      {
         var table = new Table(ValidColoumns);

         var returnedColoumns = table.GetColoumns();

         Assert.AreEqual(returnedColoumns.Length, ValidColoumns.Length);

         foreach (var validColoumn in ValidColoumns)
         {
            Assert.IsTrue(returnedColoumns.Contains(validColoumn));
         }

         Assert.AreEqual(table.GetRows().Length, 0);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void TestCannotConstructTableWithNoColoumns()
      {
         var table = new Table(new string[] { });
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentNullException))]
      public void TestCannotConstructTableWithNullColoumns()
      {
         var table = new Table(null);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentNullException))]
      public void TestCannotConstructTableWithNullColoumn()
      {
         var coloumns = new[] { ValidColoumn1, null, ValidColoumn2 };

         var table = new Table(coloumns);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentNullException))]
      public void TestCannotConstrustTableWithNullColoumns()
      {
         var table = new Table(null);
      }

      #endregion

      #region AddRow

      [TestMethod]
      public void TestInsertValidRow()
      {

         const string validValue1 = "value1";
         const string validValue2 = "value2";
         const string validValue3 = "value3";
         const string validValue4 = "value4";

         var table = new Table(ValidColoumns);

         var row = new Row();
         row.AddColoumn(ValidColoumn1, validValue1);
         row.AddColoumn(ValidColoumn2, validValue2);
         row.AddColoumn(ValidColoumn3, validValue3);
         row.AddColoumn(ValidColoumn4, validValue4);

         Assert.AreEqual(table.GetRows().Length, 0);

         table.AddRow(row);

         var returnedRows = table.GetRows();

         Assert.AreEqual(returnedRows.Length, 1);

         Assert.AreEqual(returnedRows[0], row);

      }

      #endregion

   }
}
