﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using webapi.Data.Model.Base;
using webapi.Repositories.Interfaces;

namespace ApiUnitTests.RepositoryTests
{
    [TestClass]
    public abstract class BaseRepositoryTests<T>
    where T : BaseModel
    {
        protected IBaseRepository<T> repo;
        protected Mock<IBaseRepository<T>> mockRepo;

        protected abstract T GetTestItem();
        protected abstract T GetUpdatedItem();
        protected abstract string GetNonExistingId();
        protected abstract List<T> GetTestItems();

        [TestInitialize]
        public virtual void Initialize()
        {
            mockRepo = new Mock<IBaseRepository<T>>();
            repo = mockRepo.Object;
        }

        [TestMethod]
        public async Task Get_ReturnsItem()
        {
            var testItem = GetTestItem();
            mockRepo.Setup(r => r.Get(testItem.Id)).ReturnsAsync(testItem);

            var result = await repo.Get(testItem.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(testItem.Id, result.Id);
            Assert.IsInstanceOfType(result, typeof(T));
        }

        [TestMethod]
        public async Task Get_NonExistingItem_ReturnsNull()
        {
            var nonExistingId = GetNonExistingId();
            mockRepo.Setup(r => r.Get(nonExistingId)).ReturnsAsync((T?)null);

            var result = await repo.Get(nonExistingId);

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetList_ReturnsListOfItems()
        {
            var testItems = GetTestItems();
            mockRepo.Setup(r => r.GetList()).ReturnsAsync(testItems);

            var result = await repo.GetList();

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(testItems, result);
            Assert.IsInstanceOfType(result, typeof(List<T>));
        }

        [TestMethod]
        public async Task Save_ReturnsSavedItem()
        {
            var testItem = GetTestItem();
            mockRepo.Setup(r => r.Save(testItem)).ReturnsAsync(testItem);

            var result = await repo.Save(testItem);

            Assert.IsNotNull(result);
            Assert.AreEqual(testItem.Id, result.Id);
            Assert.IsInstanceOfType(result, typeof(T));
        }

        [TestMethod]
        public async Task Update_ReturnsUpdatedItem()
        {
            var testItem = GetTestItem();
            var updatedItem = GetUpdatedItem();
            mockRepo.Setup(r => r.Update(testItem.Id, updatedItem)).ReturnsAsync(updatedItem);

            var result = await repo.Update(testItem.Id, updatedItem);

            Assert.IsNotNull(result);
            Assert.AreEqual(updatedItem.Id, result.Id);
            Assert.IsInstanceOfType(result, typeof(T));
        }

        [TestMethod]
        public async Task Update_NonExistingItem_ReturnsNull()
        {
            var nonExistingId = GetNonExistingId();
            var updatedItem = GetTestItem(); 
            mockRepo.Setup(r => r.Update(nonExistingId, updatedItem)).ReturnsAsync((T?)null);

            var result = await repo.Update(nonExistingId, updatedItem);

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Delete_ReturnsTrue()
        {
            var testItem = GetTestItem();
            mockRepo.Setup(r => r.Delete(testItem.Id)).ReturnsAsync(true);

            var result = await repo.Delete(testItem.Id);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task Delete_ReturnsFalse()
        {
            var nonExistingId = GetNonExistingId();
            mockRepo.Setup(r => r.Delete(nonExistingId)).ReturnsAsync(false);

            var result = await repo.Delete(nonExistingId);

            Assert.IsFalse(result);
        }

    }

}