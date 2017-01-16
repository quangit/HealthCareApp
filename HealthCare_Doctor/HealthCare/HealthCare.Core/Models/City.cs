using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace HealthCare.Core.Models
{
    public class City
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty("isUsedForDoctorSearch", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsUsedForDoctorSearch { get; set; }
        [JsonProperty("districts", NullValueHandling = NullValueHandling.Ignore)]
        public List<District> Districts { get; set; }

		[JsonIgnore]
		public List<string> DistrictNames { 
			get{ 
				if(Districts!=null)
					return Districts.Select (x => x.Name).ToList ();
				return null;
			}
		}

        public override string ToString()
        {
            return Name;
        }
    }
}