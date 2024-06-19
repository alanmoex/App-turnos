using System.Security.Cryptography.X509Certificates;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class MedicRepository : EfRepository<Medic>, IMedicRepository
{
    public MedicRepository(ApplicationContext context) : base(context)
    {
    }

    public List<Specialty> GetMedicSpecialties(int medicId)
    {
        return _context.Medics.Include(a => a.Specialties).Where(a => a.Id == medicId)
            .Select(a => a.Specialties).FirstOrDefault() ?? new List<Specialty>();
    }
}