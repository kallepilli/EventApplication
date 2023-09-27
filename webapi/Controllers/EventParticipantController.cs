using Microsoft.AspNetCore.Mvc;
using webapi.Data;
using webapi.Data.Model;
using webapi.Data.Model.DTOs;
using webapi.Services.Interfaces;

namespace webapi.Controllers;

[ApiController]
[Route("participant")]
public class EventParticipantController : ControllerBase
{

    private readonly IEventParticipantService<EventParticipant, EventParticipantDTO> service;

    public EventParticipantController(IEventParticipantService<EventParticipant, EventParticipantDTO> service)
    {
        this.service = service;
    }

    [HttpGet]
    [Route("id")]
    public async Task<IActionResult> GetParticipant(string id) 
    {
        var result = await service.Get(id);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet]
    [Route("list/{id}")]
    public IActionResult GetEventParticipantList(string id) => Ok(service.GetParticipantListByEventId(id));

    [HttpPost]
    public async Task<IActionResult> SaveEventParticipant([FromBody] EventParticipantDTO dto)
    {
        var result = await service.Save(dto);
        if (result is null)
            return BadRequest();
        return Ok(result);
    }

    [HttpPut]
    [Route("id")]
    public async Task<IActionResult> UpdateEventParticipant([FromBody] EventParticipantDTO dto, string id)
    {
        var result = await service.Update(id, dto);
        if (result is null)
            return BadRequest();
        return Ok(result);
    }
}
