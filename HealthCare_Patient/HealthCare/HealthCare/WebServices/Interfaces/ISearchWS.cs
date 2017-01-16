using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HealthCare.ModelApis;
using HealthCare.Models;

namespace HealthCare.WebServices.Interfaces
{
    public interface ISearchWS
    {
        Task<ObservableCollection<DoctorApiModel>> GetDoctorListBySearch(Suggestion keyword, string cityId,
            string districtId, double lat, double lng, int start, int lenght);

        Task<ObservableCollection<Suggestion>> GetSuggestions(Suggestion keyword, 
            System.Threading.CancellationToken cancellationToken);
    }
}