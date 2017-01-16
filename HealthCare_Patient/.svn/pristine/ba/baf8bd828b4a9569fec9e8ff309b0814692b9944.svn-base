using System.Collections.ObjectModel;
using Newtonsoft.Json;
using HealthCare.Helpers;

namespace HealthCare.Objects
{
    public class SystemConfig
    {
        [JsonProperty(PropertyName = "maxRegistrationLimitPerDevice")]
        public int MaxRegistrationLimitPerDevice { get; set; }

        [JsonProperty(PropertyName = "maxCheckupPerAccountPerDay")]
        public int MaxCheckupPerAccountPerDay { get; set; }

        [JsonProperty(PropertyName = "promotionCount")]
        public int PromotionCount  { get; set; }

        [JsonProperty(PropertyName = "patientAppTabs")]
        public ObservableCollection<int> PatientAppTabs { get; set; }
    }
}