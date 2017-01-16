package mono;

import java.io.*;
import java.lang.String;
import java.util.Locale;
import java.util.HashSet;
import java.util.zip.*;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ApplicationInfo;
import android.content.res.AssetManager;
import android.util.Log;
import mono.android.Runtime;

public class MonoPackageManager {

	static Object lock = new Object ();
	static boolean initialized;

	static android.content.Context Context;

	public static void LoadApplication (Context context, ApplicationInfo runtimePackage, String[] apks)
	{
		synchronized (lock) {
			if (context instanceof android.app.Application) {
				Context = context;
			}
			if (!initialized) {
				android.content.IntentFilter timezoneChangedFilter  = new android.content.IntentFilter (
						android.content.Intent.ACTION_TIMEZONE_CHANGED
				);
				context.registerReceiver (new mono.android.app.NotifyTimeZoneChanges (), timezoneChangedFilter);
				
				System.loadLibrary("monodroid");
				Locale locale       = Locale.getDefault ();
				String language     = locale.getLanguage () + "-" + locale.getCountry ();
				String filesDir     = context.getFilesDir ().getAbsolutePath ();
				String cacheDir     = context.getCacheDir ().getAbsolutePath ();
				String dataDir      = getNativeLibraryPath (context);
				ClassLoader loader  = context.getClassLoader ();

				Runtime.init (
						language,
						apks,
						getNativeLibraryPath (runtimePackage),
						new String[]{
							filesDir,
							cacheDir,
							dataDir,
						},
						loader,
						new java.io.File (
							android.os.Environment.getExternalStorageDirectory (),
							"Android/data/" + context.getPackageName () + "/files/.__override__").getAbsolutePath (),
						MonoPackageManager_Resources.Assemblies,
						context.getPackageName ());
				
				mono.android.app.ApplicationRegistration.registerApplications ();
				
				initialized = true;
			}
		}
	}

	public static void setContext (Context context)
	{
		// Ignore; vestigial
	}

	static String getNativeLibraryPath (Context context)
	{
	    return getNativeLibraryPath (context.getApplicationInfo ());
	}

	static String getNativeLibraryPath (ApplicationInfo ainfo)
	{
		if (android.os.Build.VERSION.SDK_INT >= 9)
			return ainfo.nativeLibraryDir;
		return ainfo.dataDir + "/lib";
	}

	public static String[] getAssemblies ()
	{
		return MonoPackageManager_Resources.Assemblies;
	}

	public static String[] getDependencies ()
	{
		return MonoPackageManager_Resources.Dependencies;
	}

	public static String getApiPackageName ()
	{
		return MonoPackageManager_Resources.ApiPackageName;
	}
}

class MonoPackageManager_Resources {
	public static final String[] Assemblies = new String[]{
		/* We need to ensure that "HealthCare.Droid.dll" comes first in this list. */
		"HealthCare.Droid.dll",
		"Acr.Support.Android.dll",
		"Acr.UserDialogs.dll",
		"Acr.UserDialogs.Interface.dll",
		"AndHUD.dll",
		"Cirrious.CrossCore.dll",
		"Cirrious.CrossCore.Droid.dll",
		"Cirrious.MvvmCross.Binding.dll",
		"Cirrious.MvvmCross.Binding.Droid.dll",
		"Cirrious.MvvmCross.dll",
		"Cirrious.MvvmCross.Droid.dll",
		"Cirrious.MvvmCross.Droid.Fragging.dll",
		"Cirrious.MvvmCross.Localization.dll",
		"Cirrious.MvvmCross.Plugins.Color.dll",
		"Cirrious.MvvmCross.Plugins.Color.Droid.dll",
		"Cirrious.MvvmCross.Plugins.DownloadCache.dll",
		"Cirrious.MvvmCross.Plugins.DownloadCache.Droid.dll",
		"Cirrious.MvvmCross.Plugins.File.dll",
		"Cirrious.MvvmCross.Plugins.File.Droid.dll",
		"Cirrious.MvvmCross.Plugins.Location.dll",
		"Cirrious.MvvmCross.Plugins.Location.Droid.dll",
		"Cirrious.MvvmCross.Plugins.Messenger.dll",
		"Cirrious.MvvmCross.Plugins.Network.dll",
		"Cirrious.MvvmCross.Plugins.Network.Droid.dll",
		"Cirrious.MvvmCross.Plugins.PictureChooser.dll",
		"Cirrious.MvvmCross.Plugins.PictureChooser.Droid.dll",
		"Cirrious.MvvmCross.Plugins.Visibility.dll",
		"Cirrious.MvvmCross.Plugins.Visibility.Droid.dll",
		"Lyft.Scissors.dll",
		"Microsoft.Band.Android.dll",
		"Microsoft.Band.Portable.dll",
		"ModernHttpClient.dll",
		"Newtonsoft.Json.dll",
		"Splat.dll",
		"Square.OkHttp.dll",
		"Square.OkIO.dll",
		"System.Net.Http.Extensions.dll",
		"System.Net.Http.Primitives.dll",
		"Telerik.Xamarin.Android.Common.dll",
		"Telerik.Xamarin.Android.Input.dll",
		"Telerik.Xamarin.Android.Primitives.dll",
		"Xamarin.Android.Support.Design.dll",
		"Xamarin.Android.Support.v4.dll",
		"Xamarin.Android.Support.v7.AppCompat.dll",
		"Xamarin.GooglePlayServices.Gcm.dll",
		"HealthCare.Core.dll",
		"Mono.Android.Export.dll",
		"System.Runtime.dll",
		"System.Threading.Tasks.dll",
		"System.Collections.dll",
		"System.ObjectModel.dll",
		"System.Globalization.dll",
		"System.Reflection.dll",
		"System.Linq.Expressions.dll",
		"System.IO.dll",
		"System.Resources.ResourceManager.dll",
		"System.Runtime.Extensions.dll",
		"System.Reflection.Extensions.dll",
		"System.Linq.dll",
		"System.Threading.dll",
		"System.Diagnostics.Debug.dll",
		"System.Net.Requests.dll",
		"System.Net.Primitives.dll",
		"System.Text.Encoding.dll",
		"OkHttp.dll",
		"System.Runtime.InteropServices.dll",
		"System.Dynamic.Runtime.dll",
		"System.Diagnostics.Tools.dll",
		"System.Text.RegularExpressions.dll",
		"System.Net.NetworkInformation.dll",
		"System.Collections.Concurrent.dll",
	};
	public static final String[] Dependencies = new String[]{
	};
	public static final String ApiPackageName = "Mono.Android.Platform.ApiLevel_23";
}
