using System;
using Foundation;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using System.Collections.Generic;

namespace HealthCare.Touch.Views.Cells
{
	public partial class WeekTopicCell : MvxTableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("WeekTopicCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("WeekTopicCell");

		private readonly MvxImageViewLoader _logoloader;

		public WeekTopicCell (IntPtr handle) : base (handle)
		{
			_logoloader = new MvxImageViewLoader (() => WeekTopicIconImage);
			//ContentView.BackgroundColor = UIColor.Cyan;

			this.DelayBind (() => {
				this.AddBindings(new Dictionary<object, string>() {
					{_logoloader, "ImageUrl ., Converter = WeekTopicStatus"},
					{WeekTopicTitleLabel, "Text Title"},
					{StartTimeLabel, "Text [ScheduleAdding_StartTitle]"},
					{StartTimeValueLabel, "Text StartDateTime, Converter = MilisecondToDate"},
					{EndTimeLabel, "Text [ScheduleAdding_EndTitle]"},
					{EndTimeValueLabel, "Text EndDateTime, Converter = MilisecondToDate"},
					{FileButton,"TouchUpInside ShowWeekTopicFileCommand"},
					{OrganizerLabel,"Text [WeekTopics_Organisers]"},
					{OrganizerValueLabel,"Text Owner"},
				});
			});

		}

		public static WeekTopicCell Create ()
		{
			return (WeekTopicCell)Nib.Instantiate (null, null) [0];
		}
	}
}

