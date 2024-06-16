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
    public List<Specialty> Specialties{ get; set;} = new List<Specialty>();  
    public List<Appointment> Appointments { get; set;} = new List<Appointment>();
    public List<WorkSchedule> WorkSchedules { get; set;} = new  List<WorkSchedule>();

    // Constructor sin parámetros necesario para EF
    public Medic()
    {
    }

    public Medic( string name, string lastName, string licenseNumber, List<Specialty> specialties)
    {
        
        Name = name;
        LastName = lastName;
        LicenseNumber = licenseNumber;
        Specialties = specialties;
    } 

}
