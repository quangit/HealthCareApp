using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.CrossCore.Converters;
using HealthCare.Core.Resources;

namespace HealthCare.Core.Converters
{
    public class ConsentValueConverter : MvxValueConverter<bool, string>
    {
        protected override string Convert(bool value, Type targetType, object parameter, CultureInfo culture)

        {
            
                return value
                    ? AppResources.Setting_NotificationEnabled
                    : AppResources.Setting_NotificationDisabled;
        }
    }
}
