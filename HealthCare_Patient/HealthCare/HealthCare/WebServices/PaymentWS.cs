using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using HealthCare.Helpers;
using HealthCare.ModelApis;
using HealthCare.Models;
using HealthCare.WebServices.Interfaces;
using Newtonsoft.Json;

namespace HealthCare.WebServices
{
    public class PaymentWS : BaseFakeWebService, IPaymentWS
    {
        #region IPaymentWS implementation

        public async Task<ObservableCollection<CreditCardModel>> GetCardList()
        {
            var url = AppConstant.RootUrl + AppConstant.GetCardListUrl;
            var data = await SendHttpRequest(HttpMethod.Get, url);
            return JsonUtils.ParseData<ObservableCollection<CreditCardModel>>(data, AppConstant.KeyCards);
        }
        public async Task<ObservableCollection<CreditCardModel>> GetCardListv2()
        {
            var url = AppConstant.RootUrl + AppConstant.GetCardListUrlv2;
            var data = await SendHttpRequest(HttpMethod.Get, url);
            return JsonUtils.ParseData<ObservableCollection<CreditCardModel>>(data, AppConstant.KeyCards);
        }
        public async Task<AmountWithPromotionModel> GetAmountWithPromotion(string checkupId)
        {
            var url = AppConstant.RootUrl + string.Format(AppConstant.GetAmountWithPromotion, checkupId);
            var data = await SendHttpRequest(HttpMethod.Get, url);
            var temp = JsonUtils.ParseData<AmountWithPromotionModel>(data);
            return temp;
        }

        public async Task<bool> GetPaymentstatus(string checkupId, AmountWithPromotionModel amount)
        {
            var url = AppConstant.RootUrl + string.Format(AppConstant.Paymentstatus, checkupId, amount.TotalAmount);
            var data = await SendHttpRequest(HttpMethod.Get, url);
            return true;
        }
        
        public async Task<CreditCardModel> AddCard(CreditCardModel card)
        {
            var url = AppConstant.RootUrl + AppConstant.AddCardUrl;

            var bodyParams = JsonConvert.SerializeObject(card, Formatting.Indented,
                new JsonSerializerSettings {ContractResolver = new IgnorePropertyResolver("status", "pinOrOTP")});
            await SendHttpRequest(HttpMethod.Post, url, bodyParam: bodyParams);
            return new CreditCardModel();
        }

        public async Task<ApiPaymentPromotionModel> DoPayment(string checkupId, string cardId, string paymentPassword, string cardToken)
        {
            var url = AppConstant.RootUrl + AppConstant.DoPaymentUrl;
           var dataBlock = new {medicalCheckupId = checkupId, cardId, paymentPassword, cardToken};
            //var dataBlock = new { medicalCheckupId = checkupId, cardId, paymentPassword};

            var bodyParams = JsonConvert.SerializeObject(dataBlock);
            var data = await SendHttpRequest(HttpMethod.Post, url, bodyParam: bodyParams);
            return JsonUtils.ParseData<ApiPaymentPromotionModel>(data);
        }

        public async Task SetPaymentPassword(string oldPaymentPassword, string newPaymentPassword)
        {
            var url = AppConstant.RootUrl + AppConstant.SetPaymentPasswordUrl;
            var dataBlock = new {oldPaymentPassword, newPaymentPassword};
            var bodyParams = JsonConvert.SerializeObject(dataBlock);
            await SendHttpRequest(HttpMethod.Post, url, bodyParam: bodyParams);
        }

        public async Task VerifyPinOtp(string cardId, string pinOrOTP)
        {
            var url = AppConstant.RootUrl + AppConstant.VerifyPinOtp;
            var data = JsonConvert.SerializeObject(new {cardId, pinOrOTP});
            await SendHttpRequest(HttpMethod.Post, url, bodyParam: data);
        }

        #endregion
    }
}