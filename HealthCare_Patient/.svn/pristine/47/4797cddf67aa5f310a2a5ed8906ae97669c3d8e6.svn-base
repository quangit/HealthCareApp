using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Conveters.JsonConverters;
using Newtonsoft.Json;

namespace HealthCare.Models.ChBaseModel
{
    public class CHBasePatientInfoModel
    {
        [JsonProperty(PropertyName = "status")]
        public bool Status { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "when_expired")]
        [JsonConverter(typeof(DateTimeConverterAttribute))]
        public DateTime WhenExpired { get; set; }
    }
}
