using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using HealthCare.Controls;
using HealthCare.Helpers;
using HealthCare.Resx;
using HealthCare.ViewModels;
using HealthCare.WebServices;
using Xamarin.Forms;

namespace HealthCare.Pages
{
    public partial class RegCHBasePage : TopBarContentPage
    {
        public RegCHBasePage()
        {   
            InitializeComponent();
            
            WebView.Source = UserViewModel.Instance.RegHealthVaultUri;
            if (Common.OS == TargetPlatform.WinPhone)
                WebView.HeightRequest = 600;
            if (Common.OS == TargetPlatform.Android)
            {
                UserDialogs.Instance.ShowLoading();
            }
        }
        

        private void WebView_OnNavigating(object sender, WebNavigatingEventArgs e)
        {
           UserDialogs.Instance.ShowLoading();
        }

        private  void WebView_OnNavigated(object sender, WebNavigatedEventArgs e)
        {
            SettingViewModel.Instance.CheckRegChBase((WebView)sender, e.Url);
        }
    }
}
