using System.Collections.Generic;
using Newtonsoft.Json;

namespace HealthCare.Core.Models
{
	public class TopicData : LoadMoreData<Topic>
    {
        [JsonProperty("topics", NullValueHandling = NullValueHandling.Ignore)]
		public override List<Topic> Data { get; set; }
    }
}
