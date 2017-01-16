using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
using Com.Telerik.Android.Common;
using Com.Telerik.Widget.Calendar;
using HealthCare.Core.Models;
using HealthCare.Core.ViewModels;
using HealthCare.Droid.Utilities;
using Java.Lang;
using Java.Util;
using Calendar = Java.Util.Calendar;
using Com.Telerik.Widget.Calendar.Events;
using HealthCare.Core.Resources;
using Android.Content;

namespace HealthCare.Droid.Views.Fragments
{

    public class MyEvent : Event
    {
        public Schedule Schedule { get; private set; }
        public MyEvent(Schedule s, string subject, long statTime, long endTime) : base(subject, statTime, endTime)
        {
            Schedule = s;
        }
    }
    [Register("healthcare.droid.views.fragments.ScheduleFragment"), Activity(Label = "Lich Kham")]
    public class ScheduleFragment : MvxFragment
    {
        private View _view;
        //private CustomCalendar _customCalendar;
        private HomeViewModel _vm;
        private TextView currentDayTextView;
        private RadCalendarView calendarView;
        private List<Schedule> testdays;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.HasOptionsMenu = true;
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            // Initialize the ViewPager and set an adapter
            //this.EnsureBindingContextIsSet(savedInstanceState);
            _view = this.BindingInflate(Resource.Layout.ScheduleFragment, null);
            _vm = (HomeViewModel)ViewModel;
            InitCalendar();
            if (_vm.Schedules != null)
            {
                _vm.Schedules.CollectionChanged += SchedulesChanged;
                UpdateCalendar();
            }
            else
            {
                _vm.PropertyChanged += _vm_PropertyChanged;
            }


            this.Activity.RunOnUiThread(() =>
            {
                var scheduleList = _view.FindViewById<MvxListView>(Resource.Id.scheduleList);
                RegisterForContextMenu(scheduleList);
            });

            return _view;
        }

        #region ContextMenu
        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            base.OnCreateContextMenu(menu, v, menuInfo);

            menu.Add(AppResources.Button_Delete);
        }


        public override bool OnContextItemSelected(IMenuItem item)
        {
            //delete
            var voucherDatum =
                ((MvxBaseListItemView)((AdapterView.AdapterContextMenuInfo)(item.MenuInfo)).TargetView).DataContext;
            ((HomeViewModel)ViewModel).DeleteScheduleCommand.Execute(voucherDatum);

            return base.OnContextItemSelected(item);
        }
        #endregion

        void _vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Schedules")
            {
                _vm.Schedules.CollectionChanged += SchedulesChanged;
                UpdateCalendar();
            }
        }


        private void SchedulesChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateCalendar();
        }

        void UpdateCalendar()
        {
			var s = _vm.Schedules;//.Where (x => x.StartDateTime.Day == 10 && x.StartDateTime.Month == 3);
			var events = s.Select(
				x =>
				new Event(x.ToString(), x.Date,x.EndDate
					//Core.Utils.Util.DateTimeToLong(x.StartDateTime),Core.Utils.Util.DateTimeToLong(x.EndDateTime)
				) {}).ToList();


            calendarView.EventAdapter.Events = events;
            calendarView.NotifyDataChanged();
            UpdateSelectedDate();
        }

        public void InitCalendar()
        {
            calendarView = _view.FindViewById<RadCalendarView>(Resource.Id.calendar);
            calendarView.DisplayMode = CalendarDisplayMode.Month;
            calendarView.SelectionMode = CalendarSelectionMode.Single;
            calendarView.AnimationEnabled = false;
            calendarView.SuspendDisplayModeChange = true;

            _vm.SetSelectedExamination(DateTime.Today.Date);
            _vm.SelectedDate = DateTime.Today.Date;


            //calendarView.DateToColor = new DateToColorExample();

            calendarView.SelectedDatesChanged += (sender, args) =>
            {
                UpdateSelectedDate();
            };
        }

        private void UpdateSelectedDate()
        {            
            if (calendarView.SelectedDates == null || calendarView.SelectedDates.Count == 0)
                return;
            //var r = calendarView.EventAdapter.GetEventsForDate(calendarView.SelectedDates[0].LongValue());
            var selectedday = Core.Utils.Util.LongtoDateTime(calendarView.SelectedDates[0].LongValue());
            _vm.SelectedDate = selectedday;
            _vm.SetSelectedExamination(selectedday);
        }

        


        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            // base.OnCreateOptionsMenu(menu, inflater);


            menu.Clear();

            inflater.Inflate(Resource.Menu.lichkham, menu);




            var itmMyPakaze = menu.Add("Call")
          .SetIcon(Resource.Drawable.phone_sup);
            itmMyPakaze.SetShowAsAction(ShowAsAction.Always);
            itmMyPakaze.SetOnMenuItemClickListener(new OnMenuItemClickAction((e) =>
            {
                Setup.OkCancelBox(AppResources.Warning, AppResources.AppBar_CallMessage, v =>
                {
                    if (v)
                    {
                        var uri = Android.Net.Uri.Parse("tel:" + Data.SupportPhone);
                        var intent = new Intent(Intent.ActionDial, uri);
                        StartActivity(intent);
                    }
                });
            }));

        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.TitleFormatted.ToString().Equals("Add"))
            {
                //_vm.SelectedDate = DateTime.ParseExact(currentDayTextView.Text, "dd.MM.yyyy", null);

                _vm.ShowScheduleAddingCommand.Execute(null);
            }
            return base.OnOptionsItemSelected(item);
        }

    }


    // DateToColorExample sample implementation
    class DateToColorExample : Java.Lang.Object, IFunction
    {
        private readonly Java.Util.Calendar _calendar = Java.Util.Calendar.Instance;

        public Java.Lang.Object Apply(Java.Lang.Object timeInMillis)
        {
            _calendar.TimeInMillis = (long)timeInMillis;
            if (_calendar.Get(CalendarField.DayOfWeek) == Calendar.Sunday)
            {
                return Color.Red.ToArgb();
            }
            return null;
        }
    }
}

