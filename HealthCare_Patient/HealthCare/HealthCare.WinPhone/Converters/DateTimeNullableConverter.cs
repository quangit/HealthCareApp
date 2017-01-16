using System;
using System.Globalization;
using System.Windows.Data;

namespace HealthCare.WinPhone.Converters
{
    public class DateTimeNullableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "Chưa đặt ngày";
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}