using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using HealthCare.Core.Resources;
using HealthCare.Core.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace HealthCare.Phone.Views
{
    public partial class SetPasswordView
    {
        public SetPasswordView()
        {
            InitializeComponent();
            this.Loaded += SetPasswordView_Loaded;
        }

        private void SetPasswordView_Loaded(object sender, RoutedEventArgs e)
        {
            (this.ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.SetPasswrod_Change;
            (this.ApplicationBar.Buttons[0] as ApplicationBarIconButton).Click += SetPasswordView_Click;
            //this.ApplicationBar.Buttons.Add(new ApplicationBarIconButton())
        }

        private void SetPasswordView_Click(object sender, EventArgs e)
        {
            ((SetPasswordViewModel)ViewModel).SetPasswordCommand.Execute();
        }

        private void CallSupportButton(object sender, RoutedEventArgs e)
        {
            HomeView.CallSupport();
        }
    }
}