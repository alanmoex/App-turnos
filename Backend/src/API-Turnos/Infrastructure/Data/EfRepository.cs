
namespace Infrastructure.Data
{
    public class EfRepository<T> : BaseRepository<T> where T : class
    {
        protected readonly ApplicationContext _context;
        public EfRepository(ApplicationContext context) : base(context) 
        {
            _context = context;
        }
    }
}
