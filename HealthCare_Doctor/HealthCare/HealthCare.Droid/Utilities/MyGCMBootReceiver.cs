using Android.App;
using Android.Content;

namespace HealthCare.Droid.Utilities
{
    [BroadcastReceiver]
    [IntentFilter(new[] { Intent.ActionBootCompleted })]
    public class MyGCMBootReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            MyIntentService.RunIntentInService(context, intent);
            SetResult(Result.Ok, null, null);
        }
    }
}