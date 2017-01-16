using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using HealthCare.Services.Interfaces;
using HealthCare.Helpers;
using HealthCare.Resx;
using Xamarin.Forms;

namespace HealthCare.ViewModels
{
    public class PhotoEditViewModel : BaseViewModel<PhotoEditViewModel>
    {
        private RelayCommand _cropCommand;
        private RelayCommand _cancelCommand;

        #region Properties

        public RelayCommand CropCommand => _cropCommand ??
                                          (_cropCommand = new RelayCommand(() =>
                                          {
                                              CropEvent?.Invoke(this, EventArgs.Empty);
                                          }));

        public RelayCommand CancelCommand => _cancelCommand ??
                                        (_cancelCommand = new RelayCommand(GoBack));

        #endregion

        #region Events

        public event EventHandler CropEvent;

        #endregion

        #region Methods

        public void CreateToolBar(Page p)
        {
            if (Common.OS == TargetPlatform.Android) return;
            p.ToolbarItems.Clear();
            p.ToolbarItems.Add(new ToolbarItem
            {
                Text = AppResources.crop,
                Icon = new FileImageSource { File = "crop.png" },
                Command = CropCommand
            });
            p.ToolbarItems.Add(new ToolbarItem
            {
                Text = AppResources.cancel,
                Icon = new FileImageSource { File = "cancel.png" },
                Command = CancelCommand
            });
        }

        public async void GoBack()
        {
            CropEvent = null;
            if (Common.OS == TargetPlatform.WinPhone)
            {
                Common.ShowLoading();
                await Task.Delay(100);
                UserDialogs.Instance.HideLoading();
            }
            NavigationService.GoBack();
        }
        #endregion

        public PhotoEditViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
