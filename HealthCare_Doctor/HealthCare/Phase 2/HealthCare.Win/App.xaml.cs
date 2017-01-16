using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Calls;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.Networking.PushNotifications;
using Windows.Storage;
using Windows.System;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using HealthCare.Core.Models;
using HealthCare.Core.Resources;
using HealthCare.Core.Services;
using HealthCare.Core.Services.Interfaces;
using HealthCare.Win.Services;
using Microsoft.Practices.Unity;
using Template10.Mvvm;

namespace HealthCare.Win
{
    public class RootCommand
    {

        private MvxCommand _suportCommand;

        public MvxCommand CallCommand
        {
            get { return _suportCommand ?? (_suportCommand = new MvxCommand(Call)); }
        }

        private async void Call()
        {
            try
            {
                if (ApiInformation.IsTypePresent("Windows.ApplicationModel.Calls.PhoneLine"))
                {
                    PhoneCallStore PhoneCallStore = await PhoneCallManager.RequestStoreAsync();
                    Guid lineGuid = await PhoneCallStore.GetDefaultLineAsync();

                    var phoneLine = await Windows.ApplicationModel.Calls.PhoneLine.FromIdAsync(lineGuid);
                    phoneLine.Dial(Data.SupportPhone, AppResources.ApplicationTitle + ", " + AppResources.CallSupport);
                }
                else
                {
                    await Launcher.LaunchUriAsync(new Uri($"skype:{Data.SupportPhone}"));
                }
            }
            catch (Exception ex)
            {
                await Core.IoC.Resolve<IMessageService>().ShowMessageAsync(ex.Message, "ERROR");
            }

        }
    }
    sealed partial class App : Template10.Common.BootStrapper
    {
        public static AccountManager AccountManager { get; set; }

        public App()
        {
            InitializeComponent();

            AccountManager = new AccountManager();

            Data.Platform = Data.Os.WinPhone;
            Core.IoC.Container.RegisterInstance(typeof(IHttpService), new HttpService());
            Core.IoC.Container.RegisterInstance(typeof(IMessageService), new MessageService());
            Core.IoC.Container.RegisterInstance(typeof(IFileService), new FileService());
            Core.IoC.Container.RegisterInstance(typeof(IReporterService), new ReporterService());
            Core.IoC.Container.RegisterInstance(typeof(IBandService), new BandService());
            Core.IoC.Container.RegisterInstance(typeof(ICmeService),
                new CmeService(Core.IoC.Resolve<IHttpService>(), Core.IoC.Resolve<IReporterService>()));
            Microsoft.HockeyApp.HockeyClient.Current.Configure("a022ccd0bbae4378993574c05ed9f196");
        }

        public override async Task OnInitializeAsync(IActivatedEventArgs args)

        {
            
            await base.OnInitializeAsync(args);

            BadgeUpdateManager.CreateBadgeUpdaterForApplication().Clear();
            InitCotarna();
        }

       

        async void InitCotarna()
        {

            try
            {
                // Install the main VCD. 
                StorageFile vcdStorageFile =
                     await Package.Current.InstalledLocation.GetFileAsync(
                       @"VCDCommands.xml");

                await Windows.ApplicationModel.VoiceCommands.VoiceCommandDefinitionManager.InstallCommandDefinitionsFromStorageFileAsync(vcdStorageFile);

                // Update phrase list.
                //ViewModel.ViewModelLocator locator = App.Current.Resources["ViewModelLocator"] as ViewModel.ViewModelLocator;
                //if (locator != null)
                //{
                //    await locator.TripViewModel.UpdateDestinationPhraseList();
                //}
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Installing Voice Commands Failed: " + ex.ToString());
            }
        }
        public override Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {

            var pa = args as ProtocolActivatedEventArgs;
            if (pa != null)
            {
                if (pa.Uri.OriginalString.StartsWith("windows.personalassistantlaunch:?LaunchContext", StringComparison.CurrentCultureIgnoreCase))
                {
                    Core.IoC.LastParams = System.Net.WebUtility.UrlDecode(pa.Uri.OriginalString.ToLower().Replace("windows.personalassistantlaunch:?launchcontext=", ""));
                }
            }
            var ca = args as VoiceCommandActivatedEventArgs;
            if (ca != null)
            {
                string voiceCommandName = ca.Result.RulePath.FirstOrDefault();
                Core.IoC.LastParams = voiceCommandName;
            }

            if (args.PreviousExecutionState == ApplicationExecutionState.Running || args.PreviousExecutionState == ApplicationExecutionState.Suspended)
            {
                if (!string.IsNullOrEmpty(Core.IoC.LastParams) && Core.IoC.LastParams.StartsWith("topics"))
                {
                    if (Views.Shell.Instance != null)
                    {
                        NavigationService.Navigate(typeof(Views.HomeTab.WeekTopicView));
                    }
                }
                else if (!string.IsNullOrEmpty(Core.IoC.LastParams) && Core.IoC.LastParams.StartsWith("showCheckup"))
                {
                    if (Views.Shell.Instance != null)
                    {
                        NavigationService.Navigate(typeof(Views.HomeTab.CheckupsView));
                    }
                }
                else if (!string.IsNullOrEmpty(Core.IoC.LastParams) && Core.IoC.LastParams.StartsWith("showCMELib"))
                {
                    if (Views.Shell.Instance != null)
                    {
                        NavigationService.Navigate(typeof(Views.HomeTab.CmeLibraryView));
                    }
                }
                else if (!string.IsNullOrEmpty(Core.IoC.LastParams) && Core.IoC.LastParams.StartsWith("showSchedule"))
                {
                    if (Views.Shell.Instance != null)
                    {
                        NavigationService.Navigate(typeof(Views.HomeTab.ScheduleView));
                    }
                }
                else if (!string.IsNullOrEmpty(Core.IoC.LastParams) && Core.IoC.LastParams.StartsWith("showQuestions"))
                {
                    if (Views.Shell.Instance != null)
                    {
                        NavigationService.Navigate(typeof(Views.HomeTab.ConsultView));
                    }
                }
                return Task.CompletedTask;
            }
            NavigationService.Navigate(typeof(Views.LoginView));

            return Task.CompletedTask;
        }

    }
}
