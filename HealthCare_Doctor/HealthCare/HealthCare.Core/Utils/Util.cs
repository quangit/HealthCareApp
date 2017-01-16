using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HealthCare.Core.Models;

namespace HealthCare.Core.Utils
{
    public class RegexUtilities
    {
        /// <summary>
        /// Regular expression, which is used to validate an E-Mail address.
        /// </summary>
        public const string MatchEmailPattern =
                  @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
           + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
           + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
           + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";

        /// <summary>
        /// Checks whether the given Email-Parameter is a valid E-Mail address.
        /// </summary>
        /// <param name="email">Parameter-string that contains an E-Mail address.</param>
        /// <returns>True, when Parameter-string is not null and 
        /// contains a valid E-Mail address;
        /// otherwise false.</returns>
        public static bool IsEmail(string email)
        {
            if (email != null) return Regex.IsMatch(email, MatchEmailPattern);
            else return false;
        }
    }
    public class Util
    {

        public static  string DoubleToString(double value)
        {
            return value.ToString().Replace(",", ".");
        }
		public static DateTime LongtoDateTime(long value, bool utc = false)
        {
            double result = value;
			if(!utc)
            	result += TimeZoneInfo.Local.BaseUtcOffset.TotalMilliseconds;
            var dateTime = TimeSpan.FromMilliseconds(result);
            var refDate = new DateTime(1970, 1, 1);
            refDate = refDate + dateTime;
            return refDate;
        }

        public static long DateTimeToLong(DateTime value)
        {
            return (long)(value.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
        }



        public static bool CompareDay(DateTime selDate, DateTime startDate, DateTime endDate)
        {

            var ret = (DateTime.Compare(selDate.Date.Date, startDate.Date.Date) >= 0) &&
                      (DateTime.Compare(selDate.Date.Date, endDate.Date.Date) <= 0);

            return ret;
        }

        public static string ToShortDateString(DateTime date)
        {
            return date.ToString(DateTimeFormatInfo.CurrentInfo.ShortDatePattern);
        }

        public static string ToShortTimeString(DateTime date)
        {
            return date.ToString(DateTimeFormatInfo.CurrentInfo.ShortTimePattern);
        }

		public static String ToShortDateTimeString(DateTime date)
		{  
			var ret = ToShortDateString(date) +
				" " +
				ToShortTimeString(date);
			return ret;
		}

		public static String ToShortDateTimeString(DateTime? date)
		{  
			if(date == null)
				return "";
			var ret = ToShortDateTimeString (date.Value);
			return ret;
		}

        /*
        public static string GoogleMapImg(double? lat, double? lon, int width = 800, int height = 300, int zoom = 15, string color = "red")
        {
            if (lat == null || lon == null)
                return "";
            var ret = string.Format("http://maps.google.com/maps/api/staticmap?" +
                                    "zoom={0}&size={1}x{2}&markers=color:{3}|{4},{5}&mobile=true",
                zoom, width, height, color, lat, lon);
            return ret;
        }
        */

        public static DateTime GetNextWeekday(DateTime start, int day)
        {
            var temp = new DateTime(start.Year, start.Month, start.Day);
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return temp.AddDays(daysToAdd);
        }

		public static string ImageResourceUrl(string ret, bool png = true)
        {
			var ext = png ? ".png" : ".jpg";
            if (!string.IsNullOrWhiteSpace(ret))
            {
				switch (Data.Platform) {
				case Data.Os.iOS:
					ret = "res:" + ret + ext;
					break;
				case Data.Os.Android:
					ret = "res:" + ret.ToLower ();
					break;
				case Data.Os.WinPhone:
					ret = "/Assets/" + ret + ext;
					break;
				}
            }
            return ret;
        }


    }
}
