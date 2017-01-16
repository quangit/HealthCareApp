using System;
using System.Globalization;
using HealthCare.Helpers;
using Xamarin.Forms;

namespace HealthCare.Conveters
{
    public class ThemeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return HcStyles.GreenColor;
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}