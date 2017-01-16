using System;
using HealthCare.Controls;
using HealthCare.Helpers;
using HealthCare.ViewModels;
using Xamarin.Forms;

namespace HealthCare.Pages
{
    public partial class AddCardPage : TopBarContentPage
    {
        public AddCardPage()
        {
            InitializeComponent();
            //WebView.ShouldTrustUnknownCertificate = certificate => true;
            if (Common.OS == TargetPlatform.WinPhone)
                WebView.HeightRequest = 600;
        }
        private void WebView_OnNavigating(object sender, WebNavigatingEventArgs e)
        {
            CreditCardViewModel.Instance.AddNewCardNavigating((WebView)sender, e.Url);

        }
        
    }
}