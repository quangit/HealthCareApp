using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using FFImageLoading.Forms.Touch;
using Foundation;
using HealthCare.DependencyServices;
using UIKit;
using HealthCare.Helpers;
using HealthCare.ViewModels;
using Xamarin;
using Xamarin.Forms;
using XLabs.Forms.Controls;
using XLabs.Ioc;

//[assembly: Xamarin.Forms.ExportRenderer(typeof(Telerik.XamarinForms.Input.RadCalendar), typeof(Telerik.XamarinForms.InputRenderer.iOS.CalendarRenderer))]
namespace HealthCare.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    //public partial class AppDelegate : XFormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
			CachedImageRenderer.Init();
            global::Xamarin.Forms.Forms.Init();

            FormsMaps.Init();
            ServicePointManager.ServerCertificateValidationCallback += (o, certificate, chain, errors) => true;
            //AdvancedTimer.Forms.Plugin.iOS.AdvancedTimerImplementation.Init();
            DependencyService.Get<IPushNotificationRegister>().RegiterPushNotification();
            // Code for starting up the Xamarin Test Cloud Agent
#if ENABLE_TEST_CLOUD
            Xamarin.Calabash.Start();
#endif
            LoadApplication(new App());
            AppConstant.ScreenWidth = UIScreen.MainScreen.Bounds.Width >= UIScreen.MainScreen.Bounds.Height ?
                UIScreen.MainScreen.Bounds.Height : UIScreen.MainScreen.Bounds.Width ;
            UIApplication.CheckForEventAndDelegateMismatches = false;

            return base.FinishedLaunching(app, options);
        }

        public override void DidRegisterUserNotificationSettings(UIApplication application, UIUserNotificationSettings notificationSettings)
        {
            UIApplication.SharedApplication.RegisterForRemoteNotifications();
        }

        public async override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            var deviceTokenString = deviceToken.ToString().Replace("<", "").Replace(">", "").Replace(" ", "");
            HealthCare.Helpers.Settings.DeviceChannel = deviceTokenString;
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            //Bug Record: https://forums.xamarin.com/discussion/48099/push-notitfication-ios-didrecievepushnotification
            //base.DidReceiveRemoteNotification(application, userInfo, completionHandler);
            UIApplicationState state = application.ApplicationState;
            ProcessNotification(userInfo, false);//state != UIApplicationState.Active
        }

        void ProcessNotification(NSDictionary options, bool fromFinishedLaunching)
        {
            // Check to see if the dictionary has the aps key.  This is the notification payload you would have sent
            if (null != options && options.ContainsKey(new NSString("aps")))
            {
                //Get the aps dictionary
                NSDictionary aps = options.ObjectForKey(new NSString("aps")) as NSDictionary;

                string alert = string.Empty;

                //Extract the alert text
                // NOTE: If you're using the simple alert by just specifying
                // "  aps:{alert:"alert msg here"}  " this will work fine.
                // But if you're using a complex alert with Localization keys, etc.,
                // your "alert" object from the aps dictionary will be another NSDictionary.
                // Basically the json gets dumped right into a NSDictionary,
                // so keep that in mind.
                if (aps.ContainsKey(new NSString("alert")))
                    alert = (aps[new NSString("alert")] as NSString).ToString();

                //If this came from the ReceivedRemoteNotification while the app was running,
                // we of course need to manually process things like the sound, badge, and alert.
                if (!fromFinishedLaunching)
                {
                    //Manually show an alert
                    if (!string.IsNullOrEmpty(alert))
                    {
                        UIAlertView avAlert = new UIAlertView("Healthcare", alert, null, "Ok", null);
                        avAlert.Show();
                    }
                }
            }
        }
    }
}

