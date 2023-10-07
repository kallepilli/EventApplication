using ApiUnitTests.ServiceTests.Base;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using webapi.Data.Model;
using webapi.Data.Model.DTOs;
using webapi.Repositories.Interfaces;
using webapi.Services;
using webapi.Services.Interfaces;

namespace ApiUnitTests.ServiceTests
{
    [TestClass]
    public class EventServiceTests : BaseServiceTests<Event, EventDTO>
    {
        protected override Func<EventDTO, string> GetDtoParamFunc() => x => x.Name;

        protected override Func<Event, string> GetEntityParamFunc() => x => x.Name;

        protected override EventDTO GetTestDto()
        {
            return new EventDTO
            {
                Name = "Test Event",
                Location = "Test Location",
                AdditionalInfo = "Test Info",
                EventTime = DateTime.Now
            };
        }

        protected override Event GetTestEntity()
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

        protected override List<Event> GetTestEntityList()
        {
            return new List<Event>
            {
                new Event
                {
                    Id = "1",
                    Name = "Event 1",
                    Location = "Location 1",
                    AdditionalInfo = "Info 1",
                    EventTime = DateTime.Now
                },
                new Event
                {
                    Id = "2",
                    Name = "Event 2",
                    Location = "Location 2",
                    AdditionalInfo = "Info 2",
                    EventTime = DateTime.Now
                }
            };
        }

        protected override EventDTO GetUpdateDto()
        {
            return new EventDTO
            {
                Name = "Test Updated",
                Location = "Test Updated",
                AdditionalInfo = "Test Updated",
                EventTime = DateTime.Now
            };
        }

        protected override Event GetUpdatedEntity()
        {
            return new Event
            {
                Id = "1",
                Name = "Test Updated",
                Location = "Test Updated",
                AdditionalInfo = "Test Updated",
                EventTime = DateTime.Now
            };
        }

        [TestMethod]
        public async Task GetEventWithParticipantCount_ReturnsEventWithParticipantCount()
        {
            // Arrange
            var eventId = "1";
            var eventEntity = GetTestEntity();

            var mockEventRepo = new Mock<IBaseRepository<Event>>();
            var mockEventParticipantRepo = new Mock<IEventParticipantRepository<EventParticipant>>();
            var mockParticipantRepo = new Mock<IParticipantRepository<Participant>>();
            var mockEventService = new Mock<IEventService<Event, EventDTO>>();

            var eventParticipantList = new List<EventParticipant>
            {
                new EventParticipant
                {
                    EventId = eventId,
                    ParticipantId = "testParticipant1",
                    AdditionalInfo = "Sooviks hotellituba",
                    PaymentMethod = PaymentMethod.Cash
                },
                new EventParticipant
                {
                    EventId = eventId,
                    ParticipantId = "testParticipant2",
                    AdditionalInfo = "",
                    PaymentMethod = PaymentMethod.BankTransfer
                }
            };

            var participant = new Participant
            {
                Id = "testParticipant1",
                IsCompany = false,
                FirstName = "Kalle",
                LastName = "Pilli",
                CompanyName = string.Empty,
                IdCode = "39208014225"
            };

            mockEventRepo.Setup(repo => repo.Get(eventId)).ReturnsAsync(eventEntity);
            mockEventParticipantRepo.Setup(repo => repo.GetEventParticipantListByEventId(eventId)).Returns(eventParticipantList);
            mockParticipantRepo.Setup(repo => repo.Get(It.IsAny<string>())).ReturnsAsync(participant);

            mockEventService
                .Setup(s => s.GetParticipantCount(eventId))
                .ReturnsAsync(eventParticipantList.Count);

            var eventService = new EventService(mockEventRepo.Object, mockEventParticipantRepo.Object, mockParticipantRepo.Object);

            var result = await eventService.GetEventWithParticipantCount(eventId);

            Assert.IsNotNull(result);
            Assert.AreEqual(eventId, result.EventId);
            Assert.AreEqual(eventEntity.Name, result.Name);
            Assert.AreEqual(eventEntity.EventTime, result.EventTime);
            Assert.AreEqual(eventEntity.Location, result.Location);
            Assert.AreEqual(eventEntity.AdditionalInfo ?? string.Empty, result.AdditionalInfo);
        }

