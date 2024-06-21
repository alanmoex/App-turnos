namespace Domain.Entities;

public class SysAdmin : User
{
    // Constructor sin parámetros necesario para EF
    public SysAdmin()
    {
    }
    public SysAdmin(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
}
