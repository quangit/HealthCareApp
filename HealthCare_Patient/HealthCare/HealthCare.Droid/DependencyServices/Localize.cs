using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HealthCare.DependencyServices;
using HealthCare.Droid.DependencyServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(Localize))]
namespace HealthCare.Droid.DependencyServices
{
    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            try
            {
                var androidLocale = Java.Util.Locale.Default;

                //var netLanguage = androidLocale.Language.Replace ("_", "-");
                var netLanguage = androidLocale.ToString().Replace("_", "-");

                //var netLanguage = androidLanguage.Replace ("_", "-");

                return new CultureInfo(netLanguage);
            }
            catch
            {
                return new CultureInfo("en-US");
            }
        }

        public void SetLocale()
        {
            try
            {
                var androidLocale = Java.Util.Locale.Default; // user's preferred locale
                var netLocale = androidLocale.ToString().Replace("_", "-");
                var ci = new CultureInfo(netLocale);

                Thread.CurrentThread.CurrentCulture = ci;
                Thread.CurrentThread.CurrentUICulture = ci;
            }
            catch
            {
                var ci = new CultureInfo("en-US");

                Thread.CurrentThread.CurrentCulture = ci;
                Thread.CurrentThread.CurrentUICulture = ci;
            }
        }
    }
}