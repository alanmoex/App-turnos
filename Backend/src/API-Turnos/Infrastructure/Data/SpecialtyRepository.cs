using System.Security.Cryptography.X509Certificates;
using Domain;
using Domain.Entities;

namespace Infrastructure.Data;

public class SpecialtyRepository : EfRepository<Specialty> , ISpecialtyRepository
{
    public SpecialtyRepository(ApplicationContext context) : base(context)
    {
    }
}