using System;
using System.Text.RegularExpressions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using HealthCare.Core.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HealthCare.Win.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CmeClassView : Page
    {
        public CmeClassView()
        {
            this.InitializeComponent();
        }

        private void WebViewLoaded(object sender, RoutedEventArgs e)
        {

            var b = sender as WebView;
            SetString(b);
            b.NavigationCompleted += B_Navigated;
        }

        private void B_Navigated(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            if (args.IsSuccess)
            {
                _backToString = !string.IsNullOrEmpty(args.Uri?.OriginalString);
            }

            BackButton.IsEnabled = WebBrowser.CanGoBack || _backToString;
            ForwardButton.IsEnabled = WebBrowser.CanGoForward;
        }

        private bool _backToString = false;
        private void SetString(WebView b)
        {
            var vm = this.DataContext as CmeClassViewModel;
            if (vm != null)
            {
                if (vm.CmeClass == null)
                {
                    vm.PropertyChanged += (s, e) =>
                    {
                        b?.NavigateToString(Regex.Replace(vm.CmeClass.full_description, "<img src=\"(?!http).*?\".*?/>",
                            string.Empty, RegexOptions.Singleline));
                    };
                    return;
                }
                b?.NavigateToString(Regex.Replace(vm.CmeClass.full_description, "<img src=\"(?!http).*?\".*?/>",
                            String.Empty, RegexOptions.Singleline));
            }
            _backToString = false;
        }
    }
}
