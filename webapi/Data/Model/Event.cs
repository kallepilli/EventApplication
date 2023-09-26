using System.ComponentModel.DataAnnotations;
using webapi.Data.Model.Base;

namespace webapi.Data.Model
{
    public class Event : BaseModel
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime EventTime { get; set; }

        public string Location { get; set; }

        [MaxLength(1000)]
        public string? AdditionalInfo { get; set; }
    }
}
