using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.Models.Requests;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Application;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;
    private readonly IPatientService _patientService;
    private readonly IAdminMCService _adminMCService;

    public AppointmentsController(IAppointmentService appointmentService, IPatientService patientService, IAdminMCService adminMCService)
    {
        _appointmentService = appointmentService;
        _patientService = patientService;
        _adminMCService = adminMCService;
    }

    [HttpGet]
    [Authorize(Roles = "SysAdmin")]
    public ActionResult<List<AppointmentDto>> GetAll()
    {
        return Ok(_appointmentService.GetAll());
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "SysAdmin")]
    public ActionResult<AppointmentDto> GetById(int id)
    {
        try
        {
            return Ok(_appointmentService.GetById(id));
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
    public IActionResult Create(AppointmentCreateRequest appointmentCreateRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdAppointment = _appointmentService.Create(appointmentCreateRequest);
            return CreatedAtAction(nameof(GetById), new { id = createdAppointment.Id }, createdAppointment);
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
    public IActionResult Update(int id, [FromBody] AppointmentUpdateRequest appointmentUpdateRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _appointmentService.Update(id, appointmentUpdateRequest);
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
    public IActionResult Delete(int id)
    {
        try
        {
            _appointmentService.Delete(id);
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

    [HttpGet("[action]")]
    [Authorize(Roles = "Patient")]
    public ActionResult<List<AppointmentDto>> GetAppointmentsByPatient()
    {
        var patientId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");

        try
        {
            var patient = _patientService.GetById(patientId);
            var appointments = _appointmentService.GetAll();
            var appointmentsByPatient = appointments.Where(a => a.Patient?.Id == patient.Id).ToList();

            return Ok(appointmentsByPatient);
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

    [HttpPost("[action]")]
    [Authorize(Roles = "SysAdmin")]
    public IActionResult CreateAutomaticAppointments()
    {
        _appointmentService.CreateAutomaticAppointments();
        return Ok();
    }

    [HttpPut("[action]/{appointmentId}")]
    [Authorize(Roles = "Patient")]
    public IActionResult TakeAppointment(int appointmentId)
    {
        var patientId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");

        try
        {
            _appointmentService.TakeAppointment(appointmentId, patientId);
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

    [HttpPut("[action]/{appointmentId}")]
    [Authorize(Roles = "Patient")]
    public IActionResult CancelAppointment(int appointmentId)
    {
        var patientId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");

        try
        {
            _appointmentService.CancelAppointment(appointmentId, patientId);
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

    [HttpGet("[action]")]
    [Authorize(Roles = "AdminMC")]
    public IActionResult GetAppointmentsByMedicalCenter()
    {
        var adminMCId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");

        try
        {
            var adminMC = _adminMCService.GetById(adminMCId);
            if (adminMC == null)
            {
                return NotFound(new { Message = $"AdminMC with ID {adminMCId} not found." });
            }

            var medicalCenterId = adminMC.MedicalCenter.Id;
            var appointments = _appointmentService.GetAppointmentsByMedicalCenterId(medicalCenterId);

            return Ok(appointments);
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
