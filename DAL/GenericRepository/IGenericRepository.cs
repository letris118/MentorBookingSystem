namespace DAL.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        //queryable
        IQueryable<T> Entities { get; }

        //void
        T? GetById(object id);
        IEnumerable<T> GetAll();
        void Insert(T obj);
        void Delete(T entity);
        void Update(T entity);
        void Save();

        //Task
        Task<T?> GetByIdAsync(object id);
        Task<IList<T>> GetAllAsync();
        Task InsertAsync(T obj);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
        Task SaveAsync();

    }
}
