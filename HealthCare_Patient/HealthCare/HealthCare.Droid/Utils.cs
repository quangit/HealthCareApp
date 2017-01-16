using System;

namespace HealthCare.Droid
{
    public static class Utils
    {
        public static Android.Graphics.Color ConvertFormColorToAndroidColor(Xamarin.Forms.Color color){
            int a = (int)(color.A * 255d);
            int g = (int)(color.G * 255d);
            int r = (int)(color.R * 255d);
            int b = (int)(color.B * 255d);
            return Android.Graphics.Color.Argb(a, r, g ,b);
        }
    }
}