        [TestMethod]
        public async Task GetEventWithParticipantsList_ReturnsArrayOfLists()
        {
            var mockEventRepo = new Mock<IBaseRepository<Event>>();
            var mockEventParticipantRepo = new Mock<IEventParticipantRepository<EventParticipant>>();
            var mockParticipantRepo = new Mock<IParticipantRepository<Participant>>();
            var mockEventService = new Mock<IEventService<Event, EventDTO>>();

            var events = GetFutureAndPastEventList();

            mockEventRepo.Setup(repo => repo.GetList()).ReturnsAsync(events);

            foreach (var item in events)
            {
                mockEventRepo.Setup(r => r.Get(item.Id)).ReturnsAsync(item);
                mockEventService.Setup(s => s.GetEventWithParticipantCount(item.Id)).ReturnsAsync(EventToEventWithParticipantCount(item));
                mockEventService.Setup(s => s.GetParticipantCount(item.Id)).ReturnsAsync(1);
                mockEventParticipantRepo.Setup(r => r.GetEventParticipantListByEventId(item.Id)).Returns(new List<EventParticipant>());
            }

            var eventService = new EventService(mockEventRepo.Object, mockEventParticipantRepo.Object, mockParticipantRepo.Object);

            var result = await eventService.GetEventWithParticipantsList();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(2, result[0].Count);
            Assert.AreEqual(2, result[1].Count);
        }

        [TestMethod]
        public async Task GetParticipantCount_ReturnsCount()
        {
            var eventId = "1";
            var mockEventRepo = new Mock<IBaseRepository<Event>>();
            var mockEventParticipantRepo = new Mock<IEventParticipantRepository<EventParticipant>>();
            var mockParticipantRepo = new Mock<IParticipantRepository<Participant>>();
            var eventParticipants = GetEventParticipantsList();

            mockEventParticipantRepo.Setup(repo => repo.GetEventParticipantListByEventId(eventId)).Returns(eventParticipants);

            foreach (var eventParticipant in eventParticipants)
            {
                var isCompany = eventParticipant.ParticipantId == "2" || eventParticipant.ParticipantId == "4" ? true : false;
                var participant = new Participant
                {
                    Id = eventParticipant.ParticipantId,
                    IsCompany = isCompany,
                };

                mockParticipantRepo.Setup(repo => repo.Get(eventParticipant.ParticipantId)).ReturnsAsync(participant);
            }

            var eventService = new EventService(mockEventRepo.Object, mockEventParticipantRepo.Object, mockParticipantRepo.Object);

            var result = await eventService.GetParticipantCount(eventId);

            Assert.AreEqual(9, result);
        }

        private List<EventParticipant> GetEventParticipantsList()
        {
            return new List<EventParticipant>
            {
                new EventParticipant
                {
                    EventId = "1",
                    ParticipantId = "1"
                },
                new EventParticipant
                {
                    EventId = "1",
                    ParticipantId = "2",
                    ParticipantCount = 2
                },
                new EventParticipant
                {
                    EventId = "1",
                    ParticipantId = "3"
                },
                new EventParticipant
                {
                    EventId = "1",
                    ParticipantId = "4",
                    ParticipantCount = 5
                },
            };
        }

        private List<Event> GetFutureAndPastEventList()
        {
            return new List<Event>()
            {
                new Event
                {
                    Id = "test1",
                    Name = "Konverents",
                    EventTime = DateTime.Today.AddDays(-3).ToUniversalTime(),
                    Location = "Tallinn",
                    AdditionalInfo = "Investor Toomase konverents"
                },
                new Event
                {
                    Id = "test2",
                    Name = "Ettevõtte pidu",
                    EventTime = DateTime.Today.AddDays(-1).ToUniversalTime(),
                    Location = "Pärnu",
                    AdditionalInfo = "Open bar"
                },
                new Event
                {
                    Id = "test3",
                    Name = "Automess",
                    EventTime = DateTime.Today.ToUniversalTime(),
                    Location = "Tartu",
                    AdditionalInfo = "Luksuslikud autod"
                },
                new Event
                {
                    Id = "test4",
                    Name = "Etendus",
                    EventTime = DateTime.Today.AddDays(10).ToUniversalTime(),
                    Location = "Viljandi",
                    AdditionalInfo = "Head näitlejad"
                }
            };
        }

        private EventWithParticipantCount EventToEventWithParticipantCount(Event eventItem)
        {
            return new EventWithParticipantCount
            {
                EventId = eventItem.Id,
                Name = eventItem.Name,
                EventTime = eventItem.EventTime,
                Location = eventItem.Location,
                AdditionalInfo = eventItem.AdditionalInfo ?? string.Empty,
                ParticipantCount = 1
            };
        }
    }
}


