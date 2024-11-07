using BLL;
using DAL;
using DAL.Entities;
using DAL.UnitOfWork;
using MentorBookingSystemNew;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace MentorBookingSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StudentMainWindow : Window
    {
        private readonly UserService _userService = new(new UnitOfWork());
        private readonly SlotService _slotService = new(new UnitOfWork());

        public static DateOnly Today { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        public static List<Slot>? AllSlots { get; set; }
        private int schedulePage = 1;
        private List<TextBlock> dayTextBlocks;
        private List<Tuple<Button, DateOnly>>? day1Btns;
        private List<Tuple<Button, DateOnly>>? day2Btns;
        private List<Tuple<Button, DateOnly>>? day3Btns;
        private List<Tuple<Button, DateOnly>>? day4Btns;
        private List<Tuple<Button, DateOnly>>? day5Btns;
        private List<Tuple<Button, DateOnly>>? day6Btns;
        private List<Tuple<Button, DateOnly>>? day7Btns;

        public StudentMainWindow()
        {
            InitializeComponent();
            App.CurrentUser = _userService.GetUser();
            AllSlots = _slotService.GetAllSlots();
            dayTextBlocks = [Day1, Day2, Day3, Day4, Day5, Day6, Day7];

            Refresh();
        }

        MentorBookingSystemDbContext context = new();

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            if (schedulePage >= 3)
            {
                schedulePage = 3;
                return;
            }
            schedulePage++;
            if (schedulePage == 2)
            {
                BtnNext.IsEnabled = true;
                BtnPrevious.IsEnabled = true;
            }
            if (schedulePage == 3)
            {
                BtnNext.IsEnabled = false;
                BtnPrevious.IsEnabled = true;
            }
            SetSchedulePanel();
        }

        private void BtnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (schedulePage <= 1)
            {
                schedulePage = 1;
                return;
            }
            schedulePage--;
            if (schedulePage == 2)
            {
                BtnNext.IsEnabled = true;
                BtnPrevious.IsEnabled = true;
            }
            if (schedulePage == 1)
            {
                BtnNext.IsEnabled = true;
                BtnPrevious.IsEnabled = false;
            }

            SetSchedulePanel();
        }

        private void Refresh()
        {
            NameTxt.Text = App.CurrentUser!.Name;
            WalletTxt.Text = ((double)(App.CurrentUser.Wallet!)).ToString("#,0.##", CultureInfo.InvariantCulture) + " 💎";
            SetSchedulePanel();
        }

        private void SetSchedulePanel()
        {

            DateOnly startDate = Today.AddDays(7 * (schedulePage - 1));

            for (int i = 0; i < dayTextBlocks.Count; i++)
            {
                DateOnly date = startDate.AddDays(i);

                dayTextBlocks[i].Text = $"{date.DayOfWeek.ToString().Substring(0, 3)} {date.Day:D2}";
            }

            day1Btns = new()
            {
                Tuple.Create(Day1_1, startDate),
                Tuple.Create(Day1_2, startDate),
                Tuple.Create(Day1_3, startDate),
                Tuple.Create(Day1_4, startDate)
            };
            day2Btns = new()
            {
                Tuple.Create(Day2_1, startDate.AddDays(1)),
                Tuple.Create(Day2_2, startDate.AddDays(1)),
                Tuple.Create(Day2_3, startDate.AddDays(1)),
                Tuple.Create(Day2_4, startDate.AddDays(1))
            };

            day3Btns = new()
            {
                Tuple.Create(Day3_1, startDate.AddDays(2)),
                Tuple.Create(Day3_2, startDate.AddDays(2)),
                Tuple.Create(Day3_3, startDate.AddDays(2)),
                Tuple.Create(Day3_4, startDate.AddDays(2))
            };

            day4Btns = new()
            {
                Tuple.Create(Day4_1, startDate.AddDays(3)),
                Tuple.Create(Day4_2, startDate.AddDays(3)),
                Tuple.Create(Day4_3, startDate.AddDays(3)),
                Tuple.Create(Day4_4, startDate.AddDays(3))
            };

            day5Btns = new()
            {
                Tuple.Create(Day5_1, startDate.AddDays(4)),
                Tuple.Create(Day5_2, startDate.AddDays(4)),
                Tuple.Create(Day5_3, startDate.AddDays(4)),
                Tuple.Create(Day5_4, startDate.AddDays(4))
            };

            day6Btns = new()
            {
                Tuple.Create(Day6_1, startDate.AddDays(5)),
                Tuple.Create(Day6_2, startDate.AddDays(5)),
                Tuple.Create(Day6_3, startDate.AddDays(5)),
                Tuple.Create(Day6_4, startDate.AddDays(5))
            };

            day7Btns = new()
            {
                Tuple.Create(Day7_1, startDate.AddDays(6)),
                Tuple.Create(Day7_2, startDate.AddDays(6)),
                Tuple.Create(Day7_3, startDate.AddDays(6)),
                Tuple.Create(Day7_4, startDate.AddDays(6))
            };

            List<List<Tuple<Button, DateOnly>>> allDayBtns = new()
            {
                day1Btns, day2Btns, day3Btns, day4Btns, day5Btns, day6Btns, day7Btns
            };

            // Iterate over each day's button list
            for (int dayIndex = 0; dayIndex < allDayBtns.Count; dayIndex++)
            {
                var dayBtns = allDayBtns[dayIndex];

                // Iterate over each button tuple in the day's list
                foreach (var buttonTuple in dayBtns)
                {
                    var button = buttonTuple.Item1;
                    var buttonDate = buttonTuple.Item2;

                    // Set initial content to "O"
                    button.Content = "";

                    // Check each slot in StudentSlots for a match
                    foreach (var slot in AllSlots!)
                    {
                        // Check if dates match and button position corresponds to the duration
                        if (buttonDate == slot.Date && dayBtns.IndexOf(buttonTuple) + 1 == slot.Duration)
                        {
                            // Set content to "X" if there's a match and break out of the loop
                            if (slot.Status == 1)
                                button.Content = "Available";
                            else if (slot.StudentId == App.CurrentUser!.Id)
                                button.Content = "✅";
                            break;
                        }
                    }
                }
            }

        }

        private void WalletBtn_Click(object sender, RoutedEventArgs e)
        {
            WalletWindow walletWindow = new();
            this.Close();
            walletWindow.Show();
        }

        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
            StudentLoginWindow studentLoginWindow = new();
            this.Close();
            studentLoginWindow.Show();
        }

        private void SlotButton_Click(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;
            if (button is null)
            {
                return;
            }

            List<List<Tuple<Button, DateOnly>>> allDayBtns = new()
            {
                day1Btns!, day2Btns!, day3Btns!, day4Btns!, day5Btns!, day6Btns!, day7Btns!
            };
            // Đầu tiên tìm ngày và slot của button đó]]'
            int duration = int.Parse(button.Tag.ToString()!);
            DateOnly? date = allDayBtns
                .SelectMany(list => list) // Flatten the list of lists into a single list of tuples
                .Where(tuple => tuple.Item1 == button) // Find the tuple where the button matches
                .Select(tuple => tuple.Item2) // Select the DateOnly value
                .FirstOrDefault();
            foreach (var slot in AllSlots!)
            {
                //TH 1: Học sinh click vào ngày trống và có set (ngày có trong mentorSlots)

                if (slot.Status == 1 && duration == slot.Duration && date == slot.Date)
                {
                    Test.Text = "Giáo viên có set lịch nhưng chưa ai đặt";
                    return;
                }

                //TH 2:Học sinh click vào ngày đã được đặt rồi (ngày có nằm trong All slot của mình) => Hiển thị bảng thông tin buổi gặp
                if (slot.Status == 2 && slot.StudentId == App.CurrentUser!.Id && duration == slot.Duration && date == slot.Date)
                {
                    SlotInformationDialog slotInformationDialog = new();
                    slotInformationDialog.MentorTxt.Text = slot.Mentor!.Name;

                    slotInformationDialog.TimeTxt.Text = slot.Duration switch
                    {
                        1 => "07:00 - 09:15, ngày ",
                        2 => "09:30 - 11:45, ngày ",
                        3 => "12:30 - 14:45, ngày ",
                        4 => "15:00 - 17:15, ngày ",
                        _ => "Unknown "
                    };

                    slotInformationDialog.TimeTxt.Text += ((DateOnly)date).ToString("dd-MM-yyyy");
                    slotInformationDialog.ShowDialog();
                }

            }
        }
    }
}