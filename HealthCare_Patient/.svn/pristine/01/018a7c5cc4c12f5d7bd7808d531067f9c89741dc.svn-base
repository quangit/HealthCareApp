using System.Net;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;
using HealthCare.Helpers;
using HealthCare.ViewModels;
using Java.IO;
using Java.Lang;
using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;

namespace HealthCare.Droid.PushNotifications
{
    [Service]
    public class PushNotificationIntentService : IntentService
    {
        private static PowerManager.WakeLock _sWakeLock;
        private static readonly object Lock = new object();

        public static void RunIntentInService(Context context, Intent intent)
        {
            lock (Lock)
            {
                if (_sWakeLock == null)
                {
                    // This is called from BroadcastReceiver, there is no init.
                    var pm = PowerManager.FromContext(context);
                    _sWakeLock = pm.NewWakeLock(
                        WakeLockFlags.Partial, "My WakeLock Tag");
                }
            }

            _sWakeLock.Acquire();
            intent.SetClass(context, typeof(PushNotificationIntentService));
            context.StartService(intent);
        }

        protected override void OnHandleIntent(Intent intent)
        {
            try
            {
                var context = ApplicationContext;
                var action = intent.Action;

                if (action.Equals("com.google.android.c2dm.intent.REGISTRATION"))
                {
                    HandleRegistration(context, intent);
                }
                else if (action.Equals("com.google.android.c2dm.intent.RECEIVE"))
                {
                    HandleMessage(context, intent);
                }
            }
            finally
            {
                lock (Lock)
                {
                    //Sanity check for null as this is a public method
                    if (_sWakeLock != null)
                        _sWakeLock.Release();
                }
            }
        }

        private void HandleMessage(Context context, Intent intent)
        {
            var resultIntent = new Intent(this, typeof(MainActivity));


            var stackBuilder = TaskStackBuilder.Create(this);

            var c = Class.FromType(typeof(MainActivity));
            stackBuilder.AddParentStack(c);
            stackBuilder.AddNextIntent(resultIntent);

            var id = intent.GetStringExtra("id");
            var from = intent.GetStringExtra("from");
            var title = intent.GetStringExtra("title");
            var content = intent.GetStringExtra("message");
            if (string.IsNullOrEmpty(title))
            {
                title = "BacSi24x7";
            }
            //var bundle = intent.Extras.KeySet();
            //foreach (var val in bundle)
            //{
            //    Log.Debug("aaa", val + ": " + intent.GetStringExtra(val));
            //}


            Bitmap bitmap = //GetImageBitmapFromUrl(image);
            BitmapFactory.DecodeResource(Resources, Resource.Drawable.icon);

            var resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);
            //var cancelPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.CancelCurrent);

            var builder = new NotificationCompat.Builder(this)
                .SetAutoCancel(true) // dismiss the notification from the notification area when the user clicks on it
                //.SetContentIntent(resultPendingIntent) // start up this activity when the user clicks the intent.
                .SetContentTitle(title) // Set the title
                .SetSmallIcon(Resource.Drawable.icon) // This is the icon to display
                .SetLargeIcon(bitmap)
                .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                .SetContentTitle(title)
                .SetContentText(content); // the message to display.

            try
            {
                if (!App.IsAppActivated)
                    builder.SetContentIntent(resultPendingIntent);
                //else
                //{
                //}
            }
            catch
            {
                //builder.SetContentIntent(resultPendingIntent);
            }

            // Build the notification:
            var notification = builder.Build();

            // Get the notification manager:
            var notificationManager =
                GetSystemService(NotificationService) as NotificationManager;

            // Publish the notification:
            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);
        }

        private void HandleRegistration(Context context, Intent intent)
        {
            var token = intent.GetStringExtra("registration_id");
            Settings.DeviceChannel = token;
            //await LoginViewModel.Instance.RegisterDevice();
        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }

    }
}