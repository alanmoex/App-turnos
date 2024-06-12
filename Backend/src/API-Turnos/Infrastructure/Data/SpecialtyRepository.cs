using System.Security.Cryptography.X509Certificates;
using Domain;
using Domain.Entities;

namespace Infrastructure.Data;

public class SpecialtyRepository : ISpecialtyRepository
{
    static int LastIdAssigned = 0;
    static List<Specialty> specialties = [];

    public Specialty Add(Specialty specialty)
    {
        specialty.Id = ++LastIdAssigned;
        specialties.Add(specialty);
        return specialty;
    }
    public List<Specialty> GetAll()
    {
        return specialties;
    }

    public Specialty? GetById(int id)
    {
        return specialties.FirstOrDefault(x => x.Id == id);
    }

    public void Delete(Specialty specialty)
    {
        specialties.Remove(specialty);
    }

    public Specialty Update(Specialty specialty)
    {
        var obj = specialties.FirstOrDefault(p => p.Id == specialty.Id) ?? throw new Exception();
        obj.Name = specialty.Name;
        return obj;
    }
}