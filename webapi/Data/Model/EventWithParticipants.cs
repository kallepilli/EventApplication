namespace webapi.Data.Model
{
    public class EventWithParticipants
    {
        public string EventId { get; set; }
        public string Name { get; set; }
        public DateTime EventTime { get; set; }
        public string Location { get; set; }
        public string AdditionalInfo { get; set; }
        public int ParticipantCount { get; set; }
    }
}
