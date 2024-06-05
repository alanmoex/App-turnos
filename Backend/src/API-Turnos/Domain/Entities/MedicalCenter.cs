namespace Domain.Entities;

public class MedicalCenter
{
    public int Id { get; set;}
    public string Name { get; set;}
    public List<Appointment> Appointments { get; set;} = new List<Appointment>();
    public List<Specialty> Specialties{ get; set;} = new List<Specialty>();  

    public MedicalCenter(int id, string name)
    {
        Id = id;
        Name = name;
    }

}
