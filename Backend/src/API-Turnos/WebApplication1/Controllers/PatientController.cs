using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Domain.Exceptions;
using Application;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "SysAdmin")]
    public ActionResult<PatientDto> GetById(int id)
    {
        try
        {
            return Ok(_patientService.GetById(id));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

    [HttpGet]
    [Authorize(Roles = "SysAdmin")]
    public ActionResult<List<PatientDto>> GetAll()
    {
        return Ok(_patientService.GetAll());
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult Create(PatientCreateRequest patientCreateRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var patient = _patientService.Create(patientCreateRequest);
            return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient);
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
    [Authorize(Roles = "SysAdmin,AdminMC")]
    public IActionResult Delete(int id)
    {
        try
        {
            _patientService.Delete(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "SysAdmin,AdminMC")]
    public IActionResult Update(int id, PatientUpdateRequest patientUpdateRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _patientService.Update(id, patientUpdateRequest);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred.");
        }
    }
}
