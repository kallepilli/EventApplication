using System.ComponentModel.DataAnnotations;

namespace webapi.Data.Model.DTOs
{
    public class EventDTO
    {
        public string Name { get; set; }
        public DateTime EventTime { get; set; }
        public string Location { get; set; }
        public string? AdditionalInfo { get; set; }
    }
}
