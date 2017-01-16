using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using HealthCare.Controls;
using HealthCare.Helpers;
using HealthCare.Models.ChBaseModel;
using HealthCare.Services.Interfaces;
using HealthCare.Pages;
using HealthCare.Pages.CHBases;
using HealthCare.Resx;
using HealthCare.WebServices.Interfaces;
using Xamarin.Forms;
using XLabs;

namespace HealthCare.ViewModels.CHBases
{
    public class ChBaseViewModel : BaseViewModel<ChBaseViewModel>
    {
        private readonly IChBaseWS _chBaseWs;
        private readonly IPatientCHBaseWS _patientChBaseWs;
        public PersonModel _userInfo { get; set; }
        private string _fullName;
        private string _imageAvatar;
        private string _statusAccount;
        private string _statusButton;
        private bool _visibleLable;
        private bool _visibleButton;
        private ImageSource _avatarImageSource;
        private bool _loggedChBase, _showedAlert;
        private CHBasePatientInfoModel _chBasePatientInfo;

        public PersonModel UserInfo
        {
            get { return _userInfo; }
            set
            {
                _userInfo = value;
                FullNamne = _userInfo.Name;
                RaisePropertyChanged();
            }
        }

        public string FullNamne
        {
            get
            {
                if (_fullName?.Length > 20)
                {
                    return _fullName.Substring(0, 20);
                }
                return _fullName;
            }
            set { _fullName = value; RaisePropertyChanged(); }
        }

        public string StatusAccount
        {
            get { return _statusAccount; }
            set { _statusAccount = value; RaisePropertyChanged(); }
        }

        public string StatusButton
        {
            get { return _statusButton; }
            set { _statusButton = value; RaisePropertyChanged(); }
        }

        public bool VisibleLable
        {
            get { return _visibleLable; }
            set { _visibleLable = value; RaisePropertyChanged(); }
        }

        public bool VisibleButton
        {
            get { return _visibleButton; }
            set { _visibleButton = value; RaisePropertyChanged(); }
        }

        public DateTime ExpirationDate
        {
            get
            {
                if(_chBasePatientInfo != null) return _chBasePatientInfo.WhenExpired;
                else
                {
                    return  DateTime.Now;
                }
            }
            //set { _chBasePatientInfo.WhenExpired = value; RaisePropertyChanged(); }
        }

        public ImageSource AvatarImageSource
        {
            get
            {
                return _avatarImageSource ??
                       (_avatarImageSource = new FileImageSource { File = "ic_avatar_placeholder.png" });
            }
            set
            {
                _avatarImageSource = value;
                RaisePropertyChanged();
            }
        }

        public bool LoggedChBase
        {
            get { return _loggedChBase; }
            set
            {
                _loggedChBase = value;
                RaisePropertyChanged();
            }
        }
        public ChBaseViewModel(INavigationService navigationService, IChBaseWS chBaseWs, IPatientCHBaseWS patientChBaseWs) : base(navigationService)
        {
            _chBaseWs = chBaseWs;
            //_chBaseWs.GetPatientInfo();
            LoggedChBase = false;
            StatusAccount = AppResources.payment_pending_account;
            StatusButton = AppResources.payment;
            //ExpirationDate = DateTime.Now;
            _patientChBaseWs = patientChBaseWs;
            _chBasePatientInfo = new CHBasePatientInfoModel();
            _chBasePatientInfo.Status = false;
            _chBasePatientInfo.Name = "name";
            _chBasePatientInfo.WhenExpired = DateTime.Now;

        }

        public CHBasePatientInfoModel ChBasePatientInfo
        {
            get { return _chBasePatientInfo; }
            set
            {
                if (value != null)
                {
                    if (!string.IsNullOrEmpty(value.Name))
                    {
                        _chBasePatientInfo = value;
                        if (value.Status)
                        {
                            VisibleButton = true;
                            VisibleLable = false;

                            StatusButton = AppResources.buy_more;
                            StatusAccount = AppResources.accounts_in_use;
                        }
                        else
                        {
                            VisibleButton = false;
                            VisibleLable = true;

                            StatusButton = AppResources.extensions;
                            StatusAccount = AppResources.expired_account;
                        }
                    }
                    else
                    {
                        VisibleButton = false;
                        VisibleLable = true;

                        StatusButton = AppResources.payment;
                        StatusAccount = AppResources.payment_pending_account;
                    }
                    RaisePropertyChanged("ExpirationDate");
                    //RaisePropertyChanged("FullNamne");
                    RaisePropertyChanged("StatusButton");
                    RaisePropertyChanged("StatusAccount");
                }
                
            }
        }

