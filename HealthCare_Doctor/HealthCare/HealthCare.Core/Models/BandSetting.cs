using System;
using System.Collections.Generic;
using System.Diagnostics;
using HealthCare.Core.Resources;
using System.Globalization;
using HealthCare.Core.Utils;
using System.Linq;
using System.Drawing;

#if MVVMCROSS
using Cirrious.MvvmCross.ViewModels;
using Acr.UserDialogs;
using Cirrious.CrossCore;
using MyMvxViewModel = HealthCare.Core.ViewModels.MyMvxViewModel;
#else
using MyMvxViewModel = Template10.Mvvm.ViewModelBase;
using Template10.Mvvm;
using HealthCare.Core;
#endif

using Acr.UserDialogs;
using HealthCare.Core.Services;
using HealthCare.Core.Services.Interfaces;
using Microsoft.Band.Portable;

namespace HealthCare.Core.Models
{
    public class BandSetting : MyNotifyPropertyChanged
    {
        private IBandService _bandService;
        private IReporterService _reporterService;
        public BandSetting()
        {
#if MVVMCROSS
            _bandService = Mvx.Resolve<IBandService> ();
            _reporterService = Mvx.Resolve<IReporterService>();
#else
            _bandService = IoC.Resolve<IBandService>();
            _reporterService = IoC.Resolve<IReporterService>();
            if (!Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey("BandDeviceName"))
            {
                return;

            }
            try
            {
                var settings = Windows.Storage.ApplicationData.Current.LocalSettings.Values["BandDeviceName"].ToString();

                DeviceName = settings.Split('|')[0];

                LastSyncStr = settings.Split('|')[1];
            }
            catch (Exception)
            {

            }
#endif
        }

        private bool _connected;
        public bool Connected
        {
            get { return _connected; }
            set
            {
                _connected = value;
                RaisePropertyChanged("Connected");
                RaisePropertyChanged("StatusImage");
            }

        }

        private DateTime? _lastSync;
        public DateTime? LastSync
        {
            get { return _lastSync; }
            set
            {
                _lastSync = value;
                RaisePropertyChanged("LastSync");
                LastSyncStr = _lastSync == null ? "" : Util.ToShortDateTimeString(_lastSync);
            }
        }

        private string _deviceName;
        public string DeviceName
        {
            get { return _deviceName; }
            set
            {
                _deviceName = value;
                if (string.IsNullOrEmpty(_deviceName))
                {
                    _deviceName = AppResources.Setting_BandEmpty;
                }
                RaisePropertyChanged("DeviceName");

            }
        }

        private string lastSyncStr;

        public string StatusImage => Connected ?
              Util.ImageResourceUrl("band_enabled") :
              Util.ImageResourceUrl("band_disabled");


        private MvxCommand _connectCommand;
        public MvxCommand ConnectCommand
        {
            get
            {
                _connectCommand = _connectCommand ?? new MvxCommand(DoConnect);
                return _connectCommand;
            }
        }

        private async void DoConnect()
        {
#if !MVVMCROSS
            _reporterService.ShowProgress();
#endif
            try
            {


                var devices = await _bandService.GetDevices();
                var bandDeviceInfos = devices as IList<BandDeviceInfo> ?? devices.ToList();
                var deviceNames = bandDeviceInfos.Select(d => d.Name);


                //ToastConfig.ErrorTextColor = Color.Red;
                if (deviceNames == null || !bandDeviceInfos.Any())
                    UserDialogs.Instance.Alert(AppResources.Setting_BandNotPaired, AppResources.Warning);
                else
                {

                    var deviceName = await UserDialogs.Instance.ActionSheetAsync(AppResources.Setting_BandChooseTitle, AppResources.Message_Cancel, "", deviceNames.ToArray());

                    var device = bandDeviceInfos.FirstOrDefault(d => d.Name.Equals(deviceName));
                    if (device != null)
                    {
                        if (!Connected || deviceName != DeviceName)
                            Connected = await _bandService.Connect(device);

                        DeviceName = deviceName;

#if !MVVMCROSS
                        if (!Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey("BandDeviceName"))
                            Windows.Storage.ApplicationData.Current.LocalSettings.Values.Add("BandDeviceName", DeviceName);
                        else
                            Windows.Storage.ApplicationData.Current.LocalSettings.Values["BandDeviceName"] = DeviceName;      
#endif
                        DoSync();
                    }
                }
            }
            catch (Exception)
            {
                /**/
                //throw;
            }
#if !MVVMCROSS
            _reporterService.StopProgress();
#endif
            ConnectCommand.RaiseCanExecuteChanged();
        }
#if !MVVMCROSS
        public async void DoConnectDefault()
        {
            try
            {
                if (string.IsNullOrEmpty(DeviceName))
                {
                    return;
                }
                var deviceName = DeviceName;
                var devices = await _bandService.GetDevices();
                var bandDeviceInfos = devices as IList<BandDeviceInfo> ?? devices.ToList();

                var device = bandDeviceInfos.FirstOrDefault(d => d.Name.Equals(deviceName));
                if (device != null)
                {
                    if (!Connected || deviceName != DeviceName)
                        Connected = await _bandService.Connect(device);                    
                    DeviceName = deviceName;
                }
            }
            catch (Exception)
            {
                //                throw;
            }

            ConnectCommand.RaiseCanExecuteChanged();

        }
#endif


        private MvxCommand _syncCommand;
        public MvxCommand SyncCommand
        {
            get
            {
                _syncCommand = _syncCommand ?? new MvxCommand(DoSync);
                return _syncCommand;
            }
        }

        public string LastSyncStr
        {
            get
            {
                return lastSyncStr;
            }

            set
            {
                SetProperty(ref lastSyncStr, value);
            }
        }



        private async void DoSync()
        {
            try
            {
#if !MVVMCROSS
                _reporterService.ShowProgress();
#endif
                if (Connected)
                {
                    await _bandService.SyncData();

                    LastSync = DateTime.Now;
#if MVVMCROSS
                    UserDialogs.Instance.SuccessToast(AppResources.Setting_BandSyncSuccess);
#else
                    if (!Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey("BandDeviceName"))
                        Windows.Storage.ApplicationData.Current.LocalSettings.Values.Add("BandDeviceName", DeviceName + "|" + LastSyncStr);
                    else
                        Windows.Storage.ApplicationData.Current.LocalSettings.Values["BandDeviceName"] = DeviceName + "|" + LastSyncStr;

                    await HealthCare.Core.IoC.Resolve<IMessageService>().ShowMessageAsync(AppResources.Setting_BandSyncSuccess, "Microsoft Band");
#endif
                }
                else
                {
                    DoConnect();
                }
            }
            catch (Exception e)
            {
                UserDialogs.Instance.Alert(e.Message);
                Connected = false;
            }
#if !MVVMCROSS
            _reporterService.StopProgress();
#endif
        }


    }
}

