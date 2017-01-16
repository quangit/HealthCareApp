using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using HealthCare.Controls;
using HealthCare.Helpers;
using HealthCare.ViewModels.CHBases;
using Xamarin.Forms;

namespace HealthCare.Pages.CHBases
{
    public partial class ShareViaCHBaseAccountPage:TopBarContentPage
    {
        public ShareViaCHBaseAccountPage()
        {
            InitializeComponent();
            if (Common.OS == TargetPlatform.WinPhone)
                WebView.HeightRequest = 600;
            WebView.Source = ChBaseHelper.ShareViaChbaseUrl;
        }
        private void WebView_OnNavigating(object sender, WebNavigatingEventArgs e)
        {
            UserDialogs.Instance.ShowLoading();
        }

        private void WebView_OnNavigated(object sender, WebNavigatedEventArgs e)
        {
            UserDialogs.Instance.HideLoading();
        }
    }
}
