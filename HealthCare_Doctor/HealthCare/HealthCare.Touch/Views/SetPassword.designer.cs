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
	[Register ("SetPassword")]
	partial class SetPassword
	{
		[Outlet]
		UIKit.UILabel ConfirmLabel { get; set; }

		[Outlet]
		UIKit.UITextField ConfirmTF { get; set; }

		[Outlet]
		UIKit.UIBarButtonItem EditBarButton { get; set; }

		[Outlet]
		UIKit.UIView EditView { get; set; }

		[Outlet]
		UIKit.UILabel EmailLabel { get; set; }

		[Outlet]
		UIKit.UILabel NewPasswordLabel { get; set; }

		[Outlet]
		UIKit.UITextField NewPasswordTF { get; set; }

		[Outlet]
		UIKit.UILabel OldPasswordLabel { get; set; }

		[Outlet]
		UIKit.UITextField OldPasswordTF { get; set; }

		[Outlet]
		UIKit.UIButton ResetButton { get; set; }

		[Outlet]
		UIKit.UILabel SetPasswordInfoLabel { get; set; }

		[Outlet]
		UIKit.UIBarButtonItem SubmitBarButton { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ConfirmTF != null) {
				ConfirmTF.Dispose ();
				ConfirmTF = null;
			}
			if (NewPasswordTF != null) {
				NewPasswordTF.Dispose ();
				NewPasswordTF = null;
			}
			if (OldPasswordTF != null) {
				OldPasswordTF.Dispose ();
				OldPasswordTF = null;
			}
			if (ResetButton != null) {
				ResetButton.Dispose ();
				ResetButton = null;
			}
			if (SetPasswordInfoLabel != null) {
				SetPasswordInfoLabel.Dispose ();
				SetPasswordInfoLabel = null;
			}
			if (SubmitBarButton != null) {
				SubmitBarButton.Dispose ();
				SubmitBarButton = null;
			}
		}
	}
}
