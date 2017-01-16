using System;
using Windows.ApplicationModel.Background;
using Windows.Networking.PushNotifications;
using Windows.Security.Cryptography;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using HealthCare.Core.Models;
using HealthCare.Core.Resources;
using HealthCare.Core.Services.Interfaces;
using HealthCare.Core.ViewModels;
using HealthCare.Win.Controls;
using HealthCare.Win.Views.HomeTab;
using Template10.Common;
using Template10.Controls;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using HealthCare.Core.Services;

namespace HealthCare.Win.Views
{
    public sealed partial class Shell : Page
    {
        public static Shell Instance { get; set; }
        public static HamburgerMenu HamburgerMenu { get { return Instance.MyHamburgerMenu; } }
        public static HomeViewModel ViewModel { get; set; }
        public Shell(INavigationService navigationService)
        {
            Instance = this;
            InitializeComponent();
            HamburgerMenu.NavigationService = navigationService;
            HamburgerMenu.DataContext = ViewModel = new HomeViewModel();

            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {

            ViewModel.Init();

            if (Core.IoC.LastParams != null && Core.IoC.LastParams.StartsWith("topics"))
            {
                HamburgerMenu.NavigationService.Navigate(typeof(WeekTopicView));
            }
            else if (Core.IoC.LastParams != null && Core.IoC.LastParams.StartsWith("showCheckup"))
            {
                HamburgerMenu.NavigationService.Navigate(typeof(CheckupsView));
            }
            else if (Core.IoC.LastParams != null && Core.IoC.LastParams.StartsWith("showCMELib"))
            {
                HamburgerMenu.NavigationService.Navigate(typeof(HomeTab.CmeLibraryView));
            }
            else if (Core.IoC.LastParams != null && Core.IoC.LastParams.StartsWith("showSchedule"))
            {
                HamburgerMenu.NavigationService.Navigate(typeof(HomeTab.ScheduleView));
            }
            else if (Core.IoC.LastParams != null && Core.IoC.LastParams.StartsWith("showQuestions"))
            {
                HamburgerMenu.NavigationService.Navigate(typeof(HomeTab.ConsultView));
            }
            else
                HamburgerMenu.NavigationService.Navigate(typeof(ScheduleView));


            App.AccountManager.Username = Data.UserName;
            App.AccountManager.Password = Data.Password;

            await App.AccountManager.StoreAsync();

            PushInit();

            this.Loaded -= OnLoaded;
        }
        private static async void PushInit()
        {
            try
            {
                
                var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
                Data.PushChannelUri = channel.Uri;
                Data.PushPlatform = "wns";
                await HealthCareService.Current.NotificationRegister();

            }
            catch (Exception ex)
            {
                //throw;
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        private async void LogoutTapped(object sender, RoutedEventArgs e)
        {
            var mr = await Core.IoC.Resolve<IMessageService>()
                  .ShowConfirmMessageAsync(AppResources.Logout_contentMessageBox, AppResources.MainPage_logoutAppBar,
                      AppResources.Messsage_Yes, AppResources.Messsage_No);

            if (mr)
            {
                ViewModel.DoLogoutTap();

                BootStrapper.Current.NavigationService.Frame.BackStack.Clear();
                BootStrapper.Current.NavigationService.ClearHistory();
                BootStrapper.Current.NavigationService.ClearCache(true);
                WindowWrapper.Current().NavigationServices.Clear();

                var frame = BootStrapper.Current.CreateRootFrame(null);

                frame = (BootStrapper.Current.NavigationServiceFactory(BootStrapper.BackButton.Attach,
                    BootStrapper.ExistingContent.Exclude, frame)).FrameFacade.Frame;

                frame.BackStack.Clear();

                var modal = new ModalDialog
                {
                    DisableBackButtonWhenModal = true,
                    ModalContent = new BusyIndicator(),
                    Content = frame,
                };
                
                Window.Current.Content = modal;

                modal.Loaded += (s, args) =>
                {
                    var navigationService = BootStrapper.Current.NavigationService as NavigationService;
                    if (navigationService != null)
                        navigationService.LastNavigationType = null;
                    frame.Navigate(typeof(LoginView));
                };

                Window.Current.Activate();
            }
        }
    }
}

