using HealthCare.Controls;
using HealthCare.Helpers;
using HealthCare.ViewModels;
using Xamarin.Forms;

namespace HealthCare.Pages
{
    public partial class VnPayPaymentPage : TopBarContentPage
    {
        public VnPayPaymentPage()
        {
            InitializeComponent();
            if (Common.OS == TargetPlatform.WinPhone)
                WebView.HeightRequest = 600;
        }

        private void WebView_OnNavigating(object sender, WebNavigatingEventArgs e)
        {
            CreditCardViewModel.Instance.VnPayNavigating((WebView)sender,e.Url);
        }
    }
}