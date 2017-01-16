using HealthCare.Conveters.JsonConverters;
using Newtonsoft.Json;

namespace HealthCare.Models
{
    public class CheckupTypeModel : Entity
    {
        [JsonProperty(PropertyName = "name")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "status")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public bool Status { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}