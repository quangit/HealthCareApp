using Newtonsoft.Json;
using HealthCare.Core.Utils;

namespace HealthCare.Core.Models
{
    public class Photos
    {
		private string _logo{get; set;}
        [JsonProperty("logo", NullValueHandling = NullValueHandling.Ignore)]
		public string Logo {
			get { 
				if (string.IsNullOrEmpty (_logo))
					return Util.ImageResourceUrl ("logo_bs");
				return _logo;
			}
			set { _logo = value; }
		}
    }
}