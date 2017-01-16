using System;
using System.Diagnostics;
using HealthCare.Conveters.JsonConverters;
using HealthCare.Enums;
using Newtonsoft.Json;

namespace HealthCare.Models
{
    public class PersonModel : Entity
    {
        private CityModel _city;
        private DistrictModel _district;

        [JsonProperty(PropertyName = "code")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public string UserCode { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        public string VerifyPassword { get; set; }

        [JsonProperty(PropertyName = "gender")]
        [JsonConverter(typeof (GenderConverterAttribute))]
        public Gender Gender { get; set; }

        [JsonProperty(PropertyName = "idNo")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public string IdNo { get; set; }

        [JsonProperty(PropertyName = "birthDay")]
        [JsonConverter(typeof (DateTimeConverterAttribute))]
        public DateTime BirthDay { get; set; }

        [JsonProperty(PropertyName = "phone")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public string PhoneNo { get; set; }

        [JsonProperty(PropertyName = "email")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "city")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public CityModel City
        {
            get { return _city; }
            set
            {
                _city = value;
                CityId = value.Id;
            }
        }

        [JsonProperty(PropertyName = "district")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public DistrictModel District
        {
            get { return _district; }
            set
            {
                _district = value;
                DistrictId = value.Id;
            }
        }

        [JsonProperty(PropertyName = "cityId")]
        public string CityId { get; set; }

        [JsonProperty(PropertyName = "districtId")]
        public string DistrictId { get; set; }

        [JsonProperty(PropertyName = "address")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "profilePictureUrl")]
        public string ProfilePictureUrl { get; set; }

        [JsonProperty(PropertyName = "status")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public PersonStatus Status { get; set; }

        [JsonProperty(PropertyName = "photo")]
        public string Photo { get; set; }

        [JsonProperty(PropertyName = "chbaseId")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string ChbaseId { get; set; }

        public virtual string FullName => (LastName + " " + FirstName).Trim();
    }
}