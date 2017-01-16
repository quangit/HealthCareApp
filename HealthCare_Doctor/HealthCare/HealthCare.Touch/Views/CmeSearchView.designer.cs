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
	[Register ("CmeSearchView")]
	partial class CmeSearchView
	{
		[Outlet]
		UIKit.UILabel EmptyLabel { get; set; }

		[Outlet]
		UIKit.UIButton SearchButton { get; set; }

		[Outlet]
		UIKit.UITextField SearchTF { get; set; }

		[Outlet]
		UIKit.UITableView TableView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (EmptyLabel != null) {
				EmptyLabel.Dispose ();
				EmptyLabel = null;
			}
			if (SearchButton != null) {
				SearchButton.Dispose ();
				SearchButton = null;
			}
			if (SearchTF != null) {
				SearchTF.Dispose ();
				SearchTF = null;
			}
			if (TableView != null) {
				TableView.Dispose ();
				TableView = null;
			}
		}
	}
}
