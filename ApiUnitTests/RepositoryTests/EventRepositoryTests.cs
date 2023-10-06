using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webapi.Data.Model;

namespace ApiUnitTests.RepositoryTests
{
    [TestClass]
    public class EventRepositoryTests : BaseRepositoryTests<Event>
    {
        protected override Event GetTestItem()
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

        protected override Event GetUpdatedItem()
        {
            return new Event
            {
                Id = "1",
                Name = "Test Event Updated",
                Location = "Test Location Updated",
                AdditionalInfo = "Test Info Updated",
                EventTime = DateTime.Now
            };
        }

        protected override string GetNonExistingId()
        {
            return "999";
        }

        protected override List<Event> GetTestItems()
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
    }
}