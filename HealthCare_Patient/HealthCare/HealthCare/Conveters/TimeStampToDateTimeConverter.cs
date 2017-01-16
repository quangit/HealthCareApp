using System;
using System.Globalization;
using HealthCare.Helpers;
using Xamarin.Forms;

namespace HealthCare.Conveters
{
    public class TimeStampToDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Common.UnixTimeStampToDateTime(System.Convert.ToInt64(value ?? 0));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Common.ConvertToTimestamp(((DateTime) value).Date);
        }
    }
}