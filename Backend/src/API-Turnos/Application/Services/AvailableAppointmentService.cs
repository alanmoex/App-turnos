using Application.Interfaces;
using Application.Models.Requests;
using Domain;
using Domain.Entities;

namespace Application.Services;

public class AvailableAppointmentService : IAvailableAppointmentService
{
    private readonly IAvailableAppointmentRepository _availableAppointmentRepository;
    public AvailableAppointmentService(IAvailableAppointmentRepository availableAppointmentRepository)
    {
        _availableAppointmentRepository = availableAppointmentRepository;
    }

    public Patient Create(PatientCreateRequest availableAppointmentCreateRequest)
    {
        var newPatient = new Patient(availableAppointmentCreateRequest.Name, AvailableAppointmentCreateRequest.LastName, AvailableAppointmentCreateRequest.Email, AvailableAppointmentCreateRequest.Password);
        return _availableAppointmentRepository.Add(newPatient);
    }

    public void Delete(int id)
    {
        var obj = _AvailableAppointmentRepository.GetById(id);

        if (obj == null)
            throw new Exception("");

        _AvailableAppointmentRepository.Delete(obj);
    }

    public List<PatientDto> GetAll()
    {
        var list = _AvailableAppointmentRepository.GetAll();

        return PatientDto.CreateList(list);
    }

    public PatientDto GetById(int id)
    {
        var obj = _AvailableAppointmentRepository.GetById(id)
            ?? throw new Exception("");

        return PatientDto.Create(obj);
    }

    public void Update(int id, PatientUpdateRequest AvailableAppointmentUpdateRequest)
    {
        var obj = _AvailableAppointmentRepository.GetById(id)
            ?? throw new Exception("");

        if (AvailableAppointmentUpdateRequest.Name != string.Empty) obj.Name = AvailableAppointmentUpdateRequest.Name;

        if (AvailableAppointmentUpdateRequest.LastName != string.Empty) obj.LastName = AvailableAppointmentUpdateRequest.LastName;

        if (AvailableAppointmentUpdateRequest.Email != string.Empty) obj.Email = AvailableAppointmentUpdateRequest.Email;

        if (AvailableAppointmentUpdateRequest.Password != string.Empty) obj.Password = AvailableAppointmentUpdateRequest.Password;

        _AvailableAppointmentRepository.Update(obj);
    }

}
