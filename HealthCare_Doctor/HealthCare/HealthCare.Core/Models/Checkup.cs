using System;
using Newtonsoft.Json;
using HealthCare.Core.Utils;
using System.Collections.Generic;
using HealthCare.Core.Resources;

namespace HealthCare.Core.Models
{
    public class IndexObject
    {
        public string _id;
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [JsonProperty("userId", NullValueHandling = NullValueHandling.Ignore)]
        public string UserId
        {
            get { return _id; }
            set { _id = value; }
        }
    }


	public class CheckupData : LoadMoreData<Checkup> {
		[JsonProperty("checkups", NullValueHandling = NullValueHandling.Ignore)]
		public override List<Checkup> Data { get; set; }
	}

    public class Checkup
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("appointmentNo", NullValueHandling = NullValueHandling.Ignore)]
        public string AppointmentNo { get; set; }

		[JsonIgnore]
		public string AppointmentNoStr => $"({AppResources.CheckupView_appointmentno} {AppointmentNo})";

        [JsonProperty("userId", NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }
        [JsonProperty("hospitalId", NullValueHandling = NullValueHandling.Ignore)]
        public string HospitalId { get; set; }
        [JsonProperty("doctorId", NullValueHandling = NullValueHandling.Ignore)]
        public string DoctorId { get; set; }
        [JsonProperty("checkupTypeId", NullValueHandling = NullValueHandling.Ignore)]
        public string CheckupTypeId { get; set; }
        [JsonProperty("symptom", NullValueHandling = NullValueHandling.Ignore)]
        public string Symptom { get; set; }
        [JsonProperty("scheduleId", NullValueHandling = NullValueHandling.Ignore)]
        public string ScheduleId { get; set; }
        [JsonProperty("paymentMethod", NullValueHandling = NullValueHandling.Ignore)]
        public object PaymentMethod { get; set; }
        [JsonProperty("patientType", NullValueHandling = NullValueHandling.Ignore)]
        public object PatientType { get; set; }
        [JsonProperty("serviceType", NullValueHandling = NullValueHandling.Ignore)]
        public object ServiceType { get; set; }
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public int Status { get; set; }
        [JsonProperty("isPaid", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsPaid { get; set; }
        [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
        public string Result { get; set; }
        [JsonProperty("whenCreated", NullValueHandling = NullValueHandling.Ignore)]
        public object WhenCreated { get; set; }
        [JsonProperty("whenUpdated", NullValueHandling = NullValueHandling.Ignore)]
        public object WhenUpdated { get; set; }
        [JsonProperty("appointmentDate", NullValueHandling = NullValueHandling.Ignore)]
        public object AppointmentDate { get; set; }
        [JsonProperty("endAppointmentDate", NullValueHandling = NullValueHandling.Ignore)]
        public object EndAppointmentDate { get; set; }
        [JsonProperty("payment", NullValueHandling = NullValueHandling.Ignore)]
        public object Payment { get; set; }
        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public double Amount { get; set; }
        [JsonProperty("checkupFee", NullValueHandling = NullValueHandling.Ignore)]
		public double CheckupFee { get; set; }
        [JsonProperty("schedulingFee", NullValueHandling = NullValueHandling.Ignore)]
		public double SchedulingFee { get; set; }
        [JsonProperty("hospital", NullValueHandling = NullValueHandling.Ignore)]
        public Hospital Hospital { get; set; }
        [JsonProperty("checkupType", NullValueHandling = NullValueHandling.Ignore)]
        public CheckupType CheckupType { get; set; }
        [JsonProperty("doctor", NullValueHandling = NullValueHandling.Ignore)]
        public Doctor Doctor { get; set; }
        [JsonProperty("dataUser", NullValueHandling = NullValueHandling.Ignore)]
        public Patient Patient { get; set; }

        [JsonIgnore]
        public string Date
        {
            get
            {
                if (AppointmentDate != null && EndAppointmentDate != null)
				{
                    var start = Util.LongtoDateTime((long)AppointmentDate);
                    var end = Util.LongtoDateTime((long)EndAppointmentDate);
                    if (start.Date.Equals(end.Date))
                        return string.Format("{0:HH':'mm} - {1:HH':'mm}, {0:d}  ", start, end);
                    else
                    {
                        return string.Format("{0:HH':'mm}, {0:d} - {1:HH':'mm}, {1:d}  ", start, end);
                    }
                }
                return "";
            }
        }


    }
}