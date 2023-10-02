namespace webapi.Data.Model.DTOs
{
    public class EventParticipantDTO
    {
        public string EventId { get; set; }
        public string IdCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public int ParticipantCount { get; set; }
        public string AdditionalInfo { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public bool IsCompany { get; set; }
    }
}
