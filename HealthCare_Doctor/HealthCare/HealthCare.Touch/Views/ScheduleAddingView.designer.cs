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
	[Register ("ScheduleAddingView")]
	partial class ScheduleAddingView
	{
		[Outlet]
		UIKit.UILabel DateLabel { get; set; }

		[Outlet]
		UIKit.UITextField DateTF { get; set; }

		[Outlet]
		UIKit.UILabel EndTimeLabel { get; set; }

		[Outlet]
		UIKit.UITextField EndTimeTF { get; set; }

		[Outlet]
		UIKit.UILabel HospitalLabel { get; set; }

		[Outlet]
		UIKit.UITextField HospitalTF { get; set; }

		[Outlet]
		UIKit.UIStepper QualityButton { get; set; }

		[Outlet]
		UIKit.UILabel QualityLabel { get; set; }

		[Outlet]
		UIKit.UITextField QualityTF { get; set; }

		[Outlet]
		UIKit.UILabel RepeatLabel { get; set; }

		[Outlet]
		UIKit.UITextField RepeatTF { get; set; }

		[Outlet]
		UIKit.UIButton SaveButton { get; set; }

		[Outlet]
		UIKit.UILabel StartTimeLabel { get; set; }

		[Outlet]
		UIKit.UITextField StartTimeTF { get; set; }

		[Outlet]
		UIKit.UILabel WeekCountLabel { get; set; }

		[Outlet]
		UIKit.UITextField WeekCountTF { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (DateLabel != null) {
				DateLabel.Dispose ();
				DateLabel = null;
			}
			if (DateTF != null) {
				DateTF.Dispose ();
				DateTF = null;
			}
			if (EndTimeLabel != null) {
				EndTimeLabel.Dispose ();
				EndTimeLabel = null;
			}
			if (EndTimeTF != null) {
				EndTimeTF.Dispose ();
				EndTimeTF = null;
			}
			if (HospitalLabel != null) {
				HospitalLabel.Dispose ();
				HospitalLabel = null;
			}
			if (HospitalTF != null) {
				HospitalTF.Dispose ();
				HospitalTF = null;
			}
			if (QualityButton != null) {
				QualityButton.Dispose ();
				QualityButton = null;
			}
			if (QualityLabel != null) {
				QualityLabel.Dispose ();
				QualityLabel = null;
			}
			if (QualityTF != null) {
				QualityTF.Dispose ();
				QualityTF = null;
			}
			if (RepeatLabel != null) {
				RepeatLabel.Dispose ();
				RepeatLabel = null;
			}
			if (RepeatTF != null) {
				RepeatTF.Dispose ();
				RepeatTF = null;
			}
			if (SaveButton != null) {
				SaveButton.Dispose ();
				SaveButton = null;
			}
			if (StartTimeLabel != null) {
				StartTimeLabel.Dispose ();
				StartTimeLabel = null;
			}
			if (StartTimeTF != null) {
				StartTimeTF.Dispose ();
				StartTimeTF = null;
			}
			if (WeekCountLabel != null) {
				WeekCountLabel.Dispose ();
				WeekCountLabel = null;
			}
			if (WeekCountTF != null) {
				WeekCountTF.Dispose ();
				WeekCountTF = null;
			}
		}
	}
}
