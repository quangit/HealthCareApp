﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace HealthCare.Phone.Views
{
    public partial class CheckupView
    {
        public CheckupView()
        {
            InitializeComponent();
        }

        private void CallSupportButton(object sender, RoutedEventArgs e)
        {
            HomeView.CallSupport();
        }
    }
}