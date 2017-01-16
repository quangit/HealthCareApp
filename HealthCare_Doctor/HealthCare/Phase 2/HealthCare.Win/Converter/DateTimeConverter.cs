using System;
using Windows.UI.Xaml.Data;

namespace HealthCare.Win.Converter
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var dateTime = value as DateTime?;
            if (dateTime == null || dateTime.Value.Equals(default(DateTime)))
                return DateTimeOffset.Now;
            else
            {
                return new DateTimeOffset(dateTime.Value);
            }            
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var dateTimeOf = value as DateTimeOffset?;
            if (dateTimeOf == null)
                return null;
            return
                dateTimeOf.Value.Date;
        }
    }
}