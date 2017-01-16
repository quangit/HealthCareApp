using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using HealthCare.Core.Models;
using HealthCare.Core.Resources;
using HealthCare.Core.Services.Interfaces;
using HealthCare.Core.Utils;
using HealthCare.Core.ViewModels;
using Windows.UI.Xaml.Navigation;
using Acr.UserDialogs;
using HealthCare.Core.Services;
using Template10.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HealthCare.Win.Views.HomeTab
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WeekTopicView : Page
    {
        public HomeViewModel ViewModel { get; set; }
        public WeekTopicView()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel = Shell.ViewModel;
            txtAll.Text = AppResources.WeekTopic_All.ToUpper();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            BootStrapper.Current.NavigationService.Frame.BackStack.Clear();
            BootStrapper.Current.NavigationService.ClearHistory();
            BootStrapper.Current.NavigationService.ClearCache(true);


        }


        public async Task<bool> ShowConfirmMessageAsync(string content, string title, string yes = "yes", string no = "no")
        {
            try
            {

                var messageDialog = new MessageDialog(content, title);
                messageDialog.Commands.Add(new UICommand(yes) { Id = 0 });
                messageDialog.Commands.Add(new UICommand(no) { Id = 1 });
                messageDialog.DefaultCommandIndex = 0;

                var r = await messageDialog.ShowAsync();
                return (int)r.Id == 0;
            }
            catch
            {
                return false;
            }
            finally
            {

            }

        }
        private async void ListViewBase_OnItemClick(object sender, ItemClickEventArgs e)
        {
            // ShowConfirmMessageAsync("Hello", "test");
            if (pdf)
                return;

            var topic = e.ClickedItem as Topic;
            if (!string.IsNullOrEmpty(topic?.SkypeForBusinessUrl) && topic.IsOnline)
            {
                if (!topic.IsOnlineNow)
                {
                    if (topic.StartDateTime > Util.DateTimeToLong(DateTime.UtcNow))
                        await Core.IoC.Resolve<IMessageService>().ShowMessageAsync(AppResources.WeakTopics_NotNow, AppResources.Warning);
                    else
                        await Core.IoC.Resolve<IMessageService>().ShowMessageAsync(AppResources.WeekTopic_Happened, AppResources.Warning);


                    return;
                }
#if DEBUG
                await Launcher.LaunchUriAsync(new Uri("skype:19:2961f0bdd42a4c4b87ee1b0e72256c24@thread.skype?chat"));
#else
                await Launcher.LaunchUriAsync(new Uri(topic.SkypeForBusinessUrl));
#endif
            }
            else if (topic?.Location != null)
            {
                try
                {
                    var vm = this.DataContext as HomeViewModel;
                    if (vm != null)
                    {
                        var b = await vm.GoToMap(topic);
                        if (!b)
                            return;
                    }

                    var uri =
                        $"bingmaps:?rtp=~pos.{topic.Location[1]}_{topic.Location[0]}_{System.Net.WebUtility.UrlEncode(string.IsNullOrEmpty(topic.Address) ? topic.Title : topic.Address)}";


                    // Launch the Windows Maps app
                    var launcherOptions = new Windows.System.LauncherOptions
                    {
                        TargetApplicationPackageFamilyName = "Microsoft.WindowsMaps_8wekyb3d8bbwe"
                    };
                    var success = await Windows.System.Launcher.LaunchUriAsync(new Uri(uri), launcherOptions);

                    //MapsDirectionsTask mapsDirectionsTask = new MapsDirectionsTask();

                    //GeoCoordinate spaceNeedleLocation = new GeoCoordinate(topic.Location[1], topic.Location[0]);
                    ////#endif
                    //LabeledMapLocation spaceNeedleLml = new LabeledMapLocation(string.IsNullOrEmpty(topic.Address) ? topic.Title : topic.Address, spaceNeedleLocation);
                    //mapsDirectionsTask.End = spaceNeedleLml;
                    //// If mapsDirectionsTask.Start is not set, the user's current location is used as the start point.
                    //mapsDirectionsTask.Show();
                }
                catch (Exception)
                {
                    //throw;
                    await
                        Core.IoC.Resolve<IMessageService>()
                            .ShowMessageAsync(AppResources.LocationNotFound + " (" + topic.Address + ")",
                                AppResources.WeekTopic_Maps);
                }
            }
        }

        private void Pivot_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pivot = sender as Pivot;
            ViewModel.FilterWeekTopic(pivot?.SelectedIndex ?? -1);
        }

        private bool pdf = false;
        private async void PdfOpen(object sender, TappedRoutedEventArgs e)
        {
            pdf = true;

            var topic = ((FrameworkElement)sender).DataContext as Topic;

            if (topic != null)
            {
                topic.TopicFiles = await HealthCareService.Current.GetTopicFiles(topic.Id);

                if (topic.TopicFiles != null && topic.TopicFiles.Any())
                {
                    ViewModel.ShowViewModel<WeekTopicFileViewModel>(topic);
                }
                if (topic.TopicFiles != null && topic.TopicFiles.Count == 0)
                    UserDialogs.Instance.Alert(AppResources.WeekTopicFile_Empty, AppResources.Warning);
            }

            pdf = false;
            //if (topic == null || !topic.HasFiles)
            //{
            //    var x = Core.IoC.Resolve<IMessageService>();

            //    await x.ShowMessageAsync(AppResources.WeekTopicFile_Empty, AppResources.ApplicationTitle);
            //    return;
            //}
            //((HomeViewModel)this.DataContext).ShowWeekTopicFileCommand.Execute(topic);
        }

        private Storyboard s;
        private void AppBarButtonLoaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var grid = sender as Grid;
            s = grid.Resources["Storyboard"] as Storyboard;
            s.RepeatBehavior = RepeatBehavior.Forever;

        }

        private async void RefreshClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            s.Begin();
            await ViewModel.LoadTopics(true);
            s.Stop();

        }

        private async void ItemTapped(object sender, TappedRoutedEventArgs e)
        {
            if (pdf)
                return;

            var topic = ((FrameworkElement)sender).DataContext as Topic;

            if (!string.IsNullOrEmpty(topic?.SkypeForBusinessUrl) && topic.IsOnline)
            {
                if (!topic.IsOnlineNow)
                {
                    if (topic.StartDateTime > Util.DateTimeToLong(DateTime.UtcNow))
                        await Core.IoC.Resolve<IMessageService>().ShowMessageAsync(AppResources.WeakTopics_NotNow, AppResources.Warning);
                    else
                        await Core.IoC.Resolve<IMessageService>().ShowMessageAsync(AppResources.WeekTopic_Happened, AppResources.Warning);


                    return;
                }
#if DEBUG
                await Launcher.LaunchUriAsync(new Uri("skype:19:2961f0bdd42a4c4b87ee1b0e72256c24@thread.skype?chat"));
#else
                await Launcher.LaunchUriAsync(new Uri(topic.SkypeForBusinessUrl));
#endif
            }
            else if (topic?.Location != null)
            {
                try
                {
                    var vm = this.DataContext as HomeViewModel;
                    if (vm != null)
                    {
                        var b = await vm.GoToMap(topic);
                        if (!b)
                            return;
                    }

                    var uri =
                        $"bingmaps:?rtp=~pos.{topic.Location[1]}_{topic.Location[0]}_{System.Net.WebUtility.UrlEncode(string.IsNullOrEmpty(topic.Address) ? topic.Title : topic.Address)}";


                    // Launch the Windows Maps app
                    var launcherOptions = new Windows.System.LauncherOptions
                    {
                        TargetApplicationPackageFamilyName = "Microsoft.WindowsMaps_8wekyb3d8bbwe"
                    };
                    var success = await Windows.System.Launcher.LaunchUriAsync(new Uri(uri), launcherOptions);

                    //MapsDirectionsTask mapsDirectionsTask = new MapsDirectionsTask();

                    //GeoCoordinate spaceNeedleLocation = new GeoCoordinate(topic.Location[1], topic.Location[0]);
                    ////#endif
                    //LabeledMapLocation spaceNeedleLml = new LabeledMapLocation(string.IsNullOrEmpty(topic.Address) ? topic.Title : topic.Address, spaceNeedleLocation);
                    //mapsDirectionsTask.End = spaceNeedleLml;
                    //// If mapsDirectionsTask.Start is not set, the user's current location is used as the start point.
                    //mapsDirectionsTask.Show();
                }
                catch (Exception)
                {
                    //throw;
                    await
                        Core.IoC.Resolve<IMessageService>()
                            .ShowMessageAsync(AppResources.LocationNotFound + " (" + topic.Address + ")",
                                AppResources.WeekTopic_Maps);
                }
            }
        }
    }
}
