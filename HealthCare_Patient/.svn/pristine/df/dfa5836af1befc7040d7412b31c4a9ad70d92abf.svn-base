using HealthCare.Conveters.JsonConverters;
using Newtonsoft.Json;

namespace HealthCare.Models
{
    public class DistrictModel : Entity
    {
        [JsonProperty(PropertyName = "name")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}