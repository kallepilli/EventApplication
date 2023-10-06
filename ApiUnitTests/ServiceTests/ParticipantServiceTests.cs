using ApiUnitTests.ServiceTests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using webapi.Data.Model;
using webapi.Data.Model.DTOs;

namespace ApiUnitTests.ServiceTests
{
    [TestClass]
    public class ParticipantServiceTests : BaseServiceTests<Participant, ParticipantDTO>
    {
        protected override Func<ParticipantDTO, string> GetDtoParamFunc() => x => x.IdCode;

        protected override Func<Participant, string> GetEntityParamFunc() => x => x.IdCode;

        protected override ParticipantDTO GetTestDto()
        {
            return new ParticipantDTO
            {
                IsCompany = false,
                FirstName = "Kalle",
                LastName = "Pilli",
                CompanyName = string.Empty,
                IdCode = "39208014225"
            };
        }

        protected override Participant GetTestEntity()
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

        protected override List<Participant> GetTestEntityList()
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

        protected override ParticipantDTO GetUpdateDto()
        {
            return new ParticipantDTO
            {
                IsCompany = false,
                FirstName = "Kalle updated",
                LastName = "Pilli updated",
                CompanyName = string.Empty,
                IdCode = "39208014225 updated"
            };
        }

        protected override Participant GetUpdatedEntity()
        {
            return new Participant
            {
                Id = "testParticipant1",
                IsCompany = false,
                FirstName = "Kalle updated",
                LastName = "Pilli updated",
                CompanyName = string.Empty,
                IdCode = "39208014225 updated"
            };
        }
    }
}
