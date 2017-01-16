using HealthCare.Core.Models.Enums;
using Newtonsoft.Json;

namespace HealthCare.Core.Models
{
    public sealed class ApiRootObject<T>
    {
        [JsonProperty("successful", NullValueHandling = NullValueHandling.Ignore)]
        public bool Successful { get; set; }
        [JsonProperty("errorCode", NullValueHandling = NullValueHandling.Ignore)]
        public ErrorCode ErrorCode { get; set; }
        [JsonProperty("errorDesc", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorDesc { get; set; }
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
    }
}
