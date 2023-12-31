using Microsoft.AspNetCore.Mvc;
using webapi.Aids;
using webapi.Data.Model;
using webapi.Data.Model.DTOs;
using webapi.Services.Interfaces;

namespace webapi.Controllers;

[ApiController]
[Route("participant")]
public class ParticipantController : ControllerBase
{

    private readonly IParticipantService<Participant, ParticipantDTO> service;

    public ParticipantController(IParticipantService<Participant, ParticipantDTO> service)
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

    [HttpGet]
    [Route("validateId/{idCode}")]
    public IActionResult ValidateIdCode(string idCode, [FromQuery] bool isCompany, [FromQuery] string currentIdCode = "")
    {
       if (isCompany == true)
        {
            if (service.IsIdCodeAvailable(idCode, currentIdCode))
                return Ok(new ApiResponse<object> { Success = true, Message = string.Empty });
            else
                return Ok(new ApiResponse<object> { Success = false, Message = "Sellise reg. numbriga osaleja on juba andmebaasis!" });
        }
        
        var helper = new HelperMethods();
        var result = helper.ValidateIdCode(idCode);

        if (!result)
            return Ok(new ApiResponse<object> { Success = false, Message = "Isikukood ei vasta standardile!" });

        if (service.IsIdCodeAvailable(idCode, currentIdCode))
            return Ok(new ApiResponse<object> { Success = true, Message = string.Empty });
        else
            return Ok(new ApiResponse<object> { Success = false, Message = "Sellise isikukoodiga isik on juba andmebaasis!" }); 
    }
}
