using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.System;
using HealthCare.Core.Models;

namespace HealthCare.Phone.Views
{
    public partial class WeekTopicFileView
    {
        public WeekTopicFileView()
        {
            InitializeComponent();
        }

        private async void StackPanel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var topicfile = ((FrameworkElement)sender).DataContext as TopicFiles;
            var url = new Uri(topicfile.FileUri);
            await Launcher.LaunchUriAsync(url);
        }

        private void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            HomeView.CallSupport();
        }
    }
}