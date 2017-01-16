using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using HealthCare.Helpers;
using HealthCare.Models;
using HealthCare.Objects;
using HealthCare.Pages;
using HealthCare.Resx;
using HealthCare.Services.Interfaces;
using HealthCare.Validators;
using HealthCare.WebServices.Interfaces;
using Xamarin.Forms;

namespace HealthCare.ViewModels
{
    public class CommonViewModel : BaseViewModel<CommonViewModel>
    {
        private readonly ICommonWS _commonWs;
        public readonly int DelayInMilliSec = 100;
        private ICommand _getCitiesDistrictsCommand, _getAndUpdateCitiesDistrictsCommand;
        private ObservableCollection<CityModel> _listCitiesForSearch;
        private ObservableCollection<CityModel> _listCity;
        private CityModel _selectedCity, _selectedCityForSearch;
        private DistrictModel _selectedDistrict, _selectedDistrictForSearch;
        private SystemConfig _systemConfig;
        private string _phoneNumberOtp;
        private string _otp;
        private ICommand _otpPageLoadedCommand;
        private ICommand _sendOtpCommand;

        public CommonViewModel(INavigationService navigationService, ICommonWS commonWs) : base(navigationService)
        {
            _commonWs = commonWs;
            ListCity = new ObservableCollection<CityModel>();
        }

        #region Properties      
        public ObservableCollection<CityModel> ListCity
        {
            get { return _listCity; }
            set
            {
                _listCity = value;
                RaisePropertyChanged("ListCity");
            }
        }

