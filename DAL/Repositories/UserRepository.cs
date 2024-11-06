using DAL.Entities;
using DAL.GenericRepository;
using DAL.IRepositories;

namespace DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly MentorBookingSystemDbContext _context;

        public UserRepository(MentorBookingSystemDbContext context) : base(context)
        {
            _context = context;

        }
    }
}
