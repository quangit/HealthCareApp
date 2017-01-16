using System.Collections.ObjectModel;
using HealthCare.Conveters.JsonConverters;
using Newtonsoft.Json;

namespace HealthCare.Models
{
    public class CityModel : Entity
    {
        [JsonProperty(PropertyName = "name")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "districts")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public ObservableCollection<DistrictModel> Districts { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}