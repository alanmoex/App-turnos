namespace Domain.Entities;

public class AdminMC : User
{
    public MedicalCenter MedicalCenter{ get; set; }

    // Constructor sin parámetros necesario para EF
    public AdminMC()
    {
    }
    public AdminMC(string name, string email, string password, MedicalCenter medicalCenter){
        Name = name;
        Email = email;
        Password = password;
        MedicalCenter = medicalCenter;
    }
}
