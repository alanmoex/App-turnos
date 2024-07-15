using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Application.Interfaces;
using Application;
using Application.Models.Requests;
using Domain.Entities;
namespace API.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;
    private readonly IPatientService _patientService;

    public AppointmentsController(IAppointmentService appointmentService, IPatientService patientService)
    {
        _appointmentService = appointmentService;
        _patientService = patientService;
    }

    [HttpGet]
    public ActionResult<List<AppointmentDto>> GetAll()
    {
        var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (userRole != typeof(SysAdmin).Name || userRole != typeof(AdminMC).Name || userRole != typeof(Patient).Name)
            return Forbid();

        var appointments = _appointmentService.GetAll();
        return Ok(appointments);
    }

    [HttpGet("{id}")]
    public ActionResult<AppointmentDto> GetById(int id)
    {

        var appointment = _appointmentService.GetById(id);
        return Ok(appointment);
    }
    [HttpPost]
    public IActionResult Create(AppointmentCreateRequest appointmentCreateRequest)
    {
        var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (userRole != typeof(SysAdmin).Name || userRole != typeof(AdminMC).Name)
            return Forbid();


        var createdAppointment = _appointmentService.Create(appointmentCreateRequest);


        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] AppointmentUpdateRequest appointmentUpdateRequest)
    {
        var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (userRole != typeof(SysAdmin).Name || userRole != typeof(AdminMC).Name)
            return Forbid();


        try
        {
            _appointmentService.Update(id, appointmentUpdateRequest);
            return Ok();
        }
        catch (System.Exception)
        {
            return NotFound();

        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (userRole != typeof(SysAdmin).Name || userRole != typeof(AdminMC).Name)
            return Forbid();

        _appointmentService.Delete(id);
        return Ok();
    }

    [HttpGet("[action]")]
    public ActionResult<List<AppointmentDto>> GetAppointmentsByPatient(int patientId)
    {
        var patient = _patientService.GetById(patientId);

        if (patient == null)
        {
            return NotFound($"No se encontro ningun turno");
        }

        var appointments = _appointmentService.GetAll();

        var appointmentsByPatient = appointments.Where(a => a.Patient.Id == patient.Id).ToList();

        return Ok(appointmentsByPatient);
    }
}
