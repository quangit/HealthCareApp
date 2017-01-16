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
	[Register ("ConsultCell")]
	partial class ConsultCell
	{
		[Outlet]
		UIKit.UILabel AndLabel { get; set; }

		[Outlet]
		UIKit.UIImageView DoctorImage { get; set; }

		[Outlet]
		UIKit.UILabel DoctorNameLabel { get; set; }

		[Outlet]
		UIKit.UILabel HospitalNameLabel { get; set; }

		[Outlet]
		UIKit.UILabel PatientAgeLabel { get; set; }

		[Outlet]
		UIKit.UIImageView PatientImage { get; set; }

		[Outlet]
		UIKit.UILabel PatientNameLabel { get; set; }

		[Outlet]
		UIKit.UILabel ReplyCountLabel { get; set; }

		[Outlet]
		UIKit.UITextView SymptomTV { get; set; }

		[Outlet]
		UIKit.UILabel WhenCreatedLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (DoctorImage != null) {
				DoctorImage.Dispose ();
				DoctorImage = null;
			}
			if (DoctorNameLabel != null) {
				DoctorNameLabel.Dispose ();
				DoctorNameLabel = null;
			}
			if (HospitalNameLabel != null) {
				HospitalNameLabel.Dispose ();
				HospitalNameLabel = null;
			}
			if (PatientAgeLabel != null) {
				PatientAgeLabel.Dispose ();
				PatientAgeLabel = null;
			}
			if (PatientImage != null) {
				PatientImage.Dispose ();
				PatientImage = null;
			}
			if (PatientNameLabel != null) {
				PatientNameLabel.Dispose ();
				PatientNameLabel = null;
			}
			if (ReplyCountLabel != null) {
				ReplyCountLabel.Dispose ();
				ReplyCountLabel = null;
			}
			if (SymptomTV != null) {
				SymptomTV.Dispose ();
				SymptomTV = null;
			}
			if (WhenCreatedLabel != null) {
				WhenCreatedLabel.Dispose ();
				WhenCreatedLabel = null;
			}
		}
	}
}
