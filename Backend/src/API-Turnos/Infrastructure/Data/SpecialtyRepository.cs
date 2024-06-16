using System.Security.Cryptography.X509Certificates;
using Domain;
using Domain.Entities;

namespace Infrastructure.Data;

public class SpecialtyRepository : ISpecialtyRepository
{
    private readonly ApplicationContext _context;
    public SpecialtyRepository(ApplicationContext context)
    {
        _context = context;
    }

    public Specialty Add(Specialty specialty)
    {
        _context.Specialties.Add(specialty);
        _context.SaveChanges();
        return specialty;
    }
    public List<Specialty> GetAll()
    {
        return _context.Specialties.ToList();
    }

    public Specialty? GetById(int id)
    {
        return _context.Specialties.FirstOrDefault(x => x.Id == id);
    }

    public void Delete(Specialty specialty)
    {
        _context.Specialties.Remove(specialty);
        _context.SaveChanges();
    }

    public Specialty Update(Specialty specialty)
    {
        var obj = _context.Specialties.FirstOrDefault(p => p.Id == specialty.Id) ?? throw new Exception();
        obj.Name = specialty.Name;
        _context.SaveChanges();
        return obj;
    }
}