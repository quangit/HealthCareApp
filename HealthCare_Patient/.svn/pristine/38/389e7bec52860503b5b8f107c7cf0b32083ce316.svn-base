using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using HealthCare.Helpers;
using HealthCare.ModelApis;
using HealthCare.WebServices.Interfaces;

namespace HealthCare.WebServices
{
    public class DoctorWS : BaseFakeWebService, IDoctorWS
    {
        #region IDoctorWS implementation

        public async Task<ObservableCollection<DoctorApiModel>> GetDoctorByHospitalId(string hospitalId)
        {
            var url = AppConstant.RootUrl + string.Format(AppConstant.GetDoctorsByHospitalUrl, hospitalId);
            var data = await SendHttpRequest(HttpMethod.Get, url);
            return JsonUtils.ParseData<ObservableCollection<DoctorApiModel>>(data, AppConstant.KeyDoctors);
        }


        public async Task<ObservableCollection<ScheduleApiModel>> GetScheduleOfDoctor(string doctorId, long startTime,
            long endTime, string hospitalId)
        {
            var urlRaw = AppConstant.GetSchedulesOfDoctorUrl;
            var url = "";
            if (string.IsNullOrWhiteSpace(hospitalId))
            {
                urlRaw = urlRaw.Replace("&hospitalId={3}", "");
                url = AppConstant.RootUrl + string.Format(urlRaw, doctorId, startTime, endTime);
            }
            else
            {
                url = AppConstant.RootUrl + string.Format(urlRaw, doctorId, startTime, endTime, hospitalId);
            }

            var data = await SendHttpRequest(HttpMethod.Get, url);
            return JsonUtils.ParseData<ObservableCollection<ScheduleApiModel>>(data, AppConstant.KeySchedules);
        }

        #endregion
    }
}