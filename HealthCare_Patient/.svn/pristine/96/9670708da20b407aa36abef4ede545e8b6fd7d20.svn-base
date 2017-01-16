using System;
using System.Net;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using HealthCare.Helpers;
using Acr.UserDialogs;
using Android.Content;
using Android.Content.Res;

using FFImageLoading.Forms.Droid;
using HealthCare.DependencyServices;
using Xamarin.Forms;


using HealthCare.Droid.PushNotifications;

using Microsoft.WindowsAzure.MobileServices;

namespace HealthCare.Droid
{
    [Activity(Theme = "@style/AppTheme", Icon = "@drawable/icon", Label = "BacSi24x7", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait, WindowSoftInputMode = SoftInput.AdjustResize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity //XFormsApplicationDroid  
    {
        static MainActivity instance = new MainActivity();
        public static string sPackageName;

        public bool IsLoginFacebook { get; set; }
        public static MainActivity CurrentActivity
        {
            get
            {
                return instance;
            }
        }
        // Return the Mobile Services client.
       
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            CachedImageRenderer.Init();
            UserDialogs.Init(this);
            global::Xamarin.Forms.Forms.Init(this, bundle);

            DependencyService.Get<IPushNotificationRegister>().RegiterPushNotification();

            Xamarin.FormsMaps.Init(this, bundle);
            sPackageName = PackageName;
            AppConstant.ScreenWidth = (int)(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density);
            ServicePointManager.ServerCertificateValidationCallback += (o, certificate, chain, errors) => true;

            LoadApplication(new App());

            //try
            //{
            //    // Check to ensure everything's setup right
            //    GcmClient.CheckDevice(this);
            //    GcmClient.CheckManifest(this);

            //    // Register for push notifications
            //    GcmClient.Register(this, ToDoBroadcastReceiver.senderIDs);

            //}
            //catch (Java.Net.MalformedURLException)
            //{
            //    CreateAndShowDialog("There was an error creating the client. Verify the URL.", "Error");
            //}
            //catch (Exception e)
            //{
            //    CreateAndShowDialog(e.Message, "Error");
            //}
        }
        private void CreateAndShowDialog(String message, String title)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);

            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.Create().Show();
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            App.Clean();
        }
       
        protected override void OnResume()
        {
            base.OnResume();
           
            // When do a login with facebook, we need return to Main Page before do the login in viewmodel
            // Do login in authenticate page will cause a bug leak window
            if (IsLoginFacebook)
                HealthCare.ViewModels.LoginViewModel.Instance.LoginFb();
            IsLoginFacebook = false;
        }
    }
}

