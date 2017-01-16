using System;
using HealthCare.Conveters.JsonConverters;
using HealthCare.Enums;
using HealthCare.Helpers;
using HealthCare.Resx;
using Newtonsoft.Json;

namespace HealthCare.Models
{
    public class CheckupModel : Entity
    {
        //[JsonProperty(PropertyName = "userId")]
        //public override string UserId { get; set; }

        [JsonProperty(PropertyName = "userCode")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string UserCode { get; set; }

        [JsonProperty(PropertyName = "appointmentNo")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string AppointmentNo { get; set; }

        public string FullName { get; set; }

        [JsonProperty(PropertyName = "schedule")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public ScheduleModel Schedule { get; set; }

        [JsonProperty(PropertyName = "symptom")]
        public string Symptom { get; set; }

        public string SymptomFake
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Symptom))
                {
                    var limit = Common.OnPlatform<int>(50, 50, 25);
                    if (Symptom.Length >= limit)
                        return Symptom.Substring(0, limit - 3) + "...";
                    if (Symptom.IndexOf("\n", StringComparison.Ordinal) > -1)
                    {
                        return Symptom.Substring(0, Symptom.IndexOf("\n", StringComparison.Ordinal)) + "...";
                    }
                    if (Symptom.IndexOf("\r", StringComparison.Ordinal) > -1)
                    {
                        return Symptom.Substring(0, Symptom.IndexOf("\r", StringComparison.Ordinal)) + "...";
                    }
                    return Symptom;
                }
                return Symptom;
            }
        }

        [JsonProperty(PropertyName = "status")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public CheckupStatus Status { get; set; }

        [JsonProperty(PropertyName = "isPaid")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public bool IsPaid { get; set; }

        [JsonProperty(PropertyName = "result")]
        public string Result { get; set; }

        [JsonProperty(PropertyName = "schedulingFee")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public double SchedulingFee { get; set; }

        [JsonProperty(PropertyName = "checkupFee")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public double CheckupFee { get; set; }

        [JsonProperty(PropertyName = "whenCreated")]
        [JsonConverter(typeof(DateTimeConverterAttribute))]
        public DateTime WhenCreated { get; set; }

        [JsonProperty(PropertyName = "whenUpdated")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public long WhenUpdated { get; set; }

        [JsonProperty(PropertyName = "rating")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public virtual int Rating { get; set; }


        [JsonProperty(PropertyName = "healthInsurance")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public bool IsHealthInsurance { get; set; }

        [JsonProperty(PropertyName = "room")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public Room Room { get; set; }

        public string IsHealthInsuranceString => IsHealthInsurance ? AppResources.yes : AppResources.no;


        [JsonProperty(PropertyName = "cancelCheckupFee")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public double CancelCheckupFee { get; set; }

        [JsonProperty(PropertyName = "paymentExpiredDate")]
        [JsonConverter(typeof(DateTimeConverterAttribute))]
        public DateTime PaymentExpiredDate { get; set; }
    }

    public class Room
    {
        [JsonProperty(PropertyName = "id")]
        public virtual string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string Name { get; set; }
    }
}