using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Core.Models
{
    public class PushConfig
    {
        [JsonProperty("channel", NullValueHandling = NullValueHandling.Ignore)]
        public string Channel { get; set; }
        [JsonProperty("platform", NullValueHandling = NullValueHandling.Ignore)]
        public string Platform { get; set; }
        [JsonProperty("registrationId", NullValueHandling = NullValueHandling.Ignore)]
        public string RegistrationId { get; set; }
        [JsonProperty("active", NullValueHandling = NullValueHandling.Ignore)]
        public string Active { get; set; }
        [JsonProperty("userType", NullValueHandling = NullValueHandling.Ignore)]
        public int UserType { get; set; }
    }

}
