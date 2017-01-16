// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace HealthCare.Touch.Views.Tabs
{
	[Register ("WeekTopicHomeTab")]
	partial class WeekTopicHomeTab
	{
		[Outlet]
		UIKit.UILabel EmptyLabel { get; set; }

		[Outlet]
		UIKit.UITableView TableView { get; set; }

		[Outlet]
		UIKit.UILabel TitleLabel { get; set; }

		[Outlet]
		UIKit.UISegmentedControl WeekTopicFilterSegControl { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (EmptyLabel != null) {
				EmptyLabel.Dispose ();
				EmptyLabel = null;
			}
			if (TableView != null) {
				TableView.Dispose ();
				TableView = null;
			}
			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}
			if (WeekTopicFilterSegControl != null) {
				WeekTopicFilterSegControl.Dispose ();
				WeekTopicFilterSegControl = null;
			}
		}
	}
}
