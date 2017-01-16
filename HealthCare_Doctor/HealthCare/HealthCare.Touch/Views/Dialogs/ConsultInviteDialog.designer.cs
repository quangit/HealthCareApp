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

namespace HealthCare.Touch.Views.Dialogs
{
	[Register ("ConsultInviteDialog")]
	partial class ConsultInviteDialog
	{
		[Outlet]
		UIKit.UILabel ContentHintLabel { get; set; }

		[Outlet]
		UIKit.UILabel ContentLabel { get; set; }

		[Outlet]
		UIKit.UITextView ContentValueTV { get; set; }

		[Outlet]
		UIKit.UILabel EmailLabel { get; set; }

		[Outlet]
		UIKit.UITextField EmailValueTF { get; set; }

		[Outlet]
		UIKit.UIButton SubmitButton { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ContentHintLabel != null) {
				ContentHintLabel.Dispose ();
				ContentHintLabel = null;
			}
			if (ContentLabel != null) {
				ContentLabel.Dispose ();
				ContentLabel = null;
			}
			if (ContentValueTV != null) {
				ContentValueTV.Dispose ();
				ContentValueTV = null;
			}
			if (EmailLabel != null) {
				EmailLabel.Dispose ();
				EmailLabel = null;
			}
			if (EmailValueTF != null) {
				EmailValueTF.Dispose ();
				EmailValueTF = null;
			}
			if (SubmitButton != null) {
				SubmitButton.Dispose ();
				SubmitButton = null;
			}
		}
	}
}
