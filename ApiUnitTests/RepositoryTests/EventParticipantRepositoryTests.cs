using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using webapi.Data.Model;
using webapi.Repositories.Interfaces;

namespace ApiUnitTests.RepositoryTests
{
    [TestClass]
    public class EventParticipantRepositoryTests : BaseRepositoryTests<EventParticipant>
    {
        protected override EventParticipant GetTestItem()
        {
            return new EventParticipant
            {
                EventId = "test1",
                ParticipantId = "testParticipant1",
                AdditionalInfo = "Sooviks hotellituba",
                PaymentMethod = PaymentMethod.Cash
            };
        }

        protected override EventParticipant GetUpdatedItem()
        {
            return new EventParticipant
            {
                EventId = "test1",
                ParticipantId = "testParticipant1",
                AdditionalInfo = "Sooviks hotellituba Updated",
                PaymentMethod = PaymentMethod.BankTransfer
            };
        }

        protected override string GetNonExistingId()
        {
            return "999";
        }

        protected override List<EventParticipant> GetTestItems()
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
        public void GetEventParticipantListByEventId_ReturnsListOfParticipants()
        {
            var eventId = "test1";
            var participants = GetTestItems();

            var mockEventParticipantRepository = new Mock<IEventParticipantRepository<EventParticipant>>();
            var eventParticipantRepository = mockEventParticipantRepository.Object;

            mockEventParticipantRepository.Setup(r => r.GetEventParticipantListByEventId(eventId)).Returns(participants);

            var result = eventParticipantRepository.GetEventParticipantListByEventId(eventId);

            Assert.IsNotNull(result);
            Assert.AreEqual(participants.Count, result.Count);
        }
    }
}