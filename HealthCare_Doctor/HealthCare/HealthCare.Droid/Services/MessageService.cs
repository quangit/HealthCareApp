using System;
using System.Threading.Tasks;
using Android.App;
using Android.Views;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid.Platform;
using HealthCare.Core.Services.Interfaces;

namespace HealthCare.Droid.Services
{
    public class MessageService : IMessageService
    {

        public void ShowSystemTray(string text, int delay = 2000)
        {
            throw new NotImplementedException();
        }

        public void HideSystemTray()
        {
            throw new NotImplementedException();
        }

        public async Task ShowMessageAsync(string content, string title)
        {
            var top = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            top.Activity.RunOnUiThread(() =>
            {
                AlertDialog.Builder l = new AlertDialog.Builder((new ContextThemeWrapper(top.Activity, Resource.Style.Dialog)));
                l.SetTitle(title);
                l.SetMessage(content);
                l.SetPositiveButton("Ok", (s, e) =>
                {
                    tcs.SetResult(true);
                });

                l.Create().Show();
            });
            await tcs.Task;
        }

        public Activity TopActivity { get; set; }
        public async Task<bool> ShowConfirmMessageAsync(string content, string title, string yes = "yes", string no = "no")
        {
            var top = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            if (top != null)
                TopActivity = top.Activity;
            if (TopActivity == null)
            {
                tcs.TrySetResult(false);
            }
            else
                TopActivity.RunOnUiThread(() =>
                {
                    AlertDialog.Builder l = new AlertDialog.Builder((new ContextThemeWrapper(top.Activity, Resource.Style.Dialog)));
                    l.SetTitle(title);
                    l.SetMessage(content);
                    l.SetPositiveButton(yes, (s, e) =>
                    {
                        l.Dispose();
                        tcs.TrySetResult(true);                        
                    });
                    l.SetNegativeButton(no, (s, e) =>
                    {
                        l.Dispose();
                        tcs.TrySetResult(false);                        
                    });
                    l.Create().Show();
                });
            await tcs.Task;
            return tcs.Task.Result;
        }

        public Task ShowNoInternetAsync()
        {
            return ShowMessageAsync("Failed to connection to server, please check your internet connection or try again later", "Unable to connect");
        }
    }
}
