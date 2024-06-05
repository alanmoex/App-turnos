using System.Security.Cryptography.X509Certificates;
using Domain;
using Domain.Entities;

namespace Infrastructure.Data;

public class PatientRepository : IPatientRepository
{
    static int LastIdAssigned = 0;
    static List<Patient> patients = [];

    public Patient Add(Patient patient)
    {
        patient.Id = ++LastIdAssigned;
        patients.Add(patient);
        return patient;
    }
    public List<Patient> GetAll()
    {
        return patients;
    }

    public Patient? GetById(int id)
    {
        return patients.FirstOrDefault(x => x.Id == id);
    }

    public void Delete(Patient patient)
    {
        patients.Remove(patient);
    }

    public Patient Update(Patient patient)
    {
        var obj = patients.FirstOrDefault(p => p.Id == patient.Id) ?? throw new Exception();
        obj.Name = patient.Name;
        return obj;
    }
}
