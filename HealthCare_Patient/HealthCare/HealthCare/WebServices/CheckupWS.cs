using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using HealthCare.Helpers;
using HealthCare.ModelApis;
using HealthCare.Models;
using HealthCare.Objects;
using HealthCare.WebServices.Interfaces;
using Newtonsoft.Json;

namespace HealthCare.WebServices
{
    public class CheckupWS : BaseFakeWebService, ICheckupWS
    {
        #region ICheckupWS implementation

        public async Task<ObservableCollection<ApiCheckupModel>> GetCheckupList()
        {
            var url = AppConstant.RootUrl + AppConstant.GetCheckupListUrl;
            var data = await SendHttpRequest(HttpMethod.Get, url, AppConstant.GetCheckupListFn);
            return JsonUtils.ParseData<ObservableCollection<ApiCheckupModel>>(data, AppConstant.KeyCheckups);
        }

        public async Task<CheckupModel> AddCheckup(ProxyAddCheckupModel checkupData)
        {
            var url = AppConstant.RootUrl + AppConstant.AddCheckupUrl;
            var bodyParamsRaw = new
            {
                scheduleId = checkupData.Schedule.Id,
                userId = checkupData.UserId,
                symptom = checkupData.Symptom,
                dataUser = checkupData.DataUser,
                healthInsurance = checkupData.IsHealthInsurance
            };
            var bodyParams = JsonConvert.SerializeObject(bodyParamsRaw);
            var data = await SendHttpRequest(HttpMethod.Post, url, bodyParam: bodyParams);
            return JsonUtils.ParseData<CheckupModel>(data, AppConstant.KeyCheckup);
        }

        public async Task DeleteCheckup(string checkupId)
        {
            var url = AppConstant.RootUrl + string.Format(AppConstant.DeleteCheckupUrl, checkupId, "-2");
            var data = await SendHttpRequest(HttpMethod.Get, url);
        }

        public async Task CancelCheckup(string checkupId)
        {
            var url = AppConstant.RootUrl + string.Format(AppConstant.DeleteCheckupUrl, checkupId, "-1");
            var data = await SendHttpRequest(HttpMethod.Get, url);
        }

        public async Task EditCheckup(string checkupId, string scheduleId)
        {
            var url = AppConstant.RootUrl + AppConstant.EditCheckupUrl;
            var data = new { id = checkupId, scheduleId };
            await SendHttpRequest(HttpMethod.Post, url, bodyParam: JsonConvert.SerializeObject(data));
        }

        public async Task<ObservableCollection<ApiCheckupModel>> GetHistoricalCheckups(string userId, int start = 0, int length = 10)
        {
            var url = AppConstant.RootUrl + string.Format(AppConstant.GetHistoricalCheckupsUrl, userId, start, length);
            var data = await SendHttpRequest(HttpMethod.Get, url);
            return JsonUtils.ParseData<ObservableCollection<ApiCheckupModel>>(data, AppConstant.KeyCheckups);
        }

        public async Task RatingCheckup(string checkupId, int rate)
        {
            var url = AppConstant.RootUrl + string.Format(AppConstant.RatingCheckupUrl, checkupId, rate);
            await SendHttpRequest(HttpMethod.Get, url);
        }

        #endregion
    }
}