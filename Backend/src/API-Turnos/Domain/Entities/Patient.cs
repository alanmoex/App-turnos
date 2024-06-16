namespace Domain.Entities;

public class Patient : User
{
    
    public string LastName { get; set; }
    public List<Appointment> Appointments { get; set; } = new List<Appointment>();

    // Constructor sin parámetros necesario para EF
    public Patient()
    {
    }

    public Patient(string name, string lastName, string email, string password){
        Name = name;
        LastName = lastName;
        Email = email;        
        Password = password;
    }
}
