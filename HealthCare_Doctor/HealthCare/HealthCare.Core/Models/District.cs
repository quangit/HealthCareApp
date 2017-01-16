 using System.Collections.Generic;
using Newtonsoft.Json;

namespace HealthCare.Core.Models
{
    public class District
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        [JsonProperty("cityId", NullValueHandling = NullValueHandling.Ignore)]
        public string CityId { get; set; }
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public List<double> Location { get; set; }

        [JsonIgnore]
        public City City { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}