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
	[Register ("ConsultResponseCell")]
	partial class ConsultResponseCell
	{
		[Outlet]
		UIKit.UILabel AnswerCountLabel { get; set; }

		[Outlet]
		UIKit.UIImageView DoctorImage { get; set; }

		[Outlet]
		UIKit.UILabel NameLabel { get; set; }

		[Outlet]
		UIKit.UILabel ReplyLabel { get; set; }

		[Outlet]
		UIKit.UITextView ReplyTV { get; set; }

		[Outlet]
		UIKit.UIView ResponseView { get; set; }

		[Outlet]
		UIKit.UILabel TimeLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (AnswerCountLabel != null) {
				AnswerCountLabel.Dispose ();
				AnswerCountLabel = null;
			}
			if (DoctorImage != null) {
				DoctorImage.Dispose ();
				DoctorImage = null;
			}
			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}
			if (ReplyLabel != null) {
				ReplyLabel.Dispose ();
				ReplyLabel = null;
			}
			if (ReplyTV != null) {
				ReplyTV.Dispose ();
				ReplyTV = null;
			}
			if (ResponseView != null) {
				ResponseView.Dispose ();
				ResponseView = null;
			}
			if (TimeLabel != null) {
				TimeLabel.Dispose ();
				TimeLabel = null;
			}
		}
	}
}
