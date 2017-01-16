using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Conveters.JsonConverters;
using Newtonsoft.Json;

namespace HealthCare.Models.ChBaseModel
{
    public class FeeModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public double Amount { get; set; }

        [JsonProperty(PropertyName = "startDate")]
        [JsonConverter(typeof(DateTimeConverterAttribute))]
        public DateTime StartDate { get; set; }

        [JsonProperty(PropertyName = "endDate")]
        [JsonConverter(typeof(DateTimeConverterAttribute))]
        public DateTime EndDate { get; set; }

        [JsonProperty(PropertyName = "numberOfAddedPromotionMonths")]
        public string NumberOfAddedPromotionMonths { get; set; }

        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }

        [JsonProperty(PropertyName = "whenCreated")]
        [JsonConverter(typeof(DateTimeConverterAttribute))]
        public DateTime WhenCreated { get; set; }

        [JsonProperty(PropertyName = "whenUpdated")]
        [JsonConverter(typeof(DateTimeConverterAttribute))]
        public DateTime WhenUpdated { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
