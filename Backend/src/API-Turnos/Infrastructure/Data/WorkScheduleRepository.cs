using System.Security.Cryptography.X509Certificates;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class WorkScheduleRepository: EfRepository<WorkSchedule>, IWorkScheduleRepository
{
    public WorkScheduleRepository(ApplicationContext context) : base(context)
    {
    }
}