using System;
using System.Globalization;
using System.Windows;
using Cirrious.CrossCore.WindowsPhone.Converters;
using HealthCare.Core.Converters;

namespace HealthCare.Phone.Converters
{
    public class NativeBoolVisValueConverter : MvxNativeValueConverter<BoolVisValueConverter>
    {
    }

    public class NativeReverseBoolVisValueConverter : MvxNativeValueConverter<ReverseBoolVisValueConverter>
    {
    }

    public class NativeWeekTopicStatus : MvxNativeValueConverter<WeekTopicStatusValueConverter>
    {
    }

    public class NativeMilisecondToDate : MvxNativeValueConverter<MilisecondToDateValueConverter>
    {
        
    } 
    public class NativeConsentValueConverter : MvxNativeValueConverter<ConsentValueConverter>
    { }

    public class NativeListVisValueConverter : MvxNativeValueConverter<ListVisValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool) base.Convert(value, targetType, parameter, culture))
                ? Visibility.Visible
                : Visibility.Collapsed;
        }
    }
}
