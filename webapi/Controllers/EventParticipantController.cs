using Microsoft.AspNetCore.Mvc;
using webapi.Data;
using webapi.Data.Model;
using webapi.Services.Interfaces;

namespace webapi.Controllers;

[ApiController]
[Route("participant")]
public class EventParticipantController : ControllerBase
{

    private readonly IBaseService<EventParticipant> _service;

    public EventParticipantController(IBaseService<EventParticipant> service)
    {
        _service = service;
    }

    [HttpPost(Name = "Add EventParticipant")]
    public IActionResult AddParticipant()
    {
        return Ok();
    }
}
