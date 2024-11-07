using DAL.Entities;
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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void StudentMainWindowButton_Click(object sender, RoutedEventArgs e)
        {
            // Điều hướng đến StudentMainWindow
            var studentWindow = new StudentMainWindow();
            studentWindow.Show();
        }

        private void MentorMainWindowButton_Click(object sender, RoutedEventArgs e)
        {
            // Điều hướng đến MentorMainWindow
            var mentorWindow = new MentorMainWindow();
            mentorWindow.Show();
        }

        private void SlotWindowButton_Click(object sender, RoutedEventArgs e)
        {
            // Điều hướng đến SlotWindow
            var slotWindow = new SlotWindow();
            slotWindow.Show();
        }
        private void WalletWindowButton_Click(object sender, RoutedEventArgs e)
        {
            // Điều hướng đến WalletWindow
            var walletWindow = new WalletWindow();
            walletWindow.Show();
        }
    }
}
