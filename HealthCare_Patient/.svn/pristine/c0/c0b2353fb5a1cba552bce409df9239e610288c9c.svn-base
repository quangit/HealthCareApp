using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;
using HealthCare.DependencyServices;
using HealthCare.Helpers;
using HealthCare.ViewModels;
using HealthCare.WinPhone.DependencyServices;
using Microsoft.Phone.Shell;
using Microsoft.WindowsAzure.Messaging;

[assembly: Xamarin.Forms.Dependency(typeof(PushNotificationRegister))]
namespace HealthCare.WinPhone.DependencyServices
{
    public class PushNotificationRegister : IPushNotificationRegister
    {
        public async Task UnRegisterPushNotification()
        {
            try
            {
                var hub = new NotificationHub("test_healthcare",
                   "Endpoint=sb://testhealthcare.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=x5muxLBg0Dpru14TSUGFvnqHGgCuy09UOG1HAXEM+SU=");
                await hub.UnregisterNativeAsync();
            }
            catch
            {
                // ignored
            }
        }

        public async Task RegiterPushNotification()
        {
            try
            {
                var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
                Settings.DeviceChannel = channel.Uri;
                channel.PushNotificationReceived += (sender, args) =>
                {
                    //if (string.IsNullOrWhiteSpace(Settings.CurrentUser?.UserCode))
                    //{
                    //    args.Cancel = true; //make sure unregister/not-handle push notification when user logout
                    //}
                    //else
                    //{
                    try
                    {
                        ShellToast toast = new ShellToast();
                        toast.Title =
                            args.ToastNotification?.Content.GetElementById("1")
                                ?.InnerText;
                        toast.Content =
                            args.ToastNotification?.Content.GetElementById("2")
                                ?.InnerText;
                        toast.Show();
                    }
                    catch
                    {
                        // ignored
                    }
                    //}
                };


                //var hub = new NotificationHub("test_healthcare",
                //    "Endpoint=sb://testhealthcare.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=x5muxLBg0Dpru14TSUGFvnqHGgCuy09UOG1HAXEM+SU=");

                //var result = await hub.RegisterNativeAsync(channel.Uri);

                ////register success
                //if (result.RegistrationId != null)
                //{
                //    Settings.DeviceChannel = result.ChannelUri;
                //    Debug.WriteLine("Registered push: " + result.ChannelUri);

                //    channel.PushNotificationReceived += (sender, args) =>
                //    {
                //        if (string.IsNullOrWhiteSpace(Settings.CurrentUser?.UserCode))
                //        {
                //            args.Cancel = true; //make sure unregister/not-handle push notification when user logout
                //        }
                //        else
                //        {
                //            ShellToast toast = new ShellToast();
                //            toast.Title =
                //                args.ToastNotification?.Content.GetElementsByTagName("wp:Text1")
                //                    .FirstOrDefault()
                //                    ?.InnerText;
                //            toast.Content =
                //                args.ToastNotification?.Content.GetElementsByTagName("wp:Text2")
                //                    .FirstOrDefault()
                //                    ?.InnerText;
                //            toast.Show();
                //        }
                //    };
                //}
            }
            catch
            {
                // ignored
            }
        }
    }
}
