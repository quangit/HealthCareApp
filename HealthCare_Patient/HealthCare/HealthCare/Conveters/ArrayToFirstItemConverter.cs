using System;
using System.Collections;
using System.Globalization;
using Xamarin.Forms;

namespace HealthCare.Conveters
{
    public class ArrayToFirstItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value != null)
                {
                    if (value is string[]) return ((string[]) value)[0];
                    if (value is IList) return ((IList) value)[0];
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}