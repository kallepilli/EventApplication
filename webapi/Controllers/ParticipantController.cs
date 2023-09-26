using Microsoft.AspNetCore.Mvc;
using webapi.Data;
using webapi.Data.Model;

namespace webapi.Controllers;

[ApiController]
[Route("participant")]
public class ParticipantController : ControllerBase
{

    private readonly ApplicationDbContext _context;

    public ParticipantController(ApplicationDbContext context)
    {
       _context = context;
    }

    [HttpPost(Name = "Add Participant")]
    public IActionResult AddParticipant()
    {
        var newParticipant = new Participant()
        {
            CompanyName = "TESTNAME",
            Info = "SKDFLKJSFLKSJDF",
            ParticipantCount = 3,
            IdCode = "fsdf",
            PaymentMethod = PaymentMethod.Cash
        };

        _context.Participants.Add(newParticipant);
        _context.SaveChanges();
        return Ok();
    }
}
