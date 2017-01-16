using HealthCare.WebServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Enums;
using HealthCare.Helpers;
using HealthCare.ModelApis;
using HealthCare.Models;

namespace HealthCare.WebServices
{
    public class MoreSupportWS : BaseFakeWebService, IMoreSupportWS
    {
        public async Task<ObservableCollection<SupportQuestionModel>> GetQuestionList(string search, int start = 0, int length = 10, bool searchExactly = true)
        {
            var url = AppConstant.RootUrl + AppConstant.GetSupportQuestionListUrl
                .Replace("{0}", start + "")
                .Replace("{1}", length + "")
                .Replace("{2}", search)
                .Replace("{3}", searchExactly.ToString());

            var data = await SendHttpRequest(HttpMethod.Get, url);
            return JsonUtils.ParseData<ObservableCollection<SupportQuestionModel>>(data, AppConstant.KeySupportQuestionListType);
        }
        public async Task<ObservableCollection<SupportResponsesModel>> GetAnswerList(string search, int start = 0, int length = 10)
        {
            var url = AppConstant.RootUrl + string.Format(AppConstant.GetSupportAnswerListUrl, search, start, length);
            var data = await SendHttpRequest(HttpMethod.Get, url);
            return JsonUtils.ParseData<ObservableCollection<SupportResponsesModel>>(data, "responses");

        }

        public async Task ChangeStatusQuestion(string questionId, bool status)
        {
            var url = AppConstant.RootUrl + string.Format(AppConstant.SetQuestionStatusUrl, questionId, status.ToString());
            await SendHttpRequest(HttpMethod.Get, url);
        }
    }
}
