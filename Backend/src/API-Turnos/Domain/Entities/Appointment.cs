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
    public MedicalCenter MedicalCenter { get; set; }

    public bool IsCancelled { get; set; }

    // Constructor sin parámetros necesario para EF
    public Appointment() 
    {
    }
    public Appointment(DateTime appointmentDateTime, Medic medic, Patient patient, MedicalCenter medicalCenter)
    {
        AppointmentDateTime = appointmentDateTime;
        Medic = medic;
        Patient = patient;
        MedicalCenter = medicalCenter;
        IsCancelled = false;
    }

}
