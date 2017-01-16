using HealthCare.Controls;
using HealthCare.Helpers;
using HealthCare.ViewModels;
using Xamarin.Forms;

namespace HealthCare.Pages
{
    using Acr.UserDialogs;

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
            CreditCardViewModel.Instance.VtcPayNavigating((WebView)sender,e.Url);
        }

        private void WebView_OnNavigated(object sender, WebNavigatedEventArgs e)
        {
            CreditCardViewModel.Instance.VtcPayNavigated((WebView)sender, e.Url);
        }       
       
    }
}