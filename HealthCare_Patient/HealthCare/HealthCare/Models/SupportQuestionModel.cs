using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Conveters.JsonConverters;
using HealthCare.Enums;
using HealthCare.Helpers;
using HealthCare.Resx;
using HealthCare.ViewModels;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace HealthCare.Models
{
    public class SupportQuestionModel : Entity
    {
        private bool _shared;

        [JsonProperty(PropertyName = "firstName")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "patientPhoto")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string PatientPhoto { get; set; }

        [JsonProperty(PropertyName = "age")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public int Age { get; set; }

        [JsonProperty(PropertyName = "gender")]
        [JsonConverter(typeof(GenderConverterAttribute))]
        public Gender Gender { get; set; }

        [JsonProperty(PropertyName = "email")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string Email { get; set; }

        //[JsonProperty(PropertyName = "doctorName")]
        //[JsonConverter(typeof(NullConverterAttribute))]
        //public string DoctorName { get; set; }

        //[JsonProperty(PropertyName = "hospitalName")]
        //[JsonConverter(typeof(NullConverterAttribute))]
        //public string HospitalName { get; set; }

        [JsonProperty(PropertyName = "symptom")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string Symptom { get; set; }

        [JsonProperty(PropertyName = "whenCreated")]
        [JsonConverter(typeof(DateTimeConverterAttribute))]
        public DateTime WhenCreated { get; set; }

        [JsonProperty(PropertyName = "createdByUserId")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string CreatedByUserId { get; set; }

        [JsonProperty(PropertyName = "replyCount")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public int ReplyCount { get; set; }

        [JsonProperty(PropertyName = "landscapeImage")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string LandscapeImage { get; set; }

        [JsonProperty(PropertyName = "latestResponseDoctorId")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string LatestResponseDoctorId { get; set; }

        [JsonProperty(PropertyName = "latestDoctorPhoto")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string LatestDoctorPhoto { get; set; }

        [JsonProperty(PropertyName = "latestDoctorGender")]
        [JsonConverter(typeof(GenderConverterAttribute))]
        public Gender LatestDoctorGender { get; set; }

        [JsonProperty(PropertyName = "latestDoctorFullName")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string LatestDoctorFullName { get; set; }

        [JsonProperty(PropertyName = "shared")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public bool Shared
        {
            get { return _shared; }
            set
            {
                _shared = value;
                RaisePropertyChanged();
                RaisePropertyChanged("StatusRevertString");
            }
        }

        /// <summary>
        /// Use this property to fix the "lable cannot wrap in grid cell in WP" 
        /// </summary>
        public string LastestDoctorFullNameFake
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(LatestDoctorFullName))
                {
                    LatestDoctorFullName = LatestDoctorFullName.Trim();
                    if (LatestDoctorFullName.Length >= 15 && Common.OS == TargetPlatform.WinPhone)
                        return LatestDoctorFullName.Substring(0, 8) + "...";

                    return LatestDoctorFullName;
                }
                return LatestDoctorFullName;
            }
        }


        [JsonProperty(PropertyName = "latestReplyTime")]
        [JsonConverter(typeof(DateTimeConverterAttribute))]
        public DateTime LatestReplyTime { get; set; }

        public virtual string FullName => (LastName + " " + FirstName).Trim();

        public string BasicInfo
            => (Age > 0 ? (AppResources.age + ": " + Age + ", ") : "") + (Gender == Gender.None ? "" : Gender == Gender.Male ? AppResources.male : AppResources.female);

        public bool IsReplied => ReplyCount > 0;

        public string ReplyCountString
        {
            get
            {
                if (ReplyCount == 1)
                    return string.Format(AppResources.answers_doctor, ReplyCount);
                return (ReplyCount > 1
                    ? string.Format(AppResources.answer_doctors, ReplyCount)
                    : AppResources.answer_doctor);
            }
        }

        public string ReplyCountString2 => ReplyCount + " " + (ReplyCount > 1 ? string.Format(AppResources.anwsers, ReplyCount) : string.Format(AppResources.anwser, ReplyCount));

        public string SymptomFake
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Symptom))
                {
                    Symptom = Symptom.Trim();
                    if (Symptom.Length >= 80)
                        return Symptom.Substring(0, 77) + "...";
                    if (Symptom.IndexOf("\n", StringComparison.Ordinal) > -1)
                    {
                        return Symptom.Substring(0, Symptom.IndexOf("\n", StringComparison.Ordinal)) + "...";
                    }
                    if (Symptom.IndexOf("\r", StringComparison.Ordinal) > -1)
                    {
                        return Symptom.Substring(0, Symptom.IndexOf("\r", StringComparison.Ordinal)) + "...";
                    }
                    return Symptom;
                }
                return Symptom;
            }
        }

        //public ImageSource PatientAvatarImageSource
        //{
        //    get
        //    {
        //        if (string.IsNullOrWhiteSpace(PatientPhoto))
        //            return new FileImageSource
        //            {
        //                File = Gender == Gender.Male ? "avatar_patient_male.jpg" : "avatar_patient_female.jpg"
        //            };
        //        else return new UriImageSource
        //        {
        //            Uri = new Uri(PatientPhoto),
        //            CachingEnabled = true,
        //            CacheValidity = new TimeSpan(7, 0, 0, 0)
        //        };
        //    }
        //}

        public string PatientAvatarImageSource
        {
            get
            {
                if (string.IsNullOrWhiteSpace(PatientPhoto))
                    return Gender == Gender.Female ? AppConstant.AvatarPatientFeMale : AppConstant.AvatarPatientMale;
                else
                    return PatientPhoto;
            }
        }

        public string DoctorAvatarImageSource
        {
            get
            {
                if (string.IsNullOrWhiteSpace(LatestDoctorPhoto))
                    return LatestDoctorGender == Gender.Female ? AppConstant.AvatarDoctorFeMale : AppConstant.AvatarDoctorMale;
                else
                    return LatestDoctorPhoto;
            }
        }

        public string StatusRevertString => Shared ? AppResources.unshare_question : AppResources.share_question;

        public bool IsCreateByUser => !string.IsNullOrWhiteSpace(CreatedByUserId) &&
                                      CreatedByUserId.Equals(UserViewModel.Instance.CurrentUser.Id);

        //public ImageSource DoctorAvatarImageSource
        //{
        //    get
        //    {
        //        if (string.IsNullOrWhiteSpace(LatestDoctorPhoto))
        //            return new FileImageSource
        //            {
        //                File = LatestDoctorGender == Gender.Male ? "avatar_doctor_male.jpg" : "avatar_doctor_female.jpg"
        //            };
        //        else return new UriImageSource
        //        {
        //            Uri = new Uri(LatestDoctorPhoto),
        //            CachingEnabled = true,
        //            CacheValidity = new TimeSpan(7, 0, 0, 0)
        //        };
        //    }
        //}
    }
}
