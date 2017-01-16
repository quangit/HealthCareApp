using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using HealthCare.Core.Resources;

namespace HealthCare.Core
{
    public class MvxBaseVisibilityValueConverter<T> : MvxValueConverter<T, Visibility>
    {
        protected virtual Visibility Convert(T value, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        protected override Visibility Convert(T value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, parameter, culture);
        }
    }
    public class MvxValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return Convert(value, targetType, parameter, new CultureInfo(language));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return ConvertBack(value, targetType, parameter, new CultureInfo(language));
        }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class MvxValueConverter<T, TU> : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return this.Convert((T) value, targetType, parameter, string.IsNullOrEmpty(language) ? null : new CultureInfo(language));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return this.ConvertBack((TU)value, targetType, parameter, new CultureInfo(language));
        }

        protected virtual T ConvertBack(TU value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }

        protected virtual TU Convert(T value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
namespace HealthCare.Win.Converter
{

    public class ConsentValueConverter : IValueConverter
    {


        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value is bool && (bool)value
               ? AppResources.Setting_NotificationEnabled
               : AppResources.Setting_NotificationDisabled;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
