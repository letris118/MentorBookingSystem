using BLL;
using DAL;
using DAL.Entities;
using DAL.UnitOfWork;
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

        private void SwitchButton_Click(object sender, RoutedEventArgs e)
        {
            MentorLoginWindow mentorWindow = new MentorLoginWindow();
            mentorWindow.Show();
            this.Close();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string email = MailInput.Text;
            string password = PassInput.Password;
            string name = FullNameInput.Text;
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ mail, password và họ tên");
                return;
            }

            if (!_userService.IsUniqueMail(email))
            {
                MessageBox.Show("Email đã tồn tại trong hệ thống");
                return;
            }

            User newUser = new()
            {
                Name = name,
                Mail = email,
                Password = password,
                Role = 3,
                Wallet = 0
            };
            _userService.RegisterUser(newUser);

            MessageBox.Show("Đăng ký tài khoản thành công!");
        }


        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            string mail = MailInput.Text;
            string password = PassInput.Password;

            if (string.IsNullOrWhiteSpace(mail) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập mail và password");
                return;
            }

            User? account = _userService.GetUser(mail, password);
            if (account == null || (account.Role != 1 && account.Role != 3))
            {
                MessageBox.Show("Đăng nhập thất bại!");
                return;
            }
            App.CurrentUser = account;
            StudentMainWindow StudentmainWindow = new StudentMainWindow();
            StudentmainWindow.Show();
            this.Close();
        }
    }
}
