using Microsoft.AspNetCore.Mvc;
using webapi.Data;
using webapi.Data.Model;

namespace webapi.Controllers;

[ApiController]
[Route("participant")]
public class EventParticipantController : ControllerBase
{

    private readonly ApplicationDbContext _context;

    public EventParticipantController(ApplicationDbContext context)
    {
       _context = context;
    }

    [HttpPost(Name = "Add EventParticipant")]
    public IActionResult AddParticipant()
    {
        var newEvent = new Event()
        {
            Name= "nammmeeme",
            EventTime = DateTime.Now.ToUniversalTime(),
            Location = "Tallinn",
        };

        _context.Events.Add(newEvent);
        _context.SaveChanges();

        var eventId = _context.Events.FirstOrDefault().Id;
        var newParticipant = new EventParticipant()
        {
            CompanyName = "TESTNAME",
            AdditionalInfo = "SKDFLKJSFLKSJDF",
            EventId = eventId,
            ParticipantCount = 3,
            IdCode = "fsdf",
            PaymentMethod = PaymentMethod.Cash
        };

        _context.EventParticipants.Add(newParticipant);
        _context.SaveChanges();
        return Ok();
    }
}
