
using System;

using Foundation;
using UIKit;
using TelerikUI;
using HealthCare.Core.ViewModels;
using HealthCare.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using HealthCare.Touch.Views.Cells;
using HealthCare.Touch.Utilities;
using System.Globalization;

namespace HealthCare.Touch.Views.Tabs
{
	public partial class ScheduleHomeTab : BaseViewController
	{
		private HomeViewModel _vm{
			get { return ViewModel as HomeViewModel;}
		}
		private TKCalendar _calendar;


		public ScheduleHomeTab () : base ("ScheduleHomeTab", null,true)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			if(_vm.SelectedDate != null)
				_calendar.SelectedDate = Util.DateTimeToNSDate(_vm.SelectedDate);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_vm.PropertyChanged += ViewModel_PropertyChanged;
			TableView.RowHeight = 80;

			var source = new DeleteTableViewSource(TableView, ScheduleCell.Key, ScheduleCell.Key){ViewModel = _vm};
			TableView.Source = source;

			var set = this.CreateBindingSet<ScheduleHomeTab, HomeViewModel>();
			set.Bind(source).To(vm => vm.SelectedSchedule);
			//set.Bind(source).For(s => s.SelectionChangedCommand).To(vm => vm.MyDealCommand);
			set.Apply();
			//TableView.ReloadData();

			this.AddBindings(new Dictionary<object, string>() {
				{EmptyLabel, "Visible SelectedSchedule,Converter=ListVis; Text [Schedule_Empty]"},
			});

			// Perform any additional setup after loading the view, typically from a nib.
			_calendar = new TKCalendar (CalendarView.Bounds);
			_calendar.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
			_calendar.DataSource = new CalendarDataSource (this);
			_calendar.Delegate = new CalendarDelegate (_vm);
			_calendar.ViewMode = TKCalendarViewMode.Month;
			_calendar.Locale = new NSLocale (CultureInfo.CurrentUICulture.Name);
			var s = _calendar.Zone;
			_vm.SetSelectedExamination(DateTime.Today.Date);
			_vm.SelectedDate = DateTime.Today.Date;

			CalendarView.AddSubview (_calendar);
			ReloadEvents ();

			TitleLabel.Text = Core.Resources.AppResources.Schedule_Title;
		}



		public List<TKCalendarEvent> Events {
			get;
			set;
		}
		public void ReloadEvents()
		{
			if (_vm.Schedules != null) {
				Events = _vm.Schedules.Select (x => new TKCalendarEvent () {
					
					StartDate =  Util.DateTimeToNSDate(x.StartDateTime),
					EndDate = Util.DateTimeToNSDate(x.EndDateTime), 
					Title = x.ToString (),
					EventColor = UIColor.Red,
				}).ToList ();
				_calendar.ReloadData ();
			}
		}

		private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if ((e.PropertyName == "Schedules")||(e.PropertyName == "SchedulesChanged")){
				ReloadEvents ();
			}
		}


		private class CalendarDataSource : TKCalendarDataSource
		{
			ScheduleHomeTab main;
			public CalendarDataSource(ScheduleHomeTab main)
			{
				this.main = main;
			}

			public override TKCalendarEventProtocol[] EventsForDate (TKCalendar calendar, NSDate date)
			{
				


//				NSDateComponents components = calendar.Calendar.Components (NSCalendarUnit.Day | NSCalendarUnit.Month | NSCalendarUnit.Year, date);
//				components.Hour = 23;
//				components.Minute = 59;
//				components.Second = 59;
//				NSDate endDate = calendar.Calendar.DateFromComponents (components);
				List<TKCalendarEventProtocol> filteredEvents = new List<TKCalendarEventProtocol> ();
				if (main.Events != null) {
					for (int i = 0; i < main.Events.Count; i++) {
						var ev = main.Events [i];

						if(CompareDates (date,ev.StartDate,ev.EndDate ))
							filteredEvents.Add (ev);

//						if (ev.StartDate.SecondsSinceReferenceDate <= date.SecondsSinceReferenceDate &&
//						   ev.EndDate.SecondsSinceReferenceDate >= date.SecondsSinceReferenceDate) {
//							if(Util.NSDateToDateTime(ev.StartDate).Day == 28){
//								var t = "";
//							}
//
//
//							filteredEvents.Add (ev);
//						}
					}
				}


				return filteredEvents.ToArray ();
			}

			private bool CompareDates(NSDate date, NSDate startDate, NSDate endDate){

				var datetime = Util.NSDateToDateTime(date);

				var startDt = Util.NSDateToDateTime (startDate,true);
				var endDt = Util.NSDateToDateTime (endDate,true);

				var ret = (startDt.Day == datetime.Day && startDt.Month == datetime.Month && startDt.Year == datetime.Year );
				return ret;

			}
		}



		private class CalendarDelegate : TKCalendarDelegate
		{

			HomeViewModel _vm;
			public CalendarDelegate(HomeViewModel vm)
			{
				_vm = vm;
			}

			public override void DidSelectDate (TKCalendar calendar, NSDate date)
			{
				var datetime = Util.NSDateToDateTime(date);
				_vm.SetSelectedExamination(datetime);
			}
		}

//		public class ScheduleAppointment : TKCalendarEvent{
//			public ScheduleAppointment(Schedule schedule){
//				StartDate = NSDate.FromTimeIntervalSince1970(schedule.StartTime/1000);
//				EndDate = NSDate.FromTimeIntervalSince1970(schedule.EndTime/1000);
//				Title = schedule.ToString();
//			}
//		}
	}
}

