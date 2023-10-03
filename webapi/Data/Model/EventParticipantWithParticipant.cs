using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webapi.Data.Model.Base;

namespace webapi.Data.Model
{
    public class EventParticipantWithParticipant : BaseModel
    {
        public string EventId { get; set; }
        public string ParticipantId { get; set; }
        public string IdCode { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CompanyName { get; set; }
        public bool IsCompany { get; set; }
        public int? ParticipantCount { get; set; }
        public string? AdditionalInfo { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
