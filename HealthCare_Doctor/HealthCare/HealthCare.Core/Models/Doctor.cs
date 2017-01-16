using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace HealthCare.Core.Models
{

    public class Doctor : SignUpInfo
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        [JsonProperty("parentId", NullValueHandling = NullValueHandling.Ignore)]
        public object ParentId { get; set; }
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }
        
       
        [JsonProperty("middleName", NullValueHandling = NullValueHandling.Ignore)]
        public object MiddleName { get; set; }
       
       
           
        [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
        public object Username { get; set; }
                                                
        [JsonProperty("hospitalId", NullValueHandling = NullValueHandling.Ignore)]
        public object HospitalId { get; set; }
        [JsonProperty("facebookId", NullValueHandling = NullValueHandling.Ignore)]
        public object FacebookId { get; set; }
        
       
        
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public int Type { get; set; }
        [JsonProperty("roleMap", NullValueHandling = NullValueHandling.Ignore)]
        public object RoleMap { get; set; }
        [JsonProperty("doctorInfos", NullValueHandling = NullValueHandling.Ignore)]
        public List<DoctorInfo> DoctorInfos { get; set; }
        [JsonProperty("roles", NullValueHandling = NullValueHandling.Ignore)]
        public object Roles { get; set; }
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public bool Status { get; set; }
        [JsonProperty("maritalStatus", NullValueHandling = NullValueHandling.Ignore)]
        public object MaritalStatus { get; set; }
        [JsonProperty("getMedicalCheckups", NullValueHandling = NullValueHandling.Ignore)]
        public object GetMedicalCheckups { get; set; }                                        
        [JsonProperty("checkupTypeId", NullValueHandling = NullValueHandling.Ignore)]
        public object CheckupTypeId { get; set; }
        [JsonProperty("ownerHospitalId", NullValueHandling = NullValueHandling.Ignore)]
        public object OwnerHospitalId { get; set; }
        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public object Location { get; set; }

		public string _fullName;
		[JsonProperty("fullName", NullValueHandling = NullValueHandling.Ignore)]
		public string FullName
		{
			get { return _fullName; }
			set { SetProperty(ref _fullName, value); }
		}


		public string _title;
		[JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}

        [JsonIgnore]
        public string Name
        {
			get { return FullName; }//Title + " " + LastName + " " + FirstName; }
        }

        [JsonIgnore]
        public List<Hospital> Hospitals
        {
            get
            {
                if (DoctorInfos != null && DoctorInfos.Any())
                {
                    return DoctorInfos.Where(x => x.Roles.Contains(1)).Select(x => x.Hospital).ToList();// role 1 = this user is Doctor
                }
                return new List<Hospital>();
            }
        }

        [JsonIgnore]
        public string Desc
        {
            get
            {
                if (Hospitals != null && Hospitals.Count > 0)
                {
                    return Hospitals.Select(x => x.Name).Aggregate((x, y) => x + ", " + y);
                }
                return "";
            }
        }
        [JsonIgnore]
        public List<Hospital> AllHospitals
        {
            get
            {
                if (DoctorInfos != null && DoctorInfos.Any())
                {
                    return DoctorInfos.Select(x => x.Hospital).ToList();// role 1 = this user is Doctor & 9 - this user is Admin
                }
                return new List<Hospital>();
            }
        }
    }
}