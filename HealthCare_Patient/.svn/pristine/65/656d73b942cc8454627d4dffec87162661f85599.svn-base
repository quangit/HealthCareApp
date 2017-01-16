using HealthCare.Controls;
using HealthCare.Helpers;
using HealthCare.ViewModels;
using Xamarin.Forms;

namespace HealthCare.Pages
{
    public partial class PasswordPage : TopBarContentPage
    {
        public PasswordPage()
        {
            InitializeComponent();
            WebView.ShouldTrustUnknownCertificate = certificate => true;
            if (Common.OS == TargetPlatform.WinPhone)
                WebView.HeightRequest = 600;
        }

        private void WebView_OnNavigating(object sender, WebNavigatingEventArgs e)
        {
            PasswordViewModel.Instance.ChangePinNavigating((WebView)sender, e.Url);

        }
    }
}