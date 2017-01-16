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
	[Register ("ConsultReplyDialog")]
	partial class ConsultReplyDialog
	{
		[Outlet]
		UIKit.UILabel ReplyHintLabel { get; set; }

		[Outlet]
		UIKit.UILabel ReplyTitleLabel { get; set; }

		[Outlet]
		UIKit.UITextView ReplyTV { get; set; }

		[Outlet]
		UIKit.UIButton SubmitButton { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ReplyHintLabel != null) {
				ReplyHintLabel.Dispose ();
				ReplyHintLabel = null;
			}
			if (ReplyTitleLabel != null) {
				ReplyTitleLabel.Dispose ();
				ReplyTitleLabel = null;
			}
			if (ReplyTV != null) {
				ReplyTV.Dispose ();
				ReplyTV = null;
			}
			if (SubmitButton != null) {
				SubmitButton.Dispose ();
				SubmitButton = null;
			}
		}
	}
}
