using System.Security.Cryptography.X509Certificates;
using Domain;
using Domain.Entities;

namespace Infrastructure.Data;

public class MedicalCenterRepository : EfRepository<MedicalCenter>, IMedicalCenterRepository
{
    public MedicalCenterRepository(ApplicationContext context) : base(context)
    {
    }
}
