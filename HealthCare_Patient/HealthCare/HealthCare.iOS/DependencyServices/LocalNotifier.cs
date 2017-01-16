using System.Linq;
using HealthCare.DependencyServices;
using HealthCare.Objects;


#if __UNIFIED__
using UIKit;
using Foundation;
using HealthCare.Helpers;
using HealthCare.Models;
using HealthCare.iOS.DependencyServices;
using System;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif

[assembly: Xamarin.Forms.Dependency(typeof(LocalNotifier))]
namespace HealthCare.iOS.DependencyServices
{
    /// <summary>
    /// Implementation of ILocalNotifier for iOS
    /// </summary>
    public class LocalNotifier : ILocalNotifier
    {
        private const string NotificationKey = "HealthcareNotificationKey";

        /// <summary>
        /// Notifies the specified notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        public void Notify(LocalNotification notification)
        {
            var nativeNotification = createNativeNotification(notification);

            UIApplication.SharedApplication.ScheduleLocalNotification(nativeNotification);
        }

        /// <summary>
        /// Cancels the specified notification identifier.
        /// </summary>
        /// <param name="notificationId">The notification identifier.</param>
        public void Cancel(int notificationId)
        {
            var notifications = UIApplication.SharedApplication.ScheduledLocalNotifications;
            var notification = notifications.Where(n => n.UserInfo.ContainsKey(NSObject.FromObject(NotificationKey)))
                .FirstOrDefault(n => n.UserInfo[NotificationKey].Equals(NSObject.FromObject(notificationId)));

            if (notification != null)
            {
                UIApplication.SharedApplication.CancelLocalNotification(notification);
            }
        }

        private UILocalNotification createNativeNotification(LocalNotification notification)
        {
            var nativeNotification = new UILocalNotification
            {
                AlertAction = notification.Title,
                AlertBody = notification.Text,
                FireDate = Utils.DateTimeToNSDate(notification.NotifyTime),
                UserInfo = NSDictionary.FromObjectAndKey(NSObject.FromObject(notification.Id), NSObject.FromObject(NotificationKey))
            };

            return nativeNotification;
        }

    }
}