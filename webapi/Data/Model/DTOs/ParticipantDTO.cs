namespace webapi.Data.Model.DTOs
{
    public class ParticipantDTO
    {
        public string IdCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public bool IsCompany { get; set; }
    }
}
