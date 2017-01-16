using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Helpers;
using LumiaLoyalty.Core.Services;

namespace HealthCare.Services
{
    public enum LoginType { Clas, Microsoft, Facebook, Google };
    public class SacombankService
    {
        private static string CorrectUrlEncode(string rawEncode)
        {
            return rawEncode?.Replace("%25", "%");
        }

        private const string KEY = "FBD8B6D1E9F6FE8D";
        public static string GenerateChangePinUrl()
        {
            bool isFacebook = !string.IsNullOrEmpty(Settings.CurrentUser.FacebookId);
            string customerId = Settings.UserId;
            string p2 = "{{\"ProviderID\":\"{0}\",\"AccessToken\":\"{1}\",\"CustomerID\":\"{2}\",\"Language\":\"{3}\"}}";
            p2 = string.Format(p2, isFacebook ? LoginType.Facebook : LoginType.Clas, "", customerId, LanguageParams());
            TripleDESCryptoService cryptoService = new TripleDESCryptoService { Key = KEY };
            var _p2 = WebUtility.UrlEncode(cryptoService.Encrypt(p2, false));

            string source = CorrectUrlEncode(String.Format(AppConstant.SacombankHost + "/eShop?s=ES06&param1=3&param2={0}", _p2));
            return source;
        }
        public static string GenerateLinkCardUrl()
        {
            bool isFacebook = !string.IsNullOrEmpty(Settings.CurrentUser.FacebookId);
            string customerId = Settings.UserId;
            string p2 = "{{\"ProviderID\":\"{0}\",\"AccessToken\":\"{1}\",\"CustomerID\":\"{2}\",\"Language\":\"{3}\"}}";
            p2 = string.Format(p2, "clas", "", customerId, LanguageParams());
            TripleDESCryptoService cryptoService = new TripleDESCryptoService { Key = KEY };
            var _p2 = WebUtility.UrlEncode(cryptoService.Encrypt(p2, false));
            string source =
                CorrectUrlEncode(String.Format(AppConstant.SacombankHost + "/eShop?s=ES01&param1=3&param2={0}", _p2));
            return source;
        }

        //public static bool IsOrderSuccess(string originalString)
        //{
        //    return originalString.ToUpper().StartsWith("http://menu.clas.mobi/Shop/OrderSuccessful".ToUpper());
        //}

        public static bool IsSuccess(string url)
        {
            var ret = (AppConstant.SacombankHost + "/eShop?s=ES09&pr=1&param1=3").ToUpper() == url.ToUpper();
            return ret;
        }
        public static bool IsFailed(string url)
        {
            var ret = (AppConstant.SacombankHost + "/eShop?s=ES10&pr=1&param1=3").ToUpper() == url.ToUpper();
            return ret;
        }

        private static string LanguageParams()
        {
            return Common.GetDeviceLanguage();
        }
    }
}
