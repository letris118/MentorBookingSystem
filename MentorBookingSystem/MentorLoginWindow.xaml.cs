﻿using BLL;
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

            string mail = MailInput.Text;
            string password = PassInput.Password;

            if (string.IsNullOrWhiteSpace(mail) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập mail và password");
                return;
            }

            User? account = _userService.GetUser(mail, password);
            if (account == null || (account.Role != 1 && account.Role != 2))
            {
                MessageBox.Show("Đăng nhập thất bại!");
                return;
            }
            App.CurrentUser = account;
            MentorMainWindow mentorMainWindow = new MentorMainWindow();
            mentorMainWindow.Show();
            this.Close();
            mentorMainWindow.Show();

            //string name = MailInput.Text;
            //string password = PassInput.Password;

            //User? account = _userService.GetUser(name, password);
            //if (account == null)
            //{
            //    MessageBox.Show("Error UserName or Password");
            //    return;
            //}
            //if (account.Role == 3)
            //{
            //    MessageBox.Show("Error. please use Mentor account or Admin account");
            //    return;
            //}
            //App.CurrentUser = account;
            //StudentMainWindow StudentmainWindow = new StudentMainWindow();
            //StudentmainWindow.Show();
            //this.Close();

        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string email = MailInput.Text;
            string password = PassInput.Password;
            string name = FullNameInput.Text;
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password)||string.IsNullOrWhiteSpace(name) )
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
                Role = 2,
                Wallet = 0
            };
            _userService.RegisterUser(newUser);

            MessageBox.Show("Đăng ký tài khoản thành công!");
        }
    

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void SwitchButton_Click(object sender, RoutedEventArgs e)
        {
            StudentLoginWindow studentWindow = new StudentLoginWindow();
            studentWindow.Show();
            this.Close();
        }
    }
}
