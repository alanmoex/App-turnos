using Application.Interfaces;
using Application.Models.Requests;
using Domain;
using Domain.Entities;

namespace Application.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public Patient Create(PatientCreateRequest patientCreateRequest)
    {
        var newPatient = new Patient(patientCreateRequest.Name, patientCreateRequest.LastName, patientCreateRequest.Email, patientCreateRequest.Password);
        return _patientRepository.Add(newPatient);
    }

    public void Delete(int id)
    {
        var obj = _patientRepository.GetById(id);

        if (obj == null)
            throw new Exception("");

        _patientRepository.Delete(obj);
    }

    public List<PatientDto> GetAll(){
        var list = _patientRepository.GetAll();

        return PatientDto.CreateList(list);
    }

    public PatientDto GetById(int id)
    {
        var obj = _patientRepository.GetById(id)
            ?? throw new Exception("");
        
        return PatientDto.Create(obj);
    }

    public void Update(int id,PatientUpdateRequest patientUpdateRequest)
    {
        var obj = _patientRepository.GetById(id) 
            ?? throw new Exception("");
        
        if (patientUpdateRequest.Name != string.Empty) obj.Name = patientUpdateRequest.Name;

        if (patientUpdateRequest.LastName != string.Empty) obj.LastName = patientUpdateRequest.LastName;

        if (patientUpdateRequest.Email != string.Empty) obj.Email = patientUpdateRequest.Email;

        if (patientUpdateRequest.Password != string.Empty) obj.Password = patientUpdateRequest.Password;
        
        _patientRepository.Update(obj);
    }

}
