namespace Domain.Entities;

public class SysAdmin : User
{
    public SysAdmin(int id, string name, string email, string password)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
    }
}
