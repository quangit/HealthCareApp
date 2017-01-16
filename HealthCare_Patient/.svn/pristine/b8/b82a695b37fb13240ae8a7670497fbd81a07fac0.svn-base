using HealthCare.Models;
using Newtonsoft.Json;
using HealthCare.Conveters.JsonConverters;

namespace HealthCare.ModelApis
{
    public class ApiPaymentPromotionModel
    {
        [JsonProperty(PropertyName = "promotion")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public PromotionModel Promotion { get; set; }

        [JsonProperty(PropertyName = "total")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public double Total { get; set; }
    }
}