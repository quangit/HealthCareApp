using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HealthCare.Models;

namespace HealthCare.WebServices.Interfaces
{
    public interface IHospitalWS
    {
        Task<ObservableCollection<HospitalModel>> GetHospitalList(string search, int start = 0, int length = 10,
            bool searchExactly = true);//(int start = 0, int length = 10);
        Task<HospitalModel> GetHospitalById(string hospitalId);
    }
}