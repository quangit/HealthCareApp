using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Windows.System;
using HealthCare.Core.Resources;
using HealthCare.Core.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace HealthCare.Phone.Views
{
    public partial class ConsultView
    {
        public ConsultView()
        {
            InitializeComponent();
            this.Loaded += ConsultView_Loaded;
        }

        private void ConsultView_Loaded(object sender, RoutedEventArgs e)
        {
            this.ApplicationBar.BackgroundColor = (Color)App.Current.Resources["MainColor"];
            this.ApplicationBar.ForegroundColor = Colors.White;
            (ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.ConsultView_Invite.ToLower();
            (ApplicationBar.Buttons[1] as ApplicationBarIconButton).Text = AppResources.ConsultView_Reply.ToLower();

        }

        private void CallSupportButton(object sender, RoutedEventArgs e)
        {
            HomeView.CallSupport();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            (ApplicationBar.Buttons[0] as ApplicationBarIconButton).IsEnabled = ((ConsultViewModel)DataContext).Request.CanReply;
            (ApplicationBar.Buttons[1] as ApplicationBarIconButton).IsEnabled = ((ConsultViewModel)DataContext).Request.CanReply;
            (ApplicationBar.Buttons[2] as ApplicationBarIconButton).IsEnabled =
                !string.IsNullOrEmpty(((ConsultViewModel) DataContext).Request?.PatientSkypeUrl);

            ((ConsultViewModel)DataContext).MesssageSent += ConsultView_MesssageSent;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            windowReply.IsOpen = window.IsOpen = false;
        }



        private void ConsultView_MesssageSent(object sender, EventArgs e)
        {
            window.IsOpen = false;
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            // (sender as PhoneTextBox).Focus();
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            window.IsOpen = true;
        }

        private void ApplicationBarIconButtonReply_Click(object sender, EventArgs e)
        {
            windowReply.IsOpen = true;
        }

        private void ImageTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                ImageView.ImageUrl = ((ConsultViewModel)DataContext).Request.LandscapeImage;
                this.NavigationService.Navigate(new Uri("/Views/ImageView.xaml", UriKind.Relative));
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void FrameworkLoaded(object sender, RoutedEventArgs e)
        {
            
            var size = this.ActualWidth;
            if (size > 0 && !double.IsInfinity(size))
            {
                img.Height = size*9/16;
            }
        }

        private async void ApplicationBarIconButtonSkype_Click(object sender, EventArgs e)
        {
            var vm = ((ConsultViewModel) DataContext);
            await Launcher.LaunchUriAsync(new Uri(vm.Request.PatientSkypeUrl));
        }
    }
}