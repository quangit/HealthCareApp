using System;
using System.Collections.Generic;
using System.Text;
using Foundation;
using HealthCare.DependencyServices;
using HealthCare.iOS.DependencyServices;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(DialPhone))]
namespace HealthCare.iOS.DependencyServices
{
    public class DialPhone : IDialPhone
    {
        public bool MakePhoneCall(string number)
        {
            return UIApplication.SharedApplication.OpenUrl(new NSUrl("tel:" + number.Replace(" ","")));
        }
    }
}

