using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HealthCare.Models.ChBaseModel
{
    public class DetailPackageFeeModel
    {
        [JsonProperty(PropertyName = "promotionId")]
        public string PromotionId { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public double Amount { get; set; }

        [JsonProperty(PropertyName = "discountMoney")]
        public double DiscountMoney { get; set; }

        [JsonProperty(PropertyName = "totalAmount")]
        public double TotalAmount { get; set; }

        [JsonProperty(PropertyName = "discountPercent")]
        public double DiscountPercent { get; set; }
    }
}
