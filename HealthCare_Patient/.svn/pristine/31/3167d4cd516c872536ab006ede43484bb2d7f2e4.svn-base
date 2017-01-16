using System;
using System.Globalization;
using Xamarin.Forms;

namespace HealthCare.Conveters
{
    public class UriToImageLoaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
            {
                var imgSrc = new UriImageSource
                {
                    Uri = new Uri(value.ToString()),
                    CachingEnabled = true,
                    CacheValidity = TimeSpan.FromHours(3)
                };
                var weakRef = new WeakReference(imgSrc);
                return weakRef.Target;
            }
            return new FileImageSource {File = "placeholder.png"};
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var uri = "";
            if (value is UriImageSource)
                uri = ((UriImageSource) value).Uri.AbsoluteUri;
            else if (value is FileImageSource)
                uri = ((FileImageSource) value).File;
            return uri;
        }
    }
}