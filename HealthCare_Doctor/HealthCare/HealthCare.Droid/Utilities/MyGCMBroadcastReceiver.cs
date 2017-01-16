using Android.App;
using Android.Content;

namespace HealthCare.Droid.Utilities
{
    [BroadcastReceiver(Permission = "com.google.android.c2dm.permission.SEND")]
    [IntentFilter(new string[] { "com.google.android.c2dm.intent.RECEIVE" }, Categories = new string[] { "com.clas.healthcare.dr" })]
	[IntentFilter(new string[] { "com.google.android.c2dm.intent.REGISTRATION" }, Categories = new string[] { "com.clas.healthcare.dr" })]
	[IntentFilter(new string[] { "com.google.android.gcm.intent.RETRY" }, Categories = new string[] { "com.clas.healthcare.dr" })]
    public class MyGCMBroadcastReceiver : BroadcastReceiver
    {
        const string TAG = "PushHandlerBroadcastReceiver";
        public override void OnReceive(Context context, Intent intent)
        {
            MyIntentService.RunIntentInService(context, intent);
            SetResult(Result.Ok, null, null);
            
        }
    }
}