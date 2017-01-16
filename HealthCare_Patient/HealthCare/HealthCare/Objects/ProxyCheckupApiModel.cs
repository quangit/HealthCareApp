using HealthCare.Models;
using Newtonsoft.Json;

namespace HealthCare.Objects
{
    public class ProxyCheckupApiModel : CheckupModel
    {
        [JsonProperty(PropertyName = "hospital")]
        public HospitalModel Hospital { get; set; }

        [JsonProperty(PropertyName = "checkupType")]
        public CheckupTypeModel CheckupType { get; set; }

        [JsonProperty(PropertyName = "doctor")]
        public DoctorModel Doctor { get; set; }
    }
}