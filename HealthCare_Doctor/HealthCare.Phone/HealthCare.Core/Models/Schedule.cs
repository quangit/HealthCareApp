using System;
using System.Collections.Generic;
using System.Linq;
using HealthCare.Core.Models.Enums;
using HealthCare.Core.Resources;
using Newtonsoft.Json;
using HealthCare.Core.Utils;
using System.Diagnostics;

namespace HealthCare.Core.Models
{
    public class Schedule
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        [JsonProperty("userId", NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }
        [JsonProperty("hospitalId", NullValueHandling = NullValueHandling.Ignore)]
        public string HospitalId { get; set; }
        [JsonProperty("date", NullValueHandling = NullValueHandling.Ignore)]
        public long Date { get; set; }
        [JsonProperty("endDate", NullValueHandling = NullValueHandling.Ignore)]
        public long EndDate { get; set; }
        [JsonProperty("dayOfWeeks", NullValueHandling = NullValueHandling.Ignore)]
        public List<int> DayOfWeeks { get; set; }
        [JsonProperty("excludedDates", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> ExcludedDates { get; set; }
        [JsonProperty("quality", NullValueHandling = NullValueHandling.Ignore)]
        public int Quality { get; set; }
        [JsonProperty("startHour", NullValueHandling = NullValueHandling.Ignore)]
        public int StartHour { get; set; }
        [JsonProperty("startMinute", NullValueHandling = NullValueHandling.Ignore)]
        public int StartMinute { get; set; }
        [JsonProperty("endHour", NullValueHandling = NullValueHandling.Ignore)]
        public int EndHour { get; set; }
        [JsonProperty("endMinute", NullValueHandling = NullValueHandling.Ignore)]
        public int EndMinute { get; set; }
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public bool Status { get; set; }
        [JsonProperty("timezone", NullValueHandling = NullValueHandling.Ignore)]
        public object Timezone { get; set; }
        [JsonProperty("interval", NullValueHandling = NullValueHandling.Ignore)]
        public int Interval { get; set; }

        [JsonIgnore]
        public Doctor Doctor { get; set; }
        [JsonIgnore]
        public Hospital Hospital
        {
            get
            {
                if (Data.User == null || Data.User.Profile == null)
                    return null;
                var ret = Data.User.Profile.AllHospitals.FirstOrDefault(x => x.Id == HospitalId);
                if (ret == null)
                {
                    ret = new Hospital() { Name = HospitalId, Photos = new Photos() { Logo = Util.ImageResourceUrl("logo_bs") } };
                }
                return ret;
            }
        }




        [JsonIgnore]
        public DateTime StartDateTime
        {
            get
            {
                var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                var dateUTC = unixEpoch.AddMilliseconds(Date);
                if (StartHour == 0 && StartMinute == 0)
                {
                    StartHour = dateUTC.Hour;
                    StartMinute = dateUTC.Minute;
                }
                var ret = new DateTime(dateUTC.Year, dateUTC.Month, dateUTC.Day, StartHour, StartMinute, 0, DateTimeKind.Utc).ToLocalTime();
                return ret;
            }
        }

        [JsonIgnore]
        public DateTime EndDateTime
        {
            get
            {
                var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                var dateUTC = unixEpoch.AddMilliseconds((EndDate == 0)? Date : EndDate);
                if (EndHour == 0 && EndMinute == 0)
                {
                    EndHour = dateUTC.Hour;
                    EndMinute = dateUTC.Minute;
                }
                var ret = new DateTime(dateUTC.Year, dateUTC.Month, dateUTC.Day, EndHour, EndMinute, 0, DateTimeKind.Utc).ToLocalTime();
                return ret;
            }
        }

        public override string ToString()
        {
            var ret = string.Format("{0}: {1} - {2}", AppResources.Examination,
                StartDateTime.TimeOfDay.ToString("hh':'mm"),
                EndDateTime.TimeOfDay.ToString("hh':'mm"));
            return ret;
        }

        [JsonIgnore]
        public string TimeExam
        {
            get
            {
                //Debugger.Break ();
                return StartDateTime.ToString("HH:mm") + " - " + EndDateTime.ToString("HH:mm");
            }
        }

       
        //[JsonProperty("excludedDates", NullValueHandling = NullValueHandling.Ignore)]
        //public List<string> ExcludedDates { get; set; }
    }
}