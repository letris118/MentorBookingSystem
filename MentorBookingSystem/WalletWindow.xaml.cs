using BLL;
using DAL;
using DAL.UnitOfWork;
using System.Windows;
using System.Windows.Controls;

namespace MentorBookingSystem
{
    /// <summary>
    /// Interaction logic for WalletWindow.xaml
    /// </summary>
    public partial class WalletWindow : Window
    {
        private readonly UserService _userService = new(new UnitOfWork(new MentorBookingSystemDbContext()));
        private readonly SlotService _slotService = new(new UnitOfWork(new MentorBookingSystemDbContext()));
        public WalletWindow()
        {

            InitializeComponent();
        }

        private void RechargeButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            double amount = Convert.ToDouble(button.Tag);

            // Hiển thị hộp thoại xác nhận
            var result = MessageBox.Show($"Bạn có chắc chắn muốn nạp {amount} VND không?", "Xác nhận nạp tiền", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Lấy thông tin người dùng hiện tại

                if (App.CurrentUser != null)
                {
                    try
                    {
                        // Nạp tiền vào ví người dùng
                        _userService.RechargeWallet(App.CurrentUser, amount);

                        // Thông báo nạp tiền thành công
                        MessageBox.Show($"Đã nạp {amount} VND vào tài khoản của bạn.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Hiển thị nút chuyển sang StudentMainWindow
                        GoToStudentMainWindowButton.Visibility = Visibility.Visible; // Hiển thị nút chuyển

                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi nếu có
                        MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin người dùng. Vui lòng đăng nhập lại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // Người dùng chọn "Không", quay lại WalletWindow
                MessageBox.Show("Hủy nạp tiền.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        // Sự kiện khi nhấn nút chuyển đến StudentMainWindow
        private void GoToStudentMainWindowButton_Click(object sender, RoutedEventArgs e)
        {
            var studentMainWindow = new StudentMainWindow();
            studentMainWindow.Show(); // Hiển thị cửa sổ StudentMainWindow

            // Đặt cửa sổ chính của ứng dụng
            App.Current.MainWindow = studentMainWindow;

            // Đóng cửa sổ WalletWindow hiện tại
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
