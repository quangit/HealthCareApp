using System;
using System.Windows;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.WindowsPhone.Platform;
using HealthCare.Core.Models;
using HealthCare.Core.Resources;
using HealthCare.Core.Services;
using HealthCare.Core.Services.Interfaces;
using HealthCare.Phone.Services;
using Microsoft.Phone.Controls;
using Telerik.Windows.Controls;

namespace HealthCare.Phone
{
    public class Setup : MvxPhoneSetup
    {
        public Setup(PhoneApplicationFrame rootFrame) : base(rootFrame)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            Data.PushPlatform = "mpns";
            Data.Platform = Data.Os.WinPhone;
            return new Core.App();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        public class MyInputStringLoader : IStringResourceLoader
        {
            public string GetString(string key)
            {
                switch (key)
                {
                    case "ListPullToRefresh":
                        return AppResources.ListPullToRefresh;
                    case "ListPullToRefreshLoading":
                        return AppResources.ListPullToRefreshLoading;
                    case "ListPullToRefreshTime":
                        return AppResources.ListPullToRefreshTime;
                    case "ListReleaseToRefresh":
                        return AppResources.ListReleaseToRefresh;
                }
                return null;
            }
        }

        protected override void InitializeFirstChance()
        {
            LocalizationManager.GlobalStringLoader = new MyInputStringLoader();
            
           // Telerik.Windows.Controls.InputLocalizationManager.Instance.ResourceManager = Core.Resources.AppResources.ResourceManager;
            Mvx.RegisterSingleton<IMessageService>(new MessageService());
            Mvx.RegisterSingleton<IHttpService>(new Phone.Services.HttpService());
            Data.Platform = Data.Os.WinPhone;
            base.InitializeFirstChance();
        }
        #region For reporter/messsages

        protected override void InitializeViewLookup()
        {
            var errorSource = Mvx.Resolve<IReporterService>();

            errorSource.ErrorReported += (sender, args) => DoError(args);

            base.InitializeViewLookup();
        }



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
                            App.Current.Terminate();
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
                        Mvx.Resolve<IMessageService>().ShowSystemTray(AppResources.Messsage_Loading, 0);
                        break;
                    }
                case ErrorType.StopProgress:
                    {
                        Mvx.Resolve<IMessageService>().HideSystemTray();
                        break;
                    }
            }
        }
        public static void MessageBox(String title, String message, Action success = null)
        {
            System.Windows.MessageBox.Show(message, title, MessageBoxButton.OK);
            if (success != null)
                success();
        }

        public void OkCancelBox(String title, String message, Action<bool> success = null)
        {
            if (System.Windows.MessageBox.Show(message, title, MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                if (success != null)
                    success(true);
            }
            else
            {
                if (success != null)
                    success(false);
            }


        }


        #endregion
    }


}