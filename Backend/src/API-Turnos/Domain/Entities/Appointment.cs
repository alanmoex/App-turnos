using System.Data;

namespace Domain.Entities;

public class Appointment
{
    public int Id { get; set; }
    public DateTime AppointmentDateTime { get; set; }

    public Medic Medic { get; set; }

    public Patient Patient { get; set; }

    public bool IsCancelled { get; set; }

    public Appointment(DateTime appointmentDateTime, Medic medic, Patient patient, int id)
    {
        Id = id;
        AppointmentDateTime = appointmentDateTime;
        Medic = medic;
        Patient = patient;
        IsCancelled = false;
    }

}
