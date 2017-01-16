using System;
using System.Diagnostics;
using System.Globalization;
using HealthCare.Resx;
using Xamarin.Forms;

namespace HealthCare.Conveters
{

    public class StringNullConverter : IValueConverter
    {
        public bool IsFadeText { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(value as string) || value.ToString().Equals(AppResources.empty))
            {
                var lb = parameter as Label;
                if (lb != null && IsFadeText)
                {
                    lb.TextColor = Color.Silver;
                    lb.Opacity = 0.4;
                }
                return AppResources.empty;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}