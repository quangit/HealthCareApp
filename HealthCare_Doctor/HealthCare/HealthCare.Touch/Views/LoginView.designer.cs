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
	[Register ("LoginView")]
	partial class LoginView
	{
		[Outlet]
		UIKit.UIButton LoginButton { get; set; }

		[Outlet]
		UIKit.UITextField PassTF { get; set; }

		[Outlet]
		UIKit.UITextField PhoneTF { get; set; }

		[Outlet]
		UIKit.UIButton RegisterButton { get; set; }

		[Outlet]
		UIKit.UILabel RemeberLabel { get; set; }

		[Outlet]
		UIKit.UISwitch RememberSwitch { get; set; }

		[Outlet]
		UIKit.UIButton ResetButton { get; set; }

		[Outlet]
		UIKit.UILabel TitleLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (LoginButton != null) {
				LoginButton.Dispose ();
				LoginButton = null;
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
			if (RemeberLabel != null) {
				RemeberLabel.Dispose ();
				RemeberLabel = null;
			}
			if (RememberSwitch != null) {
				RememberSwitch.Dispose ();
				RememberSwitch = null;
			}
			if (ResetButton != null) {
				ResetButton.Dispose ();
				ResetButton = null;
			}
			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}
		}
	}
}