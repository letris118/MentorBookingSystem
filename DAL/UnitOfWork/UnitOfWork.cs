using DAL.GenericRepository;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private MentorBookingSystemDbContext _context = new();

        protected virtual void Dispose(bool disposing)
        {

            _context = new();
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
            _context = new();
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            _context = new();
            return new GenericRepository<T>(_context);
        }

        public void Save()
        {
            _context = new();
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            _context = new();
            await _context.SaveChangesAsync();
        }
    }
}
