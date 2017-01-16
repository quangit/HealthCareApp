using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using HealthCare.Enums;
using HealthCare.Helpers;
using HealthCare.Models;
using HealthCare.Objects;
using HealthCare.WebServices.Interfaces;
using Newtonsoft.Json;

namespace HealthCare.WebServices
{
    public class CommonWS : BaseFakeWebService, ICommonWS
    {
        #region ICommonWS implementation

        public async Task<SystemConfig> GetSystemConfig()
        {
            var url = AppConstant.RootUrl + AppConstant.SystemConfigUrl;
            var data = await SendHttpRequest(HttpMethod.Get, url);
            return JsonUtils.ParseData<SystemConfig>(data, AppConstant.KeyConfig);
        }

        public async Task<ObservableCollection<CityModel>> GetCityList()
        {
            var url = AppConstant.RootUrl + string.Format(AppConstant.GetCityListUrl, "true");
            var data =
                await SendHttpRequest(HttpMethod.Get, url);
            return JsonUtils.ParseData<ObservableCollection<CityModel>>(data, AppConstant.KeyCities);
        }
        public async Task SendAdditionalSupport(string symptom, byte[] arrayImg, bool shared)
        {
            Dictionary<string, string> formData = new Dictionary<string, string>();
            formData.Add("symptom", symptom);
            formData.Add("shared", (!shared).ToString());
            var data = await SendMultipartRequest(AppConstant.AddQuestion, "file", arrayImg, formData);

        }
        public async Task SendAdditionalSupport(string firstName, string lastName, string age, Gender gender,
            string email,
            string doctorName,
            string hospitalName, string symptom)
        {
            var url = AppConstant.RootUrl + AppConstant.SendAdditionalSupportUrl;
            var dataObj = new
            {
                firstName,
                lastName,
                age,
                gender = gender == Gender.Male ? "M" : "F",
                email,
                doctorName,
                hospitalName,
                symptom
            };
            var body = JsonConvert.SerializeObject(dataObj);

            await SendHttpRequest(HttpMethod.Post, url, bodyParam: body);
        }

        public async Task<string> RegisterDevice(string channel, string platform, string registrationId)
        {
            var url = AppConstant.RootUrl + AppConstant.RegisterDeviceUrl;
            var dataObj = new
            {
                channel,
                platform,
                registrationId,
                userType = 1,
                active = Settings.CanReceivePushNotify ? "true" : "false"
            };
            var body = JsonConvert.SerializeObject(dataObj);
            var data = await SendHttpRequest(HttpMethod.Post, url, bodyParam: body);
            //TODO: if active == false == OK => clear registerId in Settings
            return JsonUtils.ParseData<string>(data, "id");
        }

        public async Task SendOtp(string phoneNumber, string otp)
        {
            var url = AppConstant.RootUrl + string.Format(AppConstant.SendOtpUrl, phoneNumber, otp);

            await SendHttpRequest(HttpMethod.Get, url);
        }

        #endregion
    }
}