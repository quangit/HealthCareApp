using System;
using HealthCare.Conveters.JsonConverters;
using HealthCare.Models;
using Newtonsoft.Json;

namespace HealthCare.ModelApis
{
    public class ApiCheckupModel : CheckupModel, IApiModel<CheckupModel>
    {
        [JsonProperty(PropertyName = "scheduleId")]
        public string ScheduleId { get; set; }

        [JsonProperty(PropertyName = "appointmentDate")]
        [JsonConverter(typeof (DateTimeConverterAttribute))]
        public DateTime AppointmentDateTime { get; set; }

        [JsonProperty(PropertyName = "endAppointmentDate")]
        [JsonConverter(typeof (DateTimeConverterAttribute))]
        public DateTime EndAppointmentDate { get; set; }

        [JsonProperty(PropertyName = "doctor")]
        public DoctorModel Doctor { get; set; }

        [JsonProperty(PropertyName = "hospital")]
        public HospitalModel Hospital { get; set; }

        [JsonProperty(PropertyName = "checkupType")]
        public CheckupTypeModel CheckupType { get; set; }

        [JsonProperty(PropertyName = "dataUser")]
        public UserModel DataUser { get; set; }

        public CheckupModel ToBaseModel()
        {
            Schedule = new ScheduleModel
            {
                Id = ScheduleId,
                CheckupType = CheckupType,
                Doctor = Doctor,
                Hospital = Hospital,
                StartDateTime = AppointmentDateTime,
                EndDateTime = EndAppointmentDate
            };

            UserCode = DataUser?.UserCode;
            FullName = DataUser?.FullName;
            return this;
        }
    }
}