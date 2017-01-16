using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Navigation;
using Windows.System;
using Cirrious.CrossCore;
using HealthCare.Core.Models;
using HealthCare.Core.Models.Enums;
using HealthCare.Core.Resources;
using HealthCare.Core.Services.Interfaces;
using HealthCare.Core.Utils;
using HealthCare.Core.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Telerik.Windows.Controls;
namespace HealthCare.Phone.Views.HomeTab
{
    public partial class WeekTopicTab : UserControl
    {
        private HomeViewModel _vm => DataContext as HomeViewModel;
        public WeekTopicTab()
        {
            //Telerik.Windows.Controls.InputLocalizationManager.Instance.ResourceManager = Core.Resources.AppResources.ResourceManager;

            InitializeComponent();
            WeekTopicList.SetValue(InteractionEffectManager.IsInteractionEnabledProperty, true);
            InteractionEffectManager.AllowedTypes.Add(typeof(RadDataBoundListBoxItem));

            WeekTopicList.DataVirtualizationMode = DataVirtualizationMode.OnDemandAutomatic;
            WeekTopicList.DataRequested += OnDataRequested;
        }

        private async void OnDataRequested(object sender, EventArgs e)
        {
            var m = await _vm.LoadTopics();            
        }

        private void WeekTopicTab_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = ((HomeViewModel)this.DataContext);
            var r = new[] { ToggleButtonAll, ToggleButtonOn, ToggleButtonOff };
            foreach (var source in r.Where(x => x != null && x.Tag != vm.WeekTopicsMode.ToString()))
            {
                source.IsChecked = true;
                ToggleButtonOnChecked(source, null);
                return;
            }
            ToggleButtonAll.IsChecked = true;
            ToggleButtonOnChecked(ToggleButtonAll, null);
        }

        private async void ImageTopic_Click(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (!isPdfTap)
            {
                var topic = ((FrameworkElement)sender).DataContext as Topic;
                if (!string.IsNullOrEmpty(topic?.SkypeForBusinessUrl) && topic.IsOnline)
                {
                    if (!topic.IsOnlineNow)
                    {
                        if (topic.StartDateTime > Util.DateTimeToLong(DateTime.UtcNow))
                            await Mvx.Resolve<IMessageService>().ShowMessageAsync(AppResources.WeakTopics_NotNow, AppResources.Warning);
                        else
                            await Mvx.Resolve<IMessageService>().ShowMessageAsync(AppResources.WeekTopic_Happened, AppResources.Warning);


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
                        MapsDirectionsTask mapsDirectionsTask = new MapsDirectionsTask();
                        // You can specify a label and a geocoordinate for the end point.
                        //#if DEBUG
                        //                GeoCoordinate spaceNeedleLocation = new GeoCoordinate(10.7604580, 106.6910100);
                        //#else
                        GeoCoordinate spaceNeedleLocation = new GeoCoordinate(topic.Location[1], topic.Location[0]);
                        //#endif
                        LabeledMapLocation spaceNeedleLml = new LabeledMapLocation(string.IsNullOrEmpty(topic.Address) ? topic.Title : topic.Address, spaceNeedleLocation);
                        mapsDirectionsTask.End = spaceNeedleLml;
                        // If mapsDirectionsTask.Start is not set, the user's current location is used as the start point.
                        mapsDirectionsTask.Show();
                    }
                    catch (Exception ex)
                    {
                        //throw;
                        MessageBox.Show(AppResources.LocationNotFound + " (" + topic.Address + ")",
                            AppResources.WeekTopic_Maps, MessageBoxButton.OK);
                    }
                }
            }
            else { isPdfTap = false; }
        }

        private async void WeekTopicList_OnRefreshRequested(object sender, EventArgs e)
        {
            var vm = this.DataContext as HomeViewModel;
            if (vm != null)
            {
                await vm.LoadTopics(true);
            }
            WeekTopicList.StopPullToRefreshLoading(true);
        }

        

        private void ToggleButtonOnChecked(object sender, RoutedEventArgs e)
        {
            if (this.DataContext == null)
                return;

            var r = new[] { ToggleButtonAll, ToggleButtonOn, ToggleButtonOff };
            foreach (var source in r.Where(x => x != sender))
            {
                if (source == null)
                    return;
                source.IsChecked = false;
            }
            ((HomeViewModel)this.DataContext).FilterWeekTopic(int.Parse((sender as ToggleButton).Tag.ToString()));

        }


        private bool isPdfTap = false;

        private void Border_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            isPdfTap = true;
            var topic = ((FrameworkElement)sender).DataContext as Topic;

            ((HomeViewModel)this.DataContext).ShowWeekTopicFileCommand.Execute(topic);
        }
    }
}
