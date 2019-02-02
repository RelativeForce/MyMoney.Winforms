using MyMoney.Infrastructure.Repositories;
using System;
using Xunit;
using System.Data.Entity;
using Moq;
using MyMoney.Core.Data;
using MyMoney.Infrastructure.Interfaces;

namespace MyMoney.Infrastructure.Tests
{

    public class RepositoryShould
    {
        private const double TestAmount = 32.2;
        private const string TestDescription = "test description";

        [Fact]
        public void ShouldAddWhenEntityIsNotNull()
        {
            const int testId = 14;
            const int testSaveResult = 5;

            var testTransaction = new Transaction {
                Id = testId,
                Date = DateTime.Today,
                Amount = TestAmount,
                Description = TestDescription
            };

            var moqDbSet = new Mock<DbSet<Transaction>>();
            moqDbSet.Setup(mdbs => mdbs.Add(testTransaction)).Returns(testTransaction);

            var moqDbContext = new Mock<IDatabaseContext>();
            moqDbContext.Setup(mdbc => mdbc.Transactions).Returns(moqDbSet.Object);
            moqDbContext.Setup(mdbc => mdbc.SaveChanges()).Returns(testSaveResult);

            var testRepository = new TestRepository<Transaction>(moqDbContext.Object, moqDbSet.Object);

            var result = testRepository.Add(testTransaction);

            Assert.NotNull(result);
            Assert.Equal(result.Id, testTransaction.Id);
            Assert.Equal(result.Date, testTransaction.Date);
            Assert.Equal(result.Amount, testTransaction.Amount);
            Assert.Equal(result.Description, testTransaction.Description);

            moqDbContext.Verify(mdbc => mdbc.SaveChanges(), Times.Once);

        }

        private class TestRepository<T> : Repository<T> where T : class
        {
            public TestRepository(IDatabaseContext model, DbSet<T> table) : base(model, table)
            {

            }
        }
    }
}
