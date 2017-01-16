using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using HealthCare.Core.Models;
using HealthCare.Core.ViewModels;
using Microsoft.Phone.Shell;
using Telerik.Windows.Controls;

namespace HealthCare.Phone.Views.HomeTab
{
    public partial class ScheduleTab : UserControl
    {
        public ScheduleTab()
        {
            InitializeComponent();
            this.AppointmentsList.SetValue(InteractionEffectManager.IsInteractionEnabledProperty, true); InteractionEffectManager.AllowedTypes.Add(typeof(RadDataBoundListBoxItem));

        }


        private HomeViewModel ViewModel
        {
            get { return this.DataContext as HomeViewModel; }
        }
        private void CalendarSelectedValueChanged(object sender, ValueChangedEventArgs<object> e)
        {
            if (e.NewValue == null)
            {
                this.AppointmentsList.ItemsSource = null;
                return;
            }
            DisplayAppointmentsForDate((e.NewValue as DateTime?).Value);
        }

        private void DisplayAppointmentsForDate(DateTime date)
        {
            ViewModel.SelectedDate = date;
            if (_scheduleSource != null)
                this.AppointmentsList.ItemsSource = _scheduleSource.GetAppointments((IAppointment appointment) =>
                {
                    DateTime currentAppointmentStart = appointment.StartDate;
                    DateTime currentAppointmentEnd = appointment.EndDate;
                    DateTime requiredAppointmentsStartDate = date.Date;
                    DateTime requiredAppointmentsEndDate = date.Date.AddDays(1);

                    if (requiredAppointmentsEndDate > currentAppointmentStart && requiredAppointmentsStartDate < currentAppointmentEnd)
                    {
                        return true;
                    }

                    return false;
                });
        }
        private void OnCalendarLoaded(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Schedules != null)
            {
                ViewModel.Schedules.CollectionChanged += SchedulesChanged;
                UpdateSource();
            }
            else
            {
                ViewModel.PropertyChanged += ViewModelPropertyChanged;
            }

        }

        private void ViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Schedules")
            {
                ViewModel.Schedules.CollectionChanged += SchedulesChanged;
                UpdateSource();
            }
        }

        private void SchedulesChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateSource();
        }

        private ScheduleSource _scheduleSource;
        void UpdateSource()
        {
            this.Calendar.AppointmentSource = _scheduleSource = new ScheduleSource(ViewModel.Schedules);
            DisplayAppointmentsForDate(Calendar.SelectedValue ?? DateTime.Now);
        }
        public class ScheduleSource : AppointmentSource
        {
            private IEnumerable<Schedule> _schedules;
            public ScheduleSource(IEnumerable<Schedule> schedules)
            {
                _schedules = schedules;
            }

            public override void FetchData(DateTime startDate, DateTime endDate)
            {
                this.AllAppointments.Clear();
                this.AllAppointments.AddRange(_schedules.Select(x => new ScheduleAppointment(x)));
                this.OnDataLoaded(); // notify that there is new data to display
            }
        }

        public class ScheduleAppointment : IAppointment
        {
            public DateTime StartDate { get; }
            public DateTime EndDate { get; }
            public string Subject { get; set; }
            public string Location { get; set; }
            public Schedule Schedule { get; set; }

            public ScheduleAppointment(Schedule schedule)
            {
                StartDate = schedule.StartDateTime;
                EndDate = schedule.EndDateTime;
                Subject = schedule.Hospital.Name;
                Location = schedule.Hospital.Name;
                Schedule = schedule;
            }
        }

        private void OnMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            var focusedItem = e.FocusedElement as FrameworkElement;
            
            if (focusedItem != null)
            {
                var data = (ScheduleAppointment) focusedItem.DataContext;
                deleteMenu.Command = ((HomeViewModel)ViewModel).DeleteScheduleCommand;
                deleteMenu.CommandParameter = data.Schedule;                
            }
        }
    }
}
