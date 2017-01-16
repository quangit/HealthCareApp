using HealthCare.Controls;
using HealthCare.Helpers;
using HealthCare.ViewModels;
using HealthCare.ViewModels.CHBases;
using Xamarin.Forms;

namespace HealthCare.Pages.CHBases
{
    public partial class VtcPayPaymentPage : TopBarContentPage
    {
        public VtcPayPaymentPage()
        {
            InitializeComponent();
            if (Common.OS == TargetPlatform.WinPhone)
                WebView.HeightRequest = 600;
        }

        private void WebView_OnNavigating(object sender, WebNavigatingEventArgs e)
        {
            PaymentViewModel.Instance.VtcPayNavigating((WebView)sender,e.Url);
        }
    }
}