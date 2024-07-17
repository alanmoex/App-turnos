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

    public override List<Medic> GetAll()
    {
        return _context.Medics
            .Include(m => m.Specialties)
            .Include(m => m.MedicalCenter)
            .Include(m => m.WorkSchedules)
            .ToList();
    }
}