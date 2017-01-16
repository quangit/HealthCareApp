
using Android.Content;
using Android.Net;
using HealthCare.DependencyServices;
using HealthCare.Droid.DependencyServices;

using Xamarin.Forms;

[assembly: Dependency(typeof(NextworkInfoAndroid))]
namespace HealthCare.Droid.DependencyServices
{
    public class NextworkInfoAndroid : INetworkInfo
    {
        public bool IsConnected()
        {          
            ConnectivityManager connectivityManager = (ConnectivityManager) Forms.Context.GetSystemService(Context.ConnectivityService);
            NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
            bool isOnline = (activeConnection != null) && activeConnection.IsConnected;
            return isOnline;
        }
    }
}