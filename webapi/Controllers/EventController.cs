using Microsoft.AspNetCore.Mvc;
using webapi.Data.Model;
using webapi.Data.Model.DTOs;
using webapi.Services.Interfaces;

namespace webapi.Controllers;

[ApiController]
[Route("event")]
public class EventController : ControllerBase
{

    private readonly IBaseService<Event, EventDTO> service;

    public EventController(IBaseService<Event, EventDTO> service)
    {
        this.service = service;
    }

    [HttpGet]
    [Route("eventList")]
    public async Task<IActionResult> GetEvents() => Ok(await service.GetList());

    [HttpPost]
    public async Task<IActionResult> SaveEvent([FromBody] EventDTO dto)
    {
        var result = await service.Save(dto);
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
