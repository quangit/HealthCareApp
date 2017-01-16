using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using ExifLib;
using GalaSoft.MvvmLight.Command;
using HealthCare.Controls;
using HealthCare.DependencyServices;
using HealthCare.Enums;
using HealthCare.Helpers;
using HealthCare.Models;
using HealthCare.Pages;
using HealthCare.Resx;
using HealthCare.Services.Interfaces;
using HealthCare.Validators;
using HealthCare.WebServices.Interfaces;
using Media.Plugin;
using Media.Plugin.Abstractions;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace HealthCare.ViewModels
{
    public class UserViewModel : BaseViewModel<UserViewModel>
    {
        private readonly PersonValidator _personValidator;
        private readonly IUserWS _userWs;
        private readonly IChBaseWS _chBaseWs;
        private ImageSource _avatarImageSource;
        private bool _canGoBack;
        private UserModel _currentUser, _cloneCurrentUser;
        private bool _isAddRelatedAccount;
        private ObservableCollection<UserModel> _relatedAccounts;
        private ICommand _resetUserCommand, _registerCommand, _avatarTappedCommand;
        private RelayCommand<ImageRounderCorner> _saveProfileCommand;
        private RelayCommand _changePwdCommand;
        private UserModel _selectedRelatedAccount;
        private ICommand _selectedRelatedAccountCommand;
        public UserViewModel(INavigationService ns, IUserWS userWs, IChBaseWS chBaseWs, PersonValidator personValidator) : base(ns)
        {
            _userWs = userWs;
            _chBaseWs = chBaseWs;
            _personValidator = personValidator;
            ResetUser();
            _canGoBack = true;
        }

        #region Properties

        public string AppResoucesCrop = AppResources.crop;
        public string AppResoucesCancel = AppResources.cancel;
        public string AppResoucesRotate = AppResources.rotate;
        public UserModel CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                RaisePropertyChanged("CurrentUser");
            }
        }

        public Uri HealthVaultUri
        {
            get
            {
                var url = "http://chbase.bacsi24x7.vn/?lang=";
                if (Common.GetDeviceLanguage() == "vi")
                {
                    url += "vi";
                }
                else
                {
                    url += "en";
                }
                return new Uri(url);
            }
        }
        public Uri RegHealthVaultUri
        {
            get
            {
                var url = AppConstant.ChBaseRegUrl;
                if (Common.GetDeviceLanguage() == "vi")
                {
                    url += "?lcid=1066";
                }
                else
                {
                    url += "?lcid=1033";
                }
                return new Uri(url);
            }
        }
        public bool CanInputPassword { get; set; }

        public UserModel CloneCurrentUser
        {
            get { return _cloneCurrentUser; }
            set
            {
                _cloneCurrentUser = value;
                RaisePropertyChanged("CloneCurrentUser");
            }
        }

        public ObservableCollection<CityModel> CloneListCity { get; set; }

        public ObservableCollection<UserModel> RelatedAccounts
        {
            get { return _relatedAccounts; }
            set
            {
                _relatedAccounts = value;
                RaisePropertyChanged("RelatedAccounts");
                SelectedRelatedAccount = (value != null) ? value[0] : null;
            }
        }


        private ObservableCollection<UserModel> _relatedAccountsHistorical;

        public ObservableCollection<UserModel> RelatedAccountsHistorical
        {
            get
            {
                var l = new ObservableCollection<UserModel>(RelatedAccounts ?? new ObservableCollection<UserModel>());
                var currentUserClone = CurrentUser.Clone();
                currentUserClone.FirstName = AppResources.checkup_myselft;
                currentUserClone.LastName = "";
                l.Insert(0, currentUserClone);
                return l;
            }
        }

        public ObservableCollection<UserModel> AddCheckupRelatedAccounts
        {
            get
            {
                var l = new ObservableCollection<UserModel>(RelatedAccounts);
                var currentUserClone = CurrentUser.Clone();
                currentUserClone.FirstName = AppResources.checkup_myselft;
                currentUserClone.LastName = "";

                NewRelatedAccount = new UserModel { FirstName = AppResources.patient_other };

                l.Insert(0, currentUserClone);
                l.Insert(l.Count, NewRelatedAccount);
                return l;
            }
        }

        public UserModel NewRelatedAccount { get; set; }

        public UserModel SelectedRelatedAccount
        {
            get { return _selectedRelatedAccount; }
            set
            {
                _selectedRelatedAccount = value;
                if (value != null && string.IsNullOrWhiteSpace(value.UserCode)) //usercode empty is Other
                {
                    _isAddRelatedAccount = true;
                }
                else
                    _isAddRelatedAccount = false;
                RaisePropertyChanged();
                RaisePropertyChanged("IsAddRelatedAccount");
            }
        }

        public ObservableCollection<Gender> ListGender => new ObservableCollection<Gender> { Gender.Female, Gender.Male };

        public ObservableCollection<MaritalStatus> ListMaritalStatus =>
            new ObservableCollection<MaritalStatus> { MaritalStatus.Single, MaritalStatus.Married };

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

        public byte[] AvatarByteArray { get; set; }

        public bool IsShowChangePwd => string.IsNullOrEmpty(CurrentUser?.FacebookId);

        #endregion

        #region Methods

        public void GoBack()
        {
            NavigationService.GoBack();
        }

        public async void UpdateProfile(ImageRounderCorner image)
        {
            try
            {
                Common.ShowLoading();
                var commonVm = CommonViewModel.Instance;
                CloneCurrentUser.City = commonVm.SelectedCity;
                CloneCurrentUser.District = commonVm.SelectedDistrict;
                CurrentUser.CityId = commonVm.SelectedCity.Id;
                CurrentUser.DistrictId = commonVm.SelectedDistrict.Id;

                var validateResult = _personValidator.ValidateForProfile(CloneCurrentUser);
                if (!validateResult.IsValid)
                {
                    await Common.AlertAsync(validateResult.Errors[0]);
                    return;
                }
                if (!string.IsNullOrEmpty(CloneCurrentUser.SkypeId))
                {
                    CloneCurrentUser.SkypeId = CloneCurrentUser.SkypeId.Trim();
                    if (CloneCurrentUser.SkypeId.Contains(" "))
                    {
                        await Common.AlertAsync(AppResources.invalid_skype);
                        return;
                    }

                    if (!await _userWs.ValidateSkype(CloneCurrentUser.SkypeId))
                    {
                        await Common.AlertAsync(AppResources.invalid_skype);
                        return;
                    }
                }
                var userData = await _userWs.EditProfile(CloneCurrentUser, AvatarByteArray);

                image.ClearValue(Image.SourceProperty);
                image.Source = null;

                SaveUserData(userData);
                ResetAvatarResource();
                NavigationService.GoBack();
                await Common.AlertAsync(AppResources.profile_update_success);
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }

            UserDialogs.Instance.HideLoading();
        }

        public void ChangeUserPassword()
        {
            var passwordVm = SimpleIocV2.Default.GetInstance<PasswordViewModel>();
            passwordVm.SetStylePasswordPage(PasswordViewModel.ChangeUserPwdStyle);
            NavigationService.NavigateTo(typeof(PasswordPage));
        }

        public void ResetUser()
        {
            CurrentUser = new UserModel { BirthDay = DateTime.Now.AddYears(-30) };
        }

        public void ResetAvatarResource()
        {
            AvatarImageSource = null;
            AvatarByteArray = null;
        }

        public void SetAvatarResourceUrl(string avatarUrl)
        {
            ResetAvatarResource();
            ImageSource imgSrc = null;
            if (!string.IsNullOrWhiteSpace(avatarUrl))
                imgSrc = new UriImageSource
                {
                    Uri = new Uri(avatarUrl),
                    CachingEnabled = true,
                    CacheValidity = new TimeSpan(7, 0, 0, 0)
                };
            else
                imgSrc = new FileImageSource { File = "ic_avatar_placeholder.png" };

            var weakRef = new WeakReference(imgSrc);

            AvatarImageSource = (ImageSource)weakRef.Target;

            //string.IsNullOrWhiteSpace(avatarUrl)
            //    ? new FileImageSource { File = "ic_avatar_placeholder.png" }
            //    : ImageSource.FromUri(new Uri(avatarUrl, UriKind.Absolute));
        }

        public void SetAvatarByByteArray(byte[] avatarBytes)
        {
            if (avatarBytes == null) return;

            ResetAvatarResource();

            //this for avatar
            AvatarByteArray = DependencyService.Get<IImageResize>().ResizeImageAvatar(avatarBytes, 256, 256);

            //if (Common.OS == TargetPlatform.Android)
            //    AvatarByteArray = DependencyService.Get<IPhotoEdit>().CorrectOrientation(AvatarByteArray);

            ImageSource imgSrc = ImageSource.FromStream(() =>
            {
                //var stream = file.GetStream();
                Stream stream = new MemoryStream(AvatarByteArray);
                return stream;
            });

            var weakRef = new WeakReference(imgSrc);

            AvatarImageSource = (ImageSource)weakRef.Target;
        }

        /// <summary>
        ///     FacebookId = null is register without fb
        ///     FacebookId != null is register with fb
        /// </summary>
        /// <param name="user"></param>
        public async Task Register(UserModel user)
        {
            try
            {
                //var commonVm = CommonViewModel.Instance;
                //CurrentUser.City = commonVm.SelectedCity;
                //CurrentUser.District = commonVm.SelectedDistrict;
                //CurrentUser.CityId = commonVm.SelectedCity.Id;
                //CurrentUser.DistrictId = commonVm.SelectedDistrict.Id;

                var validateResult = CanInputPassword ?
                 _personValidator.ValidateWithVerifyPassword(CurrentUser)
                 : _personValidator.ValidateWithoutPassword(CurrentUser);

                if (!validateResult.IsValid)
                {
                    await Common.AlertAsync(validateResult.Errors[0]);
                    return;

                }

                if (string.IsNullOrWhiteSpace(CurrentUser.FacebookId))
                    CurrentUser = await _userWs.Register(user, AvatarByteArray);
                else
                    CurrentUser = await _userWs.RegisterWithFb(user, AvatarByteArray);

                ResetAvatarResource();

                //increase the user register success count for this device
                Settings.RegisterSuccessCount++;

                if (CurrentUser.ActiveType == ActiveType.Sms)
                {
                    if (
                        await
                            Common.ConfirmAsync(AppResources.active_by_otp_reg, AppResources.active, AppResources.skip))
                    {
                        NavigationService.NavigateTo(typeof(OTPPage));
                    }
                    else
                    {
                        NavigationService.GoBack();
                    }
                }
                else
                {
                    await Common.AlertAsync(AppResources.active_by_email);
                    NavigationService.GoBack();
                }
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
        }

        /// <summary>
        /// User must have Email and DOB
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task TryToRegisterChBase(UserModel user)
        {
            try
            {
                if (CheckupViewModel.Instance.GetCountPaidCheckup() == 1 && !string.IsNullOrWhiteSpace(user.Email) &&
                    user.IsBirthDaySet)
                {
                    await _chBaseWs.Register(user);
                    Debug.WriteLine("Auto Register CHBase success");
                }
            }
            catch (Exception e)
            {
                //await Common.AlertAsync(e.Message);
            }
        }

        public bool IsUserAddressCompleted()
        {
            return !string.IsNullOrWhiteSpace(CurrentUser?.Address) &&
                   !string.IsNullOrWhiteSpace(CurrentUser?.CityId) &&
                   !string.IsNullOrWhiteSpace(CurrentUser?.DistrictId);
        }

        public void SaveUseInfo(UserModel currentUser, string sessionId = null, string sessionExpired = null)
        {
            Settings.CurrentUser = currentUser;
            if (!string.IsNullOrWhiteSpace(sessionId))
                Settings.SessionId = sessionId;
            if (!string.IsNullOrWhiteSpace(sessionExpired))
                Settings.SessionExpired = Convert.ToInt64(sessionExpired);
            Settings.UserId = currentUser.Id;

            Debug.WriteLine("SessionId: " + Settings.SessionId);
            Debug.WriteLine("UserId: " + Settings.UserId);
        }

        /// <summary>
        ///     Handle hardware back button on WP and Android when ActionSheet is show
        /// </summary>
        public void HandleHardwareBackButton()
        {
            if (_canGoBack)
            {
                NavigationService.GoBack();
            }
            else
            {
                _canGoBack = true;
            }
        }

        public async Task RefreshData(bool isShowLoading)
        {
            if (isShowLoading)
                Common.ShowLoading();
            try
            {
                //GC.Collect();
                SearchViewModel.Instance.ResetSearch();
                var response = await _userWs.RefreshData();
                await LoginViewModel.Instance.ParseDataFromLogin(response, false);
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
            if (isShowLoading)
                UserDialogs.Instance.HideLoading();
        }

        public void SaveUserData(UserModel userData)
        {
            CurrentUser = userData;
            SaveUseInfo(userData);
        }

        public async void GetAvatar()
        {
            //NavigationService.NavigateTo(typeof(CropPage));
            var photoFolder = Common.OnPlatform("", "", "Camera Roll");
            var fileName = string.Format("HC_{0}.jpg", DateTime.Now.ToString("MMddyy_Hmmss"));

            _canGoBack = false;

            var result = await UserDialogs.Instance.ActionSheetAsync(
                AppResources.create_avatar,
                //Bug: 2 cancel and destuctive in WP have terrible style, don't show its in WP
                Common.OS != TargetPlatform.WinPhone ? AppResources.cancel : null,
                null, AppResources.pick_photo, AppResources.take_photo);

            _canGoBack = true; //enable goback if press hardware back button 

            try
            {
                bool isPhotoPicked = false;
                if (!string.IsNullOrWhiteSpace(result))
                {
                    if (result.Equals(AppResources.pick_photo))
                    {
                        if (!CrossMedia.Current.IsPickPhotoSupported)
                        {
                            await Common.AlertAsync(AppResources.cannot_access_photo_lib);
                            return;
                        }

                        using (var file = await CrossMedia.Current.PickPhotoAsync())
                        {
                            if (file != null)
                            {
                                var byteArray = file.GetStream().ReadByteArrayFromStream();
                                //AvatarByteArray = DependencyService.Get<IImageResize>().ResizeImageAvatar(byteArray, 256, 256);
                                //SetAvatarByByteArray(byteArray);

                                AvatarByteArray = byteArray;

                                //AvatarImageSource = null;

                                //AvatarImageSource = ImageSource.FromStream(() =>
                                //{
                                //    //var stream = file.GetStream();
                                //    Stream stream = new MemoryStream(AvatarByteArray);
                                //    return stream;
                                //});

                                isPhotoPicked = true;
                            }
                        }
                    }
                    else if (result.Equals(AppResources.take_photo))
                    {
                        if (!CrossMedia.Current.IsCameraAvailable
                            || !CrossMedia.Current.IsTakePhotoSupported)
                        {
                            await Common.AlertAsync(AppResources.not_found_camera);
                            return;
                        }

                        using (var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                        {
                            Directory = photoFolder,
                            Name = fileName
                        }))
                        {
                            if (file != null)
                            {
                                var byteArray = file.GetStream().ReadByteArrayFromStream();

                                //AvatarByteArray = DependencyService.Get<IImageResize>().ResizeImageAvatar(byteArray, 256, 256);
                                //SetAvatarByByteArray(byteArray);

                                AvatarByteArray = byteArray;

                                //_avatarImageSource = null;

                                //_avatarImageSource = ImageSource.FromStream(() =>
                                //{
                                //    //var stream = file.GetStream();
                                //    Stream stream = new MemoryStream(AvatarByteArray);
                                //    return stream;
                                //});

                                isPhotoPicked = true;
                            }
                        }
                    }

                    //call edit image
                    if (isPhotoPicked //skip if user not select any photo
                                      //&& !(AvatarImageSource is FileImageSource)
                        )
                    {
                        //if (AvatarImageSource != null)
                        if (AvatarByteArray != null)
                        {
                            if (Common.OS == TargetPlatform.Android)
                                DependencyService.Get<IPhotoEdit>().LaunchEditorControl(AvatarByteArray);
                            else if (Common.OS == TargetPlatform.WinPhone)
                                NavigationService.NavigateTo(typeof(PhotoEditPage));
                            else SetAvatarByByteArray(AvatarByteArray);
                        }
                    }
                }

                RaisePropertyChanged("AvatarImageSource");
            }
            catch
            {
                // ignored
            }
        }

        #endregion

        #region Commands

        public RelayCommand<ImageRounderCorner> SaveProfileCommand
            => _saveProfileCommand ?? (_saveProfileCommand = new RelayCommand<ImageRounderCorner>(UpdateProfile));

        public RelayCommand ChangePwdCommand
            => _changePwdCommand ?? (_changePwdCommand = new RelayCommand(ChangeUserPassword));

        public ICommand ResetUserCommand => _resetUserCommand ?? (_resetUserCommand = new RelayCommand(ResetUser));

        public ICommand RegisterCommand
        {
            get
            {
                return _registerCommand ?? (_registerCommand = new RelayCommand(async () =>
                {
                    Common.ShowLoading();
                    await Register(CurrentUser);
                    UserDialogs.Instance.HideLoading();
                }));
            }
        }

        public ICommand SelectedRelatedAccountCommand
        {
            get
            {
                return _selectedRelatedAccountCommand ??
                       (_selectedRelatedAccountCommand = new RelayCommand<UserModel>(async u =>
                       {
                           if (u != null)
                           {
                               CheckupViewModel.Instance.ResetLoadMoreHistoricals();
                               Common.ShowLoading();
                               await CheckupViewModel.Instance.LoadMoreHistoricals(u.Id);
                               UserDialogs.Instance.HideLoading();
                           }
                       }));
            }
        }

        public ICommand AvatarTappedCommand
            => _avatarTappedCommand ?? (_avatarTappedCommand = new RelayCommand(GetAvatar));

        #endregion
    }
}