using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Helpers;

namespace HealthCare.WebServices
{
   public class VnPayService: BaseFakeWebService
    {

        public async Task<string> GetUrlPayment(double amount)
        {
            var url = AppConstant.VnPayHost+ AppConstant.VnPayProcessUrl;
            var data = new Dictionary<string, string>();
            data.Add("vpc_Amount",amount.ToString());
            var result = await SendFormUrlMessage(url, data );
            if (result.StatusCode == HttpStatusCode.Found)
            {
                return result.Headers.Location.OriginalString;
            }
            else
            {
                return "";
            }
        }
    }
}
