using System;
using HealthCare.Core.Models.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HealthCare.Core.Utils;
#if MVVMCROSS
using MyNotifyPropertyChanged = HealthCare.Core.Models.MyNotifyPropertyChanged;
#else
using MyNotifyPropertyChanged= HealthCare.Core.MyNotifyPropertyChanged;
#endif
namespace HealthCare.Core.Models
{
    public class LoginResult : MyNotifyPropertyChanged
    {
        [JsonProperty("userRoles", NullValueHandling = NullValueHandling.Ignore)]
        public List<UserRole> UserRoles { get; set; }
        [JsonProperty("sessionId", NullValueHandling = NullValueHandling.Ignore)]
        public string SessionId { get; set; }
        [JsonProperty("userId", NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }
        [JsonProperty("sessionExpired", NullValueHandling = NullValueHandling.Ignore)]
        public long SessionExpired { get; set; }
        [JsonProperty("userType", NullValueHandling = NullValueHandling.Ignore)]
        public UserType UserType { get; set; }


        [JsonIgnore]
        private Doctor _profile;
        [JsonIgnore]
        public Doctor Profile
        {
            get { return _profile; }
            set { SetProperty(ref _profile, value); }
        }

        [JsonIgnore]
        public DateTime Expired { get { return Util.LongtoDateTime(SessionExpired); } }

        private ObservableCollection<Schedule> _schedules;
        [JsonIgnore]
        public ObservableCollection<Schedule> Schedules
        {
            get { return _schedules; }
            set { SetProperty(ref _schedules, value); }
        }

        private ObservableCollection<ConsultRequest> _consultRequests;
        [JsonIgnore]
        public ObservableCollection<ConsultRequest> ConsultRequests
        {
            get { return _consultRequests; }
            set { SetProperty(ref _consultRequests, value); }
        }

        public List<Schedule> SchedulesRaw { get; set; }



    }
}
