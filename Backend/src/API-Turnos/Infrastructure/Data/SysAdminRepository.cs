using System.Security.Cryptography.X509Certificates;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class SysAdminRepository : EfRepository<SysAdmin>, ISysAdminRepository
{
    public SysAdminRepository(ApplicationContext context) : base(context)
    {
    }
}
