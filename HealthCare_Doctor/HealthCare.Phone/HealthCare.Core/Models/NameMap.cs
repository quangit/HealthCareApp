using HealthCare.Core.Resources;
using Newtonsoft.Json;

namespace HealthCare.Core.Models
{
    public class NameMap
    {
        [JsonProperty("vi", NullValueHandling = NullValueHandling.Ignore)]
        public string Vi { get; set; }
        [JsonProperty("en", NullValueHandling = NullValueHandling.Ignore)]
        public string En { get; set; }

        public override string ToString()
        {
            return AppResources.ResourceLanguage == "en-US" ? En : Vi;
        }
    }
}