using HealthCare.Conveters.JsonConverters;
using HealthCare.Resx;
using Newtonsoft.Json;
using Xamarin.Forms.Maps;

namespace HealthCare.Models
{
    public class HospitalModel : Entity
    {
        private bool _status;

        [JsonProperty(PropertyName = "address")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "name")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "checkupFee")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public double CheckupFee { get; set; }

        [JsonProperty(PropertyName = "checkupTypeIds")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string[] CheckupTypeIds { get; set; }

        [JsonProperty(PropertyName = "cityId")]
        public string CityId { get; set; }

        [JsonProperty(PropertyName = "cityName")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string CityName { get; set; }

        [JsonProperty(PropertyName = "CLASSharePercent")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public double CLASSharePercent { get; set; }

        [JsonProperty(PropertyName = "districtId")]
        public string DistrictId { get; set; }

        [JsonProperty(PropertyName = "districtName")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string DistrictName { get; set; }

        [JsonProperty(PropertyName = "emails")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string[] Email { get; set; }

        [JsonProperty(PropertyName = "sharingHospitalIds")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string[] SharingHospitalIds { get; set; }

        [JsonProperty(PropertyName = "hospitalCode")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string HospitalCode { get; set; }

        [JsonProperty(PropertyName = "isCLASHospital")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public bool IsCLASHospital { get; set; }

        [JsonProperty(PropertyName = "isFamousHospital")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public bool IsFamousHospital { get; set; }

        [JsonProperty(PropertyName = "location")]
        [JsonConverter(typeof(LocationConverterAttribute))]
        public Position Location { get; set; }

        [JsonProperty(PropertyName = "photos")]
        public Photos Photos { get; set; }

        [JsonProperty(PropertyName = "parentHospitalId")]
        public string ParentHospitalId { get; set; }

        [JsonProperty(PropertyName = "phones")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string[] PhoneNo { get; set; }

        [JsonProperty(PropertyName = "schedulingFee")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public double SchedulingFee { get; set; }

        [JsonProperty(PropertyName = "sharedHospitalIds")]
        public string[] SharedHospitalIds { get; set; }

        [JsonProperty(PropertyName = "tid")]
        public string TId { get; set; }

        [JsonProperty(PropertyName = "mid")]
        public string MId { get; set; }

        [JsonProperty(PropertyName = "status")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public bool Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string AsString
            =>
            //IsCLASHospital
            //    ? AppResources.clas_hospital
            //    : 
            Name.Replace("Bệnh viện", "").Replace("Bệnh Viện", "")
                    + (Status ? "" : " " + AppResources.deleted);

        public string AsStringWithTextHospital
        =>
        //IsCLASHospital
        //? AppResources.clas_hospital
        //    :
            Name +
        (Status ? "" : " " + AppResources.deleted);

        public string LocationAsString => DistrictName + (!string.IsNullOrWhiteSpace(DistrictName) ? ", " : "") + CityName;
    }

    public class Photos
    {
        [JsonProperty(PropertyName = "logo")]
        public string Logo { get; set; }
    }
}