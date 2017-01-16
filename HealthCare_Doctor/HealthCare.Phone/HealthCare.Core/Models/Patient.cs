using System.Collections.Generic;
using HealthCare.Core.Utils;
using Newtonsoft.Json;

namespace HealthCare.Core.Models
{
    public class Patient
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        [JsonProperty("parentId", NullValueHandling = NullValueHandling.Ignore)]
        public object ParentId { get; set; }
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }
        [JsonProperty("firstName", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }
        [JsonProperty("lastName", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }
        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }
        [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }
        [JsonProperty("phone", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }
        [JsonProperty("password", NullValueHandling = NullValueHandling.Ignore)]
        public object Password { get; set; }
        [JsonProperty("gender", NullValueHandling = NullValueHandling.Ignore)]
        public string Gender { get; set; }
        [JsonProperty("birthDay", NullValueHandling = NullValueHandling.Ignore)]
        public object BirthDay { get; set; }
        [JsonProperty("idNo", NullValueHandling = NullValueHandling.Ignore)]
        public string IdNo { get; set; }
        [JsonProperty("hospitalId", NullValueHandling = NullValueHandling.Ignore)]
        public object HospitalId { get; set; }
        [JsonProperty("facebookId", NullValueHandling = NullValueHandling.Ignore)]
        public object FacebookId { get; set; }
        [JsonProperty("cityId", NullValueHandling = NullValueHandling.Ignore)]
        public string CityId { get; set; }
        [JsonProperty("districtId", NullValueHandling = NullValueHandling.Ignore)]
        public string DistrictId { get; set; }


        [JsonIgnore]
        private string _patientPhoto;
        [JsonProperty("photo", NullValueHandling = NullValueHandling.Ignore)]
        public string Photo
        {
            get
            {
                if (string.IsNullOrEmpty(_patientPhoto))
                {
                    if (Gender == null || Gender.Equals("M"))
                        return Util.ImageResourceUrl("patient_male", false);
                    else
                        return Util.ImageResourceUrl("patient_female", false);
                }
                return _patientPhoto;
            }
            set { _patientPhoto = value; }
        }
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public object Type { get; set; }
        [JsonProperty("roleMap", NullValueHandling = NullValueHandling.Ignore)]
        public object RoleMap { get; set; }
        [JsonProperty("doctorInfos", NullValueHandling = NullValueHandling.Ignore)]
        public object DoctorInfos { get; set; }
        [JsonProperty("roles", NullValueHandling = NullValueHandling.Ignore)]
        public object Roles { get; set; }
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public int Status { get; set; }
        [JsonProperty("maritalStatus", NullValueHandling = NullValueHandling.Ignore)]
        public bool? MaritalStatus { get; set; }
        [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
        public City City { get; set; }
        [JsonProperty("district", NullValueHandling = NullValueHandling.Ignore)]
        public District District { get; set; }
        [JsonProperty("ownerHospitalId", NullValueHandling = NullValueHandling.Ignore)]
        public string OwnerHospitalId { get; set; }
        [JsonProperty("occupation", NullValueHandling = NullValueHandling.Ignore)]
        public object Occupation { get; set; }
        [JsonProperty("patientType", NullValueHandling = NullValueHandling.Ignore)]
        public object PatientType { get; set; }
        [JsonProperty("patientTypeMap", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> PatientTypeMap { get; set; }
        [JsonProperty("secure", NullValueHandling = NullValueHandling.Ignore)]
        public bool Secure { get; set; }

        [JsonIgnore]
        public string Name
        {
            get { return FirstName + " " + LastName; }
        }

        public override string ToString()
        {
            return string.Format("{0}", Code);
        }
    }
}