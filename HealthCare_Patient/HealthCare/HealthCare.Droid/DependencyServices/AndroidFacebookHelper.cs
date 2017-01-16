using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HealthCare.Helpers;
using System.Threading.Tasks;
using Facebook;
using Newtonsoft.Json;
using HealthCare.ViewModels;
using HealthCare.Models;
using HealthCare.Droid.DependencyServices;
using Xamarin.Forms;
using Xamarin.Auth;
using HealthCare.Pages;
using System.Globalization;
using HealthCare.DependencyServices;
using HealthCare.Enums;
using Acr.UserDialogs;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidFacebookHelper))]
namespace HealthCare.Droid.DependencyServices
{
    public class AndroidFacebookHelper : IFacebookHelper
    {
        public async Task GetMe(string accessToken)
        {
            FacebookClient fb = new FacebookClient(accessToken);

            var taskMe = await fb.GetTaskAsync("me?fields=id,birthday,email,first_name,gender,last_name,link,locale,name,timezone");

            if (taskMe != null)
            {
                var result = JsonConvert.DeserializeObject<FacebookGraphAPIResponse>(taskMe.ToString());

                //clear current user
                UserViewModel.Instance.ResetUser();
                var userNotRegisted = new UserModel
                {
                    FacebookId = result.id,
                    BirthDay = !string.IsNullOrWhiteSpace(result.birthday) ?
                        DateTime.ParseExact(result.birthday, new string[] { "dd/MM/yyyy",
                                "MM/dd/yyyy" }, new CultureInfo("en-US"), DateTimeStyles.None) : DateTime.Now.Date,
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
            return true;
        }

        public void Login()
        {
            var activity = Forms.Context as MainActivity;

            var auth = new OAuth2Authenticator(
                clientId: AppConstant.FacebookClientId,
                scope: AppConstant.FacebookScope,
                authorizeUrl: new Uri(AppConstant.FacebookAuthorizeUrl),
                redirectUrl: new Uri(AppConstant.FacebookRedirectUrl)
            );

            //for debug
            auth.ClearCookiesBeforeLogin = true;

            auth.Completed += (sender, eventArgs) =>
            {
                if (eventArgs.IsAuthenticated)
                {
                    Settings.FacebookAccessToken = eventArgs.Account.Properties["access_token"];
                    activity.IsLoginFacebook = true;
                }
                else
                {
                    // The user cancelled
                }
            };
            activity.StartActivity(auth.GetUI(activity));
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