using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using HealthCare.Helpers;
using HealthCare.ModelApis;
using HealthCare.Models;
using HealthCare.WebServices.Interfaces;


namespace HealthCare.WebServices
{
    public class SearchWS : BaseFakeWebService, ISearchWS
    {
        #region ISearchWS implementation

        public async Task<ObservableCollection<DoctorApiModel>> GetDoctorListBySearch(Suggestion keyword,
            string cityId, string districtId, double lat, double lng, int start, int lenght)
        {
            var url = AppConstant.RootUrl + AppConstant.GetDoctorBySearchUrl;
            url = string.Format(url, keyword.Id, (int)keyword.Type,
                string.IsNullOrWhiteSpace(cityId) ? "" : cityId,
                string.IsNullOrWhiteSpace(districtId) || districtId.Equals(AppConstant.IdAllItems) ? "" : districtId,
                lat == 0 ? "" : lat.ToString().Replace(",", "."),
                lng == 0 ? "" : lng.ToString().Replace(",", "."),
                start, lenght);

            var data = await SendHttpRequest(HttpMethod.Get, url);
            
            return JsonUtils.ParseData<ObservableCollection<DoctorApiModel>>(data, AppConstant.KeyDoctors);
        }

        public async Task<ObservableCollection<Suggestion>> GetSuggestions(Suggestion keyword, 
            System.Threading.CancellationToken cancellationToken)
        {
            var url = AppConstant.RootUrl + string.Format(AppConstant.GetSuggestionsUrl, keyword.Name);
            url = string.Format(url, keyword.Name);

            await Task.Delay(AppConstant.DelaySearchSuggestions);
            cancellationToken.ThrowIfCancellationRequested();
            var data = await SendHttpRequest(HttpMethod.Get, url);
            cancellationToken.ThrowIfCancellationRequested();
            System.Diagnostics.Debug.WriteLine("Got suggestions with keyword: " + keyword.Name);
            return JsonUtils.ParseData<ObservableCollection<Suggestion>>(data, AppConstant.KeySuggestions);
        }
        #endregion
    }
}