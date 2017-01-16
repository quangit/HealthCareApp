using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HealthCare.Enums;
using HealthCare.Models;

namespace HealthCare.WebServices.Interfaces
{
    public interface IPromotionWS
    {
        Task<ObservableCollection<PromotionModel>> GetPromotionList(int start = 0, int length = 10,
            SortField sortField = SortField.PromotionStartDate,
            SortType sortType = SortType.Desc);
    }
}