using System.Linq;
using HealthCare.Models;
using Newtonsoft.Json;

namespace HealthCare.ModelApis
{
    public class ScheduleApiModel : ScheduleModel, IApiModel<ScheduleModel>
    {
        [JsonProperty(PropertyName = "doctor")]
        public DoctorApiModel DoctorApi { get; set; }

        public ScheduleModel ToBaseModel()
        {
            CheckupType =
                DoctorApi.DoctorInfos.Where(x => x.Hospital.Id.Equals(Hospital.Id))
                    .Select(x => x.CheckupType)
                    .FirstOrDefault();
            Doctor = DoctorApi.ToBaseModel();
            return this;
        }
    }
}