using System;

using UIKit;
using Cirrious.MvvmCross.Binding.BindingContext;
using System.Collections.Generic;
using HealthCare.Touch.Utilities;
using HealthCare.Touch.Views.Cells;
using Cirrious.MvvmCross.Binding.Touch.Views;
using HealthCare.Core.ViewModels;
using Foundation;

namespace HealthCare.Touch.Views
{
	public partial class WeekTopicFileView : BaseViewController
	{
		private WeekTopicFileViewModel _vm => base.ViewModel as WeekTopicFileViewModel;

		public WeekTopicFileView () : base ("WeekTopicFileView", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			var source = new MvxSimpleTableViewSource(TableView, WeekTopicFileCell.Key, WeekTopicFileCell.Key);
			TableView.Source = source;
			TableView.RowHeight = 60;

			this.AddBindings(new Dictionary<object, string>
				{
					{TitleLabel, "Text [WeakTopicsFile_Title]"},
					{TopicTitleLabel, "Text Topic.Title"},
					{OrganizerLabel, "Text [WeekTopics_Organisers]"},
					{OrganizerValueLabel, "Text Topic.Owner"},
					{StartLabel, "Text [ScheduleAdding_StartTitle]"},
					{StartValueLabel, "Text Topic.StartDateTime, Converter = MilisecondToDate"},
					{EndLabel, "Text [ScheduleAdding_EndTitle]"},
					{EndValueLabel, "Text Topic.EndDateTime, Converter = MilisecondToDate"},
					{source, "ItemsSource TopicFiles; SelectionChangedCommand ShowFileTopicCommand"}
				});

			_vm.TopicSelected = () => {
				UIApplication.SharedApplication.OpenUrl(new NSUrl(_vm.SelectedTopic.FileUri));
			};
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


