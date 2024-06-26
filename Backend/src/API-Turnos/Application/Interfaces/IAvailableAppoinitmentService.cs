using Application.Models;
using Application.Models.Requests;
using Domain.Entities;

namespace Application.Interfaces;


public interface IAvailableAppointmentService
{
    AvailableAppointment GetById(int id);
    List<AvailableAppointment> GetAll();
    AvailableAppointment Create(AppointmentCreateRequest appointmentCreateRequest);
    void Update(int id, AppointmentUpdateRequest appointmentUpdateRequest);
    void Delete(int id);
}
