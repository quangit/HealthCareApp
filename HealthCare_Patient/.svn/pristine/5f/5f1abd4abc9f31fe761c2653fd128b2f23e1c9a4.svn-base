using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HealthCare.Models;

namespace HealthCare.WebServices.Interfaces
{
    public interface ICardWS
    {
        Task<ObservableCollection<CreditCardModel>> GetCardList();
        Task<CreditCardModel> AddCard(CreditCardModel card);
        Task<CheckupModel> DoPayment(string checkupId, string cardId, string paymentPassword);
    }
}