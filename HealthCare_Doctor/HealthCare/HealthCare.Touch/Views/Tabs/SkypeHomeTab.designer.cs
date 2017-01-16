// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace HealthCare.Touch.Views.Tabs
{
	[Register ("SkypeHomeTab")]
	partial class SkypeHomeTab
	{
		[Outlet]
		UIKit.UIButton TestButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TestButton != null) {
				TestButton.Dispose ();
				TestButton = null;
			}
		}
	}
}
