using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Telerik.Windows.Controls;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using HealthCare.Core.Resources;
using Microsoft.Phone.Tasks;
using HealthCare.Core.Models;
using HealthCare.Core.ViewModels;
using Microsoft.Phone.Notification;

namespace HealthCare.Phone.Views
{
    public partial class HomeView
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void BurgerClick(object sender, RoutedEventArgs e)
        {
            if (this.RootControl.LeftOpen)
                RootControl.Reset();
            else
                RootControl.OpenLeft();
        }

        private void AppBarScheduleAdd(object sender, RoutedEventArgs e)
        {

        }

        private void OnItemSelected(object sender, EventArgs e)
        {
            foreach (var button in TopBar.ChildrenOfType<Button>())
            {
                int tag = 0;
                if (button.Tag != null && int.TryParse(button.Tag.ToString(), out tag))
                {
                    button.Visibility = (RootControl.SelectedIndex == tag) ? Visibility.Visible : Visibility.Collapsed;
                }

                if (button.Tag != null && int.Parse(button.Tag.ToString()) == 5)
                {
                    button.Visibility = Visibility.Visible;
                }
            }
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {

            if (this.RootControl.LeftOpen)
            {
                this.RootControl.Reset();
                e.Cancel = true;
                return;
            }
            MessageBoxResult mr = MessageBox.Show(AppResources.MainPage_ExitMessageBox, AppResources.Exit_titleMessageBox, MessageBoxButton.OKCancel);
            if (mr == MessageBoxResult.OK)
            {
                while (this.NavigationService.BackStack.Any())
                {
                    this.NavigationService.RemoveBackEntry();
                }
                base.OnBackKeyPress(e);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void CallSupportButton(object sender, RoutedEventArgs e)
        {
            CallSupport();
        }

        public static void CallSupport()
        {
            PhoneCallTask phoneCallTask = new PhoneCallTask();
            phoneCallTask.PhoneNumber = Data.SupportPhone;
            phoneCallTask.DisplayName = "BacSi24x7 Support";

            phoneCallTask.Show();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if(e.NavigationMode == NavigationMode.New)
            {
                var bgw = new BackgroundWorker();
                bgw.DoWork += SyncPushNotification;
                bgw.RunWorkerCompleted += SyncPushNotificationCompleted;
                bgw.RunWorkerAsync(); //sta
            };
        }



        private HttpNotificationChannel pushChannel;
        

        private void SyncPushNotification(object sender, DoWorkEventArgs e)
        {


            /// Holds the push channel that is created or found.



            // The name of our push channel.
            string channelName = "MyPushChannel";

            // Try to find the push channel.
            pushChannel = HttpNotificationChannel.Find(channelName);

            // If the channel was not found, then create a new connection to the push service.
            if (pushChannel == null)
            {
                pushChannel = new HttpNotificationChannel(channelName);
                pushChannel.Open();
            }



            // Display the URI for testing purposes. Normally, the URI would be passed back to your web service at this point.
            pushChannel.ChannelUriUpdated +=
                new EventHandler<NotificationChannelUriEventArgs>(PushChannel_ChannelUriUpdated);
            pushChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(PushChannel_ErrorOccurred);
            pushChannel.HttpNotificationReceived += PushChannel_HttpNotificationReceived;
            pushChannel.ShellToastNotificationReceived += PushChannel_ShellToastNotificationReceived;


            // Bind this new channel for Tile events.
            if (!pushChannel.IsShellTileBound)
            {
                Collection<Uri> ListOfAllowedDomains = new Collection<Uri> { new Uri("http://www.clas.mobi") };
                pushChannel.BindToShellTile(ListOfAllowedDomains);
            }

            if (!pushChannel.IsShellToastBound)
                pushChannel.BindToShellToast();

            int count = 0;
            while (pushChannel.ChannelUri == null)
            {
                Thread.Sleep(100);
                count++;
            }
            if (pushChannel.ConnectionStatus == ChannelConnectionStatus.Connected)
            {
                Debug.WriteLine(pushChannel.ChannelUri.OriginalString);
                Data.PushPlatform = "mpns";
                Data.PushChannelUri = pushChannel.ChannelUri.OriginalString;
                Dispatcher.BeginInvoke(async () =>
                    await ((HomeViewModel)ViewModel).SyncPushChannel());
            }

        }

        private void PushChannel_ShellToastNotificationReceived(object sender, NotificationEventArgs e)
        {
            var message = new StringBuilder();

            foreach (string key in e.Collection.Keys)
            {
                message.AppendFormat("{1}\n", key, e.Collection[key]);

                //if (string.Compare(key, "wp:Param", CultureInfo.InvariantCulture, CompareOptions.IgnoreCase) == 0)
                //{
                //}
            }

            // Display a dialog of all the fields in the toast.
            Debug.WriteLine(message.ToString());
            // Dispatcher.BeginInvoke(() => MessageBox.Show(message.ToString()));
        }

        private void PushChannel_HttpNotificationReceived(object sender, HttpNotificationEventArgs e)
        {
            try
            {
                string message;
                using (var reader = new StreamReader(e.Notification.Body))
                {
                    message = reader.ReadToEnd();
                }

                Debug.WriteLine(String.Format("Raw received {0}:\n{1}", DateTime.Now.ToShortTimeString(), message));
                //Dispatcher.BeginInvoke(() =>
                //{
                //    MessageBox.Show(String.Format("Raw received {0}:\n{1}", DateTime.Now.ToShortTimeString(), message));
                //});
            }
            catch
            {
                // Unable to parse the received notification
                Debugger.Break();
            }
        }

        private async void PushChannel_ChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        {
            Debug.WriteLine(e.ChannelUri.ToString());
            //var hub = new NotificationHub("clas-hub-pushhub",
            //    "Endpoint=sb://clas-hub-pushhub-ns.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=vgSuH2mZni6IfZRIIoi6peVqksPVVw6hRE8uGCiOrm0=");
            //await hub.RegisterNativeAsync(e.ChannelUri.ToString());            
        }

        private void PushChannel_ErrorOccurred(object sender, NotificationChannelErrorEventArgs e)
        {
            // Error handling logic for your particular application would be here.
            Debug.WriteLine(String.Format("A push notification {0} error occurred.  {1} ({2}) {3}",
                e.ErrorType, e.Message, e.ErrorCode, e.ErrorAdditionalData));
        }

        private void SyncPushNotificationCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine("SyncPushNotificationCompleted");
        }

        private void SettingConsent()
        {
            if (Data.Setting == null)
            {
                //MessageBoxResult locationSetting =
                //        MessageBox.Show(AppResources.Setting_privacyTextBlock,
                //        AppResources.Location_titleMessageBox,
                //        MessageBoxButton.OKCancel);

                //MessageBoxResult pushSetting =
                //    MessageBox.Show(AppResources.Setting_notificationTextBlock,
                //        AppResources.Notification_titleMessageBox,
                //        MessageBoxButton.OKCancel);

                //MessageBoxResult videoShown =
                //    MessageBox.Show(AppResources.MainPage_videoMessageBox,
                //        AppResources.MainPage_videoTitleMessageBox,
                //        MessageBoxButton.OKCancel);
            }
            else
            {

                //YoutubePlayer.Visibility = Visibility.Collapsed;
                //SetupAppbar();
            }
        }

    }
}