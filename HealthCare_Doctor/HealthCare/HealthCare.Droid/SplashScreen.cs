using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;

namespace HealthCare.Droid
{
	[Activity(
        Label = "BacSi24x7 BS"
		, MainLauncher = true
		, Icon = "@drawable/logo_bs"
		, Theme = "@style/Theme.Splash"
		, NoHistory = true
		, ScreenOrientation = ScreenOrientation.Portrait)]
	public class SplashScreen : MvxSplashScreenActivity
	{
		public SplashScreen()
			: base(Resource.Layout.SplashScreen)
		{
		}

	    protected override void OnCreate(Bundle bundle)
	    {
	        base.OnCreate(bundle);
            UserDialogs.Init(this);
	    }
	}
}