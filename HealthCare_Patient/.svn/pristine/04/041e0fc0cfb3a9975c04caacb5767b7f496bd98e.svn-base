using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Conveters.JsonConverters;
using Newtonsoft.Json;

namespace HealthCare.Models
{
    public class NameMap
    {
        [JsonProperty(PropertyName = "vi")]
        public string Vi { get; set; }
        [JsonProperty(PropertyName = "en")]
        public string En { get; set; }
        [JsonProperty(PropertyName = "viAscii")]
        public string ViAscii { get; set; }
    }

    public class ContentMap
    {
        [JsonProperty(PropertyName = "vi")]
        public string Vi { get; set; }
        [JsonProperty(PropertyName = "en")]
        public string En { get; set; }
        [JsonProperty(PropertyName = "viAscii")]
        public string ViAscii { get; set; }
    }
    public class Promotion
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "content")]

        public string Content { get; set; }
        [JsonProperty(PropertyName = "nameMap")]

        public NameMap NameMap { get; set; }
        [JsonProperty(PropertyName = "contentMap")]

        public ContentMap ContentMap { get; set; }
        [JsonProperty(PropertyName = "discountPercent")]

        public int DiscountPercent { get; set; }
        [JsonProperty(PropertyName = "image")]

        public string Image { get; set; }
        [JsonProperty(PropertyName = "startDate")]

        public long StartDate { get; set; }
        [JsonProperty(PropertyName = "endDate")]

        public long EndDate { get; set; }
        [JsonProperty(PropertyName = "hospitalId")]

        public object HospitalId { get; set; }
        [JsonProperty(PropertyName = "hospitals")]

        public object Hospitals { get; set; }
        [JsonProperty(PropertyName = "status")]
        public object Status { get; set; }
    }
    public class AmountWithPromotionModel
    {
        [JsonConverter(typeof(NullConverterAttribute))]
        [JsonProperty(PropertyName = "amount")]
        public double Amount { get; set; }

        [JsonConverter(typeof(NullConverterAttribute))]
        [JsonProperty(PropertyName = "discountMoney")]
        public double DiscountMoney { get; set; }

        [JsonProperty(PropertyName = "promotion")]
        [JsonConverter(typeof(PromotionConverter))]
        public Promotion Promotion { get; set; }

        [JsonConverter(typeof(NullConverterAttribute))]
        [JsonProperty(PropertyName = "totalAmount")]
        public double TotalAmount { get; set; }

        [JsonConverter(typeof(NullConverterAttribute))]
        [JsonProperty(PropertyName = "discountPercent")]
        public int DiscountPercent { get; set; }
    }
}
