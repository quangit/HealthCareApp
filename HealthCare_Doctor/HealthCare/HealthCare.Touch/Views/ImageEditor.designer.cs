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
	[Register ("ImageEditor")]
	partial class ImageEditor
	{
		[Outlet]
		UIKit.UIBarButtonItem ConfirmBarButton { get; set; }

		[Outlet]
		UIKit.UIBarButtonItem CropBarButton { get; set; }

		[Outlet]
		UIKit.UIBarButtonItem RotateBarButton { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ConfirmBarButton != null) {
				ConfirmBarButton.Dispose ();
				ConfirmBarButton = null;
			}
			if (CropBarButton != null) {
				CropBarButton.Dispose ();
				CropBarButton = null;
			}
			if (RotateBarButton != null) {
				RotateBarButton.Dispose ();
				RotateBarButton = null;
			}
		}
	}
}
