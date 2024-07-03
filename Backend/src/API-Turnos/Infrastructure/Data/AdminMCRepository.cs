using System.Security.Cryptography.X509Certificates;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AdminMCRepository: EfRepository<AdminMC>, IAdminMCRepository
{
    public AdminMCRepository(ApplicationContext context) : base(context)
    {
    }

    public override List<AdminMC> GetAll()
    {
        return _context.AdminMCs
            .Include(a=> a.MedicalCenter)
            .ToList();
    }
}
