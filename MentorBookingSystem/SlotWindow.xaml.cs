using BLL;
using DAL.UnitOfWork;
using DAL;
using System.Windows;
using DAL.Entities;
using Microsoft.Identity.Client.NativeInterop;

namespace MentorBookingSystem
{
    /// <summary>
    /// Interaction logic for SlotWindow.xaml
    /// </summary>
    public partial class SlotWindow : Window
    {
        private readonly UserService _userService = new(new UnitOfWork(new MentorBookingSystemDbContext()));
        private readonly SlotService _slotService = new(new UnitOfWork(new MentorBookingSystemDbContext()));
        public static DateOnly SelectedDate;
        public static int SelectedDuration;
        private List<Slot> allSlots;
        private List<User> mentorsOfTheSlot = [];
        public SlotWindow()
        {
            InitializeComponent();
            allSlots = _slotService.GetAllSlots();
            foreach (var slot in allSlots)
            {
                if (slot.Date == SelectedDate && slot.Duration == SelectedDuration)
                {
                    mentorsOfTheSlot.Add(slot.Mentor!);
                }
            }

            MentorDataGrid.ItemsSource = mentorsOfTheSlot;

        }

        private void SelectBtn_Click(object sender, RoutedEventArgs e)
        {
            User? selectedMentor = MentorDataGrid.SelectedItem as User;
            if (selectedMentor is null)
            {
                return;
            }

            var result = MessageBox.Show($"Xác nhận mentor. Phí dịch vụ 50,000 💎", "Xác nhận dịch vụ", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes) 
            {
                _slotService.ShiftToSuccess(SelectedDate, SelectedDuration, selectedMentor.Id, App.CurrentUser!);

                StudentMainWindow studentMainWindow = new();
                
                this.Close();
                studentMainWindow.Show();
            }

            else if (result == MessageBoxResult.No)
            {
                return;
            }

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
