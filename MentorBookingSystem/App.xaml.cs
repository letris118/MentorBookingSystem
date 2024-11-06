﻿using DAL.Entities;
using System.Configuration;
using System.Data;
using System.Windows;

namespace MentorBookingSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static User? CurrentUser { get; set; }
    }

}
