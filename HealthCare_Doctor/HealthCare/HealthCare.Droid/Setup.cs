using System;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid;
using Cirrious.CrossCore.Droid.Platform;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Binding;
using Cirrious.MvvmCross.Binding.Droid.Target;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.ViewModels;
using HealthCare.Core.Models;
using HealthCare.Core.Resources;
using HealthCare.Core.Services;
using HealthCare.Core.Services.Interfaces;
using HealthCare.Droid.Services;
using Cirrious.MvvmCross.Binding.Bindings.Target.Construction;
using Cirrious.MvvmCross.Binding.Droid.Views;

namespace HealthCare.Droid
{
	public class PromptSpinnerBinding : MvxAndroidTargetBinding
	{
		protected MvxSpinner EditText
		{
			get { return (MvxSpinner)Target; }
		}

		public PromptSpinnerBinding(MvxSpinner target) : base(target)
		{
		}
		protected override void SetValueImpl (object target, object value)
		{
			var editext = (MvxSpinner)target;
			if (value != null)
				editext.Prompt = value.ToString ();
		}

		public override Type TargetType
		{
			get { return typeof(string); }
		}

		public override MvxBindingMode DefaultMode
		{
			get { return MvxBindingMode.OneWay; }
		}


	}
    public class HintEditTextBinding : MvxAndroidTargetBinding
    {
        protected EditText EditText
        {
            get { return (EditText)Target; }
        }

        public HintEditTextBinding(EditText target) : base(target)
        {
        }
		protected override void SetValueImpl (object target, object value)
		{
			var editext = (EditText)target;
			if (value != null)
				editext.Hint = value.ToString ();
		}
        
		public override Type TargetType
		{
			get { return typeof(string); }
		}

		public override MvxBindingMode DefaultMode
		{
			get { return MvxBindingMode.OneWay; }
		}       
    }



    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            Data.PushPlatform = "gcm";
            Data.Platform = Data.Os.Android;
            return new Core.App();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override void InitializeFirstChance()
        {
            Mvx.RegisterSingleton<IMessageService>(new MessageService());
            Mvx.RegisterSingleton<IHttpService>(new Core.Services.HttpService());

            base.InitializeFirstChance();
        }
		protected override void FillTargetFactories(Cirrious.MvvmCross.Binding.Bindings.Target.Construction.IMvxTargetBindingFactoryRegistry registry)
		{
			registry.RegisterCustomBindingFactory<EditText> (
				"Hint",
				binary => new HintEditTextBinding (binary));
			registry.RegisterCustomBindingFactory<MvxSpinner> (
				"Prompt",
				binary => new PromptSpinnerBinding (binary));
			base.FillTargetFactories(registry);
		}
        #region For reporter/messsages

        protected override void InitializeViewLookup()
        {
            var errorSource = Mvx.Resolve<IReporterService>();

            errorSource.ErrorReported += (sender, args) => DoError(args);

            base.InitializeViewLookup();
        }

        ProgressDialog _currentProgress;

        private void DoError(ErrorEventArgs args)
        {
            var argsMsg = args.Message != null ? args.Message.ToString() : string.Empty;
            switch (args.ErrorType)
            {
                case ErrorType.Network:
                    {
                        var title = (args.Title == null) ? AppResources.ApplicationTitle : args.Title.ToString();
                        MessageBox(title, AppResources.Message_InternetFailed, () =>
                        {
                            Java.Lang.JavaSystem.Exit(0);
                        });
                        break;
                    }
                case ErrorType.Message:
                    {
                        var title = (args.Title == null) ? AppResources.ApplicationTitle : args.Title.ToString();
                        MessageBox(title, argsMsg);
                        break;
                    }
                case ErrorType.StartProgress:
                    {
                        var top = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
                        if (top != null)
                        {
                            if (_currentProgress != null && _currentProgress.IsShowing)
                            {
                                return;
                                //_currentProgress.Dismiss();
                            }

                            _currentProgress = new ProgressDialog(top.Activity);
                            _currentProgress.SetTitle(AppResources.Messsage_Loading);
                            _currentProgress.SetCancelable(false);
                            _currentProgress.Show();
                        }
                        break;
                    }
                case ErrorType.StopProgress:
                    {
                        if (_currentProgress != null)
                        {
                            _currentProgress.Dismiss();
                        }
                        break;
                    }
            }
        }
        public static void MessageBox(String title, String message, Action success = null)
        {
            var top = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
            if (top != null)
            {
                var dlgAlert = new AlertDialog.Builder( new ContextThemeWrapper(top.Activity, Resource.Style.Dialog ));
              
                dlgAlert.SetMessage(message);
                dlgAlert.SetTitle(title);
                dlgAlert.SetPositiveButton("OK", delegate
                {
                    if (success != null)
                        success();
                });
                dlgAlert.SetCancelable(true);
                dlgAlert.Show();
            }
        }

        public static void OkCancelBox(String title, String message, Action<bool> success = null)
        {
            var top = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
            if (top == null || top.Activity == null)
                return;


			var dlgAlert = new AlertDialog.Builder(top.Activity);
            dlgAlert.SetMessage(message);
            dlgAlert.SetTitle(title);
            dlgAlert.SetPositiveButton(AppResources.Message_OK, delegate
            {
                if (success != null)
                    success(true);
            });
            dlgAlert.SetNegativeButton(AppResources.Message_Cancel, delegate
            {
                if (success != null)
                    success(false);
            });
            dlgAlert.SetCancelable(false);
            dlgAlert.Show();

        }


        #endregion
    }
}