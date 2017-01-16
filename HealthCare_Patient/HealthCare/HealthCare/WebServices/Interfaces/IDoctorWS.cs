using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HealthCare.ModelApis;

namespace HealthCare.WebServices.Interfaces
{
    public interface IDoctorWS
    {
        Task<ObservableCollection<DoctorApiModel>> GetDoctorByHospitalId(string hospitalId);

        Task<ObservableCollection<ScheduleApiModel>> GetScheduleOfDoctor(string doctorId, long startTime, long endTime,
            string hospitalId);
    }
}