        public async void CheckToken(ChBaseWebview webview)
        {
            UserDialogs.Instance.ShowLoading();
            webview.IsVisible = false;
            if (!string.IsNullOrEmpty(Settings.ChBaseToken))
            {
                ChBaseHelper.AppToken = await _chBaseWs.GetAppToken();
                ChBaseHelper.UserToken = Settings.ChBaseToken;
                var user = await _chBaseWs.GetUserInfo();
                if (user != null && !string.IsNullOrEmpty(user.RecordId))
                {
                    UserDialogs.Instance.HideLoading();
                    UserInfo = user;
                    LoggedChBase = true;
                    var result = await _patientChBaseWs.LinkToPatient(UserInfo.Id);
                    ChBasePatientInfo = await _patientChBaseWs.GetCHBaseInfo();
                    
                }
                else
                {
                    webview.IsVisible = true;
                    webview.Source = AppConstant.ChBaseApiUrl;
                }
            }
            else
            {
                webview.IsVisible = true;
                webview.Source = AppConstant.ChBaseApiUrl;
            }
        }
        public async void LoginChBase(ChBaseWebview webview, string url)
        {
           
            if(url?.ToLower() == AppConstant.AboutBlank)
                return;

            if (!string.IsNullOrEmpty(webview.Token))
            {
                webview.Source = new UrlWebViewSource
                {
                    Url = AppConstant.AboutBlank
                };
                webview.IsVisible = false;
                ChBaseHelper.UserToken = webview.Token;
                Settings.ChBaseToken = ChBaseHelper.UserToken;
                ChBaseHelper.AppToken = await _chBaseWs.GetAppToken();
                UserInfo = await _chBaseWs.GetUserInfo();
                if (UserViewModel.Instance.CurrentUser.ChbaseId != null)
                {
                    if (UserInfo.Id != UserViewModel.Instance.CurrentUser.ChbaseId)
                    {
                        webview.IsVisible = false;

                        if (_showedAlert)
                            return;
                        _showedAlert = true;
                        webview.Token = "";
                        await Common.AlertAsync(AppResources.invalid_chbase_account);
                        _showedAlert = false;
                        Settings.ClearCacheChBase();
                        Settings.ChBaseToken = "";
                        webview.Source = AppConstant.ChBaseApiUrl;
                        LoggedChBase = false;
                    }
                    else
                    {
                        webview.IsVisible = false;
                        LoggedChBase = true;
                    }
                }
                else
                {
                    webview.IsVisible = false;
                    LoggedChBase = true;
                    var result = await _patientChBaseWs.LinkToPatient(UserInfo.Id);
                }
                ChBasePatientInfo = await _patientChBaseWs.GetCHBaseInfo();
            }
            else
            {
                webview.IsVisible = !url.Contains(AppConstant.ChBaseUrl + "/MedicalPage");
            }
            UserDialogs.Instance.HideLoading();
        }

        public async Task CheckEmailWhenCreateAcc(WebView webView, WebNavigatingEventArgs e)
        {
            if (string.IsNullOrEmpty(UserViewModel.Instance.CurrentUser.Email))
            {
                e.Cancel = true;
                if (await Common.ConfirmAsync(AppResources.update_email))
                {
                    var userVm = UserViewModel.Instance;
                    userVm.CloneCurrentUser = userVm.CurrentUser.Clone();
                    userVm.SetAvatarResourceUrl(userVm.CloneCurrentUser.Photo);
                    NavigationService.NavigateTo(typeof(ProfilePage));
                    if (Common.OS == TargetPlatform.WinPhone)
                    {
                        webView.IsVisible = true;
                        webView.GoBack();
                    }
                }
                else
                {
                    if (Common.OS == TargetPlatform.WinPhone)
                    {
                        webView.IsVisible = true;
                        webView.GoBack();
                    }
                }
            }
        }
        public RelayCommand ClickPamentCommand => new RelayCommand(() =>
        {
            PaymentViewModel.Instance._chBasePatientInfo = _chBasePatientInfo;
            PaymentViewModel.Instance.GetPackageFeeInfo();
            NavigationService.NavigateTo(typeof(PaymentCheckupRecordPage));
              //_patientChBaseWs.GetPackageInfo();
        });
    }
}
