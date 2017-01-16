using System;
using Xamarin.Forms;
using HealthCare.Helpers;

namespace HealthCare.Conveters
{
    public class LocalTimeZoneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                var dateTime = (DateTime)value;
                return dateTime.ToLocalTimeZone();
            }
            return DateTime.Now.AddDays(-1);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

