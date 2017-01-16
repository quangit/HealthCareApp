using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using Android.Content;
using HealthCare.Helpers;
using HealthCare.Models;
using HealthCare.Droid.DependencyServices;
using Xamarin.Forms;
using Android.App;
using Android.Support.V4.App;
using HealthCare.DependencyServices;
using Java.Util;
using HealthCare.Objects;


[assembly: Xamarin.Forms.Dependency(typeof(LocalNotifier))]
namespace HealthCare.Droid.DependencyServices
{
    public class LocalNotifier : ILocalNotifier
    {
        /// <summary>
        /// Notifies the specified notification.
        /// </summary>
        /// <param name="notification">The notification.</param>
        public void Notify(LocalNotification notification)
        {
            var intent = createIntent(notification.Id);

            var serializedNotification = serializeNotification(notification);
            intent.PutExtra(ScheduledAlarmHandler.LocalNotificationKey, serializedNotification);

            var pendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, 0, intent, PendingIntentFlags.CancelCurrent);

            var _date = new DateTime(notification.NotifyTime.Ticks);
            DateTime dtBasis = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var triggerTime = (long)_date.ToUniversalTime().Subtract(dtBasis).TotalMilliseconds;
            
            var alarmManager = getAlarmManager();

            Debug.WriteLine(string.Format("Android_Notify_{0} {1}: ", notification.Title, notification.Text) + Common.UnixTimeStampToDateTime(triggerTime).ToString("G"));

            alarmManager.Set(AlarmType.RtcWakeup, triggerTime, pendingIntent);
            //showLocalNotification(notification.Id);
        }

        public void Cancel(int notificationId)
        {
            var intent = createIntent(notificationId);
            var pendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, 0, intent, PendingIntentFlags.CancelCurrent);

            var alarmManager = getAlarmManager();
            alarmManager.Cancel(pendingIntent);

            var notificationManager = getNotificationManager();
            notificationManager.Cancel(notificationId);
        }

        private Intent createIntent(int notificationId)
        {
            return new Intent(Android.App.Application.Context, typeof(ScheduledAlarmHandler))
                .SetAction("LocalNotifierIntent" + notificationId);
        }

        private NotificationManager getNotificationManager()
        {
            var notificationManager = Android.App.Application.Context.GetSystemService(Context.NotificationService) as NotificationManager;
            return notificationManager;
        }

        private AlarmManager getAlarmManager()
        {
            var alarmManager = Android.App.Application.Context.GetSystemService(Context.AlarmService) as AlarmManager;
            return alarmManager;
        }

        private string serializeNotification(LocalNotification notification)
        {
            var xmlSerializer = new XmlSerializer(notification.GetType());
            using (var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, notification);
                return stringWriter.ToString();
            }
        }

        private long notifyTimeInMilliseconds(DateTime notifyTime)
        {
            var utcTime = notifyTime.ToLocalTime();
            var epochDifference = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;

            var utcAlarmTimeInMillis = utcTime.AddSeconds(-epochDifference).Ticks / 10000;
            return utcAlarmTimeInMillis;    
        }

    }
}