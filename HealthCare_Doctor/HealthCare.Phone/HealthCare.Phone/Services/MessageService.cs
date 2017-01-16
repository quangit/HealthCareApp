using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using HealthCare.Core.Services.Interfaces;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace HealthCare.Phone.Services
{
    public class MessageService : IMessageService
    {
        public void ShowSystemTray(string text, int delay = 2000)
        {

            //var applicationView = ApplicationView.GetForCurrentView();
            //applicationView.SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
            SystemTray.SetBackgroundColor(App.RootFrame.Content as DependencyObject, (Color)App.Current.Resources["MainColor"]);
            SystemTray.SetOpacity((App.RootFrame.Content as DependencyObject), 1);
            SystemTray.SetForegroundColor((App.RootFrame.Content as DependencyObject), Colors.White);
            SystemTray.SetProgressIndicator(App.RootFrame.Content as DependencyObject,
                new ProgressIndicator() { Text = text, IsVisible = true, IsIndeterminate = true });
        }


        public void HideSystemTray()
        {
            try
            {
                SystemTray.GetProgressIndicator(App.RootFrame.Content as DependencyObject).IsVisible = false;
            }
            catch (System.Exception)
            {
                //throw;
            }
        }

        public async Task ShowMessageAsync(string content, string title)
        {
            MessageBox.Show(content, title, MessageBoxButton.OK);

        }

        public Task<bool> ShowConfirmMessageAsync(string content, string title, string yes = "yes",
            string no = "no")
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            CustomMessageBox messageBox = new CustomMessageBox
            {
                Title = title,
                Message = content,
                LeftButtonContent = yes,
                RightButtonContent = no
            };
           
            messageBox.Dismissed += (s, e) => { tcs.TrySetResult(e.Result == CustomMessageBoxResult.LeftButton); };
            messageBox.Show();

            return tcs.Task;
        }

        public Task ShowNoInternetAsync()
        {
            return
                ShowMessageAsync(
                    "Failed to connection to server, please check your internet connection or try again later",
                    "Unable to connect");
        }
    }
}