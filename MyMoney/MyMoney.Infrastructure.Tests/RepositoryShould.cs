using MyMoney.Infrastructure.Repositories;
using Xunit;
using System.Data.Entity;
using Moq;
using MyMoney.Infrastructure.Interfaces;

namespace MyMoney.Infrastructure.Tests
{

    public class RepositoryShould
    {

        [Fact]
        public void ShouldAddWhenEntityToAddIsNotNull()
        {
            const int testId = 14;

            var testObject = new TestObject
            {
                Id = testId,
            };

            var moqDbSet = new Mock<DbSet<TestObject>>();
            moqDbSet.Setup(mdbs => mdbs.Add(testObject)).Returns(testObject);

            var moqDbContext = new Mock<IDatabaseContext>();

            var testRepository = new TestRepository<TestObject>(moqDbContext.Object, moqDbSet.Object);

            var result = testRepository.Add(testObject);

            Assert.NotNull(result);
            Assert.Equal(result.Id, testObject.Id);

            moqDbContext.Verify(mdbc => mdbc.SaveChanges(), Times.Once);
            moqDbSet.Verify(mdbs => mdbs.Add(testObject), Times.Once);

        }

        [Fact]
        public void ShouldReturnNullWhenEntityToAddIsNull()
        {
            var moqDbSet = new Mock<DbSet<TestObject>>();

            var moqDbContext = new Mock<IDatabaseContext>();

            var testRepository = new TestRepository<TestObject>(moqDbContext.Object, moqDbSet.Object);

            var result = testRepository.Add(null);

            Assert.Null(result);
            moqDbContext.Verify(mdbc => mdbc.SaveChanges(), Times.Never);
            moqDbSet.Verify(mdbs => mdbs.Add(null), Times.Never);
        }

        [Fact]
        public void ShouldRemoveWhenEntityToAddIsNotNull()
        {
            const int testId = 14;

            var testObject = new TestObject
            {
                Id = testId,
            };

            var moqDbSet = new Mock<DbSet<TestObject>>();
            
            var moqDbContext = new Mock<IDatabaseContext>();

            var testRepository = new TestRepository<TestObject>(moqDbContext.Object, moqDbSet.Object);

            var result = testRepository.Delete(testObject);

            Assert.True(result);
            moqDbContext.Verify(mdbc => mdbc.SaveChanges(), Times.Once);
            moqDbSet.Verify(mdbs => mdbs.Remove(testObject), Times.Once);

        }

        [Fact]
        public void ShouldReturnFalseWhenEntityToRemoveIsNull()
        {
            var moqDbSet = new Mock<DbSet<TestObject>>();

            var moqDbContext = new Mock<IDatabaseContext>();

            var testRepository = new TestRepository<TestObject>(moqDbContext.Object, moqDbSet.Object);

            var result = testRepository.Delete(null);

            Assert.False(result);
            moqDbContext.Verify(mdbc => mdbc.SaveChanges(), Times.Never);
            moqDbSet.Verify(mdbs => mdbs.Remove(null), Times.Never);
        }


        public class TestObject
        {
            public int Id { get; set; }
        }

        private class TestRepository<T> : Repository<T> where T : class
        {
            public TestRepository(IDatabaseContext model, DbSet<T> table) : base(model, table)
            {

            }
        }
    }
}
