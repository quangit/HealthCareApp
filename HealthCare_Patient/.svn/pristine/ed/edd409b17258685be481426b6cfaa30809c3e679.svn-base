using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using GalaSoft.MvvmLight.Command;
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
using Xamarin.Forms;
using ImageSource = Xamarin.Forms.ImageSource;

namespace HealthCare.ViewModels
{
    public class AskDoctorViewModel : BaseViewModel<AskDoctorViewModel>
    {
        private readonly ICommonWS _commonWS;
        private RelayCommand _askCommand;
        private readonly CommonValidator _validator;
        private string _symptom, _attachPhoto;
        private ImageSource _imageSource;
        private RelayCommand _submitCommand, _attachCommand, _removeAttachCommand;
        private bool _symptomChange = true, _canGoBack, _removeAttach;
        public byte[] AttachByteArray { get; set; }
        private SupportQuestionModel _supportQuestionSelectedItem;
        private RelayCommand _goBackCommand;
        private bool _shared;

        public bool Shared
        {
            get { return _shared; }
            set { _shared = value; RaisePropertyChanged(); }
        }

        public SupportQuestionModel SupportQuestionSelectedItem
        {
            get { return _supportQuestionSelectedItem; }
            set { _supportQuestionSelectedItem = value; RaisePropertyChanged(); }
        }
        public AskDoctorViewModel(INavigationService navigationService, ICommonWS commonWS, CommonValidator validator) : base(navigationService)
        {
            _commonWS = commonWS;
            _validator = validator;
            SupportQuestionSelectedItem = MoreSupportViewModel2.Instance.SupportQuestionSelectedItem;
            AttachPhoto = AppResources.attach_photo;
            ImageSource = ImageSource.FromFile("cameraIcon.png");
        }
        public RelayCommand AskCommand => _askCommand ??
                                          (_askCommand = new RelayCommand(Ask));

        public async Task<bool> CheckSkypeExist()
        {
            if (string.IsNullOrEmpty(UserViewModel.Instance.CurrentUser.SkypeId))
            {
                if (await Common.ConfirmAsync(AppResources.setting_skype, "Ok", AppResources.skip))
                {
                    var userVm = UserViewModel.Instance;
                    userVm.CloneCurrentUser = userVm.CurrentUser.Clone();
                    userVm.SetAvatarResourceUrl(userVm.CloneCurrentUser.Photo);
                    NavigationService.NavigateTo(typeof(ProfilePage));
                }

                return false;
            }
            else
            {
                return true;
            }
        }

        private async void Ask()
        {
            if (await CheckSkypeExist())
            {
                NavigationService.NavigateTo(typeof(AddQuestionPage));
                ImageSource = ImageSource.FromFile("cameraIcon.png");
            }

        }
        public bool SymptomChange
        {
            get { return _symptomChange; }
            set
            {
                _symptomChange = value;
                RaisePropertyChanged("SymptomChange");
            }
        }
        public bool IsShowRemoveAttach
        {
            get { return _removeAttach; }
            set
            {
                _removeAttach = value;
                RaisePropertyChanged("IsShowRemoveAttach");
            }
        }

