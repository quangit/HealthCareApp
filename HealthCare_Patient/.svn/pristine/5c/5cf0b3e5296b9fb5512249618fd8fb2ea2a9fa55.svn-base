using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Connectivity.Plugin;
using GalaSoft.MvvmLight.Command;
using HealthCare.Controls;
using HealthCare.DependencyServices;
using HealthCare.Helpers;
using HealthCare.Pages;
using HealthCare.Pages.CHBases;
using HealthCare.Resx;
using HealthCare.Services.Interfaces;
using HealthCare.Validators;
using Xamarin.Forms;

namespace HealthCare.ViewModels
{
    public class SettingViewModel : BaseViewModel<SettingViewModel>
    {
        private readonly CommonValidator _validator;
        private string _email, _pass;
        private ICommand _goRegisterCommand;
        private bool _isShowPopup;
        private bool _keyboardHidden;
        public SettingViewModel(INavigationService ns, CommonValidator validator) : base(ns)
        {
            _validator = validator;
            GoProfileCommand = new RelayCommand(async () =>
            {
                if (await Common.CheckNetwordStatus())
                {
                    var userVm = UserViewModel.Instance;
                    userVm.CloneCurrentUser = userVm.CurrentUser.Clone();
                    userVm.SetAvatarResourceUrl(userVm.CloneCurrentUser.Photo);
                    NavigationService.NavigateTo(typeof(ProfilePage));
                }
            });
            LogoutCommand = new RelayCommand(async () =>
            {
                if (await Common.ConfirmAsync(AppResources.confirm_logout))
                {
                    Settings.Reset();
                    SimpleIocV2.Default.Reset();
                    ((ViewModelLocator)App.Instance.Resources["Locator"]).InitHcServices();
                    NavigationService.ReplaceRootPage(typeof(LoginPage));
                }
            });

            LoginCommand = new RelayCommand( async () =>
            {
              await LoginChbase();
            });
            CancelCommand = new RelayCommand(() =>
            {
               
                ShowPopup = false;
                Email = Password = "";
                if (WebViewJavascript != null)
                {
                    WebViewJavascript = null;
                    NavigationService.GoBack();
                }
            });
        }

        #region properties

        // CHBase
        public bool KeyboardHidden
        {
            get { return _keyboardHidden; }
            set
            {
                _keyboardHidden = value;
                RaisePropertyChanged("KeyboardHidden");
            }
        }
        public bool ShowPopup
        {
            get { return _isShowPopup; }
            set
            {
                _isShowPopup = value;
                RaisePropertyChanged("ShowPopup");
            }
        }
      
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                RaisePropertyChanged("Email");
            }
        }

        public string Password
        {
            get { return _pass; }
            set
            {
                _pass = value;
                RaisePropertyChanged("Password");
            }
        }
        public RelayCommand LoginCommand { protected set; get; }
        public RelayCommand CancelCommand { protected set; get; }


        public ICommand GoRegisterCommand
        {
            get
            {
                return _goRegisterCommand ?? (_goRegisterCommand = new RelayCommand( () =>
                {
                    NavigationService.NavigateTo(typeof(RegCHBasePage));
                }));
            }
        }



        // End CHBase

        public bool CanReceivePushNotify
        {
            get { return Settings.CanReceivePushNotify; }
            set { Settings.CanReceivePushNotify = value; }
        }
        
        public WebViewJavascript WebViewJavascript { get; set; }

        private ICommand _canRecivePushNotifyChangedCommand;
        public ICommand CanRecivePushNotifyChangedCommand
        {
            get
            {
                return _canRecivePushNotifyChangedCommand ??
                       (_canRecivePushNotifyChangedCommand = new RelayCommand(async () =>
                       {
                           Common.ShowLoading();
                           await LoginViewModel.Instance.RegisterDevice();
                           UserDialogs.Instance.HideLoading();
                       }));
            }
        }

        private ICommand _goToConfigurationPageCommand;
        public ICommand GoToConfigurationPageCommand
        {
            get
            {
                return _goToConfigurationPageCommand ??
                       (_goToConfigurationPageCommand =
                           new RelayCommand(() => { NavigationService.NavigateTo(typeof(ConfigurationPage)); }));
            }
        }

        public RelayCommand GoProfileCommand { private set; get; }
        public RelayCommand LogoutCommand { private set; get; }


        private ICommand _gotoCreditCardPageCommand;
        public ICommand GotoCreditCardPageCommand
        {
            get
            {
                return _gotoCreditCardPageCommand ?? (_gotoCreditCardPageCommand = new RelayCommand( () =>
              {
                  //if (string.IsNullOrEmpty(Settings.ChBaseEmail))
                  //{
                  //    ShowPopup = true;
                  //}
                  //else
                  //UserDialogs.Instance.Alert(AppResources.thanks_using_service);
                  NavigationService.NavigateTo(typeof(CreditCardPage));

              }));
            }
        }
        #endregion

        #region Methods

        public string Get_rs_failure_login_chbase_fail()
        {
            return AppResources.rs_failure_login_chbase_fail;
        }

        public void HidePopup()
        {
            Email = Password = "";
            ShowPopup = false;

        }

        public void ResetFormChBase()
        {
            Email = Password = "";

        }
        public void HideKeyboard()
        {
            KeyboardHidden = !KeyboardHidden;
        }

        public async void CheckRegChBase(WebView wv, string url)
        {
            UserDialogs.Instance.HideLoading();
            if (url.ToLower().Contains("home"))
            {
                wv.IsVisible = false;
                await UserDialogs.Instance.AlertAsync(AppResources.chbase_reg_success);
                ShowPopup = true;
                var service = DependencyService.Get<IClearCacheWebView>();
                service.ClearWebViewCache();
                NavigationService.GoBack();
            }
          
        }
        public async Task LoginChbase(WebViewJavascript wv = null, string email = null)
        {
            HideKeyboard();
            var res = _validator.ValidateChBaseLoginInput(Email, Password);
                     wv = WebViewJavascript;
                    if (!res.IsValid)
                    {
                        await Common.AlertAsync(res.Errors[0]);
                    }
                    else
                    {
                        ShowPopup = false;
                        Settings.ChBaseEmail = Email;
                        Settings.ChBasePass = Password;
                        if (wv == null)
                            NavigationService.NavigateTo(typeof (HealthVaultPage));
                        else
                        {
                            wv.IsVisible = true;
                            wv.Javascript = string.Format(AppConstant.JavascripLogin, Settings.ChBaseEmail, Settings.ChBasePass);
                            wv.JavascriptLogined = false;
                            if (Common.OS != TargetPlatform.WinPhone)
                                wv.Eval(wv.Javascript);
                            else
                            {
                                wv.Source = (wv.Source as UrlWebViewSource).Url;
                            }
                        }
                    }
            }
        #endregion
    }
}