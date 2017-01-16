using HealthCare.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HealthCare.Objects
{
    public class ApiResponse
    {
        [JsonProperty(PropertyName = "successful")]
        public bool IsSuccessful { get; set; }

        [JsonProperty(PropertyName = "errorCode")]
        public ResponseCode ErrorCode { get; set; }

        [JsonProperty(PropertyName = "errorDesc")]
        public string ErrorDescription { get; set; }

        [JsonProperty(PropertyName = "data")]
        public JObject Data { get; set; }
    }
}