using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using webapi.Data.Model.Base;
using webapi.Services.Interfaces;

namespace ApiUnitTests.ServiceTests.Base
{
    public abstract class BaseServiceTests<TEntity, TDto>
    where TEntity : BaseModel
    where TDto : class
    {
        protected IBaseService<TEntity, TDto> service;
        protected Mock<IBaseService<TEntity, TDto>> mockService;


        [TestInitialize]
        public virtual void Initialize()
        {
            mockService = new Mock<IBaseService<TEntity, TDto>>();
            service = mockService.Object;
        }

        protected abstract TEntity GetTestEntity();
        protected abstract TEntity GetUpdatedEntity();
        protected abstract TDto GetTestDto();
        protected abstract TDto GetUpdateDto();
        protected abstract List<TEntity> GetTestEntityList();
        protected abstract Func<TEntity, string> GetEntityParamFunc();
        protected abstract Func<TDto, string> GetDtoParamFunc();
        protected string GetNonExistingId() => "999";


        [TestMethod]
        public async Task Get_ReturnsItem()
        {
            var testEntity = GetTestEntity();
            mockService.Setup(s => s.Get(testEntity.Id)).ReturnsAsync(testEntity);

            var result = await service.Get(testEntity.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(testEntity.Id, result.Id);
            Assert.IsInstanceOfType(result, typeof(TEntity));
        }

        [TestMethod]
        public async Task Get_NonExistingItem_ReturnsNull()
        {
            var testEntity = GetTestEntity();
            mockService.Setup(s => s.Get(testEntity.Id)).ReturnsAsync((TEntity?)null);

            var result = await service.Get(testEntity.Id);

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetList_ReturnsListOfItems()
        {
            var testItems = GetTestEntityList();
            mockService.Setup(s => s.GetList()).ReturnsAsync(testItems);

            var result = await service.GetList();

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(testItems, result);
            Assert.IsInstanceOfType(result, typeof(List<TEntity>));
        }

        [TestMethod]
        public async Task Save_ReturnsSavedItem()
        {
            var testDto = GetTestDto();
            var testEntity = GetTestEntity();
            mockService.Setup(s => s.Save(testDto)).ReturnsAsync(testEntity);

            var result = await service.Save(testDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(GetDtoParamFunc()(testDto), GetEntityParamFunc()(result));
            Assert.IsInstanceOfType(result, typeof(TEntity));
        }

        [TestMethod]
        public async Task Update_ReturnsUpdatedItem()
        {
            var testDto = GetUpdateDto();
            var updatedItem = GetUpdatedEntity();
            mockService.Setup(s => s.Update(updatedItem.Id, testDto)).ReturnsAsync(updatedItem);

            var result = await service.Update(updatedItem.Id, testDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(updatedItem.Id, result.Id);
            Assert.AreEqual(GetDtoParamFunc()(testDto), GetEntityParamFunc()(updatedItem));
            Assert.IsInstanceOfType(result, typeof(TEntity));
        }

        [TestMethod]
        public async Task Update_NonExistingItem_ReturnsNull()
        {
            var nonExistingId = GetNonExistingId();
            var updatedDto = GetUpdateDto();

            mockService.Setup(s => s.Update(nonExistingId, updatedDto)).ReturnsAsync((TEntity?)null);

            var result = await service.Update(nonExistingId, updatedDto);

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Delete_ReturnsTrue()
        {
            var testEntity = GetTestEntity();
            mockService.Setup(r => r.Delete(testEntity.Id)).ReturnsAsync(true);

            var result = await service.Delete(testEntity.Id);

            Assert.IsTrue(result);
        }


        [TestMethod]
        public async Task Delete_ReturnsFalse()
        {
            mockService.Setup(r => r.Delete(GetNonExistingId())).ReturnsAsync(false);

            var result = await service.Delete(GetNonExistingId());

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DtoToEntity_ReturnEntity()
        {
            var testDto = GetTestDto();
            var testEntity = GetTestEntity();

            mockService.Setup(r => r.DtoToEntity(testDto)).Returns(testEntity);
            var result = service.DtoToEntity(testDto);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TEntity));
            Assert.AreEqual(GetDtoParamFunc()(testDto), GetEntityParamFunc()(testEntity));
        }
    }
}
