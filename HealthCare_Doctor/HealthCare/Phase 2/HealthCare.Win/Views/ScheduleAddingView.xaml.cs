using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using HealthCare.Core.Models.Enums;
using HealthCare.Core.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HealthCare.Win.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ScheduleAddingView : Page
    {
        ScheduleAddingViewModel ViewModel => this.DataContext as ScheduleAddingViewModel;
        public ScheduleAddingView()
        {
            this.InitializeComponent();
        }

        private void OnTimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {
            var timePicker = sender as TimePicker;
            if (timePicker != null && timePicker.Tag != null)
                if (timePicker.Tag.Equals("Start"))
                {
                    ViewModel.StartTime = new DateTime(ViewModel.CurrentDate.Year, ViewModel.CurrentDate.Month,
                        ViewModel.CurrentDate.Day, e.NewTime.Hours, e.NewTime.Minutes, 0);
                }
                else
                {
                    ViewModel.EndTime = new DateTime(ViewModel.CurrentDate.Year, ViewModel.CurrentDate.Month,
                        ViewModel.CurrentDate.Day, e.NewTime.Hours, e.NewTime.Minutes, 0);
                }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listview = sender as ListView;
            if (listview == null)
                return;

            ViewModel.DayOfWeeks = listview.SelectedItems.Cast<DoctorDayOfWeekObject>().Select(x => x.Value).ToArray();
            if (ViewModel.DayOfWeeks == null || ViewModel.DayOfWeeks.Length == 0)
            {
                WeeksHeader.Visibility = WeeksSelect.Visibility = Visibility.Collapsed;
            }
            else
            {
                WeeksHeader.Visibility = WeeksSelect.Visibility = Visibility.Visible;
            }
        }
    }
}
