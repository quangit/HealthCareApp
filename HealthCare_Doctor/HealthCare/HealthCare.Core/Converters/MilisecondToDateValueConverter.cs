using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if MVVMCROSS
using Cirrious.CrossCore.Converters;
#endif
using HealthCare.Core.Models;
using System.Diagnostics;

using HealthCare.Core.Utils;

namespace HealthCare.Core.Converters
{
    public class MilisecondToDateValueConverter : MvxValueConverter<long, string>
    {
        protected override string Convert(long value, Type targetType, object parameter, CultureInfo culture)
        {
			if (value == 0)
				return "";
            double result = value;
            result += TimeZoneInfo.Local.BaseUtcOffset.TotalMilliseconds;
            var dateTime = TimeSpan.FromMilliseconds(result);
            var refDate = new DateTime(1970, 1, 1);
            refDate = refDate + dateTime;

			var ret = Util.ToShortDateTimeString (refDate);
            return ret;
        }

        
    }
}
