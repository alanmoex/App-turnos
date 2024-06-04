namespace Domain.Entities;

public class Medic
{
    public int Id { get; set;}
    public string Name { get; set;}
    public string LastName { get; set;}
    public string LicenseNumber { get; set;}    
    public List<Specialty> Specialties{ get; set;} = new List<Specialty>();  
    public List<Appointment> Appointments { get; set;} = new List<Appointment>();
    public List<WorkSchedule> WorkSchedules { get; set;} = new  List<WorkSchedule>();

    public Medic(int id, string name, string lastName, string licenseNumber, List<Specialty> specialties)
    {
        Id = id;
        Name = name;
        LastName = lastName;
        LicenseNumber = licenseNumber;
        Specialties = specialties;
    } 

}
