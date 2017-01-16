using System;
using System.Globalization;
using Xamarin.Forms;

namespace HealthCare.Conveters
{
    public class UriToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new FileImageSource {File = value.ToString()};
            }
            return new FileImageSource {File = "placeholder.png"};
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return ((FileImageSource) value).File;
            return null;
        }
    }
}