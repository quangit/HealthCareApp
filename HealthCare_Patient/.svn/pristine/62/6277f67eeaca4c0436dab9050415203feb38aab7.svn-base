using System;
using HealthCare.Conveters.JsonConverters;
using HealthCare.Enums;
using HealthCare.Helpers;
using HealthCare.Resx;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace HealthCare.Models
{
    public class DoctorModel : PersonModel
    {
        [JsonProperty(PropertyName = "isFamilyDoctor")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public bool IsFamilyDoctor { get; set; }

        [JsonProperty(PropertyName = "checkupFee")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public double CheckupFee { get; set; }

        [JsonProperty(PropertyName = "ownerHospitalId")]
        public string OwnerHospitalId { get; set; }

        [JsonProperty(PropertyName = "checkupTypeId")]
        public string CheckupTypeId { get; set; }

        [JsonProperty(PropertyName = "languages")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string Languages { get; set; }

        [JsonProperty(PropertyName = "experience")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string Experience { get; set; }

        [JsonProperty(PropertyName = "certification")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string Certification { get; set; }

        [JsonProperty(PropertyName = "isActivated")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public bool IsActivated { get; set; }

        [JsonProperty(PropertyName = "location")]
        [JsonConverter(typeof(LocationConverterAttribute))]
        public Position Location { get; set; }

        [JsonProperty(PropertyName = "avgRating")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public float AvgRating { get; set; }

        [JsonProperty(PropertyName = "rating")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public int Rating { get; set; }

        [JsonProperty(PropertyName = "rateCount")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public int RateCount { get; set; }

        [JsonProperty(PropertyName = "checkupCount")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public int CheckupCount { get; set; }

        public string AvgRatingText
        {
            get { return "(" + AvgRating + ")"; } 
        }

        //public override string FullName
        //    => base.FullName + (Status == PersonStatus.Deactivated ? (" " + AppResources.deleted) : "");

        [JsonProperty(PropertyName = "fullName")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string FullName { get; set; }


        [JsonProperty(PropertyName = "title")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string Title { get; set; }

        //public ImageSource Avatar
        //{
        //    get
        //    {
        //        if (string.IsNullOrWhiteSpace(Photo))
        //            return new FileImageSource
        //            {
        //                File = Gender == Gender.Male ? "avatar_patient_male.jpg" : "avatar_patient_female.jpg"
        //            };
        //        else return new UriImageSource
        //        {
        //            Uri = new Uri(Photo),
        //            CachingEnabled = true,
        //            CacheValidity = new TimeSpan(7, 0, 0, 0)
        //        };
        //    }
        //}

        public string Avatar
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Photo))
                    return Gender == Gender.Female ? AppConstant.AvatarDoctorFeMale : AppConstant.AvatarDoctorMale;
                else return Photo;
            }
        }
    }
}