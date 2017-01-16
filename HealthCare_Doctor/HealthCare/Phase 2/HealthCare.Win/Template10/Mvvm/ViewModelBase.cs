using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Newtonsoft.Json;
using Template10.Common;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using HealthCare.Core.Models;
using HealthCare.Core.ViewModels;
using HealthCare.Win.Controls;
using HealthCare.Win.Views;
using Template10.Controls;
using ScheduleView = HealthCare.Win.Views.HomeTab.ScheduleView;

namespace Template10.Mvvm
{
    // DOCS: https://github.com/Windows-XAML/Template10/wiki/Docs-%7C-MVVM
    public abstract class ViewModelBase : BindableBase, INavigable
    {
        private object _parameter;

        public virtual Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            _parameter = parameter;
            if (mode == NavigationMode.New)
                Init();
            if (GetType() == typeof (HomeViewModel))
            {
                if (Shell.Instance != null)
                {
                    Shell.HamburgerMenu.DisplayMode = SplitViewDisplayMode.CompactOverlay;
                    Shell.HamburgerMenu.HamburgerButtonVisibility = Visibility.Visible;
                }
            }
            else
            {
                if (Shell.Instance != null)
                {
                    Shell.HamburgerMenu.DisplayMode = SplitViewDisplayMode.Overlay;
                    Shell.HamburgerMenu.HamburgerButtonVisibility = Visibility.Collapsed;
                }
            }
            return Task.CompletedTask;
        }

        public virtual Task OnNavigatedFromAsync(IDictionary<string, object> pageState, bool suspending)
        {
            return Task.CompletedTask;
        }

        public virtual Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            return Task.CompletedTask;
        }

        [JsonIgnore]
        public INavigationService NavigationService { get; set; }

        [JsonIgnore]
        public IDispatcherWrapper Dispatcher { get; set; }

        [JsonIgnore]
        public IStateItems SessionState { get; set; }

        public void ShowViewModel<T>()
        {
            ShowViewModel<T>(null);
        }

        public void ShowViewModel<T>(object param)
        {
            Data.PageParamAdd(typeof(T).Name, param);
            var type = GetView<T>();
            if (type != null)
            {
                if (Shell.Instance != null)
                {
                    Shell.HamburgerMenu.DisplayMode = SplitViewDisplayMode.Overlay;
                    Shell.HamburgerMenu.HamburgerButtonVisibility = Visibility.Collapsed;
                }
                NavigationService.Navigate(type);
            }
            else
            {

                var currentViewType = GetView(GetType());
               
                
                if (typeof(T) == typeof(HomeViewModel) && currentViewType.GetInterfaces().Contains(typeof(IRoot)))
                {
                    Window.Current.Content = new ModalDialog
                    {
                        DisableBackButtonWhenModal = true,
                        ModalContent = new BusyIndicator(),
                        Content = new Shell(NavigationService)
                    };
                    Window.Current.Activate();
                }
            }
        }

        private static Type GetView<T>()
        {
            var viewmodelName = typeof(T).Name.Replace("ViewModel", "View");
            var type = Type.GetType("HealthCare.Win.Views." + viewmodelName, false);
            return type;
        }
        private static Type GetView(Type viewmodelType)
        {
            var viewmodelName = viewmodelType.Name.Replace("ViewModel", "View");
            var type = Type.GetType("HealthCare.Win.Views." + viewmodelName, false);
            return type;
        }

        protected void Close(ViewModelBase obj)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        public virtual void Init()
        {

        }

        public T GetParam<T>()
        {
            return (T)(Data.PageParamGet((this.GetType()).Name));
        }

        public T GetParam<T>(string viewModelKey)
        {
            return (T)(Data.PageParamGet(viewModelKey));
        }

    }
}