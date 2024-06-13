using System.Security.Cryptography.X509Certificates;
using Domain;
using Domain.Entities;

namespace Infrastructure.Data;
public class AppointmentRepository : IAppointmentRepository
{
    static int LastIdAssigned = 0;
    static List<Appointment> appointments = new List<Appointment>();

    public Appointment Add(Appointment appointment)
    {
        appointment.Id = ++LastIdAssigned;
        appointments.Add(appointment);
        return appointment;
    }

    public List<Appointment> GetAll()
    {
        return appointments;
    }

    public Appointment? GetById(int id)
    {
        return appointments.FirstOrDefault(x => x.Id == id);
    }

    public void Delete(Appointment appointment)
    {
        appointments.Remove(appointment);
    }

    public Appointment Update(Appointment appointment)
    {
        var existingAppointment = appointments.FirstOrDefault(a => a.Id == appointment.Id)
            ?? throw new Exception("Appointment not found");


        existingAppointment.AppointmentDateTime = appointment.AppointmentDateTime;


        return existingAppointment;
    }
}
