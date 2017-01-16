using System;
using System.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace HealthCare.Win.Converter
{
    /// <summary>
    /// Value converter that translates true to <see cref="Visibility.Visible"/> and false to
    /// <see cref="Visibility.Collapsed"/>.
    /// </summary>
    public sealed class SourceToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
         {

            if (parameter == null)
                return (value == null || (value is IList && ((IList)value).Count == 0) || (value is string && string.IsNullOrEmpty((string)value)) || ((value is int) && (int)value == 0)) ? Visibility.Visible : Visibility.Collapsed;
            else
                return (value == null || (value is IList && ((IList)value).Count == 0) || (value is string && string.IsNullOrEmpty((string)value)) || ((value is int) && (int)value == 0)) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value is Visibility && (Visibility)value == Visibility.Visible;
        }
    }
}
