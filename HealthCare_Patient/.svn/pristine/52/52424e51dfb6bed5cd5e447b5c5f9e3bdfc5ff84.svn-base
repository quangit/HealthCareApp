using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using HealthCare.Enums;
using HealthCare.Helpers;
using HealthCare.Models;
using HealthCare.WebServices.Interfaces;

namespace HealthCare.WebServices
{
    public class PromotionWS : BaseWebService, IPromotionWS
    {
        public async Task<ObservableCollection<PromotionModel>> GetPromotionList(int start = 0, int length = 10,
            SortField sortField = SortField.PromotionStartDate,
            SortType sortType = SortType.Desc)
        {
            var url = AppConstant.RootUrl + AppConstant.GetPromotionsUrl
                .Replace("{0}", start + "")
                .Replace("{1}", length + "")
                .Replace("{2}", Description.ToString(sortField))
                .Replace("{3}", Description.ToString(sortType));
            var data = await SendHttpRequest(HttpMethod.Get, url);
            return JsonUtils.ParseData<ObservableCollection<PromotionModel>>(data, AppConstant.KeyPromotions);
        }
    }
}