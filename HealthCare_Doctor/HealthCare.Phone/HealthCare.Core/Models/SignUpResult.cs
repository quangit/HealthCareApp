using Newtonsoft.Json;

namespace HealthCare.Core.Models
{
    public class SignUpResult
    {
        [JsonProperty("user", NullValueHandling = NullValueHandling.Ignore)]
        public User User { get; set; }
    }
}