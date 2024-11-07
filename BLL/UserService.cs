using DAL.Entities;
using DAL.UnitOfWork;
using DAL.Repositories;
using System.Data;
using DAL;

namespace BLL
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;


        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void RegisterUser(User user)
        {
            _unitOfWork.GetRepository<User>().Insert(user);
            _unitOfWork.Save();
        }

        public bool IsUniqueMail(string mail)
        {
            User? user = _unitOfWork.GetRepository<User>().Entities
                .Where(u => u.Mail == mail)
                .FirstOrDefault();

            return user is null;
        }

        public User? GetUser(string mail, string password)
        {
            User? user = _unitOfWork.GetRepository<User>()
               .Entities
               .Where(u => u.Mail == mail && u.Password == password)
               .FirstOrDefault();

            return user;
        }

        public void RechargeWallet(User student, double amount)
        {
            student.Wallet += amount;
            //student.Wallet = student.Wallet + amount;
            _unitOfWork.Save();
            
        }

    }

}
