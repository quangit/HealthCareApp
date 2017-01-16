using Android.App;
using Android.Content;
using Android.Gms.Gcm;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;
using Cirrious.CrossCore;
using HealthCare.Core.Models;
using HealthCare.Droid.Views;
using String = System.String;

namespace HealthCare.Droid.Utilities
{
    [Service]
    public class MyIntentService : IntentService
    {
        static PowerManager.WakeLock sWakeLock;
        static object LOCK = new object();

        public static void RunIntentInService(Context context, Intent intent)
        {
            lock (LOCK)
            {
                if (sWakeLock == null)
                {
                    // This is called from BroadcastReceiver, there is no init.
                    var pm = PowerManager.FromContext(context);
                    sWakeLock = pm.NewWakeLock(
                    WakeLockFlags.Partial, "My WakeLock Tag");
                }
            }

            sWakeLock.Acquire();
            intent.SetClass(context, typeof(MyIntentService));
            context.StartService(intent);
        }

        protected override void OnHandleIntent(Intent intent)
        {
            try
            {
                Context context = this.ApplicationContext;
                string action = intent.Action;

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
                lock (LOCK)
                {
                    //Sanity check for null as this is a public method
                    if (sWakeLock != null)
                        sWakeLock.Release();
                }
            }
        }

        private async void HandleRegistration(Context context, Intent intent)
        {
            string registrationId = intent.GetStringExtra("registration_id");
            if (!String.IsNullOrEmpty(registrationId))
            {
                Data.PushChannelUri = registrationId;
                if (!string.IsNullOrEmpty(registrationId))
                {
                    Log.Debug("registration_id", registrationId);
                    await HealthCare.Core.Services.HealthCareService.Current.NotificationRegister();

                }
            }
        }

        private void HandleMessage(Context context, Intent intent)
        {
            Bundle extras = intent.Extras;
            GoogleCloudMessaging gcm = GoogleCloudMessaging.GetInstance(context);
            // The getMessageType() intent parameter must be the intent you received
            // in your BroadcastReceiver.
            String messageType = gcm.GetMessageType(intent);

            if (!extras.IsEmpty)
            {  // has effect of unparcelling Bundle
                /*
                 * Filter messages based on message type. Since it is likely that GCM
                 * will be extended in the future with new message types, just ignore
                 * any message types you're not interested in, or that you don't
                 * recognize.
                 */
                if (GoogleCloudMessaging.MessageTypeSendError == messageType)
                {
                    sendNotification(extras, context);
                }
                else if (GoogleCloudMessaging.MessageTypeDeleted ==
                      messageType)
                {
                    sendNotification(
                            extras, context);
                    // If it's a regular GCM message, do some work.
                }
                else if (GoogleCloudMessaging.MessageTypeMessage == messageType)
                {
                    // This loop represents the service doing some work.
                    //for (int i = 0; i < 5; i++)
                    //{
                    //    Log.Debug("GCM", "Working... " + (i + 1)
                    //            + "/5 @ " + SystemClock.ElapsedRealtime());
                    //    try
                    //    {
                    //        Thread.Sleep(5000);
                    //    }
                    //    catch (InterruptedException e)
                    //    {
                    //    }
                    //}
                    Log.Debug("GCM", "Completed work @ " + SystemClock.ElapsedRealtime());
                    // Post notification of received message.
                    sendNotification(extras, context);
                    Log.Debug("GCM", "Received: " + extras.ToString());
                }
            }
            // Release the wake lock provided by the WakefulBroadcastReceiver.


        }

        private void sendNotification(Bundle msg, Context context)
        {
            var mNotificationManager = (NotificationManager)
                this.GetSystemService(Context.NotificationService);


            Intent myIntent = new Intent(this, typeof(LoginView));
            myIntent.PutExtras(msg);
            myIntent.SetFlags(ActivityFlags.SingleTop);

            PendingIntent contentIntent = PendingIntent.GetActivity(context, 0,
                myIntent, PendingIntentFlags.UpdateCurrent, msg);


            NotificationCompat.Builder mBuilder =
                new NotificationCompat.Builder(this)
                    .SetSmallIcon(Resource.Drawable.logo_bs)
                    .SetContentTitle(msg.GetString("title"))
                    .SetStyle(new NotificationCompat.BigTextStyle()
                        .BigText(msg.GetString("title")))
                    .SetContentText(msg.GetString("content"));

            mBuilder.SetContentIntent(contentIntent);
            var notification = mBuilder.Build();
            notification.Flags |= NotificationFlags.AutoCancel;
            notification.Defaults |= NotificationDefaults.Sound;
            notification.Defaults |= NotificationDefaults.Vibrate; 
            mNotificationManager.Notify(1, notification);
        }

    }
}