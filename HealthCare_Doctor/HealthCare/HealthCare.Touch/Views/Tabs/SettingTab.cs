
using System;

using Foundation;
using UIKit;
using HealthCare.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using System.Collections.Generic;
using Cirrious.MvvmCross.Binding.Touch.Views;

namespace HealthCare.Touch.Views.Tabs
{
	public partial class SettingTab : BaseViewController
	{
		private MvxImageViewLoader _logoloader;
		private MvxImageViewLoader _bandloader;

		public SettingTab () : base ("SettingTab", null, true)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			_logoloader = new MvxImageViewLoader (() => ProfileImage);
			_bandloader = new MvxImageViewLoader (() => BandStatusImage);

			this.AddBindings (new Dictionary<object, string> () {
				{ ViewTitleLabel, "Text [Settings]" },
				{ ProfileLabel, "Text [UpdateProfile_Profile]" },
				{ ProfileNameLabel, "Text Doctor.Name" },
				{ ProfileHospitalName, "Text Doctor.Desc" },
				{ _logoloader,"ImageUrl Doctor.Photo" },
				{ ProfileButton,"TouchUpInside UpdateProfileCommand"},
				{PushNotificationLabel, "Text Settings.PushConsent,Converter=Consent"},
				{PushNotificationSwitch, "On Settings.PushConsent,Mode=TwoWay"},
				{PushNotificationInfoLabel, "Text [Setting_NotificationInfo]"},
				{ChangePasswordButton,"Title [SetPassword_Title]; TouchUpInside ChangePassCommand"},
				{_bandloader,"ImageUrl BandSetting.StatusImage"},
				{BandNameLabel,"Text BandSetting.DeviceName"},
				{BandSyncTimeLabel,"Text BandSetting.LastSyncStr"},
				{BandConnectButton,"TouchUpInside BandSetting.ConnectCommand; Title [Setting_BandConnect]"},
				{BandSyncButton,"TouchUpInside BandSetting.SyncCommand;"},

			});
		}
	}
}

