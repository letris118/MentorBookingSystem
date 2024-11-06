using DAL.Entities;
using DAL.GenericRepository;
using DAL.IRepositories;

namespace DAL.Repositories
{
    public class SlotRepository : GenericRepository<Slot>, ISlotRepository
    {
        private readonly MentorBookingSystemDbContext _context;

        public SlotRepository(MentorBookingSystemDbContext context) : base(context)
        {
            _context = context;

        }
    }
}
