using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.DependencyServices;
using HealthCare.Helpers;
using HealthCare.Resx;
using Org.BouncyCastle.Crypto.Digests;
using Xamarin.Forms;

namespace HealthCare.Services
{
    public class VtcPayService
    {
        public static string GeneratePaymentUrl(string orderCode, double amount)
        {
            string url = string.Format(AppConstant.VtcPayUrlFormat, AppConstant.VtcPayUrl, AppConstant.VtcPayWebId,
                orderCode, amount, AppConstant.VtcPayReceiver, AppConstant.VtcPayUrlReturn, AppResources.payment, Sha256(orderCode, amount));
            if (Common.GetDeviceLanguage() == "vi")
            {
                url += "&l=vi";
            }
            else
            {
                url += "&l=en";
            }
            return url; 
        }


        public static string GenerateStatusPaymentUrl(string orderCode, double amount)
        {
            string url = string.Format(AppConstant.VtcPayUrlFormat, AppConstant.VtcPayUrl, AppConstant.VtcPayWebId,
                orderCode, amount, AppConstant.VtcPayReceiver, AppConstant.VtcPayUrlReturn, AppResources.payment, Sha256(orderCode, amount));
            if (Common.GetDeviceLanguage() == "vi")
            {
                url += "&l=vi";
            }
            else
            {
                url += "&l=en";
            }
            return url;
        }

        private static string Sha256(string orderCode, double amount)
        {
            string txt = string.Format("{0}-1-{1}-{2}-{3}--{4}-{5}", AppConstant.VtcPayWebId, orderCode, amount, AppConstant.VtcPayReceiver, AppConstant.VtcPayKey, AppConstant.VtcPayUrlReturn);
     
            var service = DependencyService.Get<ISHA256>();
           return service.GenerateSha256(txt);
         
        }
    }
}
