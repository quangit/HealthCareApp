using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using UIKit;
using System.Threading.Tasks;
using HealthCare.ViewModels;
using HealthCare.Models;
using Xamarin.Forms;
using Xamarin.Auth;
using HealthCare.Helpers;
using HealthCare.Pages;
using Facebook;
using HealthCare.DependencyServices;
using HealthCare.Enums;
using Newtonsoft.Json;
using HealthCare.iOS.DependencyServices;
using System.Globalization;

[assembly: Xamarin.Forms.Dependency(typeof(FacebookHelperiOS))]
namespace HealthCare.iOS.DependencyServices
{
    public class FacebookHelperiOS : IFacebookHelper
    {
        public async Task GetMe(string accessToken)
        {
            FacebookClient fb = new FacebookClient(accessToken);

            var taskMe = await fb.GetTaskAsync("me?fields=id,birthday,email,first_name,gender,last_name,link,locale,name,timezone");

            if (taskMe != null)
            {
                var result = JsonConvert.DeserializeObject<FacebookGraphAPIResponse>(taskMe.ToString());
                var birth = !string.IsNullOrWhiteSpace(result.birthday)
                    ? DateTime.ParseExact(result.birthday, new[] { "dd/MM/yyyy", "MM/dd/yyyy" },
                                new CultureInfo("en-US"), DateTimeStyles.None)
                    : DateTime.Now.Date;
                //clear current user
                UserViewModel.Instance.ResetUser();
                var userNotRegisted = new UserModel
                {
                    FacebookId = result.id,
//                    BirthDay = !string.IsNullOrWhiteSpace(result.birthday) ?
//                   DateTime.Parse(result.birthday)
//                   : DateTime.Now.Date,
                        BirthDay =  birth,
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
            throw new NotImplementedException();
        }

        public void Login()
        {
            var auth = new OAuth2Authenticator(
                           clientId: AppConstant.FacebookClientId,
                           scope: AppConstant.FacebookScope,
                           authorizeUrl: new Uri(AppConstant.FacebookAuthorizeUrl),
                           redirectUrl: new Uri(AppConstant.FacebookRedirectUrl)
                       );

            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            while (vc.PresentedViewController != null)
            {
                vc = vc.PresentedViewController;
            }

            //for debug
            auth.ClearCookiesBeforeLogin = true;

            auth.Completed += (sender, eventArgs) =>
            {
                if (eventArgs.IsAuthenticated)
                {
                    // Use eventArgs.Account to do wonderful things
                    Settings.FacebookAccessToken = eventArgs.Account.Properties["access_token"];
                    LoginViewModel.Instance.LoginFb();
                }
                else
                {
                    // The user cancelled
                }

                vc.DismissViewController(true, () =>
                    {
                    });
            };

            auth.Error += (sender, args) =>
            {

            };


            vc.PresentViewController(auth.GetUI(), true, null);
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