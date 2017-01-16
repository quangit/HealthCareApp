using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HealthCare.DependencyServices;
using HealthCare.Droid.DependencyServices;


[assembly: Xamarin.Forms.Dependency(typeof(PushNotificationRegister))]
namespace HealthCare.Droid.DependencyServices
{
    public class PushNotificationRegister : IPushNotificationRegister
    {
        public async Task UnRegisterPushNotification()
        {
            throw new NotImplementedException();
        }

        public async Task RegiterPushNotification()
        {
            string senders = "866019113436";//472036076201
            Intent intent = new Intent("com.google.android.c2dm.intent.REGISTER");
            intent.SetPackage("com.google.android.gsf");
            intent.PutExtra("app", PendingIntent.GetBroadcast(Application.Context, 0, new Intent(), 0));
            intent.PutExtra("sender", senders);
            Application.Context.StartService(intent);
        }
    }
}