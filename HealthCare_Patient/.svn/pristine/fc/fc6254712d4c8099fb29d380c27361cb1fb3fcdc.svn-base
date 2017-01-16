using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using Connectivity.Plugin;
using HealthCare.DependencyServices;
using HealthCare.Helpers;
using HealthCare.Pages;
using HealthCare.Resx;
using HealthCare.Services;
using HealthCare.ViewModels;
using Xamarin.Forms;

namespace HealthCare
{
    public partial class App : Application
    {
        public static App Instance => ((App)Current);

        public static bool IsAppActivated { get; set; }

        public App()
        {
            IsAppActivated = true;

            InitializeComponent();

            if (Device.OS != TargetPlatform.WinPhone)
            {
                DependencyService.Get<ILocalize>().SetLocale();
            }

            AppResources.Culture = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();

            MainPage = new NavigationPage(new LoginPage()) { Style = HcStyles.NavigationPageStyle };
        }

        protected override void OnStart()
        {
            IsAppActivated = true;
            // The data which need to be called whenever app start
            if (Device.OS != TargetPlatform.WinPhone)
                DependencyService.Get<ILocalize>().SetLocale();
            CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
            {
                if (args.IsConnected)
                {
                    // The data which need to be called whenever network availables again                        
                    CommonViewModel.Instance.GetSystemConfig();
                }
            };

            LoginViewModel.Instance.AutoLogin();
            CommonViewModel.Instance.GetListCity();
            CommonViewModel.Instance.GetSystemConfig();
        }

        protected override void OnResume()
        {
            IsAppActivated = true;
            // The data which need to be updated whenever app resume 
            CommonViewModel.Instance.GetSystemConfig();
        }

        protected override void OnSleep()
        {
            IsAppActivated = false;
        }

        public static void Clean()
        {
            IsAppActivated = false;
            SimpleIocV2.Default.Reset();
        }
    }
}