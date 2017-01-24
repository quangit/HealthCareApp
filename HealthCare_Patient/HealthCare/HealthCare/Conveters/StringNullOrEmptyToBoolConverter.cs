using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HealthCare.Conveters
{
    public class StringNullOrEmptyToBoolConverter : IValueConverter
    {
        public bool IsInvert { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return IsInvert;
            string text = value.ToString();
            if (string.IsNullOrEmpty(text)) return IsInvert;
            else return !IsInvert;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
