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
	[Register ("CmeLibraryCell")]
	partial class CmeLibraryCell
	{
		[Outlet]
		UIKit.UILabel CategoryName { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (CategoryName != null) {
				CategoryName.Dispose ();
				CategoryName = null;
			}
		}
	}
}
