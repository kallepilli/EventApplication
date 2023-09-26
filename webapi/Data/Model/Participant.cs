using System.ComponentModel.DataAnnotations;
using webapi.Data.Model.Base;

namespace webapi.Data.Model
{
    public class Participant : BaseModel
    {
        [Required]
        public string IdCode { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? CompanyName { get; set; }

        public int? ParticipantCount { get; set; }

        public string? Info { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }
    }
}
