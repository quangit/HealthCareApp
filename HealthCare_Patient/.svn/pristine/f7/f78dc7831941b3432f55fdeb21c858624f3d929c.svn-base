using GalaSoft.MvvmLight;
using HealthCare.Helpers;
using HealthCare.Services.Interfaces;
using Xamarin.Forms;

namespace HealthCare.ViewModels
{
    public class BaseViewModel<T> : ViewModelBase where T : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public static T Instance => SimpleIocV2.Default.GetInstance<T>();

        protected INavigationService NavigationService
        {
            get
            {
                if (_navigationService != null)
                {
                    if (!_navigationService.IsInitialized &&
                        Application.Current.MainPage != null && Application.Current.MainPage is NavigationPage)
                    {
                        _navigationService.Initialize((NavigationPage) Application.Current.MainPage);
                        return _navigationService;
                    }
                    return _navigationService;
                }
                return null;
            }
        }
    }
}