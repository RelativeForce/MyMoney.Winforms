using System;
using System.CodeDom;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyMoney.Core.Table;

namespace MyMoney.Test
{
    [TestClass]
    public class RowTest
    {
        private const string ValidColoumnName = "test";
        private const string ValidValue = "value";

        private static Row GetNewRow()
        {
            return new Row();
        }

        #region AddColoumn

        [TestMethod]
        public void TestAddValidColoumn()
        {

            var row = GetNewRow();

            row.AddColoumn(ValidColoumnName, ValidValue);

            Assert.AreEqual(row.GetValue(ValidColoumnName), ValidValue);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCannotAddNullColoumnName()
        {

            var row = GetNewRow();

            row.AddColoumn(null, ValidValue);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCannotAddEmptyColoumnName()
        {
            var row = GetNewRow();

            row.AddColoumn("", ValidValue);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCannotAddDuplicateColoumn()
        {

            var row = GetNewRow();

            row.AddColoumn(ValidColoumnName, ValidValue);

            row.AddColoumn(ValidColoumnName, ValidValue);

        }

        #endregion

        #region GetColoumns

        [TestMethod]
        public void TestColoumnNamesStored()
        {

            var testColoumns = new[] { "test1", "test2", "test3" };

            var row = GetNewRow();

            foreach (var testColoumn in testColoumns)
            {
                row.AddColoumn(testColoumn, ValidValue);
            }

            var returnedColoumns = row.GetColoumns();

            Assert.AreEqual(returnedColoumns.Length, testColoumns.Length);

            foreach (var testColoumn in testColoumns)
            {
                Assert.IsTrue(returnedColoumns.Contains(testColoumn));
            }

        }

        #endregion

        #region GetValue

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCannotGetValueOfNotPresentColoumn()
        {

            var row = GetNewRow();

            row.GetValue(ValidColoumnName);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCannotGetValueFromNullColoumn()
        {

            var row = GetNewRow();

            row.GetValue(null);

        }

        [TestMethod]
        public void TestGetValueFromValidColoumn()
        {

            var row = GetNewRow();

            row.AddColoumn(ValidColoumnName, ValidValue);

            var returnedValue = row.GetValue(ValidColoumnName);

            Assert.AreEqual(ValidValue, returnedValue);

        }

        #endregion

        #region UpdateColoumn

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCannotUpdateNotPresentColoumn()
        {

            var row = GetNewRow();

            row.UpdateColoumn(ValidColoumnName, ValidValue);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCannotUpdateNullColoumn()
        {

            var row = GetNewRow();

            row.UpdateColoumn(null, ValidValue);

        }

        #endregion

    }
}
