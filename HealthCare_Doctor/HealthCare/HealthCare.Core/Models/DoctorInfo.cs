using System.Collections.Generic;
using Newtonsoft.Json;

namespace HealthCare.Core.Models
{
    public class DoctorInfo
    {
        [JsonProperty("hospitalId", NullValueHandling = NullValueHandling.Ignore)]
        public string HospitalId { get; set; }
        [JsonProperty("checkupTypeId", NullValueHandling = NullValueHandling.Ignore)]
        public object CheckupTypeId { get; set; }
        [JsonProperty("roles", NullValueHandling = NullValueHandling.Ignore)]
        public List<int> Roles { get; set; }
        [JsonProperty("hospital", NullValueHandling = NullValueHandling.Ignore)]
        public Hospital Hospital { get; set; }
    }
}