using DAL.Entities;
using DAL.GenericRepository;

namespace DAL.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
    }
}
