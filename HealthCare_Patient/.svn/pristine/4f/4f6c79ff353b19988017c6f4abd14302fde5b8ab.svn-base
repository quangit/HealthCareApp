using System;
using System.Globalization;
using Xamarin.Forms;

namespace HealthCare.Conveters
{
    public class SayHelloConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "Xin chào " + value != null ? value.ToString() : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}