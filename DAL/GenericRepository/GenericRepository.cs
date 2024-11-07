using Microsoft.EntityFrameworkCore;

namespace DAL.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MentorBookingSystemDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(MentorBookingSystemDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> Entities => _context.Set<T>();

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await Task.CompletedTask;
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public T? GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Insert(T obj)
        {
            _dbSet.Add(obj);
        }

        public async Task InsertAsync(T obj)
        {
            await _dbSet.AddAsync(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Update(T obj)
        {
            _dbSet.Update(obj);
        }

        public async Task UpdateAsync(T obj)
        {
            _dbSet.Update(obj);
            await Task.CompletedTask;
        }

    }
}
