using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webapi.Data.Model.Base;

namespace webapi.Data.Model
{
    public class EventParticipant : BaseModel
    {
        [ForeignKey("Event")]
        public string EventId { get; set; }
        public Event Event { get; set; }

        [Required]
        public string IdCode { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? CompanyName { get; set; }

        public int? ParticipantCount { get; set; }

        public string? AdditionalInfo { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }
    }
}
