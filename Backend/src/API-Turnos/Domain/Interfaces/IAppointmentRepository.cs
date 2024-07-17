using Domain.Entities;
using Domain.Interfaces;

namespace Domain;

public interface IAppointmentRepository : IBaseRepository<Appointment>
{
    bool Exists(DateTime appointmentDateTime, int medicId, int medicalCenterId);
}
