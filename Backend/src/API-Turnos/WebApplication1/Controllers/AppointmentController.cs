using Application.Interfaces;
using Application.Models.Requests;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public ActionResult<List<Appointment>> GetAll()
        {
            var appointments = _appointmentService.GetAll();
            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public ActionResult<Appointment> GetById(int id)
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
                var UpdateAppointment = _appointmentService.Update(appointmentUpdateRequest);
                return Ok(UpdateAppointment);
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
}