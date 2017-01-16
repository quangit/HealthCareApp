using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
#if MVVMCROSS
using Cirrious.CrossCore.Converters;
using Cirrious.MvvmCross.Plugins.Color;
#endif
using HealthCare.Core.Models;
using HealthCare.Core.Models.Enums;
using HealthCare.Core.Utils;

namespace HealthCare.Core.Converters
{
    public class WeekTopicStatusValueConverter : MvxValueConverter
    {
       
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = value as Topic;

            var ret = "";
			var longDateTimeNow = Util.DateTimeToLong(DateTime.Now) - TimeZoneInfo.Local.BaseUtcOffset.TotalMilliseconds;
            if (result.IsOnline)
                ret = longDateTimeNow >= result.StartDateTime &&
                      longDateTimeNow <= result.EndDateTime
                    ? "skype_online"
                    : "skype_offline";
            else
                ret = "map";


            switch (Data.Platform)
            {
                case Data.Os.iOS:
                    ret = "res:" + ret + ".png";
                    break;
                case Data.Os.Android:
                    ret = "res:" + ret;
                    break;
                case Data.Os.WinPhone:
                    ret = "/Assets/" + ret + ".png";
                    break;
            }
            return ret;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
