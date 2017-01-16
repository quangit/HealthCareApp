using System.Collections.Generic;
using Newtonsoft.Json;
using HealthCare.Core.Utils;
using HealthCare.Core.Resources;

namespace HealthCare.Core.Models
{
    public class ConsultRequsetDetail : MyNotifyPropertyChanged
    {
        [JsonProperty("question", NullValueHandling = NullValueHandling.Ignore)]
        public ConsultRequest Question { get; set; }
        [JsonProperty("responses", NullValueHandling = NullValueHandling.Ignore)]
        public List<ConsultResponse> Responses { get; set; }
    }

    public class ConsultRequest : MyNotifyPropertyChanged
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        [JsonProperty("firstName", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }
        [JsonProperty("lastName", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }
		[JsonProperty("patientFullName", NullValueHandling = NullValueHandling.Ignore)]
		public string FullName { get; set; }
        [JsonProperty("age", NullValueHandling = NullValueHandling.Ignore)]
        public int Age { get; set; }
        [JsonProperty("gender", NullValueHandling = NullValueHandling.Ignore)]
        public string Gender { get; set; }
        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }
        [JsonProperty("doctorName", NullValueHandling = NullValueHandling.Ignore)]
        public string DoctorName { get; set; }
        [JsonProperty("hospitalName", NullValueHandling = NullValueHandling.Ignore)]
        public string HospitalName { get; set; }
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public object Status { get; set; }
        [JsonProperty("whenCreated", NullValueHandling = NullValueHandling.Ignore)]
        public long WhenCreated { get; set; }
        [JsonProperty("replied", NullValueHandling = NullValueHandling.Ignore)]
        public bool Replied { get; set; }


		[JsonIgnore]
		private string _symptom = "";
		[JsonProperty("symptom", NullValueHandling = NullValueHandling.Ignore)]
		public string Symptom
		{
			get { return _symptom.Trim(); }
			set { SetProperty(ref _symptom, value); }
		}



		[JsonIgnore]
		private string _replyContent;
		[JsonProperty("replyContent", NullValueHandling = NullValueHandling.Ignore)]
		public string ReplyContent
		{
			get { return _replyContent; }
			set { SetProperty(ref _replyContent, value); }
		}

		[JsonIgnore]
		private int _replyCount;
		[JsonProperty("replyCount", NullValueHandling = NullValueHandling.Ignore)]
		public int ReplyCount {
			get { return _replyCount;}
			set { 
				SetProperty(ref _replyCount, value); 
				if(_replyCount != 0)
					ReplyCountStr = _replyCount + ((_replyCount > 1) ? AppResources.Consult_Answers : AppResources.Consult_Answer);

//				if(_replyCount == 0){
//					AndStr = "";
//					ReplyCountStr = "";
//				}
//				else if(_replyCount == 1){
//					AndStr = "";
//					ReplyCountStr = AppResources.Consult_Answered;
//				}
//				else{
//					AndStr = "&";
//					ReplyCountStr = (_replyCount-1) + AppResources.Consult_AndAnswered; 
//				}
				//RaisePropertyChanged ("AndStr");
				//RaisePropertyChanged ("ReplyCountStr");
			}
		}
		[JsonIgnore]
		public string AndStr{ get ; set;}

		[JsonIgnore]
		public string ReplyCountStr{ get ; set;}


        [JsonProperty("landscapeImage", NullValueHandling = NullValueHandling.Ignore)]
        public string LandscapeImage
        {
            get { return _landscapeImage; }
            set { _landscapeImage = value; }
        }


        [JsonProperty("patientSkypeId", NullValueHandling = NullValueHandling.Ignore)]
		public string PatientSkypeId { get ; set;}

		[JsonIgnore]
		public string PatientSkypeUrl { get {
				if (!string.IsNullOrEmpty(PatientSkypeId))
					return "skype:" + PatientSkypeId+"?chat";
				return "";
			}
		}

		[JsonProperty("latestDoctorGender", NullValueHandling = NullValueHandling.Ignore)]
		public string LatestDoctorGender { get ; set;}

		[JsonIgnore]
		private string _latestDoctorPhoto;
		[JsonProperty("latestDoctorPhoto", NullValueHandling = NullValueHandling.Ignore)]
		public string LatestDoctorPhoto {
			get { 
				if (string.IsNullOrEmpty (_latestDoctorPhoto)) {
					if((LatestDoctorGender == null) || LatestDoctorGender.Equals("M"))
							return Util.ImageResourceUrl ("doctor_male",false);
						else
							return Util.ImageResourceUrl ("doctor_female",false);
				}
				return _latestDoctorPhoto;
			}
			set { SetProperty(ref _latestDoctorPhoto, value); }
		}

		[JsonProperty("latestResponseDoctorId", NullValueHandling = NullValueHandling.Ignore)]
		public string LatestResponseDoctorId { get; set; }
		[JsonProperty("latestDoctorFullName", NullValueHandling = NullValueHandling.Ignore)]
		public string LatestDoctorFullName { get; set; }
		[JsonProperty("latestReplyTime", NullValueHandling = NullValueHandling.Ignore)]
		public long LatestReplyTime { get; set; }


		[JsonIgnore]
		private string _patientPhoto;

        private string _landscapeImage;

        [JsonProperty("patientPhoto", NullValueHandling = NullValueHandling.Ignore)]
		public string PatientPhoto {
			get { 
				
				if (string.IsNullOrEmpty (_patientPhoto)) {
					if(Gender == null || Gender.Equals("M"))
						return Util.ImageResourceUrl ("patient_male",false);
					else
						return Util.ImageResourceUrl ("patient_female",false);
				}
				return _patientPhoto;
			}
			set { SetProperty(ref _patientPhoto, value); }
		}

        [JsonIgnore]
		public string PatientName { get { return FullName; } }
        [JsonIgnore]
        public string PatientDesc { get { return Age + ", " + Gender; } }

        [JsonIgnore]
        public bool CanReply { get { return !Replied; } }

    }
}