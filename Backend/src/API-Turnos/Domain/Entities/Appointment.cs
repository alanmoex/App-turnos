using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Domain.Entities;

public class Appointment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime AppointmentDateTime { get; set; }

    public Medic Medic { get; set; }

    public Patient Patient { get; set; }

    public bool IsCancelled { get; set; }

    // Constructor sin parámetros necesario para EF
    public Appointment() 
    {
    }
    public Appointment(DateTime appointmentDateTime, Medic medic, Patient patient)
    {
        AppointmentDateTime = appointmentDateTime;
        Medic = medic;
        Patient = patient;
        IsCancelled = false;
    }

}
