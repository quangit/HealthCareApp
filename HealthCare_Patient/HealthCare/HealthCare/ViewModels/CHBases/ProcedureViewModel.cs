using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using HealthCare.DependencyServices;
using HealthCare.Helpers;
using HealthCare.Models;
using HealthCare.Models.ChBaseModel;
using HealthCare.Pages.CHBases;
using HealthCare.Resx;
using HealthCare.Services.Interfaces;
using HealthCare.Validators.CHBasesValidator;
using HealthCare.WebServices.Interfaces;
using Media.Plugin;
using Media.Plugin.Abstractions;
using Xamarin.Forms;

namespace HealthCare.ViewModels.CHBases
{
    public class ProcedureViewModel : BaseViewModel<ProcedureViewModel>
    {
        private readonly IChBaseWS _chBaseWs;
        private bool _canGoBack;
        private MediaFile _file;
        private ICommand _getimage;
        private ImageSource _imageSource;

        ObservableCollection<GalleryImage> _images = new ObservableCollection<GalleryImage>();

        public ProcedureViewModel(INavigationService navigationService, ProcedureValidator procedureValidator, IChBaseWS chBaseWs) : base(navigationService)
        {
            _chBaseWs = chBaseWs;
            _canGoBack = true;
            _procedureValidator = procedureValidator;
            ClearViewModel();
        }


