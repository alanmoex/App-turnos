using Domain.Entities;

namespace Domain;

public interface IPatientRepository
{
    Patient? GetById(int id);
    List<Patient> GetAll();
    Patient Add(Patient patient);
    Patient Update(Patient patient);
    void Delete(Patient patient);

}
