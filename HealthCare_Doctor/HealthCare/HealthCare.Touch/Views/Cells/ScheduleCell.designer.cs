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

namespace HealthCare.Touch.Views.Cells
{
	[Register ("ScheduleCell")]
	partial class ScheduleCell
	{
		[Outlet]
		UIKit.UIImageView HospitalLogoImage { get; set; }

		[Outlet]
		UIKit.UILabel HospitalNameLabel { get; set; }

		[Outlet]
		UIKit.UILabel ScheduleTitleLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (HospitalLogoImage != null) {
				HospitalLogoImage.Dispose ();
				HospitalLogoImage = null;
			}
			if (HospitalNameLabel != null) {
				HospitalNameLabel.Dispose ();
				HospitalNameLabel = null;
			}
			if (ScheduleTitleLabel != null) {
				ScheduleTitleLabel.Dispose ();
				ScheduleTitleLabel = null;
			}
		}
	}
}
