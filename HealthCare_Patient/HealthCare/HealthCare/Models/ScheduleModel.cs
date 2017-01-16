using System;
using HealthCare.Conveters.JsonConverters;
using Newtonsoft.Json;

namespace HealthCare.Models
{
    /// <summary>
    ///     Customers call it "Ô ca khám"
    /// </summary>
    public class ScheduleModel : Entity
    {
        [JsonProperty(PropertyName = "doctor")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public DoctorModel Doctor { get; set; }

        [JsonProperty(PropertyName = "startTime")]
        [JsonConverter(typeof (DateTimeConverterAttribute))]
        public DateTime StartDateTime { get; set; }

        [JsonProperty(PropertyName = "endTime")]
        [JsonConverter(typeof (DateTimeConverterAttribute))]
        public DateTime EndDateTime { get; set; }

        [JsonProperty(PropertyName = "hospital")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public HospitalModel Hospital { get; set; }

        [JsonProperty(PropertyName = "checkupType")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public CheckupTypeModel CheckupType { get; set; }

        public string AsString => ToString();

        public string AsFullString => string.Format("{0} - {1} {2} " + Environment.NewLine + "{3}",
            StartDateTime.ToString("HH:mm"),
            EndDateTime.ToString("HH:mm"),
            EndDateTime.ToString("d"),
            Hospital.AsString);

        public string AsTime
            => $"{StartDateTime.ToString("HH:mm")} - {EndDateTime.ToString("HH:mm")} {EndDateTime.ToString("d")}";

        public string AsDate => string.Format("{0}", StartDateTime.ToString("d"));

        public override string ToString()
        {
            return $"{StartDateTime.ToString("HH:mm")} - {EndDateTime.ToString("HH:mm")}, {Hospital.AsString}";
        }
    }
}