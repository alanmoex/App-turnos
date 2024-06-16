namespace Domain.Entities;

public class SysAdmin : User
{
    // Constructor sin parámetros necesario para EF
    public SysAdmin()
    {
    }
    public SysAdmin(int id, string name, string email, string password)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
    }
}
