using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Enums;
using HealthCare.Models;

namespace HealthCare.WebServices.Interfaces
{
    public interface IMoreSupportWS
    {
        Task<ObservableCollection<SupportQuestionModel>> GetQuestionList(string search, int start = 0, int length = 10, bool searchExactly = true);
        Task<ObservableCollection<SupportResponsesModel>> GetAnswerList(string search, int start = 0, int length = 10);
        Task ChangeStatusQuestion(string questionId, bool status);

        //Task<CreditCardModel> AddCard(CreditCardModel card);
        //Task<CheckupModel> DoPayment(string checkupId, string cardId, string paymentPassword);
    }
}
