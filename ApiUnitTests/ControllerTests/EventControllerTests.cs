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
    public class EventControllerTests
    {
        private Mock<IEventService<Event, EventDTO>> mockEventService;
        private EventController eventController;

        private EventWithParticipantCount GetEventWithParticipantCount()
        {
            return new EventWithParticipantCount
            {
                EventId = "test1",
                Name = "Test",
                EventTime = DateTime.Today,
                Location = "Tallinn",
                AdditionalInfo = "",
                ParticipantCount = 3
            };
        }

        private List<EventWithParticipantCount>[] GetEventWithParticipantCountList()
        {
            var list = new List<EventWithParticipantCount>[2];
            list[0] = new List<EventWithParticipantCount>();
            list[1] = new List<EventWithParticipantCount>();

            list[0].Add(new EventWithParticipantCount
            {
                EventId = "test1",
                Name = "Test",
                EventTime = DateTime.Today.AddDays(1),
                Location = "Tallinn",
                AdditionalInfo = "",
                ParticipantCount = 3
            });
            list[1].Add(new EventWithParticipantCount
            {
                EventId = "test2",
                Name = "Test2",
                EventTime = DateTime.Today.AddDays(-1),
                Location = "Tallinn",
                AdditionalInfo = "",
                ParticipantCount = 3
            });
            return list;
        }

        private EventDTO GetTestDto()
        {
            return new EventDTO
            {
                Name = "Test Event",
                Location = "Test Location",
                AdditionalInfo = "Test Info",
                EventTime = DateTime.Now
            };
        }

        private Event GetTestEntity()
        {
            return new Event
            {
                Id = "1",
                Name = "Test Event",
                Location = "Test Location",
                AdditionalInfo = "Test Info",
                EventTime = DateTime.Now
            };
        }

        [TestInitialize]
        public void Initialize()
        {
            mockEventService = new Mock<IEventService<Event, EventDTO>>();
            eventController = new EventController(mockEventService.Object);
        }

        [TestMethod]
        public async Task GetEvent_ReturnsOk()
        {
            var eventId = "test1";
            var eventWithParticipantCount = GetEventWithParticipantCount();

            mockEventService.Setup(s => s.GetEventWithParticipantCount(eventId)).ReturnsAsync(eventWithParticipantCount);

            var result = await eventController.GetEvent(eventId) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreSame(eventWithParticipantCount, result.Value);
        }

        [TestMethod]
        public async Task GetEventList_ReturnsOk()
        {
            var eventList = GetEventWithParticipantCountList();

            mockEventService.Setup(s => s.GetEventWithParticipantsList()).ReturnsAsync(eventList);

            var result = await eventController.GetEventList() as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreSame(eventList, result.Value);
        }

        [TestMethod]
        public async Task SaveEvent_ReturnsOk()
        {
            var eventDto = GetTestDto();
            var savedEvent = GetTestEntity();
            mockEventService.Setup(s => s.Save(eventDto)).ReturnsAsync(savedEvent);

            var result = await eventController.SaveEvent(eventDto) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreSame(savedEvent, result.Value);
        }

        [TestMethod]
        public async Task UpdateEventParticipant_ReturnsOk()
        {
            string eventId = "1";
            var eventDto = GetTestDto();
            var updatedEvent = GetTestEntity();
            mockEventService.Setup(s => s.Update(eventId, eventDto)).ReturnsAsync(updatedEvent);

            var result = await eventController.UpdateEventParticipant(eventDto, eventId) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreSame(updatedEvent, result.Value);
        }

        [TestMethod]
        public async Task DeleteEvent_ReturnsOk()
        {
            string eventId = "1";
            mockEventService.Setup(s => s.Delete(eventId)).ReturnsAsync(true);

            var result = await eventController.DeleteEvent(eventId) as OkResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public async Task DeleteEvent_ReturnsNotFound()
        {
            string eventId = "1";
            mockEventService.Setup(s => s.Delete(eventId)).ReturnsAsync(false);

            var result = await eventController.DeleteEvent(eventId) as NotFoundResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }
    }
}

