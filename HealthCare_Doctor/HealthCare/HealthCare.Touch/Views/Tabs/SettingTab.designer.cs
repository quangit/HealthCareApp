// WARNING
//
// This file has been generated automatically by Xamarin Studio Enterprise to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace HealthCare.Touch.Views.Tabs
{
	[Register ("SettingTab")]
	partial class SettingTab
	{
		[Outlet]
		UIKit.UIButton BandConnectButton { get; set; }

		[Outlet]
		UIKit.UILabel BandNameLabel { get; set; }

		[Outlet]
		UIKit.UIImageView BandStatusImage { get; set; }

		[Outlet]
		UIKit.UIButton BandSyncButton { get; set; }

		[Outlet]
		UIKit.UIActivityIndicatorView BandSyncIndicator { get; set; }

		[Outlet]
		UIKit.UILabel BandSyncTimeLabel { get; set; }

		[Outlet]
		UIKit.UIButton ChangePasswordButton { get; set; }

		[Outlet]
		UIKit.UIButton ProfileButton { get; set; }

		[Outlet]
		UIKit.UILabel ProfileHospitalName { get; set; }

		[Outlet]
		UIKit.UIImageView ProfileImage { get; set; }

		[Outlet]
		UIKit.UILabel ProfileLabel { get; set; }

		[Outlet]
		UIKit.UILabel ProfileNameLabel { get; set; }

		[Outlet]
		UIKit.UILabel PushNotificationInfoLabel { get; set; }

		[Outlet]
		UIKit.UILabel PushNotificationLabel { get; set; }

		[Outlet]
		UIKit.UISwitch PushNotificationSwitch { get; set; }

		[Outlet]
		UIKit.UILabel ViewTitleLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ChangePasswordButton != null) {
				ChangePasswordButton.Dispose ();
				ChangePasswordButton = null;
			}

			if (ProfileButton != null) {
				ProfileButton.Dispose ();
				ProfileButton = null;
			}

			if (ProfileHospitalName != null) {
				ProfileHospitalName.Dispose ();
				ProfileHospitalName = null;
			}

			if (ProfileImage != null) {
				ProfileImage.Dispose ();
				ProfileImage = null;
			}

			if (ProfileLabel != null) {
				ProfileLabel.Dispose ();
				ProfileLabel = null;
			}

			if (ProfileNameLabel != null) {
				ProfileNameLabel.Dispose ();
				ProfileNameLabel = null;
			}

			if (PushNotificationInfoLabel != null) {
				PushNotificationInfoLabel.Dispose ();
				PushNotificationInfoLabel = null;
			}

			if (PushNotificationLabel != null) {
				PushNotificationLabel.Dispose ();
				PushNotificationLabel = null;
			}

			if (PushNotificationSwitch != null) {
				PushNotificationSwitch.Dispose ();
				PushNotificationSwitch = null;
			}

			if (ViewTitleLabel != null) {
				ViewTitleLabel.Dispose ();
				ViewTitleLabel = null;
			}

			if (BandSyncButton != null) {
				BandSyncButton.Dispose ();
				BandSyncButton = null;
			}

			if (BandConnectButton != null) {
				BandConnectButton.Dispose ();
				BandConnectButton = null;
			}

			if (BandNameLabel != null) {
				BandNameLabel.Dispose ();
				BandNameLabel = null;
			}

			if (BandSyncTimeLabel != null) {
				BandSyncTimeLabel.Dispose ();
				BandSyncTimeLabel = null;
			}

			if (BandStatusImage != null) {
				BandStatusImage.Dispose ();
				BandStatusImage = null;
			}

			if (BandSyncIndicator != null) {
				BandSyncIndicator.Dispose ();
				BandSyncIndicator = null;
			}
		}
	}
}
