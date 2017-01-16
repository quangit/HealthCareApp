using System.Collections.Generic;
using Newtonsoft.Json;

namespace HealthCare.Core.Models
{
    public class ConsultRequestData : LoadMoreData<ConsultRequest> {
        [JsonProperty("requests", NullValueHandling = NullValueHandling.Ignore)]
        public override List<ConsultRequest> Data { get; set; }
    }
}