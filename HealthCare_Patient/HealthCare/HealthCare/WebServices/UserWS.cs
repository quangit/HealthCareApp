using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HealthCare.Enums;
using HealthCare.Helpers;
using HealthCare.Models;
using HealthCare.WebServices.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HealthCare.WebServices
{
    public class UserWS : BaseFakeWebService, IUserWS
    {
        #region IUserWS implementation

        public Task<JObject> Login(string userName, string password, string timeZone)
        {
            var url = AppConstant.RootUrl + AppConstant.LoginUrl;
            var data = new { id = userName?.Trim(), password, timezone = timeZone };
            return SendHttpRequest(HttpMethod.Post, url, bodyParam: JsonConvert.SerializeObject(data));
        }

        public Task<JObject> LoginWithFacebook(string facebookId)
        {
            var url = AppConstant.RootUrl + AppConstant.LoginFacebookUrl;
            var data = new { facebookId, fbAccessToken = Settings.FacebookAccessToken };
            return SendHttpRequest(HttpMethod.Post, url, bodyParam: JsonConvert.SerializeObject(data));
        }

        public Task<JObject> RefreshData()
        {
            var url = AppConstant.RootUrl + AppConstant.RefreshDataUrl;
            return SendHttpRequest(HttpMethod.Get, url);
        }

        public async Task ResetPassword(string email)
        {
            var requestUrl = AppConstant.RootUrl + string.Format(AppConstant.RequestResetPasswordUrl,
                email, Common.GetDeviceLanguage());
            //            var hashObj = 
            await SendHttpRequest(HttpMethod.Get, requestUrl);
            //            var hashStr = JsonUtils.ParseData<string>(hashObj, AppConstant.KeyId);
            //
            //            string resetUrl = AppConstant.RootUrl + string.Format(AppConstant.ResetPasswordUrl, hashStr);
            //            await SendHttpRequest(HttpMethod.Get, resetUrl);
        }

        public async Task<int> CheckExistEmail(string email)
        {
                var requestUrl = AppConstant.RootUrl + string.Format(AppConstant.CheckExistedEmail,
                email);
                var result = await SendHttpRequest(HttpMethod.Get, requestUrl);
                return 3;
        }

        public async Task ChangeUserPassword(string userId, string oldPassword, string newPassword)
        {
            var url = AppConstant.RootUrl + AppConstant.ChangeUserPasswordUrl;
            var dataBlock = new { oldPassword, newPassword };
            var bodyParams = JsonConvert.SerializeObject(dataBlock);
            await SendHttpRequest(HttpMethod.Post, url, bodyParam: bodyParams);
        }

        public async Task<bool> ValidateSkype(string skypeId)
        {
          //  var requestUrl = AppConstant.SkypeValidateUrl + skypeId;
          //var obj =  await GetHttpRequest(requestUrl);
          //  if (obj.StatusCode == HttpStatusCode.OK)
          //  {
          //      string response = await obj.Content.ReadAsStringAsync();
          //      if (response.Contains("{\"markup\":\"Skype Name not available\""))
          //          return true;
          //  }
            return true;
        }
        public async Task<UserModel> EditProfile(UserModel model, byte[] avatar)
        {          
            var formData = model.UserModelToFormData();
            var data = await SendMultipartRequest(string.Format(AppConstant.EditProfileUrl, model.Id), "photo", avatar, formData);
            var user = JsonUtils.ParseData<UserModel>(data, AppConstant.KeyProfile);
            return user;
        }

        public async Task<UserModel> Register(UserModel newAccount, byte[] avatar)
        {
            var formData = newAccount.UserModelToFormData();
            var data = await SendMultipartRequest(AppConstant.SignUpUrl, "file", avatar, formData);
            var userModel = JsonUtils.ParseData<UserModel>(data, AppConstant.KeyUser);
            userModel.ActiveType = JsonUtils.ParseData<ActiveType>(data, AppConstant.KeyActiveType);

            return userModel;
        }

        public async Task<UserModel> RegisterWithFb(UserModel newAccount, byte[] avatar)
        {
            var formData = newAccount.UserModelToFormData();
            var data = await SendMultipartRequest(AppConstant.SignUpFbUrl, "file", avatar, formData);
            return JsonUtils.ParseData<UserModel>(data, AppConstant.KeyUser);
        }

        #endregion
    }
}