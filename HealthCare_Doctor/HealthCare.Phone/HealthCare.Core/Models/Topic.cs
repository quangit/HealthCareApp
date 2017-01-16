using System.Collections.Generic;
using HealthCare.Core.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
#if MVVMCROSS
using Cirrious.MvvmCross.ViewModels;
using MyMvxViewModel = HealthCare.Core.ViewModels.MyMvxViewModel;
#else
using MyMvxViewModel = Template10.Mvvm.ViewModelBase;
using Template10.Mvvm;
using HealthCare.Core;
#endif
using HealthCare.Core.ViewModels;
using HealthCare.Core.Resources;
using Acr.UserDialogs;
using HealthCare.Core.Services;

namespace HealthCare.Core.Models
{
	public class ConsultRequestRootObject : MyNotifyPropertyChanged{
		[JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
		public int Total { get; set; }
		[JsonProperty("requests", NullValueHandling = NullValueHandling.Ignore)]
		public List<Topic> Requests { get; set; }
	}

	public class Topic : MyMvxViewModel
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }
        [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }
        [JsonProperty("districtId", NullValueHandling = NullValueHandling.Ignore)]
        public string DistrictId { get; set; }
        [JsonProperty("cityId", NullValueHandling = NullValueHandling.Ignore)]
        public string CityId { get; set; }
        [JsonProperty("skypeForBusinessUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string SkypeForBusinessUrl { get; set; }
        [JsonProperty("startDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public long StartDateTime { get; set; }
        [JsonProperty("endDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public long EndDateTime { get; set; }
        [JsonProperty("isOnline", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsOnline { get; set; }
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public bool Status { get; set; }
        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public List<double> Location { get; set; }
        [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
        public City City { get; set; }
        [JsonProperty("district", NullValueHandling = NullValueHandling.Ignore)]
        public District District { get; set; }

        [JsonProperty("owner", NullValueHandling = NullValueHandling.Ignore)]
        public string Owner { get; set; }

	    [JsonIgnore]
	    public bool HasFiles => (TopicFiles != null) && (TopicFiles.Count > 0);

      
        public List<TopicFiles> TopicFiles
        {
            get; set;
        }

        public Topic()
        {
            TopicFiles = new List<TopicFiles>();
        }

        [JsonIgnore]
        public string Time
        {
            get { return Util.LongtoDateTime(StartDateTime) + "-" + Util.LongtoDateTime(EndDateTime); }
        }
        [JsonIgnore]
        public string StartDateTimeString {
            get { return Util.LongtoDateTime(StartDateTime).ToString(); }
        }
        [JsonIgnore]
        public string EndDateTimeString
        {
            get { return Util.LongtoDateTime(EndDateTime).ToString(); }
        }

		[JsonIgnore]
		public bool IsOnlineNow{
			get{ 
				var longDateTimeNow = Util.DateTimeToLong(DateTime.Now) - TimeZoneInfo.Local.BaseUtcOffset.TotalMilliseconds;
				var ret = IsOnline && (longDateTimeNow >= StartDateTime &&
				           longDateTimeNow <= EndDateTime);
				return ret;
			}
		}

		[JsonIgnore]
		public string LocationStr{
			get{ 
				if (Location == null)
					return "";
				var ret = Util.DoubleToString(Location[1])  + "," + Util.DoubleToString(Location[0]); // 1 - lat, 0 - lon
				return ret;
			}
		}

		private MvxCommand _showWeekTopicFileCommand;


		public MvxCommand ShowWeekTopicFileCommand
		{
			get
			{
				_showWeekTopicFileCommand = _showWeekTopicFileCommand ?? new MvxCommand(ShowWeekfile);
				return _showWeekTopicFileCommand;
			}
		}


		public async void ShowWeekfile()
		{
		    TopicFiles = await HealthCareService.Current.GetTopicFiles(Id);

            if (!HasFiles)
				UserDialogs.Instance.Alert(AppResources.WeekTopicFile_Empty, AppResources.Warning);
			else
				ShowViewModel<WeekTopicFileViewModel>(this);
		}
    }




    public class TopicFiles {
        [JsonProperty("FileName", NullValueHandling = NullValueHandling.Ignore)]
        public string FileName { get; set; }

        [JsonProperty("FileUri", NullValueHandling = NullValueHandling.Ignore)]
        public string FileUri { get; set; }

        public static implicit operator TopicFiles(ObservableCollection<TopicFiles> v)
        {
            throw new NotImplementedException();
        }
    }
}