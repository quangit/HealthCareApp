using System;
using System.Globalization;
using HealthCare.Enums;
using Xamarin.Forms;

namespace HealthCare.Conveters
{
    public class EnumDescriptionConverter : IValueConverter
    {
        #region IValueConverter implementation

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum)
                return Description.ToString((Enum) value);
            return "Enum do not contain description value or data type is not enum";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}