using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Core.Models.Enums;
using HealthCare.Core.Utils;
using Newtonsoft.Json;

namespace HealthCare.Core.Models
{
    public class SetScheduleObject
    {
        private string _data;
        public SetScheduleObject(Hospital hospital, int year, int month, int day, int startHour, int startMinute, int endHour, int endMinute, int quality, int weekcount, params DoctorDayOfWeek[] dayOfWeeks)
        {
            if (hospital == null)
                hospital = new Hospital();


            // int year = 1997; int month = 1; int day = 1;
            var date = new DateTime(year, month, day, startHour, startMinute, 0, DateTimeKind.Local);
            var endDate = date.AddDays(weekcount * 7);
            if (endDate.DayOfWeek != DayOfWeek.Sunday)
                endDate = Util.GetNextWeekday(endDate, 0).ToUniversalTime();

            var startTime = new DateTime(year, month, day, startHour, startMinute, 0, DateTimeKind.Local);
            var endTime = new DateTime(year, month, day, endHour, endMinute, 0, DateTimeKind.Local);

            var startTimeUTC = startTime.ToUniversalTime();
            var endTimeUTC = endTime.ToUniversalTime();
            if (startTimeUTC.DayOfWeek != startTime.DayOfWeek)
            {
                var diff = startTimeUTC.Date.Subtract(startTime.Date).TotalDays;
                for (int i = 0; i < dayOfWeeks.Length; i++)
                {
                    var time = (int)dayOfWeeks[i] + diff;
                    if (time < 1)
                        time = 7;
                    else if (time > 7)
                        time = 1;
                    dayOfWeeks[i] = (DoctorDayOfWeek)(time);
                }
            }
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var r = new
            {
                userId = Data.User.UserId,
                hospitalId = hospital.Id,
                date = (long)(startTimeUTC.Date.Subtract(unixEpoch)).TotalMilliseconds,
                quality = quality,
                endDate = (long)(endDate.Date.Subtract(unixEpoch)).TotalMilliseconds,
                startHour = startTimeUTC.Hour,
                startMinute = startTimeUTC.Minute,
                endHour = endTimeUTC.Hour,
                endMinute = endTimeUTC.Minute,
                dayOfWeeks = dayOfWeeks.Select(x => (int)x).ToArray()
            };

            _data = JsonConvert.SerializeObject(r);
        }

        public SetScheduleObject(Hospital hospital, int year, int month, int day, int startHour, int startMinute, int endHour, int endMinute, int quality)
        {
            //var sTime =;
            if (hospital == null)
                hospital = new Hospital();

            var startTime = new DateTime(year, month, day, startHour, startMinute, 0, DateTimeKind.Local);
            var endTime = new DateTime(year, month, day, endHour, endMinute, 0, DateTimeKind.Local);
            var startTimeUTC = startTime.ToUniversalTime();
            var endTimeUTC = endTime.ToUniversalTime();
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var r = new { userId = Data.User.UserId, hospitalId = hospital.Id, date = (long)(startTimeUTC.Subtract(unixEpoch)).TotalMilliseconds, quality = quality, endDate = (long)(endTimeUTC.Subtract(unixEpoch)).TotalMilliseconds };
            _data = JsonConvert.SerializeObject(r);
        }

        private string Array(DoctorDayOfWeek[] dayOfWeeks)
        {
            return "[" + dayOfWeeks.Select(x => ((int)x).ToString()).Aggregate((x, y) => x + "," + y) + "]";
        }
        public override string ToString()
        {
            return _data;
        }
    }
}
