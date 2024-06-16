using System.Security.Cryptography.X509Certificates;
using Domain;
using Domain.Entities;

namespace Infrastructure.Data;

public class PatientRepository : IPatientRepository
{
    private readonly ApplicationContext _context;
    public PatientRepository(ApplicationContext context)
    {  
        _context = context; 
    }

    public Patient Add(Patient patient)
    {
        _context.Patients.Add(patient);
        _context.SaveChanges();
        return patient;
    }
    public List<Patient> GetAll()
    {
        return _context.Patients.ToList();
    }

    public Patient? GetById(int id)
    {
        return _context.Patients.FirstOrDefault(x => x.Id == id);
    }

    public void Delete(Patient patient)
    {
        _context.Patients.Remove(patient);
        _context.SaveChanges();
    }

    public Patient Update(Patient patient)
    {
        var obj = _context.Patients.FirstOrDefault(p => p.Id == patient.Id) ?? throw new Exception();
        obj.Name = patient.Name;
        obj.LastName = patient.LastName;
        obj.Email = patient.Email;
        obj.Password = patient.Password;
        _context.SaveChanges();
        return obj;
    }
}
