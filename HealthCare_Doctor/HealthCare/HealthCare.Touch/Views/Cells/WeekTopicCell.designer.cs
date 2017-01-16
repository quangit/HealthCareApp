// WARNING
//
// This file has been generated automatically by Xamarin Studio Enterprise to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace HealthCare.Touch.Views.Cells
{
	[Register ("WeekTopicCell")]
	partial class WeekTopicCell
	{
		[Outlet]
		UIKit.UILabel EndTimeLabel { get; set; }

		[Outlet]
		UIKit.UILabel EndTimeValueLabel { get; set; }

		[Outlet]
		UIKit.UIButton FileButton { get; set; }

		[Outlet]
		UIKit.UIImageView FileImage { get; set; }

		[Outlet]
		UIKit.UILabel OrganizerLabel { get; set; }

		[Outlet]
		UIKit.UILabel OrganizerValueLabel { get; set; }

		[Outlet]
		UIKit.UILabel StartTimeLabel { get; set; }

		[Outlet]
		UIKit.UILabel StartTimeValueLabel { get; set; }

		[Outlet]
		UIKit.UIImageView WeekTopicIconImage { get; set; }

		[Outlet]
		UIKit.UILabel WeekTopicTitleLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (EndTimeLabel != null) {
				EndTimeLabel.Dispose ();
				EndTimeLabel = null;
			}

			if (EndTimeValueLabel != null) {
				EndTimeValueLabel.Dispose ();
				EndTimeValueLabel = null;
			}

			if (StartTimeLabel != null) {
				StartTimeLabel.Dispose ();
				StartTimeLabel = null;
			}

			if (StartTimeValueLabel != null) {
				StartTimeValueLabel.Dispose ();
				StartTimeValueLabel = null;
			}

			if (WeekTopicIconImage != null) {
				WeekTopicIconImage.Dispose ();
				WeekTopicIconImage = null;
			}

			if (WeekTopicTitleLabel != null) {
				WeekTopicTitleLabel.Dispose ();
				WeekTopicTitleLabel = null;
			}

			if (OrganizerLabel != null) {
				OrganizerLabel.Dispose ();
				OrganizerLabel = null;
			}

			if (OrganizerValueLabel != null) {
				OrganizerValueLabel.Dispose ();
				OrganizerValueLabel = null;
			}

			if (FileButton != null) {
				FileButton.Dispose ();
				FileButton = null;
			}

			if (FileImage != null) {
				FileImage.Dispose ();
				FileImage = null;
			}
		}
	}
}
