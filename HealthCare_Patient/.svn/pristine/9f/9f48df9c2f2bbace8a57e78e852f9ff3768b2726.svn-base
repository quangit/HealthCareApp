using System;
using System.Diagnostics;
using System.Linq;
using Windows.Networking.PushNotifications;
using FFImageLoading.Forms.WinSL;
using HealthCare.DependencyServices;
using HealthCare.Helpers;
using HealthCare.WinPhone.Converters;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Notification;
using Microsoft.Phone.Shell;
using Microsoft.WindowsAzure.Messaging;
using Telerik.XamarinForms.Common.WinPhone;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

[assembly: Xamarin.Forms.ExportRenderer(typeof(Telerik.XamarinForms.Input.RadCalendar), typeof(Telerik.XamarinForms.InputRenderer.WinPhone.CalendarRenderer))]
namespace HealthCare.WinPhone
{
    public partial class MainPage : FormsApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();

            //try
            //{
            //CurrentChannel = HttpNotificationChannel.Find("CLASHealthCarePushNotification");

            //if (CurrentChannel == null)
            //{
            //    CurrentChannel = new HttpNotificationChannel("CLASHealthCarePushNotification");
            //    CurrentChannel.Open();
            //    CurrentChannel.BindToShellToast();
            //}

            //CurrentChannel.ChannelUriUpdated += (o, args) =>
            //{
            //    // Register for notifications using the new channel
            //    Settings.DeviceChannel = args.ChannelUri.ToString();
            //};

            //CurrentChannel.ShellToastNotificationReceived += (sender, args) =>
            //{
            //    ShellToast toast = new ShellToast();
            //    toast.Title = args.Collection["wp:Text1"];
            //    toast.Content = args.Collection["wp:Text2"];
            //    toast.Show();
            //};
            //}
            //catch (Exception e)
            //{
            //}

            CachedImageRenderer.Init();

            SupportedOrientations = SupportedPageOrientation.Portrait;

            Forms.Init();
            TelerikForms.Init();
            DependencyService.Get<IPushNotificationRegister>().RegiterPushNotification();
            string applicationId = "05d3b625-3aa2-4328-988e-75b63fbfac8f", authToken = "VmE4-NOiay21ZQ73QTeX5g";
            FormsMaps.Init(applicationId, authToken);
            LoadApplication(new HealthCare.App());
            CustomFacebookUriMapper.IsNavigateByFacebookLogin = false;
            ThemeManager.ToDarkTheme();
        }
    }
}