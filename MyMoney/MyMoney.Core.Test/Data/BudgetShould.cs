using System;
using Xunit;
using MyMoney.Core.Model;

namespace MyMoney.Core.Test.Data
{

    public class BudgetShould
    {

        [Theory]
        [InlineData(2018, 4, 12, 2018, 4)]
        [InlineData(2019, 1, 30, 2019, 1)]
        [InlineData(2019, 3, 31, 2019, 3)]
        [InlineData(2018, 2, 28, 2018, 2)]
        public void FilterDateShouldReturnFirstOfMonth(int year, int month, int day, int expectedYear, int expectedMonth)
        {

            var testDate = new DateTime(year, month, day);

            var actualDate = Budget.FilterDate(testDate);

            Assert.Equal(expectedYear, actualDate.Year);
            Assert.Equal(expectedMonth, actualDate.Month);
            Assert.Equal(1, actualDate.Day);

        }

    }
}
