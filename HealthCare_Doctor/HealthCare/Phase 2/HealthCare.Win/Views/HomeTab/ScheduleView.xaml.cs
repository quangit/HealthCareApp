using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using HealthCare.Core.ViewModels;
using Template10.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HealthCare.Win.Views.HomeTab
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ScheduleView : Page
    {
        public HomeViewModel ViewModel { get; }
        public ScheduleView()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel = Shell.ViewModel;

            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                RelativePanel.Margin = new Thickness(12, 12, 24, 12);
            }

        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            BootStrapper.Current.NavigationService.Frame.BackStack.Clear();
            BootStrapper.Current.NavigationService.ClearHistory();
            BootStrapper.Current.NavigationService.ClearCache(true);
        }
        private bool CompareDates(DateTime date, DateTime startDate, DateTime endDate)
        {


            var ret = (startDate.Day == date.Day && startDate.Month == date.Month && startDate.Year == date.Year);
            return ret;

        }
        private async void ViewDayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs args)
        {
            // Render basic day items.
            if (args.Phase == 0)
            {
                // Register callback for next phase.
                args.RegisterUpdateCallback(ViewDayItemChanging);
            } // Set blackout dates.
            else if (args.Phase == 1)
            {
                // Avoid unnecessary processing.
                // You don't need to set bars on past dates or Sundays.
                if (args.Item != null)
                {
                    if (ViewModel.Schedules == null)
                    {
                        await ViewModel.GetSchedules();
                    }
                    var currentBookings = ViewModel.Schedules;
                    if (args.Item == null)
                        return;
                    List<Color> densityColors = new List<Color>();
                    // Set a density bar color for each of the days bookings.
                    // It's assumed that there can't be more than 10 bookings in a day. Otherwise,
                    // further processing is needed to fit within the max of 10 density bars.
                    foreach (var booking in currentBookings.Where(x => args.Item != null && CompareDates(args.Item.Date.DateTime, x.StartDateTime, x.EndDateTime)))
                    {
                        densityColors.Add(Colors.Green);

                    }
                    args.Item.SetDensityColors(densityColors);
                }
            }
        }

        private void CalendarView_OnSelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            foreach (var date in args.AddedDates)
            {
                ListView.ItemsSource =
                    ViewModel.Schedules?.Where(x => CompareDates(date.DateTime, x.StartDateTime, x.EndDateTime));

            }
        }
    }
}
