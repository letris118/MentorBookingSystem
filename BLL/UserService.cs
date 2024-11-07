using DAL.Entities;
using DAL.UnitOfWork;
using DAL.Repositories;
using System.Net.Mail;
using System.Data;

namespace BLL
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
 

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void RegisterUser(User user )
        {
            try
            {
                // Check if user with the same email exists
                User? existingUser = _unitOfWork.GetRepository<User>()
                    .Entities
                    .FirstOrDefault(u => u.Mail == user.Mail);

                if (existingUser != null)
                {
                    throw new Exception("A user with the same email already exists.");
                }

                // Create new user object
            

                // Attempt to insert user and save changes
                _unitOfWork.GetRepository<User>().Insert(user);
                _unitOfWork.Save();

                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to register user: {ex.Message}");
                throw new Exception($"Failed to register user: {ex.Message}");
            }
        }
        
        public User? GetFirstUser()
        {
            return _unitOfWork.GetRepository<User>().Entities.FirstOrDefault();
        }

        public User? GetUser(string mail, string password)
        {
            if (string.IsNullOrWhiteSpace(mail) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Email and Password can't null");
            }
            try
            {
                var mailAddress = new MailAddress(mail);
            }
            catch (FormatException)
            {
                throw new ArgumentException("Invalid email format ");
            }
            User? user = _unitOfWork.GetRepository<User>()
               .Entities
               .Where(u => u.Mail == mail && u.Password == password)
            .FirstOrDefault();

            return user;
        }
    
    }

}