        public CityModel SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                _selectedCity = value;
                RaisePropertyChanged("SelectedCity");
            }
        }

        public DistrictModel SelectedDistrict
        {
            get { return _selectedDistrict; }
            set
            {
                _selectedDistrict = value;
                RaisePropertyChanged("SelectedDistrict");
            }
        }

        public ObservableCollection<CityModel> ListCitiesForSearch
        {
            get { return _listCitiesForSearch; }
            set
            {
                _listCitiesForSearch = value;
                RaisePropertyChanged("ListCitiesForSearch");
                Common.DoTaskWithDelay(() =>
                {
                    if (value != null && value.Count > 0)
                        SelectedCityForSearch = value[0];
                    else
                    {
                        SelectedCityForSearch = null;
                    }
                });
            }
        }

        public CityModel SelectedCityForSearch
        {
            get { return _selectedCityForSearch; }
            set
            {
                var showAll = new DistrictModel { Id = AppConstant.IdAllItems, Name = AppResources.all };
                if (value != null &&
                    !(value.Districts.Where(x => x.Id == AppConstant.IdAllItems)).Any())
                    value.Districts.Insert(0, showAll);
                _selectedCityForSearch = value;
                RaisePropertyChanged("SelectedCityForSearch");
                Common.DoTaskWithDelay(() =>
                {
                    SelectedDistrictForSearch = value?.Districts[0];
                });
            }
        }

        public DistrictModel SelectedDistrictForSearch
        {
            get { return _selectedDistrictForSearch; }
            set
            {
                _selectedDistrictForSearch = value;
                RaisePropertyChanged("SelectedDistrictForSearch");
            }
        }

        public SystemConfig SystemConfig
        {
            get
            {
                if (_systemConfig == null)
                    _systemConfig = Settings.SystemConfig;
                return _systemConfig;
            }
            set { Settings.SystemConfig = _systemConfig = value; }
        }

        public string PhoneNumberOtp
        {
            get { return _phoneNumberOtp; }
            set { _phoneNumberOtp = value; RaisePropertyChanged(); }
        }

        public string Otp
        {
            get { return _otp; }
            set { _otp = value; RaisePropertyChanged(); }
        }

        public ICommand GetCitiesDistrictsCommand => _getCitiesDistrictsCommand ??
                                                     (_getCitiesDistrictsCommand =
                                                         new RelayCommand(async () => { await GetListCityAsync(true, false); }));

        public ICommand GetAndUpdateCitiesDistrictsCommand => _getAndUpdateCitiesDistrictsCommand ??
                                                              (_getAndUpdateCitiesDistrictsCommand =
                                                                  new RelayCommand(async () => { await GetAndUpdateListCity(false); }));

        public ICommand OtpPageLoadedCommand => _otpPageLoadedCommand ??
                                                             (_otpPageLoadedCommand =
                                                                 new RelayCommand(ResetOtp));

        public ICommand SendOtpCommand => _sendOtpCommand ??
                                                          (_sendOtpCommand =
                                                              new RelayCommand(async () => { await SendOtp(); }));

        #endregion

        #region Methods

        public async Task<bool> GetListCityAsync(bool getIfDataExisted, bool isShowMessage)
        {
            bool isShowLoading = isShowMessage || (ListCity == null || ListCity?.Count == 0);
            try
            {
                if (ListCity == null || ListCity?.Count == 0 || getIfDataExisted)
                {
                    if (isShowLoading)
                        Common.ShowLoading();

                    ListCity = await _commonWs.GetCityList();

                    if (isShowLoading)
                        UserDialogs.Instance.HideLoading();

                    return true;
                }
            }
            catch (Exception e)
            {
                if (isShowLoading)
                {
                    UserDialogs.Instance.HideLoading();
                    await Common.AlertAsync(e.Message);
                }
            }

            if (isShowLoading)
                UserDialogs.Instance.HideLoading();

            return false;
        }

        public async void GetListCity()
        {
            try
            {
                ListCity = await _commonWs.GetCityList();
            }
            catch
            {
                // ignored
            }
        }

        public async Task GetAndUpdateListCity(bool isShowMessage)
        {
            if (ListCity?.Count > 0)
                Common.DoTaskWithDelay(UpdateSelectedCityAndDistrict, DelayInMilliSec);
            await GetListCityAsync(true, isShowMessage);
            Common.DoTaskWithDelay(UpdateSelectedCityAndDistrict, DelayInMilliSec);

        }

        /// <summary>
        ///     Update and map the city and district of current user to new list City District
        ///     This will show a message if city or district is not found
        /// </summary>
        public void UpdateSelectedCityAndDistrict()
        {
            var curUser = UserViewModel.Instance.CurrentUser;
            if (curUser != null && curUser.City != null
                && curUser.District != null && !string.IsNullOrWhiteSpace(curUser.CityId) 
                && !string.IsNullOrWhiteSpace(curUser.DistrictId))
            {
                var mapCities = new List<CityModel>(ListCity.Where(x => x.Id.Equals(curUser.City.Id)));
                if (mapCities.Count > 0)
                {
                    SelectedCity = mapCities[0];
                    var mapDistricts =
                        new List<DistrictModel>(SelectedCity.Districts.Where(x => x.Id.Equals(curUser.District.Id)));
                    if (mapDistricts.Count > 0)
                        Device.StartTimer(TimeSpan.FromMilliseconds(DelayInMilliSec), () =>
                        {
                            SelectedDistrict = mapDistricts[0];
                            return false;
                        });
                    else
                        UserDialogs.Instance.Alert(
                            AppResources.user_district_deleted);
                }
                else
                    UserDialogs.Instance.Alert(AppResources.user_city_deleted);
            }
            else
            {
                SelectedCity = ListCity[0];
            }
        }

        public async void GetSystemConfig()
        {
            bool isShowMessage = SystemConfig.PromotionCount == 0 && SystemConfig.MaxCheckupPerAccountPerDay == 0 &&
                SystemConfig.MaxRegistrationLimitPerDevice == 0;
            try
            {
                try { if (isShowMessage) Common.ShowLoading(); } catch { }
                SystemConfig = await _commonWs.GetSystemConfig();
                try { if (isShowMessage) UserDialogs.Instance.HideLoading(); } catch { }
            }
            catch (Exception e)
            {
                if (isShowMessage) await Common.AlertAsync(e.Message);
            }

            LoginViewModel.Instance.FirePromotionCountChanged();
        }

        public async Task GetSystemConfigAsync()
        {
            bool isShowMessage = SystemConfig.PromotionCount == 0 && SystemConfig.MaxCheckupPerAccountPerDay == 0 &&
                SystemConfig.MaxRegistrationLimitPerDevice == 0;
            try
            {
                try { if (isShowMessage) Common.ShowLoading(); } catch { }
                SystemConfig = await _commonWs.GetSystemConfig();
                try { if (isShowMessage) UserDialogs.Instance.HideLoading(); } catch { }
            }
            catch (Exception e)
            {
                if (isShowMessage) await Common.AlertAsync(e.Message);
            }

            LoginViewModel.Instance.FirePromotionCountChanged();
        }

        public void ResetOtp()
        {
            PhoneNumberOtp = Otp = "";
        }

        public async Task SendOtp()
        {
            try
            {
                Common.ShowLoading();
                var validator = new CommonValidator();
                var result = validator.ValidateSendOtp(PhoneNumberOtp, Otp);

                if (!result.IsValid)
                {
                    await Common.AlertAsync(result.Errors[0]);
                    return;
                }

                await _commonWs.SendOtp(PhoneNumberOtp, Otp);
                await Common.AlertAsync(AppResources.send_otp_success);
                ResetOtp();
                NavigationService.ReplaceRootPage(typeof(LoginPage));
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
        }
        #endregion
    }
}