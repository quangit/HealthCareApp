using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Helpers;
using Xamarin.Forms;

namespace HealthCare.Conveters
{
    public class ColumnCheckupConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool) value)
            {
                return Common.OnPlatform(new GridLength(1, GridUnitType.Star), new GridLength(1, GridUnitType.Star),
                  new GridLength(1, GridUnitType.Auto));
            }
            else
            {
                return Common.OnPlatform(new GridLength(1, GridUnitType.Auto), new GridLength(1, GridUnitType.Auto),
                  new GridLength(1, GridUnitType.Auto));
               
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
