using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using HealthCare.Helpers;
using HealthCare.Models;
using HealthCare.WebServices.Interfaces;

namespace HealthCare.WebServices
{
    public class HospitalWS : BaseFakeWebService, IHospitalWS
    {
        #region IHospitalWS implementation

        public async Task<ObservableCollection<HospitalModel>> GetHospitalList(string search, int start = 0, int length = 10, 
            bool searchExactly = true)
        {
            var url = AppConstant.RootUrl + AppConstant.SearchHospitalUrl
                  .Replace("{0}", start + "")
                  .Replace("{1}", length + "")
                  .Replace("{2}", search)
                  .Replace("{3}", searchExactly.ToString());
            //var url = AppConstant.RootUrl + string.Format(AppConstant.SearchHospitalUrl, start, length);
            var data = await SendHttpRequest(HttpMethod.Get, url);
            return JsonUtils.ParseData<ObservableCollection<HospitalModel>>(data, AppConstant.KeyHospitals);
        }

        public async Task<HospitalModel> GetHospitalById(string hospitalId)
        {
            var url = AppConstant.RootUrl + string.Format(AppConstant.GetHospitalByIdUrl, hospitalId);
            var data =
                await SendHttpRequest(HttpMethod.Get, url);
            return JsonUtils.ParseData<HospitalModel>(data, AppConstant.KeyHospital);
        }

        #endregion
    }
}