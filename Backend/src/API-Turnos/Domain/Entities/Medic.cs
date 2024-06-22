using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Medic
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set;}
    public string LastName { get; set;}
    public string LicenseNumber { get; set;}
    public MedicalCenter MedicalCenter { get; set;}
    public List<Specialty> Specialties{ get; set;} = new List<Specialty>();  
    public List<Appointment> Appointments { get; set;} = new List<Appointment>();
    public List<AvailableAppointment> AvailableAppointments { get; set;} = new List<AvailableAppointment>();
    public List<WorkSchedule> WorkSchedules { get; set;} = new  List<WorkSchedule>();

    // Constructor sin parámetros necesario para EF
    public Medic()
    {
    }

    public Medic( string name, string lastName, string licenseNumber, MedicalCenter medicalCenter, List<Specialty> specialties)
    {
        
        Name = name;
        LastName = lastName;
        LicenseNumber = licenseNumber;
        MedicalCenter = medicalCenter;
        Specialties = specialties;
    } 

}
