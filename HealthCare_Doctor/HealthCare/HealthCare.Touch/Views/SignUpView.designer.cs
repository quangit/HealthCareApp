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
	[Register ("SignUpView")]
	partial class SignUpView
	{
		[Outlet]
		UIKit.UIButton BackButton { get; set; }

		[Outlet]
		UIKit.UITextField EmailTF { get; set; }

		[Outlet]
		UIKit.UITextField FirstNameTF { get; set; }

		[Outlet]
		UIKit.UITextField LastNameTF { get; set; }

		[Outlet]
		UIKit.UITextField PassTF { get; set; }

		[Outlet]
		UIKit.UITextField PhoneTF { get; set; }

		[Outlet]
		UIKit.UIButton RegisterButton { get; set; }

		[Outlet]
		UIKit.UITextField RePassTF { get; set; }

		[Outlet]
		UIKit.UILabel TitleLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (BackButton != null) {
				BackButton.Dispose ();
				BackButton = null;
			}
			if (EmailTF != null) {
				EmailTF.Dispose ();
				EmailTF = null;
			}
			if (FirstNameTF != null) {
				FirstNameTF.Dispose ();
				FirstNameTF = null;
			}
			if (LastNameTF != null) {
				LastNameTF.Dispose ();
				LastNameTF = null;
			}
			if (PassTF != null) {
				PassTF.Dispose ();
				PassTF = null;
			}
			if (PhoneTF != null) {
				PhoneTF.Dispose ();
				PhoneTF = null;
			}
			if (RegisterButton != null) {
				RegisterButton.Dispose ();
				RegisterButton = null;
			}
			if (RePassTF != null) {
				RePassTF.Dispose ();
				RePassTF = null;
			}
			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}
		}
	}
}
