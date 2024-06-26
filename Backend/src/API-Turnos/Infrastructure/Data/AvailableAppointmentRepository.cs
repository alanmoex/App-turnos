using System.Security.Cryptography.X509Certificates;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AvailableAppointmentRepository : EfRepository<AvailableAppointment>, IAvailableAppointmentRepository
{
    public AvailableAppointmentRepository(ApplicationContext context) : base(context)
    {
    }
}
