using System.Collections.Generic;
using Newtonsoft.Json;

namespace HealthCare.Core.Models
{
    public abstract class LoadMoreData<T> : MyNotifyPropertyChanged{
        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public int Total { get; set; }
        public int Count () {
            if (Data == null)
                return 0;
            return Data.Count;
        }
        public abstract List<T> Data { get; set; }
    }
}