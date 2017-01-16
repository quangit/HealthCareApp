using System.Collections.Generic;
using Newtonsoft.Json;

namespace HealthCare.Core.Models
{
    public class User : Doctor
    {

        [JsonProperty("whenCreated", NullValueHandling = NullValueHandling.Ignore)]
        public long WhenCreated { get; set; }
        [JsonProperty("whenUpdated", NullValueHandling = NullValueHandling.Ignore)]
        public long WhenUpdated { get; set; }
        [JsonProperty("createdByUserId", NullValueHandling = NullValueHandling.Ignore)]
        public object CreatedByUserId { get; set; }
        [JsonProperty("updatedByUserId", NullValueHandling = NullValueHandling.Ignore)]
        public object UpdatedByUserId { get; set; }
        [JsonProperty("occupation", NullValueHandling = NullValueHandling.Ignore)]
        public object Occupation { get; set; }
        [JsonProperty("bloodType", NullValueHandling = NullValueHandling.Ignore)]
        public object BloodType { get; set; }

        [JsonProperty("patients", NullValueHandling = NullValueHandling.Ignore)]
        public object Patients { get; set; }
        [JsonProperty("doctors", NullValueHandling = NullValueHandling.Ignore)]
        public object Doctors { get; set; }
        [JsonProperty("doctor", NullValueHandling = NullValueHandling.Ignore)]
        public object Doctor { get; set; }

        [JsonProperty("cards", NullValueHandling = NullValueHandling.Ignore)]
        public object Cards { get; set; }
        [JsonProperty("paymentPassword", NullValueHandling = NullValueHandling.Ignore)]
        public object PaymentPassword { get; set; }

        //[JsonProperty("isFamilyDoctor", NullValueHandling = NullValueHandling.Ignore)]
        //public object IsFamilyDoctor { get; set; }

        [JsonProperty("hospitalAdmin", NullValueHandling = NullValueHandling.Ignore)]
        public bool HospitalAdmin { get; set; }
    }
}