using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web;
using HealthCare.Core;
using HealthCare.Core.Resources;
using HealthCare.Core.Services.Interfaces;
using HealthCare.Core.ViewModels;
using Template10.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HealthCare.Win.Views.HomeTab
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChbaseView : Page
    {
        private HomeViewModel _vm => DataContext as HomeViewModel;
        public static ChbaseView Current { get; set; }
        public WebView RootWebView => WebView;

        public ChbaseView()
        {
            this.InitializeComponent();

            var vm = this.DataContext as HomeViewModel;

        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            Current = null;
            WebView.Stop();
            base.OnNavigatingFrom(e);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Current = this;
            base.OnNavigatedTo(e);
            BootStrapper.Current.NavigationService.Frame.BackStack.Clear();
            BootStrapper.Current.NavigationService.ClearHistory();
            BootStrapper.Current.NavigationService.ClearCache(true);
            var lang = AppResources.ResourceLanguage;
            //await WebView.ClearTemporaryWebDataAsync();
            this.WebView.Settings.IsJavaScriptEnabled = true;
            if (lang == "en-US")
                WebView.Source = new Uri(@"http://chbase.bacsi24x7.vn/");
            else
                WebView.Source = new Uri(@"http://chbase.bacsi24x7.vn/?lang=vi");
        }

        private void WebView_OnNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            App.Current.UpdateShellBackButton();

            IoC.Resolve<IReporterService>().StopProgress();
        }

        private void WebView_OnNavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            TextBlock.Visibility = Visibility.Collapsed;
            IoC.Resolve<IReporterService>().ShowProgress();
        }

        private void WebView_OnNavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
        {
            if (e.WebErrorStatus == WebErrorStatus.CannotConnect)
            {
                TextBlock.Visibility = Visibility.Visible;
            }
        }
    }
}
