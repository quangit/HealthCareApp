using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Navigation;
using HealthCare.Core.Models.Enums;
using HealthCare.Core.Resources;
using HealthCare.Core.ViewModels;
using Microsoft.Phone.Shell;
using Telerik.Windows.Controls;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace HealthCare.Phone.Views
{
    public partial class ScheduleAddingView
    {
        public ScheduleAddingView()
        {
            InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ApplicationBar.BackgroundColor = (Color)App.Current.Resources["MainColor"];
            ApplicationBar.ForegroundColor = Colors.White;
            (ApplicationBar.Buttons[0] as ApplicationBarIconButton).Text = AppResources.SignUp_SaveButton.ToLower();
            RepeatsPicker.SummaryForSelectedItemsDelegate = list =>
            {
                string summary = string.Empty;

                for (int i = 0; i < list.Count; i++)
                {
                    // check if the last item has been reached so we don't put a "," at the end
                    bool isLast = i == list.Count - 1;

                    // Customer item = (Customer)list[i];
                    summary = string.Concat(summary, list[i]);
                    summary += isLast ? string.Empty : ", ";
                }
                if (summary == string.Empty)
                {
                    summary = AppResources.ScheduleAdding_OnlyOne;
                    //DatePicker.Visibility = Visibility.Visible;
                    WeekCountList.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    //DatePicker.Visibility = Visibility.Collapsed;
                    WeekCountList.Visibility = System.Windows.Visibility.Visible;
                }
                return summary;
            };
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var array = e.AddedItems.Cast<DoctorDayOfWeekObject>().Select(x => x.Value).ToArray();
                ((ScheduleAddingViewModel)ViewModel).DayOfWeeks = array;
            }
            else
            {
                ((ScheduleAddingViewModel)ViewModel).DayOfWeeks = null;
            }
        }

        private void SaveButtonClicked(object sender, EventArgs e)
        {
            ((ScheduleAddingViewModel)ViewModel).SaveCommand.Execute();
        }

        private void UIElement_OnTap(object sender, GestureEventArgs e)
        {
            RadListPicker.IsExpanded = true;
        }
    }
}