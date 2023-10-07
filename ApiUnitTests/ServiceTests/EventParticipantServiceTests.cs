using ApiUnitTests.ServiceTests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using webapi.Data.Model;
using webapi.Data.Model.DTOs;
using webapi.Repositories.Interfaces;
using webapi.Services;

namespace ApiUnitTests.ServiceTests
{
    [TestClass]
    public class EventParticipantServiceTests : BaseServiceTests<EventParticipant, EventParticipantDTO>
    {
        protected override Func<EventParticipantDTO, string> GetDtoParamFunc() => x => x.AdditionalInfo;

        protected override Func<EventParticipant, string> GetEntityParamFunc() => x => x.AdditionalInfo;

        protected override EventParticipantDTO GetTestDto()
        {
            return new EventParticipantDTO
            {
                EventId = "test1",
                ParticipantId = "testParticipant1",
                AdditionalInfo = "Sooviks hotellituba",
                PaymentMethod = PaymentMethod.Cash
            };
        }

        protected override EventParticipant GetTestEntity()
        {
            return new EventParticipant
            {
                EventId = "test1",
                ParticipantId = "testParticipant1",
                AdditionalInfo = "Sooviks hotellituba",
                PaymentMethod = PaymentMethod.Cash
            };
        }

        protected override List<EventParticipant> GetTestEntityList()
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

        protected override EventParticipantDTO GetUpdateDto()
        {
            return new EventParticipantDTO
            {
                EventId = "test1",
                ParticipantId = "testParticipant1",
                AdditionalInfo = "Sooviks hotellituba Updated",
                PaymentMethod = PaymentMethod.BankTransfer
            };
        }

        protected override EventParticipant GetUpdatedEntity()
        {
            return new EventParticipant
            {
                EventId = "test1",
                ParticipantId = "testParticipant1",
                AdditionalInfo = "Sooviks hotellituba Updated",
                PaymentMethod = PaymentMethod.BankTransfer
            };
        }

        [TestMethod]
        public async Task GetParticipantListByEventId_ReturnsExpectedList()
        {
            // Arrange
            var eventId = "test1";
            var mockRepo = new Mock<IEventParticipantRepository<EventParticipant>>();
            var mockParticipantRepo = new Mock<IParticipantRepository<Participant>>();
            var eventParticipantService = new EventParticipantService(mockRepo.Object, mockParticipantRepo.Object);

            var eventParticipants = GetTestEntityList();

            mockRepo.Setup(repo => repo.GetEventParticipantListByEventId(eventId)).Returns(eventParticipants);

            foreach (var eventParticipant in eventParticipants)
            {
                var participant = new Participant
                {
                    Id = eventParticipant.ParticipantId,
                    IsCompany = false,
                    FirstName = "Kalle",
                    LastName = "Pilli",
                    CompanyName = string.Empty,
                    IdCode = "39208014225"
                };

                mockParticipantRepo.Setup(repo => repo.Get(eventParticipant.ParticipantId)).ReturnsAsync(participant);
            }

            var result = await eventParticipantService.GetParticipantListByEventId(eventId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<EventParticipantWithParticipant>));
            Assert.AreEqual(eventParticipants.Count, result.Count);
            Assert.AreEqual("test1", result[0].EventId);
        }

        [TestMethod]
        public async Task GetEventParticipantWithParticipant_ReturnsExpectedEventParticipantWithParticipant()
        {
            var mockRepo = new Mock<IEventParticipantRepository<EventParticipant>>();
            var mockParticipantRepo = new Mock<IParticipantRepository<Participant>>();
            var eventParticipantService = new EventParticipantService(mockRepo.Object, mockParticipantRepo.Object);

            var eventParticipant = GetTestEntity();

            var participant = new Participant
            {
                Id = eventParticipant.ParticipantId,
                IsCompany = false,
                FirstName = "Kalle",
                LastName = "Pilli",
                CompanyName = string.Empty,
                IdCode = "39208014225"
            };

            mockRepo.Setup(repo => repo.Get(eventParticipant.Id)).ReturnsAsync(eventParticipant);
            mockParticipantRepo.Setup(repo => repo.Get(eventParticipant.ParticipantId)).ReturnsAsync(participant);

            var result = await eventParticipantService.GetEventParticipantWithParticipant(eventParticipant.Id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(EventParticipantWithParticipant));
            Assert.AreEqual(eventParticipant.Id, result.Id);
            Assert.AreEqual(participant.IdCode, result.IdCode);
        }
    }
}
