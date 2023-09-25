using Microsoft.AspNetCore.Mvc;
using webapi.Data;
using webapi.Data.Model;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
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
        _context.Events.Add(new Event { Id = 1, Name = "test" });
        _context.SaveChanges();
        return _context.Events.ToList();
    }
}
