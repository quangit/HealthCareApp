using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using HealthCare.DependencyServices;
using HealthCare.Droid.DependencyServices;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(ClearCacheWebView))]

namespace HealthCare.Droid.DependencyServices
{
   public class ClearCacheWebView:IClearCacheWebView
    {
        public void ClearWebViewCache()
        {
            Android.Webkit.WebView wv = new Android.Webkit.WebView(Forms.Context);
            wv.ClearCache(true);
            var cookieManager = CookieManager.Instance;
            cookieManager.RemoveAllCookie();
        }
    }
}