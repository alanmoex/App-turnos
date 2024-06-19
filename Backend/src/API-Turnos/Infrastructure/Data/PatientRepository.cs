using System.Security.Cryptography.X509Certificates;
using Domain;
using Domain.Entities;

namespace Infrastructure.Data;

public class PatientRepository : EfRepository<Patient>,  IPatientRepository
{
    public PatientRepository(ApplicationContext context) : base(context)
    {
    }    
}
