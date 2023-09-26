using Microsoft.AspNetCore.Mvc;
using webapi.Data;
using webapi.Data.Model;
using webapi.Services.Interfaces;

namespace webapi.Controllers;

[ApiController]
[Route("events")]
public class EventController : ControllerBase
{

    private readonly IBaseService<Event> _service;

    public EventController(IBaseService<Event> service)
    {
       _service = service;
    }

    [HttpGet(Name = "GetEvents")]
    public List<Event> Get()
    {

        return new List<Event>();
    }
}
