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
    public partial class HealthVaultPage : TopBarContentPage
    {
        private bool _isShowLoading = false;
        private bool _logined;
        private bool _isNavigating;
        public HealthVaultPage()
        {
            InitializeComponent();
            WebView.Source = UserViewModel.Instance.HealthVaultUri;
            SettingViewModel.Instance.WebViewJavascript = WebView;
           var grid =(Grid) this.Content;
            var popup = new LoginCHBaseControl();
            if(Common.OS == TargetPlatform.WinPhone)
            Grid.SetRowSpan(popup, 3);
            grid.Children.Add(popup);
            if (Common.OS == TargetPlatform.WinPhone)
                WebView.HeightRequest = 600;
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //WebView.Javascript = string.Format(AppConstant.JavascripLogin, Settings.ChBaseEmail, Settings.ChBasePass);
            WebView.Javascript = "";

        }
        protected override bool OnBackButtonPressed()
        {
            SettingViewModel.Instance.HidePopup();
            return base.OnBackButtonPressed();

        }
        protected override void OnDisappearing()
        {
            UserDialogs.Instance.HideLoading();
            SettingViewModel.Instance.ShowPopup = false;
            SettingViewModel.Instance.WebViewJavascript = null;
            base.OnDisappearing();
        }

        private bool _navigated;
        private async void WebView_OnNavigated(object sender, WebNavigatedEventArgs e)
        {
            _navigated = true;
            _isShowLoading = false;//don't show any more if iOS
            //if(Common.OS != TargetPlatform.WinPhone)
            UserDialogs.Instance.HideLoading();
            //try
            //{
            //    if (Common.OS == TargetPlatform.iOS)
            //    {
            //        if (WebView.Html != null && WebView.Html.Contains(AppConstant.ChBaseErrorLogin))
            //        {
            //            await UserDialogs.Instance.AlertAsync(AppResources.rs_failure_login_chbase_fail);
            //           SettingViewModel.Instance.Email = Settings.ChBaseEmail;
            //            SettingViewModel.Instance.Password = Settings.ChBasePass;

            //            Settings.ChBaseEmail = Settings.ChBasePass = String.Empty;
            //            SettingViewModel.Instance.ShowPopup = true;
            //        }
            //        else if (WebView.Html != null && WebView.Html.Contains(AppConstant.ChBaseNeedLogin))
            //        {     
            //            WebView.Eval(WebView.Javascript);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    UserDialogs.Instance.ShowError(ex.Message);
            //}
        }

       //private IAdvancedTimer timer;
        private void WebView_OnNavigating(object sender, WebNavigatingEventArgs e)
        {
            _isNavigating = true;
            //if (Common.OS != TargetPlatform.Android)
            //{
            //    if (e.Url.Contains("signin.aspx"))
            //    {
            //        WebView.IsVisible = false;
            //    }
            //    else
            //    {
            //        WebView.IsVisible = true;
            //    }
            //}
            var url = e.Url.ToString();

            //if(url[url.Length - 1] != '#')
            if (!_isShowLoading && url[url.Length - 1] != '#' || url.Contains("%2f"))
            {
                _navigated = false;
                Common.ShowLoading();
                //if (Common.OS != TargetPlatform.WinPhone)
                //{
                //    if (timer != null)
                //    {
                //        timer.stopTimer();
                //        timer = null;
                //    }
                //    timer = DependencyService.Get<IAdvancedTimer>();
                //    timer.initTimer(30000, (o, args) =>
                //    {
                //        Task.Factory.StartNew(() =>
                //        {
                //            Device.BeginInvokeOnMainThread(async () =>
                //            {
                //                if (!_navigated)
                //                {
                //                    _navigated = true;
                //                    UserDialogs.Instance.HideLoading();
                //                    await UserDialogs.Instance.AlertAsync(AppResources.network_not_available);
                //                }
                //            });
                //        });
                //    }, false);
                //    timer.startTimer();
                //}
               // Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(30), () =>
               //{
               //    Task.Factory.StartNew(() =>
               //    {
               //        Device.BeginInvokeOnMainThread(async () =>
               //        {
               //            if (!_navigated)
               //            {
               //                _navigated = true;
               //                UserDialogs.Instance.HideLoading();
               //                await UserDialogs.Instance.AlertAsync(AppResources.network_not_available);
               //            }
               //        });
               //    });
               //    return false;
               //});
                Debug.WriteLine("URL "+  url);
                _isShowLoading = true;
            }
        }
    }
}
