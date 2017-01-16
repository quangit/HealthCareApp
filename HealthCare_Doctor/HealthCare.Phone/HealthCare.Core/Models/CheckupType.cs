using Newtonsoft.Json;

namespace HealthCare.Core.Models
{
    public class CheckupType
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        [JsonProperty("nameMap", NullValueHandling = NullValueHandling.Ignore)]
        public NameMap Name { get; set; }
        [JsonProperty("hospitalId", NullValueHandling = NullValueHandling.Ignore)]
        public object HospitalId { get; set; }
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public bool Status { get; set; }

        public override string ToString()
        {
            if (Name != null)
                return Name.ToString();
            return base.ToString();
        }
    }
}