using System;
using System.Globalization;
using HealthCare.Resx;
using Xamarin.Forms;
using HealthCare.Helpers;

namespace HealthCare.Conveters
{
    public class TimeLeftConverter : IValueConverter
    {
        private string MONTH_SHORT = AppResources.short_month;
        private string DAY_SHORT = AppResources.short_day;
        private string HOUR_SHORT = AppResources.short_hour;
        private string MINUTE_SHORT = AppResources.short_minute;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = AppResources.time_left;
            if (value != null)
            {
                var d = ((DateTime)value).ToLocalTimeZone();
                if (d.CompareTo(DateTime.Now) > 0)
                {
                    var span = d.Subtract(DateTime.Now);
                    var distance = (int)span.TotalDays;
                    if (distance >= 1)
                    {
                        return result + distance + DAY_SHORT;
                    }
                    distance = (int)span.TotalHours;
                    if (distance >= 1)
                    {
                        return result + distance + HOUR_SHORT;
                    }
                    distance = (int)span.TotalMinutes;
                    if (distance >= 1)
                    {
                        return result + distance + MINUTE_SHORT;
                    }
                }
                return AppResources.out_of_date;
            }

            return AppResources.out_of_date;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}