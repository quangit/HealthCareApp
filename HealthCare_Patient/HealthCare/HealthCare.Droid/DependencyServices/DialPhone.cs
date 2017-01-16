using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HealthCare.DependencyServices;
using HealthCare.Droid.DependencyServices;
using Android.App;
using Android.Content;
using Android.Net;
using Android.Telephony;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(DialPhone))]
namespace HealthCare.Droid.DependencyServices
{
    public class DialPhone : IDialPhone
    {
        /// <summary>
        /// Opens native dialog to dial the specified number.
        /// </summary>
        /// <param name="number">Number to dial.</param>
        public bool MakePhoneCall(string number)
        {
            try
            {
                var context = Forms.Context;
                if (context == null)
                    return false;

                var uri = Android.Net.Uri.Parse(string.Format("tel:{0}", number));
                var intent = new Intent(Intent.ActionCall, uri);

                if (IsIntentAvailable(context, intent))
                {
                    context.StartActivity(intent);
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        private bool IsIntentAvailable(Context context, Intent intent)
        {

            var packageManager = context.PackageManager;

            var list = packageManager.QueryIntentServices(intent, 0)
                .Union(packageManager.QueryIntentActivities(intent, 0));
            if (list.Any())
                return true;

            TelephonyManager mgr = TelephonyManager.FromContext(context);
            return mgr.PhoneType != PhoneType.None;
        }

    }
}