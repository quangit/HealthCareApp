using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Touch.Platform;
using UIKit;
using HealthCare.Core.Models;
using Cirrious.CrossCore;
using HealthCare.Core.Services;
using LumiaLoyalty.Touch.Utilities;
using HealthCare.Core.Resources;
using BigTed;
using HealthCare.Core.Services.Interfaces;
using HealthCare.Touch.Services;
using Cirrious.MvvmCross.Binding.Bindings.Target;
using System;
using Cirrious.MvvmCross.Binding;

namespace HealthCare.Touch
{
    public class Setup : MvxTouchSetup
    {
        private UIWindow _window;

        public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
            _window = window;
        }

        protected override IMvxApplication CreateApp()
        {
            Data.Platform = Data.Os.iOS;

            Data.PushPlatform = "apns";
            return new Core.App();
        }

        protected override void InitializeFirstChance()
        {
            Mvx.RegisterSingleton<IMessageService>(new MessageService());
            Mvx.RegisterSingleton<IHttpService>(new Core.Services.HttpService());

            base.InitializeFirstChance();
        }
        protected override void InitializeViewLookup()
        {
            MessageBox.Initialize(_window);
            var errorSource = Mvx.Resolve<IReporterService>();
            errorSource.ErrorReported += (sender, args) => DoError(args);
            //Tracker = GAI.SharedInstance.GetTracker(TrackingId);
            base.InitializeViewLookup();
        }
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }


        public static void DoError(ErrorEventArgs args)
        {
            //			var hud = new MTMBProgressHUD(_window.RootViewController.View)
            //            {
            //                LabelText = "Waiting...",
            //                RemoveFromSuperViewOnHide = true
            //            };
            var argsMsg = args.Message != null ? args.Message.ToString() : string.Empty;
            switch (args.ErrorType)
            {
                case ErrorType.Message:
                    {
                        var title = (args.Title == null) ? AppResources.ApplicationTitle : args.Title.ToString();
                        MessageBox.Show(title, argsMsg);
                        break;
                    }
                case ErrorType.ErrorMessage:
                    {
                        var title = (args.Title == null) ? AppResources.ApplicationTitle : args.Title.ToString();
                        MessageBox.Show(title, argsMsg);
                        break;
                    }
                case ErrorType.StartProgress:
                    {
                        //                    hud.Show(animated: true);
                        if (BTProgressHUD.IsVisible)
                            BTProgressHUD.Dismiss();

                        BTProgressHUD.Show(AppResources.Messsage_Loading, -1F, ProgressHUD.MaskType.Black);
                        break;
                    }
                case ErrorType.StopProgress:
                    {
                        BTProgressHUD.Dismiss();
                        //                    hud.Hide(animated: true, delay: 5);
                        break;
                    }

                case ErrorType.Network:
                    {
                        var title = (args.Title == null) ? AppResources.ApplicationTitle : args.Title.ToString();

                        MessageBox.Show(title, AppResources.Message_InternetFailed, MessageBoxButton.Ok, () =>
                        {

                        });
                        break;
                    }
            }
        }

    }



}