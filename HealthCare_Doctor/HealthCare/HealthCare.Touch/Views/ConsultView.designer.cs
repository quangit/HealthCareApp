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
	[Register ("ConsultView")]
	partial class ConsultView
	{
		[Outlet]
		UIKit.UILabel ConsultLabel { get; set; }

		[Outlet]
		UIKit.UITextView ConsultValueTV { get; set; }

		[Outlet]
		UIKit.UIImageView DoctorImage { get; set; }

		[Outlet]
		UIKit.UILabel DoctorNameLabel { get; set; }

		[Outlet]
		UIKit.UIButton ImageZoomButton { get; set; }

		[Outlet]
		UIKit.UIBarButtonItem InviteBarButton { get; set; }

		[Outlet]
		UIKit.UIImageView LandscapeImage { get; set; }

		[Outlet]
		UIKit.UILabel PatientDescValueLabel { get; set; }

		[Outlet]
		UIKit.UIImageView PatientImage { get; set; }

		[Outlet]
		UIKit.UILabel PatientNameLabel { get; set; }

		[Outlet]
		UIKit.UIBarButtonItem ReplyBarButton { get; set; }

		[Outlet]
		UIKit.UILabel ReplyCountLabel { get; set; }

		[Outlet]
		UIKit.UIBarButtonItem SkypeBarButton { get; set; }

		[Outlet]
		UIKit.UITableView TableView { get; set; }

		[Outlet]
		UIKit.UIToolbar Toolbar { get; set; }

		[Outlet]
		UIKit.UILabel WhenCreatedLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ConsultLabel != null) {
				ConsultLabel.Dispose ();
				ConsultLabel = null;
			}
			if (ConsultValueTV != null) {
				ConsultValueTV.Dispose ();
				ConsultValueTV = null;
			}
			if (DoctorImage != null) {
				DoctorImage.Dispose ();
				DoctorImage = null;
			}
			if (DoctorNameLabel != null) {
				DoctorNameLabel.Dispose ();
				DoctorNameLabel = null;
			}
			if (ImageZoomButton != null) {
				ImageZoomButton.Dispose ();
				ImageZoomButton = null;
			}
			if (InviteBarButton != null) {
				InviteBarButton.Dispose ();
				InviteBarButton = null;
			}
			if (LandscapeImage != null) {
				LandscapeImage.Dispose ();
				LandscapeImage = null;
			}
			if (PatientDescValueLabel != null) {
				PatientDescValueLabel.Dispose ();
				PatientDescValueLabel = null;
			}
			if (PatientImage != null) {
				PatientImage.Dispose ();
				PatientImage = null;
			}
			if (PatientNameLabel != null) {
				PatientNameLabel.Dispose ();
				PatientNameLabel = null;
			}
			if (ReplyBarButton != null) {
				ReplyBarButton.Dispose ();
				ReplyBarButton = null;
			}
			if (ReplyCountLabel != null) {
				ReplyCountLabel.Dispose ();
				ReplyCountLabel = null;
			}
			if (SkypeBarButton != null) {
				SkypeBarButton.Dispose ();
				SkypeBarButton = null;
			}
			if (TableView != null) {
				TableView.Dispose ();
				TableView = null;
			}
			if (Toolbar != null) {
				Toolbar.Dispose ();
				Toolbar = null;
			}
			if (WhenCreatedLabel != null) {
				WhenCreatedLabel.Dispose ();
				WhenCreatedLabel = null;
			}
		}
	}
}
