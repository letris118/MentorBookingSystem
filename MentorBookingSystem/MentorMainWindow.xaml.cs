using BLL;
using DAL.UnitOfWork;
using DAL;
using System.Windows;
using DAL.Entities;
using System.Windows.Controls;
using System.Globalization;
using System.Windows.Media;

namespace MentorBookingSystem
{
    /// <summary>
    /// Interaction logic for MentorMainWindow.xaml
    /// </summary>
    public partial class MentorMainWindow : Window
    {
        private readonly UserService _userService = new(new UnitOfWork(new MentorBookingSystemDbContext()));

        private readonly SlotService _slotService = new(new UnitOfWork(new MentorBookingSystemDbContext()));

        private DateOnly Today = DateOnly.FromDateTime(DateTime.Today);
        private List<Slot>? allSlots;
        private int schedulePage = 1;
        private List<TextBlock> dayTextBlocks;
        private List<Tuple<Button, DateOnly>>? day1Btns;
        private List<Tuple<Button, DateOnly>>? day2Btns;
        private List<Tuple<Button, DateOnly>>? day3Btns;
        private List<Tuple<Button, DateOnly>>? day4Btns;
        private List<Tuple<Button, DateOnly>>? day5Btns;
        private List<Tuple<Button, DateOnly>>? day6Btns;
        private List<Tuple<Button, DateOnly>>? day7Btns;

        public MentorMainWindow()
        {
            InitializeComponent();
            App.CurrentUser = _userService.GetTemporMentor();
            NameTxt.Text = App.CurrentUser!.Name;
            dayTextBlocks = [Day1, Day2, Day3, Day4, Day5, Day6, Day7];
            SetSchedulePanel();
        }

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

        private void SetSchedulePanel()
        {
            allSlots = _slotService.GetAllSlots();

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
                    foreach (var slot in allSlots!)
                    {
                        // Check if dates match and button position corresponds to the duration
                        if (buttonDate == slot.Date && dayBtns.IndexOf(buttonTuple) + 1 == slot.Duration)
                        {
                            // Set content to "✔️" if there a match and status = 1 = available
                            if (slot.Status == 1 && slot.MentorId == App.CurrentUser!.Id)
                            {
                                button.Content = "✔️";
                                button.Foreground = Brushes.Green;
                            }
                            else if (slot.Status == 2 && slot.MentorId == App.CurrentUser!.Id)
                            {
                                button.Content = "⚫";
                                button.Foreground = Brushes.Blue;
                            }
                            break;
                        }
                    }
                }
            }

        }
        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
            MentorLoginWindow mentorLoginWindow = new();
            this.Close();
            mentorLoginWindow.Show();
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
            DateOnly date = allDayBtns
                .SelectMany(list => list) // Flatten the list of lists into a single list of tuples
                .Where(tuple => tuple.Item1 == button) // Find the tuple where the button matches
                .Select(tuple => tuple.Item2) // Select the DateOnly value
                .FirstOrDefault();
            foreach (var slot in allSlots!)
            {
                //TH 1: Mentor click vào slot available của mình 

                if (slot.Status == 1 && slot.MentorId == App.CurrentUser!.Id && duration == slot.Duration && date == slot.Date)
                {
                    Slot? slotSelected = _slotService.GetSelectedSlot(date, duration, App.CurrentUser!.Id);
                    if (slotSelected is null)
                    {
                        MessageBox.Show("Null");
                        return;
                    }
                    _slotService.ShiftToNon(slotSelected);
                    SetSchedulePanel();
                    return;
                }

                //TH 2:Mentor click vào slot đã được đặt thành công của mình
                if (slot.Status == 2 && slot.MentorId == App.CurrentUser!.Id && duration == slot.Duration && date == slot.Date)
                {
                    SlotInformationDialog slotInformationDialog = new();
                    slotInformationDialog.MentorTxt.Text = slot.Mentor!.Name;
                    slotInformationDialog.StudentTxt.Text = slot.Student!.Name;

                    slotInformationDialog.TimeTxt.Text = slot.Duration switch
                    {
                        1 => "07:00 - 09:15, ngày ",
                        2 => "09:30 - 11:45, ngày ",
                        3 => "12:30 - 14:45, ngày ",
                        4 => "15:00 - 17:15, ngày ",
                        _ => "Unknown "
                    };

                    slotInformationDialog.TimeTxt.Text += date.ToString("dd-MM-yyyy");
                    slotInformationDialog.ShowDialog();
                    SetSchedulePanel();
                    return;
                }

            }

            //TH 3: Mentor click vào một ô mới
            Slot newSlot = new()
            {
                Duration = duration,
                Date = date,
                MentorId = App.CurrentUser!.Id,
                Status = 1
            };
            _slotService.ShiftToAvailable(newSlot);
            SetSchedulePanel();
        }
    }
}