        public byte[] AvatarByteArray { get; set; }
        public string FileName
        {
            get
            {
                if (_file != null) return _file.Path;
                return "";
            }
        }
        public ImageSource ImageSource
        {
            get
            {
                return _imageSource ??
                       (_imageSource = new FileImageSource {File = "ic_avatar_placeholder.png"});
            }
            set
            {
                _imageSource = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<string> ListImage { get; set; }

        public ObservableCollection<GalleryImage> Images
        {
            get { return _images; }
            set
            {
                _images = value;
                RaisePropertyChanged();
            }
        }

        public ICommand GetImage
            => _getimage ?? (_getimage = new RelayCommand(GetImageProcedure));

        #region Properties

        public ObservableCollection<ProcedureModel> ListProcedure
        {
            get { return _listProcedure; }
            set
            {
                _listProcedure = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        public void SetImageByByteArray(byte[] avatarBytes)
        {
            if (avatarBytes == null) return;


            AvatarByteArray = null;
            //this for avatar
            AvatarByteArray = DependencyService.Get<IImageResize>().ResizeImageAvatar(avatarBytes, 256, 256);

            //if (Common.OS == TargetPlatform.Android)
            //    AvatarByteArray = DependencyService.Get<IPhotoEdit>().CorrectOrientation(AvatarByteArray);

            var imgSrc = ImageSource.FromStream(() =>
            {
                //var stream = file.GetStream();
                Stream stream = new MemoryStream(AvatarByteArray);
                return stream;
            });

            var weakRef = new WeakReference(imgSrc);

            ImageSource = (ImageSource) weakRef.Target;
        }

        public async void GetImageProcedure()
        {
            //NavigationService.NavigateTo(typeof(CropPage));
            var photoFolder = Common.OnPlatform("", "", "Camera Roll");
            var fileName = string.Format("HC_{0}.jpg", DateTime.Now.ToString("MMddyy_Hmmss"));

            _canGoBack = false;

            var result = await UserDialogs.Instance.ActionSheetAsync(
                AppResources.choose_a_photo,
                //Bug: 2 cancel and destuctive in WP have terrible style, don't show its in WP
                Common.OS != TargetPlatform.WinPhone ? AppResources.cancel : null,
                null, AppResources.pick_photo, AppResources.take_photo);

            _canGoBack = true; //enable goback if press hardware back button 

            try
            {
                var isPhotoPicked = false;
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
                                _file = file;
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
                                _file = file;
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
                       
                        if (await UserDialogs.Instance.ConfirmAsync(AppResources.upload_img))
                        {
                           Common.ShowLoading();
                            var url = await _chBaseWs.UploadFile(AvatarByteArray, fileName);
                            Common.HideLoading();

                            var imageSource = ImageSource.FromStream(() => new MemoryStream(AvatarByteArray));
                            Images.Add(new GalleryImage { ImageId = url, Source = imageSource, OrgImage = AvatarByteArray });

                            ListImage.Add(url);
                        }
                        
                    }
                }

                RaisePropertyChanged("ImageSource");
            }
            catch
            {
                // ignored
            }
        }

        #region Variables

        private ICommand _addProcedure;
        private ObservableCollection<ProcedureModel> _listProcedure;
        private RelayCommand _submitAddCommand;
        private string _symptom;

        public string Symptom
        {
            get { return _symptom; }
            set
            {
                _symptom = value;
                RaisePropertyChanged();
            }
        }
        private readonly ProcedureValidator _procedureValidator;
        private ProcedureModel _procedureAdding;
        public ProcedureModel ProcedureAdding
        {
            get { return _procedureAdding; }
            set
            {
                _procedureAdding = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Commands

        public RelayCommand GoToAddProcedurePageCommand => new RelayCommand(() =>
        {
            ProcedureAdding = new ProcedureModel();
            ProcedureAdding.SecondaryProvider = new ProviderModel();
            ProcedureAdding.PrimaryProvider = new ProviderModel();
            NavigationService.NavigateTo(typeof(AddProcedurePage));
        });

        public RelayCommand GetListProcedureCommand => new RelayCommand(GetListProcedure);
        public RelayCommand<string> RemoveImageCommand => new RelayCommand<string>(RemoveImage);

       
        public RelayCommand SubmitAddCommand => _submitAddCommand ??
                                             (_submitAddCommand = new RelayCommand(Submit));

        public RelayCommand<ProcedureModel> GoToDetailPageCommand => new RelayCommand<ProcedureModel>(i =>
        {
            if (i != null)
            {

                string note = i.SecondaryProvider.Name;
                var images = ChBaseHelper.GetListImages(note);
                CHBaseDetailViewModel.Instance.Images = new ObservableCollection<GalleryImage>();
                if (images.Count > 0)
                {
                    foreach (var image in images)
                    {
                        CHBaseDetailViewModel.Instance.Images.Add(new GalleryImage { ImageId = image, Source = null, OrgImage = null });
                        note = note.Replace(image + "<br>", "");
                    }
                }
                note = note.Replace("<br>","");
                var detailInfo = new ObservableCollection<CHBaseDetailUIModel>
                {
                    new CHBaseDetailUIModel
                    {
                        Value = i.Name,
                        Type = CHBaseDetailTypeEnum.OneLineOneText
                    },
                    new CHBaseDetailUIModel
                    {
                        Value = i.When.ToString("d"),
                        Type = CHBaseDetailTypeEnum.OneLineOneText
                    },
                    new CHBaseDetailUIModel
                    {
                        Title = AppResources.procedure_name,
                        Value = i.Name,
                        Type = CHBaseDetailTypeEnum.TwoLine
                    },
                    new CHBaseDetailUIModel
                    {
                        Title = AppResources.location,
                        Value = i.Location,
                        Type = CHBaseDetailTypeEnum.OneLine
                    },
                    new CHBaseDetailUIModel
                    {
                        Title = AppResources.provider,
                        Value = i.PrimaryProvider.Name,
                        Type = CHBaseDetailTypeEnum.OneLine
                    },
                    new CHBaseDetailUIModel
                    {
                        Title = AppResources.role,
                        Value = i.PrimaryProvider.Type,
                        Type = CHBaseDetailTypeEnum.OneLine
                    },
                    new CHBaseDetailUIModel
                    {
                        Title = AppResources.note,
                        Value = note,
                        Type = CHBaseDetailTypeEnum.OneLine
                    }
                };
                var deleteAction = new Action(async () =>
                {
                    if (await Common.ConfirmAsync(Resx.AppResources.confirm_del_procedure))
                    {
                        Common.ShowLoading();
                        if (await _chBaseWs.RemoveData(i.Id))
                        {
                            // delete success 
                            ListProcedure = await _chBaseWs.GetProcedure();
                            NavigationService.GoBack();
                            Common.HideLoading();
                        }
                        else
                        {
                            Common.HideLoading();
                            //Error
                        }
                    }
                });

                CHBaseDetailViewModel.Instance.GoToDetailPage(AppResources.procedure, detailInfo, deleteAction, true);
            }
        });

        #endregion

        #region Methods

        private void ClearViewModel()
        {
            Images = new ObservableCollection<GalleryImage>();
            ListImage = new ObservableCollection<string>();
            ListProcedure = new ObservableCollection<ProcedureModel>();
        }
        private void RemoveImage(string id)
        {
            Images.Remove(Images.FirstOrDefault(image => image.ImageId == id));
            ListImage.Remove(id);
            RaisePropertyChanged("Images");

        }
        public async void GetListProcedure()
        {
            try
            {
                Common.ShowLoading();
                ListProcedure = await _chBaseWs.GetProcedure();
                Common.HideLoading();
            }
            catch (Exception e)
            {
                Common.ShowMessage(e.Message);
            }
        }

        public async void Submit()
        {
            try
            {
                if (Common.OS == TargetPlatform.iOS)
                    Symptom = AppConstant.SymptomIOS;

                var result = _procedureValidator.ValidateAddMedication(ProcedureAdding);
                if (!result.IsValid)
                {
                    UserDialogs.Instance.Alert(result.Errors[0]);
                    return;
                }
                string note = ProcedureAdding.SecondaryProvider.Name;
                note += "&lt;br&gt;";
                 note = ListImage.Aggregate(note, (current, item) => current + item + "&lt;br&gt;");
                ProcedureAdding.SecondaryProvider.Name = note;
                Common.ShowLoading();
                ProcedureAdding.When = DateTime.Now;
                var isSuccess = await _chBaseWs.AddProcedure(ProcedureAdding);
                if (isSuccess)
                {
                    ListProcedure = await _chBaseWs.GetProcedure();
                    NavigationService.GoBack();
                    Common.HideLoading();
                }
                else
                {
                    Common.HideLoading();
                    // Something wrong;
                }
                ListImage.Clear();
            }
            catch (Exception e)
            {
                await Common.AlertAsync(e.Message);
            }
            UserDialogs.Instance.HideLoading();
        }

        #endregion
    }
}