using Domain.Entities;

namespace Domain;

public interface IAppointmentRepository
{
    Appointment? GetById(int id);
    List<Appointment> GetAll();
    Appointment Create(Appointment appointment);
    Appointment Update(Appointment appointment);
    void Delete(Appointment appointment);

}
