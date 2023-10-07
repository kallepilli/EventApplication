using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using webapi.Aids;
using webapi.Controllers;
using webapi.Data.Model.DTOs;
using webapi.Data.Model;
using webapi.Services.Interfaces;

namespace ApiUnitTests.ControllerTests
{
    [TestClass]
    public class ParticipantControllerTests
    {
        private Mock<IParticipantService<Participant, ParticipantDTO>> mockParticipantService;
        private ParticipantController participantController;

        private ParticipantDTO GetTestDto()
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

        private Participant GetTestEntity()
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

        private List<Participant> GetTestEntityList()
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

        [TestInitialize]
        public void Initialize()
        {
            mockParticipantService = new Mock<IParticipantService<Participant, ParticipantDTO>>();
            participantController = new ParticipantController(mockParticipantService.Object);
        }

        [TestMethod]
        public async Task GetParticipant_ReturnsOk()
        {
            var participant = GetTestEntity();
            mockParticipantService.Setup(s => s.Get(participant.Id)).ReturnsAsync(participant);

            var result = await participantController.GetParticipant(participant.Id) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreSame(participant, result.Value);
        }

        [TestMethod]
        public async Task GetParticipant_ReturnsNotFound()
        {
            string participantId = "1";
            mockParticipantService.Setup(s => s.Get(participantId)).ReturnsAsync(null as Participant);

            var result = await participantController.GetParticipant(participantId) as NotFoundResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public async Task SaveParticipant_ReturnsOk()
        {
            var participantDto = GetTestDto();
            var savedParticipant = GetTestEntity();
            mockParticipantService.Setup(s => s.Save(participantDto)).ReturnsAsync(savedParticipant);

            var result = await participantController.SaveParticipant(participantDto) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreSame(savedParticipant, result.Value);
        }

        [TestMethod]
        public async Task UpdateParticipant_ReturnsOk()
        {
            string participantId = "testParticipant1";
            var participantDto = GetTestDto();
            var updatedParticipant = GetTestEntity();
            mockParticipantService.Setup(s => s.Update(participantId, participantDto)).ReturnsAsync(updatedParticipant);

            var result = await participantController.UpdateParticipant(participantDto, participantId) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreSame(updatedParticipant, result.Value);
        }

        [TestMethod]
        public async Task UpdateParticipant_ReturnsBadRequest()
        {
            string participantId = "invalidId";
            var participantDto = GetTestDto();
            mockParticipantService.Setup(s => s.Update(participantId, participantDto)).ReturnsAsync(null as Participant);

            var result = await participantController.UpdateParticipant(participantDto, participantId) as BadRequestResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public async Task Delete_ValidId_ReturnsOk()
        {
            string participantId = "1";
            mockParticipantService.Setup(s => s.Delete(participantId)).ReturnsAsync(true);

            var result = await participantController.Delete(participantId) as OkResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public async Task Delete_InvalidId_ReturnsNotFound()
        {
            string participantId = "1";
            mockParticipantService.Setup(s => s.Delete(participantId)).ReturnsAsync(false);

            var result = await participantController.Delete(participantId) as NotFoundResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public void ValidateIdCode_CompanyIsTrue_IdCodeAvailable_ReturnsOk()
        {
            string idCode = "123456";
            bool isCompany = true;
            string currentIdCode = "7891011";
            mockParticipantService.Setup(s => s.IsIdCodeAvailable(idCode, currentIdCode)).Returns(true);

            var result = participantController.ValidateIdCode(idCode, isCompany, currentIdCode) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var apiResponse = result.Value as ApiResponse<object>;
            Assert.IsNotNull(apiResponse);
            Assert.IsTrue(apiResponse.Success);
            Assert.AreEqual(string.Empty, apiResponse.Message);
        }

        [TestMethod]
        public void ValidateIdCode_CompanyIsTrue_IdCodeNotAvailable_ReturnsOk()
        {
            string idCode = "123456";
            bool isCompany = true;
            string currentIdCode = "7891011";
            mockParticipantService.Setup(s => s.IsIdCodeAvailable(idCode, currentIdCode)).Returns(false);

            var result = participantController.ValidateIdCode(idCode, isCompany, currentIdCode) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var apiResponse = result.Value as ApiResponse<object>;
            Assert.IsNotNull(apiResponse);
            Assert.IsFalse(apiResponse.Success);
            Assert.AreEqual("Sellise reg. numbriga osaleja on juba andmebaasis!", apiResponse.Message);
        }

        [TestMethod]
        public void ValidateIdCode_CompanyIsFalse_InvalidIdCode_ReturnsOk()
        {

            string idCode = "3920801422";
            bool isCompany = false;
            mockParticipantService.Setup(s => s.IsIdCodeAvailable(idCode, string.Empty)).Returns(true);
            var helper = new HelperMethods();

            var result = participantController.ValidateIdCode(idCode, isCompany, string.Empty) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var apiResponse = result.Value as ApiResponse<object>;
            Assert.IsNotNull(apiResponse);
            Assert.IsFalse(apiResponse.Success);
            Assert.AreEqual("Isikukood ei vasta standardile!", apiResponse.Message);
        }

        [TestMethod]
        public void ValidateIdCode_CompanyIsFalse_IdCodeAvailable_ReturnsOk()
        {
            string idCode = "39208014225";
            bool isCompany = false;
            mockParticipantService.Setup(s => s.IsIdCodeAvailable(idCode, string.Empty)).Returns(true);

            var result = participantController.ValidateIdCode(idCode, isCompany, string.Empty) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var apiResponse = result.Value as ApiResponse<object>;
            Assert.IsNotNull(apiResponse);
            Assert.IsTrue(apiResponse.Success);
            Assert.AreEqual(string.Empty, apiResponse.Message);
        }

        [TestMethod]
        public void ValidateIdCode_CompanyIsFalse_IdCodeNotAvailable_ReturnsOk()
        {
            string idCode = "39208014225";
            bool isCompany = false;
            mockParticipantService.Setup(s => s.IsIdCodeAvailable(idCode, string.Empty)).Returns(false);

            var result = participantController.ValidateIdCode(idCode, isCompany, string.Empty) as OkObjectResult;
   
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var apiResponse = result.Value as ApiResponse<object>;
            Assert.IsNotNull(apiResponse);
            Assert.IsFalse(apiResponse.Success);
            Assert.AreEqual("Sellise isikukoodiga isik on juba andmebaasis!", apiResponse.Message);
        }
    }
}
