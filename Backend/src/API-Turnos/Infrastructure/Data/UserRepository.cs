using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Data
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) :base(context) 
        {
        }

        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}
