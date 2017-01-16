using System.Collections.Generic;
using Newtonsoft.Json;

namespace HealthCare.Core.Models
{
    public class Hospital
    {
        [JsonProperty("hospitalId", NullValueHandling = NullValueHandling.Ignore)]
        public string HospitalId { get; set; }

        private string _id;

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id
        {
            get { return _id ?? HospitalId; }
            set { _id = value; }
        }

        [JsonProperty("parentId", NullValueHandling = NullValueHandling.Ignore)]
        public string ParentId { get; set; }
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }
        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public List<double> Location { get; set; }
        [JsonProperty("phones", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Phones { get; set; }
        [JsonProperty("emails", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Emails { get; set; }
        [JsonProperty("schedulingFee", NullValueHandling = NullValueHandling.Ignore)]
        public double SchedulingFee { get; set; }
        [JsonProperty("checkupFee", NullValueHandling = NullValueHandling.Ignore)]
        public double CheckupFee { get; set; }
        [JsonProperty("photos", NullValueHandling = NullValueHandling.Ignore)]
        public Photos Photos { get; set; }
        [JsonProperty("checkupTypeIds", NullValueHandling = NullValueHandling.Ignore)]
        public object CheckupTypeIds { get; set; }
        [JsonProperty("scbMid", NullValueHandling = NullValueHandling.Ignore)]
        public object ScbMid { get; set; }
        [JsonProperty("scbTid", NullValueHandling = NullValueHandling.Ignore)]
        public object ScbTid { get; set; }
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public object Status { get; set; }
        [JsonProperty("cityId", NullValueHandling = NullValueHandling.Ignore)]
        public string CityId { get; set; }
        [JsonProperty("districtId", NullValueHandling = NullValueHandling.Ignore)]
        public string DistrictId { get; set; }
        [JsonProperty("isFamousHospital", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsFamousHospital { get; set; }
        [JsonProperty("isCLASHospital", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsCLASHospital { get; set; }
        [JsonProperty("adminUsers", NullValueHandling = NullValueHandling.Ignore)]
        public object AdminUsers { get; set; }
        [JsonProperty("classharePercent", NullValueHandling = NullValueHandling.Ignore)]
        public int ClassharePercent { get; set; }
        [JsonProperty("roles", NullValueHandling = NullValueHandling.Ignore)]
        public List<int> Roles { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}