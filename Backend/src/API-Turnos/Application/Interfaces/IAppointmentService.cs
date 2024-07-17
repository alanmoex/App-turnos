using Application.Models;
using Application.Models.Requests;
using Domain.Entities;

namespace Application.Interfaces;


public interface IAppointmentService
{
    AppointmentDto? GetById(int id);
    List<AppointmentDto> GetAll();
    Appointment Create(AppointmentCreateRequest appointmentCreateRequest);
     void Update(int id, AppointmentUpdateRequest appointmentUpdateRequest);
    void Delete(int id);
}
