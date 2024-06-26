using Application.Models;
using Application.Models.Requests;
using Domain.Entities;

namespace Application.Interfaces;


public interface IAvailableAppointmentService
{

    AvailableAppointmentDto GetById(int id);
    List<AvailableAppointmentDto> GetAll();
    AvailableAppointment Create(DateTime dateTime, Medic medic);
    void Delete(int id);
}
