using HealthCare.Conveters.JsonConverters;
using Newtonsoft.Json;
using System;

namespace HealthCare.Models
{
    public class PromotionModel : Entity
    {
        [JsonProperty(PropertyName = "discountPercent")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public double DiscountPercent { get; set; }

        [JsonProperty(PropertyName = "hospitals")]
        [JsonConverter(typeof (PromotionHospitalConverterAttribute))]
        public HospitalModel Hospital { get; set; }

        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }

        [JsonProperty(PropertyName = "endDate")]
        [JsonConverter(typeof (DateTimeConverterAttribute))]
        public DateTime EndDateTime { get; set; }
        
        [JsonProperty(PropertyName = "startDate")]
        [JsonConverter(typeof (DateTimeConverterAttribute))]
        public DateTime StartDateTime { get; set; }

        [JsonProperty(PropertyName = "status")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public bool Status { get; set; }

        [JsonProperty(PropertyName = "name")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "content")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public string Content { get; set; }
    }

    public class Photo
    {
        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }
    }
}