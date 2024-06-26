using Application.Interfaces;
using Application.Models.Requests;
using Domain;
using Domain.Entities;

namespace Application.Services;

public class AvailableAppointmentService : IAvailableAppointmentService
{
    private readonly IAvailableAppointmentRepository _availableAppointmentRepository;

    private readonly IMedicRepository _medicRepository;
    public AvailableAppointmentService(IAvailableAppointmentRepository availableAppointmentRepository, IMedicRepository medicRepository)
    {
        _availableAppointmentRepository = availableAppointmentRepository;
        _medicRepository = medicRepository;
    }

    public AvailableAppointment Create(DateTime dateTime, Medic medic)
    {
        var newAvailableAppointment = new AvailableAppointment(dateTime, medic);
        return _availableAppointmentRepository.Add(newAvailableAppointment);
    }

    public void Delete(int id)
    {
        var obj = _availableAppointmentRepository.GetById(id);

        if (obj == null)
            throw new Exception("");

        _availableAppointmentRepository.Delete(obj);
    }

    public List<AvailableAppointmentDto> GetAll()
    {
        var list = _availableAppointmentRepository.GetAll();

        return AvailableAppointmentDto.CreateList(list);
    }

    public AvailableAppointmentDto GetById(int id)
    {
        var obj = _availableAppointmentRepository.GetById(id)
            ?? throw new Exception("");

        return AvailableAppointmentDto.Create(obj);
    }


}
