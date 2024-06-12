using Application.Models;
using Application.Models.Requests;
using Domain.Entities;

namespace Application.Interfaces;


public interface IAppointmentService
{
    Appointment? GetById(int id);
    List<Appointment> GetAll();
    Appointment Create(AppointmentCreateRequest appointmentCreateRequest);
    Appointment Update(AppointmentUpdateRequest appointmentUpdateRequest);
    void Delete(int id);
}
