
using Foundation;
using UIKit;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using HealthCare.Core.Models;
using LumiaLoyalty.Touch.Utilities;
using System.Diagnostics;

namespace HealthCare.Touch
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxApplicationDelegate
    {
        // class-level declarations
        UIWindow window;

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
				// registers for push for iOS8
				var settings = UIUserNotificationSettings.GetSettingsForTypes (
					UIUserNotificationType.Alert
					| UIUserNotificationType.Badge
					| UIUserNotificationType.Sound, 
					new NSSet ());
				UIApplication.SharedApplication.RegisterUserNotificationSettings (settings); 
				UIApplication.SharedApplication.RegisterForRemoteNotifications ();
			} else {

				UIApplication.SharedApplication.RegisterForRemoteNotificationTypes (UIRemoteNotificationType.Alert
					| UIRemoteNotificationType.Badge
					| UIRemoteNotificationType.Sound);

				//var enabledTypes = UIApplication.SharedApplication.EnabledRemoteNotificationTypes; 
			}




            // create a new window instance based on the screen size
            window = new UIWindow(UIScreen.MainScreen.Bounds);

            var setup = new Setup(this, window);
            setup.Initialize();

            var startup = Mvx.Resolve<IMvxAppStart>();
            startup.Start();

            // make the window visible
            window.MakeKeyAndVisible();

            return true;
        }


		public override void RegisteredForRemoteNotifications (UIApplication application, NSData deviceToken)
		{
			// NOTE: Don't call the base implementation on a Model class
			// see http://docs.xamarin.com/guides/ios/application_fundamentals/delegates,_protocols,_and_events
			var t = deviceToken.ToString ();
			t = t.Replace ("<", "");
			t = t.Replace (">", "");
			t = t.Replace (" ", "");
			Debug.WriteLine ("Push channel Uri: " + t);
			Data.PushChannelUri = t;
		}

		public override void DidReceiveRemoteNotification (UIApplication application, NSDictionary userInfo, System.Action<UIBackgroundFetchResult> completionHandler)
		{
			// NOTE: Don't call the base implementation on a Model class
			// see http://docs.xamarin.com/guides/ios/application_fundamentals/delegates,_protocols,_and_events
			if (application.ApplicationState != UIApplicationState.Active) {
				NSObject idObj;
				if (userInfo != null) {
					bool success = userInfo.TryGetValue (new NSString ("id"), out idObj);

					if (success) {
						var id = idObj.ToString ();
						MessageBox.Show ("Push received id ", id);
						Debug.WriteLine ("Push received id: " + id);

//						if (!string.IsNullOrWhiteSpace (id)) {
//							Data.ToastParam = new ToastParam { pid = id, parent = false };
//							if (Data.CurrentVm is MyMvxViewModel) {
//								((MyMvxViewModel)Data.CurrentVm).DoDealToast (Data.ToastParam);
//							}
//						} else
							//Data.ToastParam = null;
					}
				}
			}

		}

    }
}