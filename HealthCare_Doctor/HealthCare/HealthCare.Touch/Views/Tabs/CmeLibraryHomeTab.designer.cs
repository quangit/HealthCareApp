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

namespace HealthCare.Touch.Views.Tabs
{
	[Register ("CmeLibraryHomeTab")]
	partial class CmeLibraryHomeTab
	{
		[Outlet]
		UIKit.UICollectionView CategoryCollection { get; set; }

		[Outlet]
		UIKit.UISearchBar CategorySearchBar { get; set; }

		[Outlet]
		UIKit.UIBarButtonItem ClassSearchButton { get; set; }

		[Outlet]
		UIKit.UILabel EmptyLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (CategoryCollection != null) {
				CategoryCollection.Dispose ();
				CategoryCollection = null;
			}
			if (CategorySearchBar != null) {
				CategorySearchBar.Dispose ();
				CategorySearchBar = null;
			}
			if (ClassSearchButton != null) {
				ClassSearchButton.Dispose ();
				ClassSearchButton = null;
			}
			if (EmptyLabel != null) {
				EmptyLabel.Dispose ();
				EmptyLabel = null;
			}
		}
	}
}
