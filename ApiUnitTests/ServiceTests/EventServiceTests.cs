using ApiUnitTests.ServiceTests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using webapi.Data.Model;
using webapi.Data.Model.DTOs;

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
    }
}
