using BLL;
using DAL;
using DAL.Entities;
using DAL.UnitOfWork;
using MentorBookingSystem;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.Logging;
using System.Windows;

namespace MentorBookingSystem
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class StudentLoginWindow : Window
    {
        private readonly UserService _userService;

        public StudentLoginWindow()
        {
            InitializeComponent();

            _userService = new UserService(new UnitOfWork(new MentorBookingSystemDbContext()));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MentorLoginWindow mentorWindow = new MentorLoginWindow();
            mentorWindow.Show();
            this.Close();
        }
        private void quitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            string email = MailInput.Text;
            string password = PassInput.Text;

            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Email and password cannot be empty");
                    return;
                }
                User newUser = new()
                {
                    Mail = email,
                    Password = password,
                    Role = 3,
                    Wallet = 0
                };
                _userService.RegisterUser(newUser);

                MessageBox.Show("Đăng ký thành công, mời bạn đăng nhập.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Registration error: {ex.ToString()}");
                MessageBox.Show($"Registration failed: {ex.Message}");
            }
        }


        private void loginbuttonStudent_Click(object sender, RoutedEventArgs e)
        {

            string name = MailInput.Text;
            string password = PassInput.Text;

            User? account = _userService.GetUser(name, password);
            if (account == null)
            {
                MessageBox.Show("Error UserName or Password");
                return;
            }
            if (account.Role == 2)
            {
                MessageBox.Show("Error. please use Mentor account or Admin account");
                return;
            }
            App.CurrentUser = account;
            StudentMainWindow StudentmainWindow = new StudentMainWindow();
            StudentmainWindow.Show();
            this.Close();

        }
    }
}
