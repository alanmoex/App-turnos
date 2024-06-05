using Application.Models.Requests;
using Domain.Entities;

namespace Application.Interfaces;

public interface IPatientService
{
    List<PatientDto> GetAll();
    PatientDto GetById(int id);
    Patient Create(PatientCreateRequest patientCreateRequest);   
    void Update(int id, PatientUpdateRequest patientUpdateRequest);
    void Delete(int id);
}
