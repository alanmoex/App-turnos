using Application.Models;
using Application.Models.Requests;
using Domain.Entities;

namespace Application.Interfaces;


public interface IAppointmentService
{
    AppointmentDto? GetById(int id);
    List<AppointmentDto> GetAll();
    AppointmentDto Create(AppointmentCreateRequest appointmentCreateRequest);
    void Update(int id, AppointmentUpdateRequest appointmentUpdateRequest);
    void Delete(int id);
    void CheckAndCreateAutomaticAppointments();
    void CreateAutomaticAppointments();
    void TakeAppointment(int appointmentId, int patientId);
    void CancelAppointment(int appointmentId, int patientId);
    List<AppointmentDto> GetAppointmentsByMedicalCenterId(int medicalCenterId);
}
