using System;
using System.Globalization;
using Xamarin.Forms;

namespace HealthCare.Conveters
{
    public class NullToTrueConverter : IValueConverter
    {
        public bool IsInvert { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? !IsInvert : IsInvert;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}