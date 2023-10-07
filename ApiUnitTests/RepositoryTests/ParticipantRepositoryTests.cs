using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using webapi.Data.Model;
using webapi.Repositories.Interfaces;

namespace ApiUnitTests.RepositoryTests
{
    [TestClass]
    public class ParticipantRepositoryTests : BaseRepositoryTests<Participant>
    {
        protected override Participant GetTestItem()
        {
            return new Participant
            {
                Id = "testParticipant1",
                IsCompany = false,
                FirstName = "Kalle",
                LastName = "Pilli",
                CompanyName = string.Empty,
                IdCode = "39208014225"
            };
        }

        protected override Participant GetUpdatedItem()
        {
            return new Participant
            {
                Id = "testParticipant1",
                IsCompany = false,
                FirstName = "Kalle Updated",
                LastName = "Pilli UpdTaed",
                CompanyName = string.Empty,
                IdCode = "39208014225"
            };
        }

        protected override string GetNonExistingId()
        {
            return "999";
        }

        protected override List<Participant> GetTestItems()
        {
            return new List<Participant>
            {
                new Participant
                {
                    Id = "testParticipant1",
                    IsCompany = false,
                    FirstName = "Kalle",
                    LastName = "Pilli",
                    CompanyName= string.Empty,
                    IdCode = "39208014225"
                },
                new Participant
                {
                    Id = "testParticipant2",
                    IsCompany = false,
                    FirstName = "Investor",
                    LastName = "Toomas",
                    CompanyName= string.Empty,
                    IdCode = "36308010071"
                }
            };
        }


        [TestMethod]
        public void IsIdCodeAvailable_ReturnsTrue()
        {
            var idCode = "testIdCode";
            var participants = GetTestItems();

            var mockParticipantRepository = new Mock<IParticipantRepository<Participant>>();
            var participantRepository = mockParticipantRepository.Object;

            mockParticipantRepository.Setup(r => r.IsIdCodeAvailable(idCode, "")).Returns(true);

            var result = participantRepository.IsIdCodeAvailable(idCode, "");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsIdCodeAvailable_ReturnsFalse()
        {
            var idCode = "testIdCode";

            var mockParticipantRepository = new Mock<IParticipantRepository<Participant>>();
            var participantRepository = mockParticipantRepository.Object;

            mockParticipantRepository.Setup(r => r.IsIdCodeAvailable(idCode, "")).Returns(false);

            var result = participantRepository.IsIdCodeAvailable(idCode, "");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task GetByNameAndIdCode_ReturnsParticipant()
        {
            var participant = GetTestItem();

            var mockParticipantRepository = new Mock<IParticipantRepository<Participant>>();
            var participantRepository = mockParticipantRepository.Object;

            mockParticipantRepository.Setup(r => r.GetByNameAndIdCode(participant)).ReturnsAsync(participant);

            var result = await participantRepository.GetByNameAndIdCode(participant);

            Assert.IsNotNull(result);
            Assert.AreEqual(result, participant);
        }

        [TestMethod]
        public async Task GetByNameAndIdCode_ReturnsNull()
        {
            var participant = GetTestItem();

            var mockParticipantRepository = new Mock<IParticipantRepository<Participant>>();
            var participantRepository = mockParticipantRepository.Object;

            mockParticipantRepository.Setup(r => r.GetByNameAndIdCode(participant)).ReturnsAsync((Participant?)null);

            var result = await participantRepository.GetByNameAndIdCode(participant);

            Assert.IsNull(result);
        }
    }
}