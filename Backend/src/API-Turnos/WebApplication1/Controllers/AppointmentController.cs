using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Application.Interfaces;
using Application;
using Application.Models.Requests;
using Domain.Entities;
namespace API.Controllers;


[Route("api/[controller]")]
[ApiController]

public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpGet]
    public ActionResult<List<AppointmentDto>> GetAll()
    {
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
        var createdAppointment = _appointmentService.Create(appointmentCreateRequest);


        return Ok(createdAppointment);
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] AppointmentUpdateRequest appointmentUpdateRequest)
    {

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
        _appointmentService.Delete(id);
        return Ok();
    }
}
