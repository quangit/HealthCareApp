using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.DependencyServices;
using HealthCare.WinPhone.DependencyServices;
using Microsoft.Phone.Controls;

[assembly: Xamarin.Forms.Dependency(typeof(ClearCacheWebView))]

namespace HealthCare.WinPhone.DependencyServices
{
  public  class ClearCacheWebView: IClearCacheWebView
    {
      public async void ClearWebViewCache()
      {
            await new WebBrowser().ClearCookiesAsync();
            await new WebBrowser().ClearInternetCacheAsync();
      }
    }
}
