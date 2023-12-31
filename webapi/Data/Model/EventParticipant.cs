﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webapi.Data.Model.Base;

namespace webapi.Data.Model
{
    public class EventParticipant : BaseModel
    {
        [ForeignKey("Event")]
        public string EventId { get; set; }
        public Event Event { get; set; }

        [ForeignKey("Participant")]
        public string ParticipantId { get; set; }
        public Participant Participant { get; set; }

        public int? ParticipantCount { get; set; }

        public string? AdditionalInfo { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }
    }
}
