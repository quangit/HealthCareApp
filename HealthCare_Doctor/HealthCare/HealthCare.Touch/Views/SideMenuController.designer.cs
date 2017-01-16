// WARNING
//
// This file has been generated automatically by Xamarin Studio Enterprise to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace HealthCare.Touch.Views
{
	[Register ("SideMenuController")]
	partial class SideMenuController
	{
		[Outlet]
		UIKit.UIButton ChbaseButton { get; set; }

		[Outlet]
		UIKit.UIButton CmeLibraryButton { get; set; }

		[Outlet]
		UIKit.UIButton ConsultancyButton { get; set; }

		[Outlet]
		UIKit.UIButton HandbookButton { get; set; }

		[Outlet]
		UIKit.UIButton LogoutButton { get; set; }

		[Outlet]
		UIKit.UILabel MenuLabel { get; set; }

		[Outlet]
		UIKit.UILabel MoreLabel { get; set; }

		[Outlet]
		UIKit.UIButton ScheduleButton { get; set; }

		[Outlet]
		UIKit.UIButton SettingButton { get; set; }

		[Outlet]
		UIKit.UIButton SkypeButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CmeLibraryButton != null) {
				CmeLibraryButton.Dispose ();
				CmeLibraryButton = null;
			}

			if (ConsultancyButton != null) {
				ConsultancyButton.Dispose ();
				ConsultancyButton = null;
			}

			if (HandbookButton != null) {
				HandbookButton.Dispose ();
				HandbookButton = null;
			}

			if (LogoutButton != null) {
				LogoutButton.Dispose ();
				LogoutButton = null;
			}

			if (MenuLabel != null) {
				MenuLabel.Dispose ();
				MenuLabel = null;
			}

			if (MoreLabel != null) {
				MoreLabel.Dispose ();
				MoreLabel = null;
			}

			if (ScheduleButton != null) {
				ScheduleButton.Dispose ();
				ScheduleButton = null;
			}

			if (SettingButton != null) {
				SettingButton.Dispose ();
				SettingButton = null;
			}

			if (SkypeButton != null) {
				SkypeButton.Dispose ();
				SkypeButton = null;
			}

			if (ChbaseButton != null) {
				ChbaseButton.Dispose ();
				ChbaseButton = null;
			}
		}
	}
}
