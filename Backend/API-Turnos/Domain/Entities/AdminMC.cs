namespace Domain.Entities;

public class AdminMC : User
{
    public MedicalCenter MedicalCenter{ get; set; }
    public AdminMC(int id, string name, string email, string password, MedicalCenter medicalCenter){
        Id = id;
        Name = name;
        Email = email;
        Password = password;
        MedicalCenter = medicalCenter;
    }
}
