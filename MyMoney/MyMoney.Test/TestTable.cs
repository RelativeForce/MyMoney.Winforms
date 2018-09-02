using System;
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

        private static Row GetValidRow()
        {
            const string validValue1 = "value1";
            const string validValue2 = "value2";
            const string validValue3 = "value3";
            const string validValue4 = "value4";

            var row = new Row();
            row.AddColoumn(ValidColoumn1, validValue1);
            row.AddColoumn(ValidColoumn2, validValue2);
            row.AddColoumn(ValidColoumn3, validValue3);
            row.AddColoumn(ValidColoumn4, validValue4);

            return row;

        }

        private static Table GetValidTable()
        {
            return new Table(ValidColoumns);
        }

        #region Construct

        [TestMethod]
        public void TestConstructionWithValidColoumns()
        {
            var table = GetValidTable();

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
            new Table(new string[] { });

            Assert.Fail("Constructed a table with no coloumns.");
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
        public void TestAddValidRow()
        {

            var table = GetValidTable();

            var row = GetValidRow();

            Assert.AreEqual(table.GetRows().Length, 0);

            table.AddRow(row);

            var returnedRows = table.GetRows();

            Assert.AreEqual(returnedRows.Length, 1);

            Assert.AreEqual(returnedRows[0], row);

        }

        [TestMethod]
        public void TestCannotAddRowThatHasDifferentColoumns()
        {
            const string validValue1 = "value1";
            const string validValue2 = "value2";
            const string validValue3 = "value3";
            const string validValue4 = "value4";

            var table = GetValidTable();

            var row1 = new Row();
            row1.AddColoumn(ValidColoumn1, validValue1);
            row1.AddColoumn(ValidColoumn2, validValue2);
            row1.AddColoumn(ValidColoumn3, validValue3);

            try
            {
                table.AddRow(row1);

                Assert.Fail("Added Row with different number of coloumns.");
            }
            catch (ArgumentException)
            {
            }

            var row2 = new Row();
            row2.AddColoumn(ValidColoumn1, validValue1);
            row2.AddColoumn(ValidColoumn2, validValue2);
            row2.AddColoumn(ValidColoumn3, validValue3);
            row2.AddColoumn("randomColoumnName", validValue4);

            try
            {
                table.AddRow(row2);

                Assert.Fail("Added Row with a different coloumn.");
            }
            catch (ArgumentException)
            {
            }

            var row3 = new Row();
            row3.AddColoumn(ValidColoumn1, validValue1);
            row3.AddColoumn(ValidColoumn2, validValue2);
            row3.AddColoumn(ValidColoumn3, validValue3);
            row1.AddColoumn(ValidColoumn4, validValue4);
            row1.AddColoumn("test5", "value5");

            try
            {
                table.AddRow(row3);

                Assert.Fail("Added Row with different number of coloumns.");
            }
            catch (ArgumentException)
            {
            }


        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCannotAddNullRow()
        {
            var table = GetValidTable();

            table.AddRow(null);

            Assert.Fail("Added a null Row.");
        }

        #endregion

        #region Remove

        [TestMethod]
        public void TestRemovePresentRow()
        {

            var table = GetValidTable();

            var row = GetValidRow();

            Assert.AreEqual(table.GetRows().Length, 0);

            table.AddRow(row);

            Assert.AreEqual(table.GetRows().Length, 1);

            Assert.AreEqual(table.GetRows()[0], row);

            var wasRemoved = table.Remove(row);

            Assert.AreEqual(table.GetRows().Length, 0);
            Assert.IsTrue(wasRemoved);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCannotRemoveNullRow()
        {

            var table = GetValidTable();

            table.Remove(null);

            Assert.Fail("Removed a null row.");

        }

        [TestMethod]
        public void TestCannotRemoveNotPresentRow()
        {
            var table = GetValidTable();

            var row = GetValidRow();

            Assert.AreEqual(table.GetRows().Length, 0);

            var wasRemoved = table.Remove(row);

            Assert.IsFalse(wasRemoved);

        }

        #endregion

        #region FindFirst

        [TestMethod]
        public void TestFindFirstWithRowPresent()
        {


        }

        #endregion

    }
}
