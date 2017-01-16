using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using HealthCare.Core.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace HealthCare.Phone.Views
{
    public partial class CmeClassView
    {
        public CmeClassView()
        {
            InitializeComponent();
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            var b = sender as WebBrowser;
            SetString(b);
            b.Navigated += B_Navigated;
        }

        private bool backToString = false;
        private void SetString(WebBrowser b)
        {
            var vm = this.ViewModel as CmeClassViewModel;
            if (vm != null)
            {
                b?.NavigateToString(vm.CmeClass.full_description);
            }
            backToString = false;
        }

        private void B_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New)
            {
                backToString = e.Uri != null && !string.IsNullOrEmpty(e.Uri.OriginalString);
            }

            BackButton.IsEnabled = WebBrowser.CanGoBack || backToString;
            ForwardButton.IsEnabled = WebBrowser.CanGoForward;

        }

        private void HomeButton_OnClick(object sender, RoutedEventArgs e)
        {
            SetString(WebBrowser);
        }

        private void ForwardButton_OnClick(object sender, RoutedEventArgs e)
        {

            WebBrowser.GoForward();
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (backToString != null && (!WebBrowser.CanGoBack && backToString))
            {
                SetString(WebBrowser);
            }
            else
                WebBrowser.GoBack();
        }
    }
}