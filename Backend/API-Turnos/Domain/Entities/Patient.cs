namespace Domain.Entities;

public class Patient : User
{
    
    public string LastName { get; set; }
    public List<Appointment> Appointments { get; set; } = new List<Appointment>();

    public Patient(int id, string name, string lastName, string password, string email){
        Id = id;
        Name = name;
        LastName = lastName;
        Password = password;
        Email = email;
    }
}
