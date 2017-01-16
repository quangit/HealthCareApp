using System.Reflection;
using HealthCare.Conveters.JsonConverters;
using HealthCare.Enums;
using HealthCare.Resx;
using Newtonsoft.Json;

namespace HealthCare.Models
{
    public class Suggestion : Entity
    {
        [JsonProperty(PropertyName = "name")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "type")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public KeywordType Type { get; set; }

        public string TypeAsString
        {
            get
            {
                switch (Type)
                {
                    case KeywordType.CheckupType:
                        return AppResources.checkup_type;
                    case KeywordType.Doctor:
                        return AppResources.doctor;
                    case KeywordType.Hospital:
                        return AppResources.hospital_place_exam;
                    case KeywordType.Symptom:
                        return AppResources.symptom;
                    default:
                        return AppResources.symptom;
                }
            }
        }

        public static bool operator ==(Suggestion a, Suggestion b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;
            if (!ReferenceEquals(a, null) && !ReferenceEquals(b, null))
            {
                if (a.Id == null && b.Id == null)
                {
                    if (a.Name != null && a.Name != null)
                        return a.Name.Equals(b.Name);
                }
                else if (a.Id != null && b.Id != null)
                {
                    return a.Id.Equals(b.Id);
                }
            }
            return false;
        }

        public static bool operator !=(Suggestion a, Suggestion b)
        {
            return !(a == b);
        }
    }
}