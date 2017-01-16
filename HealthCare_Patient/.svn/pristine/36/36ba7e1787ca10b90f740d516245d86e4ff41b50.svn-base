using HealthCare.Conveters.JsonConverters;
using Newtonsoft.Json;

namespace HealthCare.Models
{
    public class CreditCardModel : Entity
    {
        [JsonProperty(PropertyName = "cardId")]
        public string CardId { get; set; }

        [JsonProperty(PropertyName = "address")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "birthDay")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public double BirthDay { get; set; }

        [JsonProperty(PropertyName = "email")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "idNo")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public string IdNo { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "name")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "phone")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public string PhoneNo { get; set; }

        [JsonProperty(PropertyName = "pinOrOTP")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public string PinOrOtp { get; set; }

        [JsonProperty(PropertyName = "status")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public bool Status { get; set; }
        [JsonProperty(PropertyName = "logo")]
        public string Logo { get; set; }

        [JsonProperty(PropertyName = "token")]
        public string CardToken { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonIgnore]
        public bool IsChoosePayByOtherMethod => AppConstant.VnPayCreaditCardId.Equals(CardId) ||
                                          AppConstant.VtcPayCreaditCardId.Equals(CardId);
    }
}