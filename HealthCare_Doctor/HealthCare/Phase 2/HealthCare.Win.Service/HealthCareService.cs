using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

using HealthCare.Core.Models;
using HealthCare.Core.Models.Enums;
using HealthCare.Core.Resources;
using HealthCare.Core.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
#if MVVMCROSS
using Mvx = Cirrious.CrossCore.Mvx;
#else
using Mvx = HealthCare.Core.IoC;
#endif
namespace HealthCare.Core.Services
{
    public class HealthCareService
    {
        private static HealthCareService _current;
        IFileService _fileService;
        public HealthCareService(IHttpService httpService, IReporterService reporterService, IFileService file)
        {
            _httpService = httpService;
            _reporterService = reporterService;
            _fileService = file;
        }

        public static HealthCareService Current
        {
            get
            {
                _current = _current ??
                           new HealthCareService(Mvx.Resolve<IHttpService>(), Mvx.Resolve<IReporterService>(), Mvx.Resolve<IFileService>());
                return _current;
            }
        }

        public async Task<LoginResult> LoginAsync(string id, string password)
        {
			//password = System.Uri.EscapeDataString(password);
			var ret =await PostApiCall<LoginResult>("doctor/login", new { id, password});
			if (ret != null) {
				if (ret.UserType == UserType.TypeHospitalUser)
					Data.User = ret;
				else
					_reporterService.ReportMessage (AppResources.Warning, AppResources.SignIn_NoAccess);
			}
			return Data.User;
        }

        public Task<SignUpResult> SignUpAsync(SignUpInfo signUpInfo)
        {
			//signUpInfo.Password = System.Uri.EscapeDataString(signUpInfo.Password);
			//signUpInfo.RePass = System.Uri.EscapeDataString(signUpInfo.RePass);
            return PostApiCall<SignUpResult>("doctor/signUpv2", signUpInfo.Data);
        }

        public async Task<bool> UpdateProfile(SignUpInfo signUpInfo)
        {
            var r = await PostApiCall<IndexObject>("doctor/" + Data.User.UserId + "/profile/save", signUpInfo.UpdateProfileData);
            return r != null && !string.IsNullOrEmpty(r.Id);
        }

        public async Task<List<City>> GetCities(bool isDistricts = true, bool isDisplayed = true)
        {
            var r =
                await GetApiCall<List<City>>(string.Format("public/cities?isDistricts={0}", isDistricts), "cities", true);

            return r;
        }

        public async Task<List<CheckupType>> GetCheckuptypes(int start = 0, int length = 100)
        {
            var r = await GetApiCall<List<CheckupType>>(string.Format("public/checkuptypes?start={0}&length={1}", start,
                length), "checkupTypes", true);

            return r;
        }

        public Task<List<District>> GetDistricts(string cityId)
        {
            return GetApiCall<List<District>>(string.Format("districts?cityId={0}", cityId), "districts");
        }

        public Task<List<Hospital>> GetHospitals(int start = 0, int length = 100, bool getAdminUsers = false)
        {
            return
                GetApiCall<List<Hospital>>(string.Format("hospitals?start={0}&length={1}&getAdminUsers={2}", start,
                    length, getAdminUsers), "hospitals");
        }
        public Task<List<Hospital>> GetDoctorHospitals()
        {
            return
                GetApiCall<List<Hospital>>(string.Format("doctor/refreshData?ignore=" + DateTime.Now.Ticks), "hospitals");
        }
        public Task<List<Schedule>> GetShedules(string hospitalId = "")
        {
            if (Data.User == null)
                return Task.FromResult(new List<Schedule>());
            return
                GetApiCall<List<Schedule>>(
                    string.Format(
                        "doctor/{0}/schedules?hospitalId={1}&ignore={2}",
                        Data.User.UserId,
                        hospitalId, DateTime.Now.Ticks), null, "schedules");
        }

        public Task<Hospital> GetHospital(string hospitalId, bool getAdminUsers = false)
        {
            return GetApiCall<Hospital>(string.Format("hospital/{0}/get?getAdminUsers={1}", hospitalId, getAdminUsers),
                null, "hospital");
        }
//
//		public Task<List<ConsultRequest>> GetRequests()
//        {
//			return GetApiCall<List<ConsultRequest>>(string.Format("doctor/supportRequests"), "requests");
//        }

