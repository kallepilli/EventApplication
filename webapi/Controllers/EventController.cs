using Microsoft.AspNetCore.Mvc;
using webapi.Data.Model;
using webapi.Data.Model.DTOs;
using webapi.Services.Interfaces;

namespace webapi.Controllers;

[ApiController]
[Route("event")]
public class EventController : ControllerBase
{

    private readonly IEventService<Event, EventDTO> service;

    public EventController(IEventService<Event, EventDTO> service)
    {
        this.service = service;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetEvent(string id) => Ok(await service.GetEventWithParticipantCount(id));

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetEventList() => Ok(await service.GetEventWithParticipantsList());

    [HttpPost]
    public async Task<IActionResult> SaveEvent([FromBody] EventDTO dto)
    {
        var result = await service.Save(dto);
        if (result is null)
            return BadRequest();
        return Ok(result);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateEventParticipant([FromBody] EventDTO dto, string id)
    {
        var result = await service.Update(id, dto);
        if (result is null)
            return BadRequest();
        return Ok(result);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteEvent(string id)
    {
        var result = await service.Delete(id);
        if (result)
            return Ok();
        return NotFound();
    }
}
