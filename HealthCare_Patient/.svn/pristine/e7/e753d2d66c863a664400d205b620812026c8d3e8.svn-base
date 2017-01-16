using System;
using System.IO;
using System.Xml.Serialization;
using Android;
using Android.App;
using Android.Content;
using Android.Graphics;
using HealthCare.Helpers;
using HealthCare.Models;
using Android.Support.V4.App;
using HealthCare.Objects;
using Xamarin.Forms;
using Application = Android.App.Application;


namespace HealthCare.Droid
{
    [BroadcastReceiver]
    public class ScheduledAlarmHandler : BroadcastReceiver
    {
        public const string LocalNotificationKey = "LocalNotification";

        public override void OnReceive(Context context, Intent intent)
        {
            var extra = intent.GetStringExtra(LocalNotificationKey);
            var notification = serializeFromString(extra);

            //var nativeNotification = createNativeNotification(notification);
            //var manager = getNotificationManager();
            //manager.Notify(notification.Id, nativeNotification);
            showLocalNotification(notification);
        }

        private NotificationManager getNotificationManager()
        {
            var notificationManager = Application.Context.GetSystemService(Context.NotificationService) as NotificationManager;
            return notificationManager;
        }

        private Notification createNativeNotification(LocalNotification notification)
        {
            var builder = new Notification.Builder(Application.Context)
                .SetContentTitle(notification.Title)
                .SetContentText(notification.Text)
                //                .SetSmallIcon(Resource.Drawable.IcDialogEmail);
                .SetSmallIcon(Application.Context.ApplicationInfo.Icon);

            var nativeNotification = builder.Build();
            return nativeNotification;
        }

        public void showLocalNotification(LocalNotification notification)
        {
            // Pass value to the next form:
            // Bundle valuesForActivity = new Bundle();
            // valuesForActivity.PutInt("count", count);

            // When the user clicks the notification, SecondActivity will start up.
            //Intent resultIntent = new Intent(this, typeof(SecondActivity));

            // Pass some values to SecondActivity:
            //resultIntent.PutExtras(valuesForActivity); 

            var ctx = Application.Context;

            Bitmap bitmap = //GetImageBitmapFromUrl(image);
            BitmapFactory.DecodeResource(Forms.Context.Resources, Resource.Drawable.icon);

            //Build the notification:
            NotificationCompat.Builder builder = new NotificationCompat.Builder(ctx)
                .SetAutoCancel(true)                    // Dismiss the notification from the notification area when the user clicks on it

                .SetContentTitle(notification.Title)      // Set the title
                .SetSmallIcon(Resource.Drawable.icon) // This is the icon to display
                .SetLargeIcon(bitmap)
                .SetContentText(String.Format(notification.Text)); // the message to display.

            //var builder = new NotificationCompat.Builder(this)
            //    .SetAutoCancel(true) // dismiss the notification from the notification area when the user clicks on it
            //                         //.SetContentIntent(resultPendingIntent) // start up this activity when the user clicks the intent.
            //    .SetContentTitle(title) // Set the title
            //    .SetSmallIcon(Resource.Drawable.icon) // This is the icon to display
            //    .SetLargeIcon(bitmap)
            //    .SetContentTitle(title)
            //    .SetContentText(content); // the message to display.

            // Finally, publish the notification:
            NotificationManager notificationManager = (NotificationManager)ctx.GetSystemService(Context.NotificationService);
            notificationManager.Notify(notification.Id, builder.Build());
        }

        private LocalNotification serializeFromString(string notificationString)
        {
            var xmlSerializer = new XmlSerializer(typeof(LocalNotification));
            using (var stringReader = new StringReader(notificationString))
            {
                var notification = (LocalNotification)xmlSerializer.Deserialize(stringReader);
                return notification;
            }
        }
    }
}