using Microsoft.AspNetCore.Mvc;
using webapi.Data;
using webapi.Data.Model;

namespace webapi.Controllers;

[ApiController]
[Route("events")]
public class EventController : ControllerBase
{

    private readonly ApplicationDbContext _context;

    public EventController(ApplicationDbContext context)
    {
       _context = context;
    }

    [HttpGet(Name = "GetEvents")]
    public List<Event> Get()
    {

        return new List<Event>();
    }
}
