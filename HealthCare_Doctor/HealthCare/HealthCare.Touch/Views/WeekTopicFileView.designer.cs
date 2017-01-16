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

namespace HealthCare.Touch.Views
{
	[Register ("WeekTopicFileView")]
	partial class WeekTopicFileView
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel EndLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel EndValueLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel OrganizerLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel OrganizerValueLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel StartLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel StartValueLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView TableView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel TitleLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel TopicTitleLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (EndLabel != null) {
				EndLabel.Dispose ();
				EndLabel = null;
			}
			if (EndValueLabel != null) {
				EndValueLabel.Dispose ();
				EndValueLabel = null;
			}
			if (OrganizerLabel != null) {
				OrganizerLabel.Dispose ();
				OrganizerLabel = null;
			}
			if (OrganizerValueLabel != null) {
				OrganizerValueLabel.Dispose ();
				OrganizerValueLabel = null;
			}
			if (StartLabel != null) {
				StartLabel.Dispose ();
				StartLabel = null;
			}
			if (StartValueLabel != null) {
				StartValueLabel.Dispose ();
				StartValueLabel = null;
			}
			if (TableView != null) {
				TableView.Dispose ();
				TableView = null;
			}
			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}
			if (TopicTitleLabel != null) {
				TopicTitleLabel.Dispose ();
				TopicTitleLabel = null;
			}
		}
	}
}