        public string Symptom
        {
            get { return _symptom; }
            set
            {
                _symptom = value;
                //SymptomChange = value.Length <= 0;
                RaisePropertyChanged();
            }
        }
        public string AttachPhoto
        {
            get { return _attachPhoto; }
            set
            {
                _attachPhoto = value;
                RaisePropertyChanged("AttachPhoto");
            }
        }
        public ImageSource ImageSource  
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                RaisePropertyChanged();
            }   
        }

        public RelayCommand SubmitCommand => _submitCommand ??
                                             (_submitCommand = new RelayCommand(Submit));
        public RelayCommand GoCommand => _goBackCommand ??
                                             (_goBackCommand = new RelayCommand(GoBack));
        public RelayCommand AttachCommand => _attachCommand ??
                                             (_attachCommand = new RelayCommand(Attach));
        public RelayCommand RemoveAttachCommand => _removeAttachCommand ??
                                             (_removeAttachCommand = new RelayCommand(Remove));

        private void Remove()
        {
            IsShowRemoveAttach = false;
            AttachPhoto = AppResources.attach_photo;
            AttachByteArray = null;
            ImageSource = ImageSource.FromFile("cameraIcon.png");
        }

        public async void Attach()
        {
            var photoFolder = Common.OnPlatform("", "", "Camera Roll");
            var fileName = string.Format("HC_{0}.jpg", DateTime.Now.ToString("MMddyy_Hmmss"));

            _canGoBack = false;

            var result = await UserDialogs.Instance.ActionSheetAsync(
                AppResources.attach,
                //Bug: 2 cancel and destuctive in WP have terrible style, don't show its in WP
                Common.OS != TargetPlatform.WinPhone ? AppResources.cancel : null,
                null, AppResources.pick_photo, AppResources.take_photo);

            _canGoBack = true; //enable goback if press hardware back button 

            try
            {
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

                                AttachByteArray = DependencyService.Get<IImageResize>().ResizeImage(byteArray, (float)AppConstant.ScreenWidth, (float)(AppConstant.ScreenWidth * 9 / 16));
                                //AttachByteArray = byteArray;
                                ImageSource = null;

                                if (file.Path.Contains("\\"))
                                {
                                    var temp = file.Path.Split('\\');
                                    AttachPhoto = temp[temp.Length - 1];
                                }
                                else if (file.Path.Contains("/"))
                                {
                                    var temp = file.Path.Split('/');
                                    AttachPhoto = temp[temp.Length - 1];
                                }
                                else
                                {
                                    AttachPhoto = file.Path;
                                }

                                //Stream stream = new MemoryStream(byteArray);
                                //ImageSource = ImageSource.FromStream(() => stream);
                                ImageSource = ImageSource.FromStream(() =>
                               {
                                   //var stream = file.GetStream();
                                   Stream stream = new MemoryStream(AttachByteArray);
                                   return stream;
                               });
                                IsShowRemoveAttach = true;

                                //_attachImageSource = null;

                                //_attachImageSource = ImageSource.FromStream(() =>
                                //{
                                //    //var stream = file.GetStream();
                                //    Stream stream = new MemoryStream(AttachByteArray);
                                //    return stream;
                                //});
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
                                AttachByteArray = DependencyService.Get<IImageResize>().ResizeImage(byteArray, (float)AppConstant.ScreenWidth, (float)(AppConstant.ScreenWidth * 9 / 16));
                                //AttachByteArray = byteArray;
                                ImageSource = null;
                                //AttachByteArray = DependencyService.Get<IImageResize>().ResizeImage(byteArray, 256, 256);
                                AttachPhoto = fileName;
                                //Stream stream = new MemoryStream(byteArray);
                                //ImageSource = ImageSource.FromStream(() => stream);

                                ImageSource = ImageSource.FromStream(() =>
                                {
                                    //var stream = file.GetStream();
                                    Stream stream = new MemoryStream(AttachByteArray);
                                    return stream;
                                });

                                IsShowRemoveAttach = true;
                                //_attachImageSource = null;
                                //_attachImageSource = ImageSource.FromStream(() =>
                                //{
                                //    //var stream = file.GetStream();
                                //    Stream stream = new MemoryStream(AttachByteArray);
                                //    return stream;
                                //});
                            }
                        }
                    }
                }

                //RaisePropertyChanged("AvatarImageSource");
            }
            catch
            {
            }
        }

        public void GoBack()
        {
            Shared = false;
            ResetAvatarResource();
            ClearViewModel();
            NavigationService.GoBack();
        }
        public async void Submit()
        {
            try
            {
                if (Common.OS == TargetPlatform.iOS)
                    Symptom = AppConstant.SymptomIOS;

                var result = _validator.ValidateMoreSupport(!string.IsNullOrWhiteSpace(Symptom) && Symptom.Equals(AppResources.if_question_vn) ? "" : Symptom);
                if (!result.IsValid)
                {
                    UserDialogs.Instance.Alert(result.Errors[0]);
                    return;
                }
                Common.ShowLoading();
                //await _commonWS.SendAdditionalSupport(PersonModel.FirstName, PersonModel.LastName, Age, Gender,
                //    PersonModel.Email, DoctorName, HospitalName, Symptom);
                await _commonWS.SendAdditionalSupport(Symptom, AttachByteArray, Shared);
                await Common.AlertAsync(AppResources.question_sent);

                ResetAvatarResource();
                ClearViewModel();

                NavigationService.GoBack();

                await MoreSupportViewModel2.Instance.GetQuestionList(true, true);
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
            UserDialogs.Instance.HideLoading();
        }

        public void ResetAvatarResource()
        {
            ImageSource = null;
            AttachByteArray = null;
        }

        private void ClearViewModel()
        {
            //PersonModel = new PersonModel();
            //Age = default(string);
            //DoctorName = default(string);
            //HospitalName = default(string);
            Shared = false;
            Symptom = default(string);
            AttachPhoto = AppResources.attach_photo;
            IsShowRemoveAttach = false;
        }

    }
}
