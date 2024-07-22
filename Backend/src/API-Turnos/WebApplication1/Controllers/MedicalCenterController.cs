using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.Models.Requests;
using Application;
using Microsoft.AspNetCore.Authorization;
using Domain.Exceptions;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MedicalCenterController : ControllerBase
{
    private readonly IMedicalCenterService _medicalCenterService;

    public MedicalCenterController(IMedicalCenterService medicalCenterService)
    {
        _medicalCenterService = medicalCenterService;
    }

    [HttpGet]
    [Authorize(Roles = "Patient, SysAdmin, AdminMC")]
    public ActionResult<List<MedicalCenterDto>> GetAll()
    {
        return Ok(_medicalCenterService.GetAll());
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Patient, SysAdmin, AdminMC")]
    public ActionResult<MedicalCenterDto> GetById([FromRoute] int id)
    {
        try
        {
            return Ok(_medicalCenterService.GetById(id));
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

    [HttpPost]
    [Authorize(Roles = "SysAdmin")]
    public IActionResult Create([FromBody] MedicalCenterCreateRequest medicalCenterCreateRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var medicalCenter = _medicalCenterService.Create(medicalCenterCreateRequest);
            return CreatedAtAction(nameof(GetById), new { id = medicalCenter.Id }, medicalCenter);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "SysAdmin")]
    public IActionResult Update([FromRoute] int id, [FromBody] MedicalCenterUpdateRequest medicalCenterUpdateRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _medicalCenterService.Update(id, medicalCenterUpdateRequest);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "SysAdmin")]
    public IActionResult Delete([FromRoute] int id)
    {
        try
        {
            _medicalCenterService.Delete(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred.");
        }
    }
}