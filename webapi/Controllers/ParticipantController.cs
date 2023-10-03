using Microsoft.AspNetCore.Mvc;
using webapi.Data;
using webapi.Data.Model;
using webapi.Data.Model.DTOs;
using webapi.Services.Interfaces;

namespace webapi.Controllers;

[ApiController]
[Route("participant")]
public class ParticipantController : ControllerBase
{

    private readonly IBaseService<Participant, ParticipantDTO> service;

    public ParticipantController(IBaseService<Participant, ParticipantDTO> service)
    {
        this.service = service;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetParticipant(string id) 
    {
        var result = await service.Get(id);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> SaveParticipant([FromBody] ParticipantDTO dto)
    {
        var result = await service.Save(dto);
        if (result is null)
            return BadRequest();
        return Ok(result);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateParticipant([FromBody] ParticipantDTO dto, string id)
    {
        var result = await service.Update(id, dto);
        if (result is null)
            return BadRequest();
        return Ok(result);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await service.Delete(id);
        if (result)
            return Ok();
        return NotFound();
    }
}
