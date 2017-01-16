using System;
using System.Collections.Generic;
using HealthCare.Core.Resources;
using HealthCare.Core.Utils;
using Newtonsoft.Json;

namespace HealthCare.Core.Models
{
    public class SignUpInfo : MyNotifyPropertyChanged
    {
        private string _address;
        private DateTime? _birthDate;
        private string _birthDay;
        private CheckupType _checkupType;
        private City _city;
        private string _cityId;
        private District _district;
        private string _districtId;
        private string _email;
        private string _firstName;
        private Gender _gender;
        private string _idNo;
        private string _lastName;
        private string _password;
        private string _phone;
        private string _rePass;

        public SignUpInfo()
        {
            Gender = new Gender();
        }
        [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }

        private long? _birthDayUnix;
        [JsonProperty("birthDay", NullValueHandling = NullValueHandling.Ignore)]
        public long? BirthDayUnix
        {
            get { return _birthDayUnix; }
            set
            {
                _birthDayUnix = value;
                if (value != null)
                    BirthDate = Util.LongtoDateTime(value.Value);
            }
        }
        public DateTime? BirthDate
        {
            get { return _birthDate; }
            set
            {
                SetProperty(ref _birthDate, value);
                if (_birthDate != null)
                {
                    BirthDay = _birthDate.Value.ToString("d");
                }
            }
        }

        public string BirthDay
        {
            get { return _birthDay; }
            set { SetProperty(ref _birthDay, value); }
        }
        [JsonProperty("cityId", NullValueHandling = NullValueHandling.Ignore)]
        public string CityId
        {
            get { return _cityId; }
            set { SetProperty(ref _cityId, value); }
        }

        [JsonProperty("districtId", NullValueHandling = NullValueHandling.Ignore)]
        public string DistrictId
        {
            get { return _districtId; }
            set { SetProperty(ref _districtId, value); }
        }
        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        [JsonProperty("firstName", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        public Gender Gender
        {
            get { return _gender; }
            set
            {
                SetProperty(ref _gender, value);
                RaisePropertyChanged("GenderString");
            }
        }
        [JsonProperty("gender", NullValueHandling = NullValueHandling.Ignore)]
        public string GenderString
        {
            get { return _gender?.ToString() ?? AppResources.SignUp_Gender; }
            set { _gender = new Gender() { IsFemale = value == "F" || value == AppResources.SignUp_Female }; }
        }
        [JsonProperty("idNo", NullValueHandling = NullValueHandling.Ignore)]
        public string IdNo
        {
            get { return _idNo; }
            set { SetProperty(ref _idNo, value); }
        }

        public byte[] Images { get; set; }

        [JsonProperty("lastName", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }
        [JsonProperty("password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
        [JsonProperty("phone", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone
        {
            get { return _phone; }
            set { SetProperty(ref _phone, value); }
        }

        public string RePass
        {
            get { return _rePass; }
            set { SetProperty(ref _rePass, value); }
        }

        [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
        public City City
        {
            get { return _city; }
            set
            {
                SetProperty(ref _city, value);
                if (_city != null)
                {
					CityId = _city.Id;
					if (District != null && (_city.Id != District.CityId))// city not contain district
						District = null;
                }

            }
        }


        [JsonProperty("district", NullValueHandling = NullValueHandling.Ignore)]
        public District District
        {
            get { return _district; }
            set
            {
                SetProperty(ref _district, value);
                if (_district != null)
                {
						DistrictId = _district.Id;
					}
            }
        }

        [JsonIgnore]
        public Dictionary<string, object> Data
        {
            get
            {
                return new Dictionary<string, object>
                {
                    {"phone", Phone},
                    {"password", Password},
                    {"firstName", FirstName},
                    {"lastName", LastName},
                    {"email", Email.ToLower()}
                };
            }
        }
        [JsonIgnore]
        public Dictionary<string, object> UpdateProfileData
        {
            get
            {
                return new Dictionary<string, object>
                {
                    {"phone", Phone},
                    {"password", Password},
                    {"firstName", FirstName},
                    {"lastName", LastName},
                    {"gender", Gender.Value},
                    {"idNo", IdNo},
                    {"address", Address},
                    {"birthDay", BirthDate != null ? Util.DateTimeToLong(BirthDate.Value).ToString() : "null"},
                    {"email", Email},
                    {"cityId", CityId},
                    {"districtId", DistrictId},
					{"checkupTypeId", CheckupType != null ? CheckupType.Id : "null"},
                    {"photo", Images},
                    {"experience", Experience},
                    {"languages", Languages},
                    {"certification", Certification},
                    //{"isFamilyDoctor", IsFamilyDoctor}
                };
            }
        }

		[JsonIgnore]
		private string _photo;
		[JsonProperty("photo", NullValueHandling = NullValueHandling.Ignore)]
        public string Photo
        {
            get
            {
                if (string.IsNullOrEmpty(_photo))
                {
                    if (Gender == null || !Gender.IsFemale)
                        return Util.ImageResourceUrl("doctor_male", false);
					else
                        return Util.ImageResourceUrl("doctor_female", false);
				}
				return _photo;
			}
			set { SetProperty(ref _photo, value); }
		}
        [JsonProperty("isFamilyDoctor", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsFamilyDoctor { get; set; }
        [JsonProperty("certification", NullValueHandling = NullValueHandling.Ignore)]
        public string Certification { get; set; }
        [JsonProperty("languages", NullValueHandling = NullValueHandling.Ignore)]
        public string Languages { get; set; }
        [JsonProperty("experience", NullValueHandling = NullValueHandling.Ignore)]
        public string Experience { get; set; }

        public CheckupType CheckupType
        {
            get { return _checkupType; }
            set { SetProperty(ref _checkupType, value); }
        }


        public bool IsValid()
        {
            return !string.IsNullOrEmpty(FirstName) &&
                   !string.IsNullOrEmpty(LastName) &&
                   !string.IsNullOrEmpty(Email) &&
                   !string.IsNullOrEmpty(Password) &&
                   !string.IsNullOrEmpty(RePass) &&
                   //City != null &&
                   //District != null &&
                   //BirthDate != null &&
                   //CheckupType != null &&
                   //!string.IsNullOrEmpty(IdNo) &&
                   //!string.IsNullOrEmpty(Address) &&
                   !string.IsNullOrEmpty(Phone);
        }
    }
}