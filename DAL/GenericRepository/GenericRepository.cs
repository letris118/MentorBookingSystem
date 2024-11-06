using Microsoft.EntityFrameworkCore;

namespace DAL.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MentorBookingSystemDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(MentorBookingSystemDbContext dbContext)
        {
            _context = dbContext;
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> Entities => _context.Set<T>();

        public void Delete(object entity) => _dbSet.Remove((T)entity);

        public async Task DeleteAsync(object entity)
        {
            _dbSet.Remove((T)entity);
            await Task.CompletedTask;
        }

        public IEnumerable<T> GetAll() => _dbSet.ToList();

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        

        public T? GetById(object id) => _dbSet.Find(id);

        public async Task<T?> GetByIdAsync(object id) => await _dbSet.FindAsync(id);

        public void Insert(T obj) => _dbSet.Add(obj);

        public async Task InsertAsync(T obj) => await _dbSet.AddAsync(obj);

        public void Save() => _context.SaveChanges();

        public async Task SaveAsync() => await _context.SaveChangesAsync();

    }
}
