using DAL.GenericRepository;

namespace DAL.UnitOfWork
{
    public class UnitOfWork(MentorBookingSystemDbContext context) : IUnitOfWork
    {
        private bool disposed = false;
        private MentorBookingSystemDbContext _context = context;

        protected virtual void Dispose(bool disposing)
        {

        
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
    
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
         
            return new GenericRepository<T>(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}