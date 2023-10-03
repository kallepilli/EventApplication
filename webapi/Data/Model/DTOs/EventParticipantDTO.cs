namespace webapi.Data.Model.DTOs
{
    public class EventParticipantDTO
    {
        public string EventId { get; set; }
        public string ParticipantId { get; set; }
        public int ParticipantCount { get; set; }
        public string AdditionalInfo { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
