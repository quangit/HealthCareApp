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
using Java.Lang;

namespace HealthCare.Droid.Utilities
{
    public class Utils
    {
        public static Java.Lang.Long ConvertToJavaLong(string value)
        {
            return Java.Lang.Long.Decode(value);
        }
    }
}