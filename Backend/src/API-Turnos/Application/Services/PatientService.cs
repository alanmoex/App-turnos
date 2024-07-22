using Application.Interfaces;
using Application.Models.Requests;
using Domain;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public PatientDto Create(PatientCreateRequest patientCreateRequest)
    {
        var newPatient = new Patient(patientCreateRequest.Name, patientCreateRequest.LastName, patientCreateRequest.Email, patientCreateRequest.Password);
        var obj = _patientRepository.Add(newPatient);
        return PatientDto.Create(obj);
    }

    public void Delete(int id)
    {
        var obj = _patientRepository.GetById(id)
            ?? throw new NotFoundException(typeof(Patient).ToString(), id);

        _patientRepository.Delete(obj);
    }

    public List<PatientDto> GetAll(){
        var list = _patientRepository.GetAll();

        return PatientDto.CreateList(list);
    }

    public PatientDto GetById(int id)
    {
        var obj = _patientRepository.GetById(id)
            ?? throw new NotFoundException(typeof(Patient).ToString(), id);

        return PatientDto.Create(obj);
    }

    public void Update(int id,PatientUpdateRequest patientUpdateRequest)
    {
        var obj = _patientRepository.GetById(id)
            ?? throw new NotFoundException(typeof(Patient).ToString(), id);

        if (patientUpdateRequest.Name != null) obj.Name = patientUpdateRequest.Name;

        if (patientUpdateRequest.LastName != null) obj.LastName = patientUpdateRequest.LastName;

        if (patientUpdateRequest.Email != null) obj.Email = patientUpdateRequest.Email;

        if (patientUpdateRequest.Password != null) obj.Password = patientUpdateRequest.Password;
        
        _patientRepository.Update(obj);
    }


}
