using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using HealthCare.Controls;
using HealthCare.Helpers;
using HealthCare.ViewModels;
using HealthCare.ViewModels.CHBases;
using Xamarin.Forms;

namespace HealthCare.Pages
{
    public partial class HealthRecordInformationPage : TopBarContentPage
    {
        public HealthRecordInformationPage()
        {
            InitializeComponent();
            UserDialogs.Instance.ShowLoading();
            ChBaseViewModel.Instance.CheckToken(WebView);
        }

        private async void WebView_OnNavigating(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url.Contains(ChBaseHelper.SignupUrl))
            {
                await ChBaseViewModel.Instance.CheckEmailWhenCreateAcc(WebView, e);
            }
            else
            {
                UserDialogs.Instance.ShowLoading();
            }
        }

        private void WebView_OnNavigated(object sender, WebNavigatedEventArgs e)
        {
            ChBaseViewModel.Instance.LoginChBase(WebView, e.Url);
        }
    }
}
