using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Connectivity.Plugin;
using GalaSoft.MvvmLight.Command;
using HealthCare.DependencyServices;
using HealthCare.Enums;
using HealthCare.Exceptions;
using HealthCare.Helpers;
using HealthCare.ModelApis;
using HealthCare.Models;
using HealthCare.Models.ChBaseModel;
using HealthCare.Objects;
using HealthCare.Pages;
using HealthCare.Pages.CHBases;
using HealthCare.Resx;
using HealthCare.Services.Interfaces;
using HealthCare.Validators;
using HealthCare.WebServices;
using HealthCare.WebServices.Interfaces;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace HealthCare.ViewModels
{
    public class LoginViewModel : BaseViewModel<LoginViewModel>
    {
        private readonly ICommonWS _commonWS;
        private readonly IUserWS _userWS;
        private readonly IPaymentWS _paymentWS;
        private readonly CommonValidator _validator;
        private ICommand _goRegisterCommand;
        private bool _keyboardHidden;
        private ICommand _loginFbCommand;
        private string _userName, _pass, _promotionCount = "Khuyến mãi";
        private ICommand _testCommand;

        public LoginViewModel(INavigationService ns, IUserWS userWS, ICommonWS commonWS, IPaymentWS paymentWS,
            CommonValidator validator)
            : base(ns)
        {
            _userWS = userWS;
            _commonWS = commonWS;
            _validator = validator;
            _paymentWS = paymentWS;
            GoPromotionCommand = new Command(s =>
            {
                NavigationService.NavigateTo(typeof(PromotionPage));
            });
            LoginCommand = new RelayCommand(Login);
            //LoginCommand = new RelayCommand(() =>
            //{
            //    NavigationService.NavigateTo(typeof(CropPhotoPage));
            //});

            //            LoadDataCommand = new Command(CheckRememberMeAndGetAppConfig);
            GoForgotPasswordCommand = new RelayCommand(() =>
                {
                    PasswordViewModel.Instance.ResetData();
                    NavigationService.NavigateTo(typeof(ForgotPasswordPage));
                });
        }

        #region Property

        public ICommand GoPromotionCommand { protected set; get; }

        public RelayCommand LoginCommand { protected set; get; }

        public RelayCommand GoForgotPasswordCommand { protected set; get; }

        public ICommand LoadDataCommand { protected set; get; }

        public ICommand LoginFbCommand
        {
            get
            {
                return _loginFbCommand ??
                    (_loginFbCommand = new RelayCommand(async () =>
                        {
                            try
                            {
                                if (await Common.CheckNetwordStatus())
                                {
                                    Common.ShowLoading();

                                    var isReachable = Common.OS == TargetPlatform.Android
                                        ? await Common.IsReachable("https://www.facebook.com", 60000)
                                        : await CrossConnectivity.Current.
                                            IsReachable("www.facebook.com", 60000);

                                    UserDialogs.Instance.HideLoading();

                                    if (isReachable)
                                    {
                                        DependencyService.Get<IFacebookHelper>().Login();
                                    }
                                    else
                                    {
                                        LaunchFacebookFailure();
                                    }
                                }
                            }
                            catch
                            {
                                await Common.AlertAsync(AppResources.network_not_available);
                            }
                        }));
            }
        }
        public ICommand TestCommand
        {
            get
            {
                return _testCommand ??
                    (_testCommand = new RelayCommand(() =>
                       {

                       }));
            }
        }

        public ICommand GoRegisterCommand
        {
            get
            {
                return _goRegisterCommand ?? (_goRegisterCommand = new RelayCommand(async () =>
                    {
                        if (!Common.CheckNetworkConnected() || CommonViewModel.Instance.SystemConfig == null)
                        {
                            await Common.AlertAsync(AppResources.network_not_available);
                        }
                        else if (Settings.RegisterSuccessCount >=
                            CommonViewModel.Instance.SystemConfig.MaxRegistrationLimitPerDevice)
                        {
                            await
                            Common.AlertAsync(string.Format(AppResources.limit_device_register_message,
                                CommonViewModel.Instance.SystemConfig.MaxRegistrationLimitPerDevice));
                        }
                        else
                        {
                            //reset user when normal login
                            //Settings.Reset();
                            UserViewModel.Instance.ResetUser();
                            UserViewModel.Instance.ResetAvatarResource();
                            UserViewModel.Instance.CanInputPassword = true;
                            ResetData();
                            NavigationService.NavigateTo(typeof(RegisterPage));
                        }
                    }));
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged("UserName");
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

        public void FirePromotionCountChanged()
        {
            RaisePropertyChanged("PromotionCount");
        }

        public string PromotionCount => string.Format(AppResources.promotion_number,
            CommonViewModel.Instance.SystemConfig.PromotionCount);

        public bool KeyboardHidden
        {
            get { return _keyboardHidden; }
            set
            {
                _keyboardHidden = value;
                RaisePropertyChanged("KeyboardHidden");
            }
        }

        #endregion

        #region Method

        public async void LaunchFacebookFailure()
        {
            await Common.AlertAsync(AppResources.rs_cannot_connect_to_facebook);
        }

        public void AutoLogin()
        {
#if DEBUG
            UserName = "thien92dn@gmail.com";
            Password = "Thien#123";

            UserName = "test@yopmail.com";
            Password = "Sdc12345@";

            UserName = "0906456093";
            Password = "Sdc123456@";

            UserName = "quynhquynh@yopmail.com";
            Password = "Sdc12345@";

            UserName = "01649839472";
            Password = "Sdc12345@";

            UserName = "0906456093";
            Password = "Sdc123456@";
#endif

            if (!string.IsNullOrWhiteSpace(Settings.CurrentUser?.FacebookId))
            {
                LoginFb();
            }
            else if (!string.IsNullOrWhiteSpace(Settings.CurrentUser?.UserCode)
                && !string.IsNullOrWhiteSpace(Settings.CurrentUser?.Password))
            {
                UserName = Settings.CurrentUser?.UserCode;
                Password = Settings.CurrentUser?.Password;
                Login();
            }
        }

        public async void LoginFb()
        {
            try
            {
                Common.ShowLoading();
                var facebooId = Settings.CurrentUser.FacebookId;
                if (string.IsNullOrWhiteSpace(facebooId))
                {
                    await DependencyService.Get<IFacebookHelper>().GetMe(Settings.FacebookAccessToken);
                    facebooId = UserViewModel.Instance.CurrentUser.FacebookId;
                }

                var blockData = await _userWS.LoginWithFacebook(facebooId);
                Common.IsPatientUserType(JsonUtils.ParseData<int>(blockData, AppConstant.KeyUserType));
                await ParseDataFromLogin(blockData, true);

                await MoreSupportViewModel2.Instance.GetQuestionList(true, false);

                await DependencyService.Get<IPushNotificationRegister>().RegiterPushNotification();
                await RegisterDevice();

                NavigationService.ReplaceRootPage(typeof(HomeTabbedPage));
                //ToolbarViewModel.Instance.SetSelectedPage(typeof(SearchPage));
                //NavigationService.ReplaceRootPage(typeof(AddProcedurePage));

                ResetData();
            }
            catch (ApiException ae)
            {
                if (ae.ErrorCode == ResponseCode.FAILURE_USER_NOT_FOUND)
                {
                    //navigate to Register page if this fb_id not register yet
                    if (Settings.RegisterSuccessCount >=
                        CommonViewModel.Instance.SystemConfig.MaxRegistrationLimitPerDevice)
                    {
                        await Common.AlertAsync(string.Format(AppResources.limit_device_register_message,
                            CommonViewModel.Instance.SystemConfig.MaxRegistrationLimitPerDevice));
                    }
                    else
                    {
                        UserViewModel.Instance.ResetAvatarResource();
                        UserViewModel.Instance.CanInputPassword = false;
                        ResetData();
                        NavigationService.NavigateTo(typeof(RegisterPage));
                    }
                }
                else if (ae.ErrorCode == ResponseCode.FAILURE_DISABLE_USER)
                {
                    if (await Common.ConfirmAsync(AppResources.rs_failure_disable_user, AppResources.active_by_otp, AppResources.skip))
                    {
                        NavigationService.NavigateTo(typeof(OTPPage));
                    }
                }
                else
                {
                    await Common.AlertAsync(ae.Message);
                }
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
            UserDialogs.Instance.HideLoading();
        }

        private void DebugAutoFillLogin()
        {
            if (string.IsNullOrEmpty(UserName))
                return; 
            switch (UserName)
            {
                case "hot1":
                    UserName = "001000000067";
                    Password = "Sdc123456@";
                    break;
                case "hot2":
                    UserName = "pmtam@sdc.ud.edu.vn";
                    Password = "Sdc12345@";
                    break;
                case "hot3":
                    UserName = "quang.2510@gmail.com";
                    Password = "Sdc12345@";
                    break;
                case "hot4":
                    UserName = "hiencongdang@yopmail.com";
                    Password = "Sdc12345@";
                    break;
                case "hot5":
                    UserName = "0935667171";
                    Password = "Sdc123456@";
                    break;
            }
        }

        public async Task RegisterDevice()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Settings.DeviceChannel))
                {
                    Settings.RegistrationId =
                        await _commonWS.RegisterDevice(Settings.DeviceChannel,
                            Common.OnPlatform(AppConstant.TypeOS.Ios,
                                AppConstant.TypeOS.Android, AppConstant.TypeOS.Wp), Settings.RegistrationId);

                    //await Common.AlertAsync(Settings.DeviceChannel);
                    //await Common.AlertAsync("RegistrationId: " + Settings.RegistrationId);
                    Debug.WriteLine("DeviceChannel: " + Settings.DeviceChannel);
                    Debug.WriteLine("RegistrationId: " + Settings.RegistrationId);
                }
            }
            catch
            {
                // ignored
            }
        }

        public async void Login()
        {
#if DEBUG
            DebugAutoFillLogin();
#endif

            HideKeyboard();

            var result = _validator.ValidateLoginInput(UserName, Password);
            if (!result.IsValid)
            {
                await Common.AlertAsync(result.Errors[0]);
                return;
            }
            Common.ShowLoading();
            try
            {
                var blockData = await _userWS.Login(UserName, Password, Common.GetTimeZone());
                Common.IsPatientUserType(JsonUtils.ParseData<int>(blockData, AppConstant.KeyUserType));
                await ParseDataFromLogin(blockData, true);

                await MoreSupportViewModel2.Instance.GetQuestionList(true, false);

                await DependencyService.Get<IPushNotificationRegister>().RegiterPushNotification();
                await RegisterDevice();

                //ToolbarViewModel.Instance.SetSelectedPage(typeof(SearchPage));
                NavigationService.ReplaceRootPage(typeof(HomeTabbedPage));
                //NavigationService.ReplaceRootPage(typeof(AddProcedurePage));
                //NavigationService.ReplaceRootPage(typeof(BloodPressurePage));
                //NavigationService.ReplaceRootPage(typeof(BloodGlucosePage));
                //NavigationService.ReplaceRootPage(typeof(AddWeightPage));
                //NavigationService.ReplaceRootPage(typeof(ProcedurePage));
                //NavigationService.ReplaceRootPage(typeof(ShareViaMailPage));
                //NavigationService.ReplaceRootPage(typeof(AddBloodPressurePage));
                //NavigationService.NavigateTo(typeof(TransferPaymentPage)); //HealthRecordInformationPage

                ResetData();
            }
            catch (ApiException ae)
            {
                if (ae.ErrorCode == ResponseCode.FAILURE_DISABLE_USER)
                {
                    if (
                        await
                            Common.ConfirmAsync(AppResources.rs_failure_disable_user, AppResources.active_by_otp, AppResources.skip))
                    {
                        NavigationService.NavigateTo(typeof(OTPPage));
                    }
                }
                else
                {
                    await Common.AlertAsync(ae.Message);
                }

                UserDialogs.Instance.HideLoading();
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
            UserDialogs.Instance.HideLoading();
        }


        /// <summary>
        ///     Return true: login success, False: login failure
        /// </summary>
        /// <param name="blockData"></param>
        /// <param name="isSaveUserInfo"></param>
        /// <returns></returns>
        public async Task<bool> ParseDataFromLogin(JObject blockData, bool isSaveUserInfo)
        {
            try
            {
                // Parse the block of data and archive in each of viewmodel
                var userData = JsonUtils.ParseData<UserModel>(blockData, AppConstant.KeyProfile);

                var chbaseId = JsonUtils.ParseData<string>(blockData, AppConstant.KeyChBaseId);

                var relatedAccData =
                    JsonUtils.ParseData<ObservableCollection<UserModel>>(blockData, AppConstant.KeyRelatedAccount);
                var checkupsData =
                    JsonUtils.ParseData<ObservableCollection<ApiCheckupModel>>(blockData, AppConstant.KeyCheckups).
                    ToBaseModel<CheckupModel, ApiCheckupModel>();

                var citiesForSearchData =
                    JsonUtils.ParseData<ObservableCollection<CityModel>>(blockData, AppConstant.KeyCitiesForSearch);

                var userVm = SimpleIocV2.Default.GetInstance<UserViewModel>();
                var checkupVm = SimpleIocV2.Default.GetInstance<CheckupViewModel>();
                var creditCardVm = SimpleIocV2.Default.GetInstance<CreditCardViewModel>();
                var commonVm = SimpleIocV2.Default.GetInstance<CommonViewModel>();
                var hospitalVm = SimpleIocV2.Default.GetInstance<HospitalViewModel>();

                userVm.CurrentUser = userData;
                userVm.CurrentUser.ChbaseId = chbaseId;
                userVm.RelatedAccounts = relatedAccData;
                checkupVm.ResetFilter();
                checkupVm.CheckupAddedTodaySuccessCount =
                    checkupsData.Count(x => x.WhenCreated.Date == DateTime.Now.Date);
                checkupVm.ListCheckups = new ObservableCollection<ProxyCheckupModel>(
                    checkupsData.Where(x => x.Status != CheckupStatus.Finished &&
                        x.Status != CheckupStatus.Deleted).Select(x => new ProxyCheckupModel(x)));

                CheckupViewModel.Instance.SetLocalNotificationForCheckups();
                //var cardsData =
                //    JsonUtils.ParseData<ObservableCollection<CreditCardModel>>(blockData, AppConstant.KeyCards);
                // Create Model VNPAY

                commonVm.ListCitiesForSearch = citiesForSearchData;
                hospitalVm.ListHospitalFilter =
                    HospitalViewModel.Instance.GetListHospitalFromCheckupsData(checkupVm.ListCheckups.ToBaseModel<CheckupModel, ProxyCheckupModel>());

                //save user info to LocalSetting
                if (!string.IsNullOrWhiteSpace(Password))
                    userData.Password = Password;
                if (Settings.UserId != userData.Id)
                    Settings.ClearCacheChBase();
                userVm.SaveUseInfo(userData,
                    isSaveUserInfo
                    ? JsonUtils.ParseData<string>(blockData, AppConstant.KeySessionId)
                    : null,
                    isSaveUserInfo
                    ? JsonUtils.ParseData<string>(blockData, AppConstant.KeySessionExpired)
                    : null);
                var cardsData = await _paymentWS.GetCardListv2();
                foreach (var card in cardsData)
                {
                    card.FirstName = userData.FirstName;
                    card.LastName = userData.LastName;
                    card.Address = userData.Address;
                    card.Email = userData.Email;
                    card.PhoneNo = userData.PhoneNo;
                }
                //cardsData.Add(new CreditCardModel()
                //{
                //    CardId = AppConstant.VnPayCreaditCardId,
                //    Id = "Other"
                //});
                cardsData.Add(new CreditCardModel()
                {
                    CardId = AppConstant.VtcPayCreaditCardId,
                    Id = "Other"
                });

                creditCardVm.ListCreditCard = cardsData;
                return true;
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
                return false;
            }
        }

        public void ResetData()
        {
            UserName = Password = null;
        }

        /// <summary>
        ///     Change KeyboardHidden property to raise event hide keyboard
        /// </summary>
        public void HideKeyboard()
        {
            KeyboardHidden = !KeyboardHidden;
        }

        #endregion
    }
}