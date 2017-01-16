using System;
using System.Collections.Generic;
using System.Text;
using Foundation;
using HealthCare.DependencyServices;
using HealthCare.iOS.DependencyServices;

[assembly: Xamarin.Forms.Dependency(typeof(ClearCacheWebView))]

namespace HealthCare.iOS.DependencyServices
{
   public class ClearCacheWebView: IClearCacheWebView
    {
        public void ClearWebViewCache()
        {
            NSUrlCache.SharedCache.RemoveAllCachedResponses();
            foreach (var cookie in NSHttpCookieStorage.SharedStorage.Cookies)
            {
                NSHttpCookieStorage.SharedStorage.DeleteCookie(cookie);
            }
        }
    }
}
