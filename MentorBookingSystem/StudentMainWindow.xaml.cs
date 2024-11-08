using BLL;
using DAL;
using DAL.UnitOfWork;
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
            DateBlock.Text = App.CurrentUser?.Mail;
            dayTextBlocks = new List<TextBlock> { Day1, Day2, Day3, Day4, Day5, Day6, Day7 };
            
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
            SetSchedulePanel();
        }

        private void SetSchedulePanel()
        {

            DateOnly startDate = Today.AddDays(7 * (schedulePage-1));

            for (int i = 0; i < dayTextBlocks.Count; i++)
            {
                DateOnly date = startDate.AddDays(i);

                dayTextBlocks[i].Text = $"{date.DayOfWeek.ToString().Substring(0, 3)} {date.Day:D2}";
            }

            day1Btns = new List<Tuple<Button, DateOnly>>
    {
        CreateDayButton(Day1_1, startDate),
        CreateDayButton(Day1_2, startDate),
        CreateDayButton(Day1_3, startDate),
        CreateDayButton(Day1_4, startDate)
    };

            // Day 2
            day2Btns = new List<Tuple<Button, DateOnly>>
    {
        CreateDayButton(Day2_1, startDate.AddDays(1)),
        CreateDayButton(Day2_2, startDate.AddDays(1)),
        CreateDayButton(Day2_3, startDate.AddDays(1)),
        CreateDayButton(Day2_4, startDate.AddDays(1))
    };

            // Day 3
            day3Btns = new List<Tuple<Button, DateOnly>>
    {
        CreateDayButton(Day3_1, startDate.AddDays(2)),
        CreateDayButton(Day3_2, startDate.AddDays(2)),
        CreateDayButton(Day3_3, startDate.AddDays(2)),
        CreateDayButton(Day3_4, startDate.AddDays(2))
    };

            // Day 4
            day4Btns = new List<Tuple<Button, DateOnly>>
    {
        CreateDayButton(Day4_1, startDate.AddDays(3)),
        CreateDayButton(Day4_2, startDate.AddDays(3)),
        CreateDayButton(Day4_3, startDate.AddDays(3)),
        CreateDayButton(Day4_4, startDate.AddDays(3))
    };

            // Day 5
            day5Btns = new List<Tuple<Button, DateOnly>>
    {
        CreateDayButton(Day5_1, startDate.AddDays(4)),
        CreateDayButton(Day5_2, startDate.AddDays(4)),
        CreateDayButton(Day5_3, startDate.AddDays(4)),
        CreateDayButton(Day5_4, startDate.AddDays(4))
    };

            // Day 6
            day6Btns = new List<Tuple<Button, DateOnly>>
    {
        CreateDayButton(Day6_1, startDate.AddDays(5)),
        CreateDayButton(Day6_2, startDate.AddDays(5)),
        CreateDayButton(Day6_3, startDate.AddDays(5)),
        CreateDayButton(Day6_4, startDate.AddDays(5))
    };

            // Day 7
            day7Btns = new List<Tuple<Button, DateOnly>>
    {
        CreateDayButton(Day7_1, startDate.AddDays(6)),
        CreateDayButton(Day7_2, startDate.AddDays(6)),
        CreateDayButton(Day7_3, startDate.AddDays(6)),
        CreateDayButton(Day7_4, startDate.AddDays(6))
    };
        }

        private Tuple<Button, DateOnly> CreateDayButton(Button button, DateOnly date)
        {
            button.Tag = date;
            button.Click += DayButton_Click;
            return Tuple.Create(button, date);
        }
        private void DayButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is DateOnly selectedDate)
            {
                SlotService slotService = new SlotService(new UnitOfWork());

                // Mở cửa sổ SlotWindow với ngày được chọn
                SlotWindow slotWindow = new SlotWindow(selectedDate, slotService);
                slotWindow.Show();
            }
        }

    }
}