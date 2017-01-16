using System;
using Android.Content;
using Android.Content.PM;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Cirrious.CrossCore.Core;
using Cirrious.CrossCore.Droid.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Views;
using Cirrious.MvvmCross.ViewModels;
using Object = Java.Lang.Object;
using System.ComponentModel;
using HealthCare.Core.Resources;
using HealthCare.Core.Models;

namespace HealthCare.Droid.Utilities
{
	public class OnMenuItemClickAction : Object, IMenuItemOnMenuItemClickListener
	{
		private readonly Action<IMenuItem> _action;
		public OnMenuItemClickAction(Action<IMenuItem> action)
		{
			_action = action;
		}

		public bool OnMenuItemClick(IMenuItem item)
		{
			if (_action != null)
			{
				_action(item);
			}
			return true;
		}
	}
	public abstract class MvxActionBarActivity : AppCompatActivity, IMvxEventSourceActivity, IMvxAndroidView, View.IOnClickListener
	{

		private static readonly ScreenOrientation PORTRAIT_ORIENTATION = (Build.VERSION.SdkInt <
			BuildVersionCodes.Gingerbread)
			? ScreenOrientation.Portrait
			: ScreenOrientation.SensorPortrait;
		
		protected abstract int LayoutResource
		{
			get;
		}
		public Toolbar Toolbar
		{
			get;
			set;
		}

   

        protected int ActionBarIcon
		{
			set { Toolbar.SetNavigationIcon(value); }
		}
		public IMvxBindingContext BindingContext { get; set; }

		protected MvxActionBarActivity()
		{
			BindingContext = new MvxAndroidBindingContext(this, this);
			this.AddEventListeners();
		}

		public event EventHandler<MvxValueEventArgs<MvxActivityResultParameters>> ActivityResultCalled;
		public event EventHandler<MvxValueEventArgs<Bundle>> CreateCalled;
		public event EventHandler<MvxValueEventArgs<Bundle>> CreateWillBeCalled;
		public event EventHandler DestroyCalled;
		public event EventHandler DisposeCalled;
		public event EventHandler<MvxValueEventArgs<Intent>> NewIntentCalled;
		public event EventHandler PauseCalled;
		public event EventHandler RestartCalled;
		public event EventHandler ResumeCalled;
		public event EventHandler<MvxValueEventArgs<Bundle>> SaveInstanceStateCalled;
		public event EventHandler<MvxValueEventArgs<MvxStartActivityForResultParameters>> StartActivityForResultCalled;
		public event EventHandler StartCalled;
		public event EventHandler StopCalled;



		public object DataContext
		{
			get { return BindingContext.DataContext; }
			set { BindingContext.DataContext = value; }
		}

		public IMvxViewModel ViewModel
		{
			get { return DataContext as IMvxViewModel; }
			set
			{
				DataContext = value;
				OnViewModelSet();
			}
		}

		public void MvxInternalStartActivityForResult(Intent intent, int requestCode)
		{
			base.StartActivityForResult(intent, requestCode);
		}

		public override void SetContentView(int layoutResId)
		{
#if DEBUG // This try catch is super useful when debugging bad layouts.  No real need for it in prod.
			try
			{
#endif
				var view = this.BindingInflate(layoutResId, null);
				SetContentView(view);
#if DEBUG
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);  // Because of the JNI layers, this is the easiest way to reliably get the message from the exception when debugging.  The watch window isn't as reliable/timely
				throw;
			}
#endif
		}

		public override void StartActivityForResult(Intent intent, int requestCode)
		{
			StartActivityForResultCalled.Raise(this, new MvxStartActivityForResultParameters(intent, requestCode));
			base.StartActivityForResult(intent, requestCode);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				DisposeCalled.Raise(this);
			}
			base.Dispose(disposing);
		}

	    public override bool OnCreateOptionsMenu(IMenu menu)
	    { 
            var itmMyPakaze = menu.Add("Call")
             .SetIcon(Resource.Drawable.phone_sup);
            itmMyPakaze.SetShowAsAction(ShowAsAction.Always);
            itmMyPakaze.SetOnMenuItemClickListener(new OnMenuItemClickAction((e) =>
            {
                Setup.OkCancelBox(AppResources.Warning, AppResources.AppBar_CallMessage, v =>
                {
                    if (v)
                    {
                        var uri = Android.Net.Uri.Parse("tel:" + Data.SupportPhone);
                        var intent = new Intent(Intent.ActionDial, uri);
                        StartActivity(intent);
                    }
                });
            }));
	        return base.OnCreateOptionsMenu(menu);
	    }

	    protected override void OnCreate(Bundle bundle)
		{
			CreateWillBeCalled.Raise(this, bundle);
			base.OnCreate(bundle);
			CreateCalled.Raise(this, bundle);
			RequestedOrientation = PORTRAIT_ORIENTATION;

			SetContentView(LayoutResource);
			Toolbar = FindViewById<Toolbar>(Resource.Id.toolbar_top);

			if (Toolbar != null)
			{
				SetSupportActionBar(Toolbar);
				SupportActionBar.SetDisplayHomeAsUpEnabled(true);
				SupportActionBar.SetHomeButtonEnabled(true);
                
			    if (Resources != null)
			    {
                    Drawable colorDrawable = new ColorDrawable(Resources.GetColor(Resource.Color.ButtonGreenMainColor));
                    Drawable bottomDrawable = new ColorDrawable(Android.Graphics.Color.Transparent);
			        LayerDrawable ld = new LayerDrawable(new Drawable[] { colorDrawable, bottomDrawable });
			        SupportActionBar.SetBackgroundDrawable(ld);
			    }
			    // var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
				Toolbar.SetNavigationOnClickListener(this);
				//Drawable icon = new Drawable
				SupportActionBar.SetIcon(Resource.Drawable.logo_bs);
                SupportActionBar.Title = AppResources.ApplicationTitle;
			}

        

		}

		protected override void OnDestroy()
		{
			DestroyCalled.Raise(this);
			base.OnDestroy();
		}

		protected override void OnNewIntent(Intent intent)
		{
			base.OnNewIntent(intent);
			NewIntentCalled.Raise(this, intent);
		}

		protected override void OnPause()
		{
			PauseCalled.Raise(this);
			base.OnPause();
		}

		protected override void OnRestart()
		{
			base.OnRestart();
			RestartCalled.Raise(this);
		}

		protected override void OnResume()
		{
			base.OnResume();
			ResumeCalled.Raise(this);
		}

		protected override void OnSaveInstanceState(Bundle outState)
		{
			SaveInstanceStateCalled.Raise(this, outState);
			base.OnSaveInstanceState(outState);
		}

		protected override void OnStart()
		{
			base.OnStart();
			StartCalled.Raise(this);
		}

		protected override void OnStop()
		{
			StopCalled.Raise(this);
			base.OnStop();
		}

		protected virtual void OnViewModelSet()
		{
		}


		

		public void OnClick(View v)
		{
			OnBackPressed();
		}
	}
}

