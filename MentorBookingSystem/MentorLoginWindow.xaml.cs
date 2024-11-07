using BLL;
using DAL;
using DAL.Entities;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MentorBookingSystem
{
    /// <summary>
    /// Interaction logic for MentorLoginWindow.xaml
    /// </summary>
    public partial class MentorLoginWindow : Window
    {
        private readonly UserService _userService;
        public MentorLoginWindow()
        {
            InitializeComponent();
            
            _userService = new UserService(new UnitOfWork(new MentorBookingSystemDbContext()));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StudentLoginWindow studenLogin = new StudentLoginWindow();
            studenLogin.Show();
            this.Close();
        }


        

    

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string name = MailInput.Text;
            string password = PassInput.Text;

            User? account = _userService.GetUser(name, password);
            if (account == null)
            {
                MessageBox.Show("Error UserName or Password");
                return;
            }
            if (account.Role == 3)
            {
                MessageBox.Show("Error. please use Mentor account or Admin account");
                return;
            }
            App.CurrentUser = account;
            StudentMainWindow StudentmainWindow = new StudentMainWindow();
            StudentmainWindow.Show();
            this.Close();

        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string email = MailInput.Text;
            string password = PassInput.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("You must fill in all fields");
                return;
            }

            try
            {
                User newUser = new()
                {
                    Mail = email,
                    Password = password,
                    Role = 2
                };
                _userService.RegisterUser(newUser);

                MessageBox.Show("Đăng ký thành công, mời bạn đăng nhập.");
                MailInput.Text = "";
                PassInput.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Registration failed: {ex.Message}");
            }
        }
    

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
