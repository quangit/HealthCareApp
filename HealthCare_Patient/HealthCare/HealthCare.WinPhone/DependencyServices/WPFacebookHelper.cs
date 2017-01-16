using System;
using System.Globalization;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using Facebook;
using HealthCare.DependencyServices;
using HealthCare.Enums;
using HealthCare.Helpers;
using HealthCare.Models;
using HealthCare.ViewModels;
using HealthCare.WinPhone.DependencyServices;
using Microsoft.Phone.Controls;
using Newtonsoft.Json;
using Xamarin.Forms;

[assembly: Dependency(typeof(WPFacebookHelper))]

namespace HealthCare.WinPhone.DependencyServices
{
    public class WPFacebookHelper : IFacebookHelper
    {
        public static bool IsFacebookDialogOpen { get; set; }

        public async void Login()
        {
            //if (!Debugger.IsAttached)
            await new WebBrowser().ClearCookiesAsync();

            //Facebook app id
            var clientId = AppConstant.FacebookClientId;
            //Facebook permissions
            var scope = "email,public_profile,user_birthday";

            var redirectUri = WebAuthenticationBroker.GetCurrentApplicationCallbackUri().ToString();
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = clientId,
                redirect_uri = redirectUri,
                response_type = "token",
                scope
            });

            var startUri = loginUrl;
            var endUri = new Uri(redirectUri, UriKind.Absolute);
            if (!string.IsNullOrWhiteSpace(Settings.FacebookAccessTokenLogout))
            {
                var logoutUrl = new FacebookClient().GetLogoutUrl(new
                {
                    next = startUri,
                    access_token = Settings.FacebookAccessTokenLogout
                });

                WebAuthenticationBroker.AuthenticateAndContinue(logoutUrl, endUri, null, WebAuthenticationOptions.None);
                Settings.FacebookAccessTokenLogout = "";
            }
            else
                WebAuthenticationBroker.AuthenticateAndContinue(startUri, endUri, null, WebAuthenticationOptions.None);
            IsFacebookDialogOpen = true;

            //Session.OnFacebookAuthenticationFinished += (s) =>
            //{
            //    IsFacebookDialogOpen = false;
            //};
        }

        public async Task GetMe(string accessToken)
        {
            var fb = new FacebookClient();
            fb.AccessToken = accessToken;

            var taskMe =
                await
                    fb.GetTaskAsync("me?fields=id,birthday,email,first_name,gender,last_name,link,locale,name,timezone");

            if (taskMe != null)
            {
                var result = JsonConvert.DeserializeObject<FacebookGraphAPIResponse>(taskMe.ToString());

                //clear current user
                UserViewModel.Instance.ResetUser();
                var birthDay = !string.IsNullOrWhiteSpace(result.birthday)
                    ? DateTime.ParseExact(result.birthday, new[] { "dd/MM/yyyy", "MM/dd/yyyy" },
                        new CultureInfo("en-US"), DateTimeStyles.None)
                    : DateTime.Now.Date;
                var userNotRegisted = new UserModel
                {
                    FacebookId = result.id,
                    BirthDay = birthDay,
                    Email = result.email ?? "",
                    FirstName = result.first_name ?? "",
                    LastName = result.last_name ?? "",
                    Gender = !string.IsNullOrWhiteSpace(result.gender)
                        ? (result.gender.Equals("male") ? Gender.Male : Gender.Female)
                        : Gender.Female
                };

                UserViewModel.Instance.CurrentUser = userNotRegisted;
            }
        }

        public bool IsLoginInProgress()
        {
            return IsFacebookDialogOpen;
        }

        private void LogOut()
        {
            var requestUrl = "https://login.windows.net/common/oauth2/logout";
            Task.Run(() =>
            {
                try
                {
                    WebAuthenticationBroker.AuthenticateAndContinue(new Uri(requestUrl));
                }
                catch (Exception)
                {
                    // timeout. That's expected
                }
            });
        }

        internal class FacebookGraphAPIResponse
        {
            public string id { get; set; }
            public string birthday { get; set; }
            public string email { get; set; }
            public string first_name { get; set; }
            public string gender { get; set; }
            public string last_name { get; set; }
            public string link { get; set; }
            public string locale { get; set; }
            public string name { get; set; }
            public int timezone { get; set; }
            public string updated_time { get; set; }
            public bool verified { get; set; }
        }
    }
}