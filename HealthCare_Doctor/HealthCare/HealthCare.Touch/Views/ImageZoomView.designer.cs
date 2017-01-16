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
	[Register ("ImageZoomView")]
	partial class ImageZoomView
	{
		[Outlet]
		UIKit.UIScrollView ImageScrollView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ImageScrollView != null) {
				ImageScrollView.Dispose ();
				ImageScrollView = null;
			}
		}
	}
}
