using DAL.Entities;
using DAL.UnitOfWork;
using System.Net.Mail;

namespace BLL
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public User? GetUser()
        { 
            return _unitOfWork.GetRepository<User>().Entities.FirstOrDefault();
        }

        public User GetUser(string mail, string password) 
        {
            User? user = _unitOfWork.GetRepository<User>()
                .Entities
                .Where(u => u.Mail == mail && u.Password == password)
                .FirstOrDefault();

            return user;
        }


    }
}
