using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HealthCare.DependencyServices;
using HealthCare.iOS.DependencyServices;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(PushNotificationRegister))]
namespace HealthCare.iOS.DependencyServices
{
    public class PushNotificationRegister : IPushNotificationRegister
    {
        public async Task UnRegisterPushNotification()
        {
            UIApplication.SharedApplication.UnregisterForRemoteNotifications();
        }

        public async Task RegiterPushNotification()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                UIUserNotificationType userNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
                UIUserNotificationSettings settings = UIUserNotificationSettings.GetSettingsForTypes(userNotificationTypes, null);
                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }
            else
            {
                UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
                UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
            }
        }
    }
}
