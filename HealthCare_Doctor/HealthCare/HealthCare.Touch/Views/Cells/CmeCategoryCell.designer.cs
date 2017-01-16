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
	[Register ("CmeCategoryCell")]
	partial class CmeCategoryCell
	{
		[Outlet]
		UIKit.UITextView ClassDescTV { get; set; }

		[Outlet]
		UIKit.UILabel ClassNameLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ClassDescTV != null) {
				ClassDescTV.Dispose ();
				ClassDescTV = null;
			}
			if (ClassNameLabel != null) {
				ClassNameLabel.Dispose ();
				ClassNameLabel = null;
			}
		}
	}
}
