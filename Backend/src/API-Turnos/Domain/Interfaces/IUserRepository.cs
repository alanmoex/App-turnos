using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);
    }
}
