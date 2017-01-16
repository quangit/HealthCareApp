using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HealthCare.ModelApis;
using HealthCare.Models;

namespace HealthCare.WebServices.Interfaces
{
    public interface IPaymentWS
    {
        Task<ObservableCollection<CreditCardModel>> GetCardList();
        Task<ObservableCollection<CreditCardModel>> GetCardListv2();

        Task<AmountWithPromotionModel> GetAmountWithPromotion(string checkupId);
        Task<bool> GetPaymentstatus(string checkupId, AmountWithPromotionModel amount);

        Task<CreditCardModel> AddCard(CreditCardModel card);
        Task<ApiPaymentPromotionModel> DoPayment(string checkupId, string cardId, string paymentPassword, string cardToken);
        Task SetPaymentPassword(string oldPaymentPassword, string newPaymentPassword);
        Task VerifyPinOtp(string cardId, string pinOrOTP);
    }
}