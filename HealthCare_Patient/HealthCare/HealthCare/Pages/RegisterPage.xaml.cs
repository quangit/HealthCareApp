﻿using HealthCare.Controls;
using HealthCare.ViewModels;

namespace HealthCare.Pages
{
    public partial class RegisterPage : TopBarContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            UserViewModel.Instance.HandleHardwareBackButton();
            return true;
        }
    }
}