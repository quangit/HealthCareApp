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
	[Register ("CmeClassView")]
	partial class CmeClassView
	{
		[Outlet]
		UIKit.UIBarButtonItem BackButton { get; set; }

		[Outlet]
		UIKit.UIBarButtonItem ForwardButton { get; set; }

		[Outlet]
		UIKit.UIBarButtonItem HomeButton { get; set; }

		[Outlet]
		UIKit.UILabel TitleLabel { get; set; }

		[Outlet]
		UIKit.UIWebView WebView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (BackButton != null) {
				BackButton.Dispose ();
				BackButton = null;
			}
			if (ForwardButton != null) {
				ForwardButton.Dispose ();
				ForwardButton = null;
			}
			if (HomeButton != null) {
				HomeButton.Dispose ();
				HomeButton = null;
			}
			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}
			if (WebView != null) {
				WebView.Dispose ();
				WebView = null;
			}
		}
	}
}
