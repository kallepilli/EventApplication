using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using webapi.Controllers;
using webapi.Data.Model.DTOs;
using webapi.Data.Model;
using webapi.Services.Interfaces;

namespace ApiUnitTests.ControllerTests
{
    [TestClass]
    public class EventParticipantControllerTests
    {
        private EventParticipantController controller;
        private Mock<IEventParticipantService<EventParticipant, EventParticipantDTO>> mockService;

        [TestInitialize]
        public void TestInitialize()
        {
            mockService = new Mock<IEventParticipantService<EventParticipant, EventParticipantDTO>>();
            controller = new EventParticipantController(mockService.Object);
        }

        private EventParticipantDTO GetTestDto()
        {
            return new EventParticipantDTO
            {
                EventId = "test1",
                ParticipantId = "testParticipant1",
                AdditionalInfo = "Sooviks hotellituba",
                PaymentMethod = PaymentMethod.Cash
            };
        }

        private EventParticipant GetTestEntity()
        {
            return new EventParticipant
            {
                Id = "test1",
                EventId = "test1",
                ParticipantId = "testParticipant1",
                AdditionalInfo = "Sooviks hotellituba",
                PaymentMethod = PaymentMethod.Cash
            };
        }

        private List<EventParticipant> GetTestEntityList()
        {
            return new List<EventParticipant>
            {
                new EventParticipant
                {
                    EventId = "test1",
                    ParticipantId = "testParticipant1",
                    AdditionalInfo = "Sooviks hotellituba",
                    PaymentMethod = PaymentMethod.Cash
                },
                new EventParticipant
                {
                    EventId = "test1",
                    ParticipantId = "testParticipant2",
                    AdditionalInfo = "",
                    PaymentMethod = PaymentMethod.BankTransfer
                }
            };
        }

        [TestMethod]
        public async Task GetParticipant_ReturnsOkResult()
        {
            var eventParticipantId = "test1";
            var eventParticipant = GetTestEntity();
            mockService.Setup(s => s.Get(eventParticipantId)).ReturnsAsync(eventParticipant);

            var result = await controller.GetParticipant(eventParticipantId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(eventParticipant, okResult.Value);
        }

        [TestMethod]
        public async Task GetParticipant_ReturnsNotFound()
        {
            var eventParticipantId = "invalidId";
            mockService.Setup(s => s.Get(eventParticipantId)).ReturnsAsync((EventParticipant)null);

            var result = await controller.GetParticipant(eventParticipantId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetParticipantWithParticipant_ReturnsOkResult()
        {
            var eventParticipantId = "1";
            var eventParticipantWithParticipant = new EventParticipantWithParticipant { Id = eventParticipantId };
            mockService.Setup(s => s.GetEventParticipantWithParticipant(eventParticipantId)).ReturnsAsync(eventParticipantWithParticipant);

            var result = await controller.GetParticipantWithParticipant(eventParticipantId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(eventParticipantWithParticipant, okResult.Value);
        }

        [TestMethod]
        public async Task GetParticipantListByEventId_ReturnsOkResult()
        {
            var eventId = "1";
            var eventParticipantList = new List<EventParticipantWithParticipant>();
            mockService.Setup(s => s.GetParticipantListByEventId(eventId)).ReturnsAsync(eventParticipantList);

            var result = await controller.GetEventParticipantList(eventId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(eventParticipantList, okResult.Value);
        }

        [TestMethod]
        public async Task SaveEventParticipant_ReturnsOkResult()
        {
            var eventParticipantDto = GetTestDto();
            var eventParticipant = GetTestEntity();
            mockService.Setup(s => s.Save(eventParticipantDto)).ReturnsAsync(eventParticipant);

            var result = await controller.SaveEventParticipant(eventParticipantDto);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(eventParticipant, okResult.Value);
        }

        [TestMethod]
        public async Task SaveEventParticipant_ReturnsBadRequest()
        {
            var eventParticipantDto = GetTestDto();
            mockService.Setup(s => s.Save(eventParticipantDto)).ReturnsAsync((EventParticipant)null);

            var result = await controller.SaveEventParticipant(eventParticipantDto);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task UpdateEventParticipant_ReturnsOkResult()
        {
            var eventParticipantId = "test1";
            var eventParticipantDto = GetTestDto();
            var eventParticipant = GetTestEntity();
            mockService.Setup(s => s.Update(eventParticipantId, eventParticipantDto)).ReturnsAsync(eventParticipant);

            var result = await controller.UpdateEventParticipant(eventParticipantDto, eventParticipantId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(eventParticipant, okResult.Value);
        }

        [TestMethod]
        public async Task UpdateEventParticipant_ReturnsBadRequest()
        {
            var eventParticipantId = "invalidId";
            var eventParticipantDto = GetTestDto();
            mockService.Setup(s => s.Update(eventParticipantId, eventParticipantDto)).ReturnsAsync((EventParticipant)null);

            var result = await controller.UpdateEventParticipant(eventParticipantDto, eventParticipantId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task Delete_ReturnsOkResult()
        {
            var eventParticipantId = "test1";
            mockService.Setup(s => s.Delete(eventParticipantId)).ReturnsAsync(true);

            var result = await controller.Delete(eventParticipantId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task Delete_ReturnsNotFound()
        {
            var eventParticipantId = "invalidId";
            mockService.Setup(s => s.Delete(eventParticipantId)).ReturnsAsync(false);

            var result = await controller.Delete(eventParticipantId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}

