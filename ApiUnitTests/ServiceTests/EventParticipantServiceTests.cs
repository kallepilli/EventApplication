using ApiUnitTests.ServiceTests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using webapi.Data.Model;
using webapi.Data.Model.DTOs;

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
    }
}
