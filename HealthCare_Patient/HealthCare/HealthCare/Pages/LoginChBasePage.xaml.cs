using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using HealthCare.Controls;
using HealthCare.Helpers;
using HealthCare.Models.ChBaseModel;
using HealthCare.ViewModels;
using HealthCare.ViewModels.CHBases;
using Xamarin.Forms;

namespace HealthCare.Pages
{
    public partial class LoginChBasePage : TopBarContentPage
    {
        public LoginChBasePage()
        {
            InitializeComponent();
            UserDialogs.Instance.ShowLoading();
            ChBaseViewModel.Instance.CheckToken(WebView);
        }

      

        private void WebView_OnNavigating(object sender, WebNavigatingEventArgs e)
        {
            UserDialogs.Instance.ShowLoading();
        }

        private void WebView_OnNavigated(object sender, WebNavigatedEventArgs e)
        {
            ChBaseViewModel.Instance.LoginChBase(WebView, e.Url);
        }
    }
}