		public Task<ConsultRequestData> GetRequests(int start = 0)
		{
			var resp = GetApiCall<ConsultRequestData>(string.Format("doctor/supportRequests?start={0}&length={1}", start,Data.LoadLength));
			return resp;
		}

        public Task<List<ConsultResponse>> GetRequestDetail(string id)
        {
            return GetApiCall<List<ConsultResponse>>("doctor/supportRequest/" + id + "/responses", "responses");
        }

        public Task<Doctor> GetProfile()
        {
            return GetApiCall<Doctor>(string.Format("user/{0}/get?ignore=" + DateTime.Now.Ticks, Data.User.UserId)
                , null, "user");
        }

        public async Task<bool> SetSchedule(SetScheduleObject obj)
        {
            var result = await PostApiCall<object>("doctor/schedule/save", obj.ToString());
            return result != null;
        }

        public async Task<TopicData> GetTopic(int start = 0)
        {
            return await
				GetApiCall<TopicData>(string.Format("weektopics?start={0}&length={1}", start,Data.LoadLength));
        }


        public async Task<List<TopicFiles>> GetTopicFiles(string weektopicID) {
            try
            {
                
                Debug.WriteLine("GET: " + "http://system.bacsi24x7.vn/Api/GetFileById/");
                string uri = "http://system.bacsi24x7.vn/Api/GetFileById/";
                var url = uri + weektopicID;
                var resp = (await _httpService.GetAsync(url));

                Debug.WriteLine("GET File Url: " + url);
                Debug.WriteLine("GET RESP CME: " + resp);
                return JsonConvert.DeserializeObject<List<TopicFiles>>(resp);

            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task<bool> DeleteSchedule(string id)
        {
            var r = await GetApiCall<object>(
                 string.Format("doctor/schedule/{0}/remove", id));
            return r != null;
        }

		public Task<CheckupData> GetCheckups(string hopitalId, int start = 0)
        {

			var hospitalParam = (string.IsNullOrEmpty (hopitalId)) ? "" : "hospitalId=" + hopitalId+"&";
			return GetApiCall<CheckupData>(
				string.Format("doctor/checkups?{0}start={1}&length={2}", hospitalParam, start,Data.LoadLength));

        }
        public async Task<bool> ResetPass(string email)
        {
            var p = await GetApiCall<IndexObject>(string.Format("public/doctor/requestPassword?id={0}", email));
            return p != null && !string.IsNullOrEmpty(p.Id);
        }

        public async Task<bool> SetSchedule(Schedule obj)
        {
            var result = await PostApiCall<object>("doctor/schedule/save", obj);
            return result != null;
        }
        public async Task<bool> ChangePassword(string oldPassword, string newPassword)
        {

		 	//oldPassword = System.Uri.EscapeDataString(oldPassword);
		 	//newPassword = System.Uri.EscapeDataString(newPassword);
            var result = await PostApiCall<object>("changePassword", new { oldPassword, newPassword });
            return result != null;
        }
        public async Task<bool> ReplySupportRequest(string requestId, string content)
        {
            var p =
                await
                    PostApiCall<IndexObject>(string.Format("doctor/supportRequest/{0}/reply", requestId),
                        new Dictionary<string, string>() { { "content", content } }, "application/x-www-form-urlencoded");
            return p != null && !string.IsNullOrEmpty(p.Id);
        }
        public async Task<bool> ForwardSupportRequest(string requestId, string content, string email)
        {
            var p =
                await
                PostApiCall<object>(string.Format("doctor/supportRequest/{0}/send", requestId),
                        new Dictionary<string, string>() { { "content", content }, { "email", email } },
                        "application/x-www-form-urlencoded");

            return p != null;//&& !string.IsNullOrEmpty(p.Id)
        }
        public async Task<bool> NotificationRegister()
        {
            if (string.IsNullOrEmpty(Data.PushChannelUri) || Data.Setting == null)
            {
                Data.PushSync = false;
                return false;
            }

            PushConfig p = new PushConfig();
            p.Active = (Data.Setting != null ? Data.Setting.PushConsent : false).ToString();
            if (Data.Setting != null && !string.IsNullOrEmpty(Data.Setting.PushRegistrationId))
            {
                p.RegistrationId = Data.Setting.PushRegistrationId;
            }
            p.Channel = Data.PushChannelUri;
            p.Platform = Data.PushPlatform;
            p.UserType = (int)Data.User.UserType;
            var r =
                await
                    PostApiCall<IndexObject>(string.Format("notification/register"), p);
            if (r != null && !string.IsNullOrEmpty(r.Id))
            {
                if (Data.Setting == null)
                    Data.Setting = new Setting();
                Data.Setting.PushRegistrationId = r.Id;
                await _fileService.SaveSetting();
            }
            Data.PushSync = true;
            return r != null && !string.IsNullOrEmpty(r.Id);
        }


#region Fields

        private const string OLD_HOST = "http://clas-healthcare.cloudapp.net:7181/clas-healthcare/";
        private const string NEW_HOST = "https://healthcare.clas.mobi:8445/clas-healthcare/";
        private const string V2_HOST = "https://healthcare.clas.mobi:8445/clas-healthcare-v2/";
        private const string HOST = V2_HOST;

        

        private readonly IHttpService _httpService;
        private readonly IReporterService _reporterService;

#endregion

#region API BASE FUNCTIONS

        private Task<T> GetApiCall<T>(string api, string refName = null, bool noProgress = false)
        {
            return GetApiCall<T>(api, null, refName, noProgress);
        }

        private async Task<T> GetApiCall<T>(string api, Dictionary<string, string> parameters, string refName = null,
            bool noProgress = false)
        {
            if (!noProgress)
                _reporterService.ShowProgress();
            try
            {
                TrySetHeader();
                Debug.WriteLine("GET: " + HOST + api);
                var source = parameters == null
                    ? (await _httpService.GetAsync(HOST + api))
                    : (await _httpService.GetAsync(HOST + api, parameters));
                Debug.WriteLine("RESPONSE: " + (source.Length > 150000 ? source.Substring(0, 150000) + "..." : source));
                ApiRootObject<T> result;
                if (!string.IsNullOrEmpty(refName))
                {
                    result = ParseJson<T>(source, refName);
                }
                else
                {
                    result = ParseJson<T>(source);
                }
                if (result.Successful)
                {
                    return result.Data;
                }
                else
                {

                    if (result.ErrorCode == ErrorCode.FailureSessionInvalid)
                    {
                        await LoginAsync(Data.UserName, Data.Password);
                        return await GetApiCall<T>(api, parameters, refName, noProgress);
                    }
                    if (!noProgress)
                        _reporterService.StopProgress();

                    var message = AppResources.ResourceManager.GetString(result.ErrorCode.ToString());
                    message = string.IsNullOrEmpty(message) ? result.ErrorDesc : message;
                    _reporterService.ReportError(message, ErrorType.Message);
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                _reporterService.ReportError("Exception: " + ex.Message, ErrorType.Message);
            }
            finally
            {
                if (!noProgress)
                    _reporterService.StopProgress();
            }
            return default(T);
        }

        private async Task<T> PostApiCall<T>(string api, Dictionary<string, string> data, string type = "application/json")
        {
            _reporterService.ShowProgress();
            try
            {
                TrySetHeader();
                Debug.WriteLine("POST: " + HOST + api);
                Debug.WriteLine("POST Param: " + JsonConvert.SerializeObject(data));
                string source = "";
                if (type == "application/json")
                    source =
                    await _httpService.PostAsync(HOST + api, JsonConvert.SerializeObject(data), "application/json");
                else
                {
                    FormUrlEncodedContent r = new FormUrlEncodedContent(data);
                    source =
                        await _httpService.PostAsync(HOST + api, await r.ReadAsStringAsync(), "application/x-www-form-urlencoded");
                }
                Debug.WriteLine("RESPONSE: " + source);

                var result = ParseJson<T>(source);
                if (result.Successful)
                {
                    return result.Data;
                }
                else
                {
                    if (result.ErrorCode == ErrorCode.FailureSessionInvalid)
                    {
                        await LoginAsync(Data.UserName, Data.Password);
                        return await PostApiCall<T>(api, data, type);
                    }
                    var message = AppResources.ResourceManager.GetString(result.ErrorCode.ToString());
                    message = string.IsNullOrEmpty(message) ? result.ErrorDesc : message;
                    _reporterService.ReportError(message, ErrorType.Message);
                }
            }
            catch (Exception ex)
            {
                _reporterService.ReportError("Exception: " + ex.Message, ErrorType.Message);
            }
            finally
            {
                _reporterService.StopProgress();
            }
            return default(T);
        }

        private async Task<T> PostApiCall<T>(string api, Dictionary<string, object> data)
        {
            _reporterService.ShowProgress();
            try
            {
                TrySetHeader();
                Debug.WriteLine("POST: " + HOST + api);
                Debug.WriteLine("POST Param: " + JsonConvert.SerializeObject(data));
                var source = await _httpService.PostFormAync(HOST + api, data);
                Debug.WriteLine("RESPONSE: " + source);
                var result = ParseJson<T>(source);
                if (result.Successful)
                {
                    return result.Data;
                }
                else
                {
                    if (result.ErrorCode == ErrorCode.FailureSessionInvalid)
                    {
                        await LoginAsync(Data.UserName, Data.Password);
                        return await PostApiCall<T>(api, data);
                    }
                    var message = AppResources.ResourceManager.GetString(result.ErrorCode.ToString());
                    message = string.IsNullOrEmpty(message) ? result.ErrorDesc : message;
                    _reporterService.ReportError(message, ErrorType.Message);
                }
            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                _reporterService.ReportError("Exception: " + ex.Message, ErrorType.Message);
            }
            finally
            {
                _reporterService.StopProgress();
            }
            return default(T);
        }

        private void TrySetHeader()
        {
            if (Data.User != null && !string.IsNullOrEmpty(Data.User.UserId))
            {
                _httpService.SetHeader("userId", Data.User.UserId);
                _httpService.SetHeader("sessionId", Data.User.SessionId);
            }
            var lang = ((CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.Equals("vi")) ? "vn" : "en");
            _httpService.SetHeader("lang", lang);
            _httpService.SetHeader("accept-language", lang);
        }

        private async Task<T> PostApiCall<T>(string api, object data)
        {
            _reporterService.ShowProgress();
            try
            {
                TrySetHeader();
                var postData = (data is string) ? data.ToString() : JsonConvert.SerializeObject(data);
                Debug.WriteLine("POST: " + HOST + api);
                Debug.WriteLine("DATA: " + postData);
                var source =
                    await
                        _httpService.PostAsync(HOST + api, postData
                            , "application/json");
                Debug.WriteLine("RESPONSE: " + source);

                var result = ParseJson<T>(source);
                if (result.Successful)
                {
                    return result.Data;
                }
                else
                {
                    var message = AppResources.ResourceManager.GetString(result.ErrorCode.ToString());
                    message = string.IsNullOrEmpty(message) ? result.ErrorDesc : message;
                    _reporterService.ReportError(message, ErrorType.Message);
                }
            }
            catch (Exception ex)
            {
                _reporterService.ReportError("Exception: " + ex.Message, ErrorType.Message);
            }
            finally
            {
                _reporterService.StopProgress();
            }
            return default(T);
        }

        private ApiRootObject<T> ParseJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<ApiRootObject<T>>(json);
        }

        private ApiRootObject<T> ParseJson<T>(string json, string propertyName)
        {
            var r = JsonConvert.DeserializeObject<ApiRootObject<JToken>>(json);
            var result = new ApiRootObject<T>();
            result.Successful = r.Successful;
            result.ErrorCode = r.ErrorCode;
            result.ErrorDesc = r.ErrorDesc; //.ToString();
            if (r.Data != null)
                result.Data = r.Data[propertyName].ToObject<T>();

            return result;
        }

#endregion


    }
}