using System.Windows.Input;
using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using HealthCare.DependencyServices;
using HealthCare.Helpers;
using HealthCare.Resx;
using HealthCare.Services.Interfaces;
using Xamarin.Forms;

namespace HealthCare.ViewModels
{
    public class TopBarViewModel : BaseViewModel<TopBarViewModel>
    {
        private bool _isHaveTitle;
        private bool _isVisible;
        private string _servicePhoneNo;
        private Color _textColor;
        private string _title;
        private ICommand _phoneCallCommand;

        public TopBarViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            IsVisible = true;
            IsHaveTitle = true;
            ServicePhoneNo = "1800 1090";
            TextColor = Color.Black;
        }

        public bool IsHaveTitle
        {
            set
            {
                _isHaveTitle = value;
                RaisePropertyChanged();
            }
            get
            {
                return Common.OS == TargetPlatform.WinPhone && !string.IsNullOrWhiteSpace(Title)
                       && _isHaveTitle;
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged("Title");
            }
        }

        public string ServicePhoneNo
        {
            get { return _servicePhoneNo; }
            set
            {
                _servicePhoneNo = value;
                RaisePropertyChanged("ServicePhoneNo");
            }
        }

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                RaisePropertyChanged("IsVisible");
            }
        }

        public Color TextColor
        {
            get { return _textColor; }
            set
            {
                _textColor = value;
                RaisePropertyChanged("TextColor");
            }
        }

        public ICommand PhoneCallCommand
        {
            get
            {
                return _phoneCallCommand ??(_phoneCallCommand = new RelayCommand(async () =>
                    {
                        if (Common.OS == TargetPlatform.WinPhone)
                        {
                            if (!DependencyService.Get<IDialPhone>().MakePhoneCall("08 6274 7451"))
                            {
                                await Common.AlertAsync(AppResources.phone_call_fail);
                            }
                        }
                        else if (await Common.ConfirmAsync(AppResources.diall_confirm,AppResources.yes,AppResources.no))
                        {
                            if (!DependencyService.Get<IDialPhone>().MakePhoneCall("08 6274 7451"))
                            {
                                await Common.AlertAsync(AppResources.phone_call_fail);
                            }
                        }
                    }));
            }
        }
    }
}