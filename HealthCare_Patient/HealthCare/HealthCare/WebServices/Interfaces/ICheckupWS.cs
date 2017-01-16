using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HealthCare.ModelApis;
using HealthCare.Models;
using HealthCare.Objects;

namespace HealthCare.WebServices.Interfaces
{
    public interface ICheckupWS
    {
        Task<ObservableCollection<ApiCheckupModel>> GetCheckupList();
        Task<CheckupModel> AddCheckup(ProxyAddCheckupModel checkupData);
        Task DeleteCheckup(string checkupId);
        Task EditCheckup(string checkupId, string scheduleId);
        Task<ObservableCollection<ApiCheckupModel>> GetHistoricalCheckups(string userId, int start = 0, int length = 10);
        Task RatingCheckup(string checkupId, int rate);
        Task CancelCheckup(string checkupId);
    }
}