using HealthCare.Conveters.JsonConverters;
using HealthCare.Enums;
using HealthCare.Helpers;
using Newtonsoft.Json;

namespace HealthCare.Models
{
    public class UserModel : PersonModel
    {
        private bool _isBirthDaySet;

        [JsonProperty(PropertyName = "parentId")]
        public string ParentId { get; set; }

        [JsonProperty(PropertyName = "facebookId")]
        public string FacebookId { get; set; }

        [JsonProperty(PropertyName = "secure")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public bool IsSecure { get; set; }

        [JsonProperty(PropertyName = "maritalStatus")]
        [JsonConverter(typeof(MaritalStatusConverterAttribute))]
        public MaritalStatus MaritalStatus { get; set; }

        [JsonProperty(PropertyName = "occupation")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string Occupation { get; set; }

        [JsonProperty(PropertyName = "skypeId")]
        public string SkypeId { get; set; }
        [JsonProperty(PropertyName = "isActivated")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public bool IsActivated { get; set; }

        //[JsonProperty(PropertyName = "patientType")]
        //public PatientType PatientType { get; set; }

        [JsonProperty(PropertyName = "ownerHospitalId")]
        public string OwnerHospitalId { get; set; }

        [JsonIgnoreAttribute]
        //[JsonProperty(PropertyName = "birthDay")]
        //[JsonConverter(typeof (BirthDaySetConverterAttribute))]
        public bool IsBirthDaySet => Common.ConvertToTimestamp(BirthDay) > 1;

        [JsonProperty(PropertyName = "activateType")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public ActiveType ActiveType { get; set; }

        public UserModel Clone()
        {
            var obj = this.CloneJson();
            return obj;
        }
    }
}