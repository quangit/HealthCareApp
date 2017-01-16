using System;
using System.Linq;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using HealthCare.DependencyServices;
using HealthCare.Models;
using HealthCare.Objects;
using HealthCare.WinPhone.DependencyServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocalNotifier))]

namespace HealthCare.WinPhone.DependencyServices
{
    /// <summary>
    ///     Implementation of ILocalNotifier for Windows Phone 8.1 and WindowsStore
    /// </summary>
    public class LocalNotifier : ILocalNotifier
    {
        public void Notify(LocalNotification notification)
        {
            var tileXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText02);

            var tileTitle = tileXml.GetElementsByTagName("text");
            ((XmlElement) tileTitle[0]).InnerText = notification.Title;
            ((XmlElement) tileTitle[1]).InnerText = notification.Text;

            var correctedTime = notification.NotifyTime <= DateTime.Now
                ? DateTime.Now.AddMilliseconds(100)
                : notification.NotifyTime;

            var time = new DateTime(correctedTime.Ticks);

            var scheduledToastNotification = new ScheduledToastNotification(tileXml, time)
            {
                Id = notification.Id.ToString()
            };

            Cancel(notification.Id);

            ToastNotificationManager.CreateToastNotifier().AddToSchedule(scheduledToastNotification);
        }

        public void Cancel(int notificationId)
        {
            var scheduledNotifications = ToastNotificationManager.CreateToastNotifier().GetScheduledToastNotifications();
            var notification =
                scheduledNotifications.FirstOrDefault(
                    n => n.Id.Equals(notificationId.ToString(), StringComparison.OrdinalIgnoreCase));

            if (notification != null)
            {
                ToastNotificationManager.CreateToastNotifier().RemoveFromSchedule(notification);
            }
        }
    }
}