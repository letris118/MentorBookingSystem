using BLL;
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
    /// Interaction logic for SlotWindow.xaml
    /// </summary>
    public partial class SlotWindow : Window
    {
        private readonly SlotService _slotService;
        public DateOnly SelectedDate { get; set; }

        public SlotWindow(DateOnly selectedDate, SlotService slotService)
        {
            InitializeComponent();
            _slotService = slotService;
            SelectedDate = selectedDate;
            DateBlock.Text = $"{SelectedDate:dddd, dd/MM/yyyy}";
            LoadSlots();
        }

        private void LoadSlots()
        {       
            var slots = _slotService.GetSlotsByDate(SelectedDate);      
            SlotsDataGrid.ItemsSource = slots;
        }
    }
}