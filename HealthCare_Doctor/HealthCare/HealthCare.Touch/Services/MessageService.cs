using System;
using System.Threading.Tasks;
using HealthCare.Core.Services.Interfaces;
using LumiaLoyalty.Touch.Utilities;

namespace HealthCare.Touch.Services
{
    public class MessageService : IMessageService
    {
        public void HideSystemTray()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ShowConfirmMessageAsync(string content, string title, string yes = "yes", string no = "no")
        {
            TaskCompletionSource<bool> task = new TaskCompletionSource<bool>();
            MessageBox.ShowOKCancel(title, content, (a) =>
            {
                task.SetResult(a);
            }, MessageBoxButton.YesNo);
            return task.Task;
        }

        public Task ShowMessageAsync(string content, string title)
        {
            TaskCompletionSource<bool> task = new TaskCompletionSource<bool>();
            MessageBox.Show(title, content, MessageBoxButton.Ok, () =>
             {
                 task.SetResult(true);
             }, () =>
             {
                 task.SetResult(false);
             });

            return task.Task;
        }

        public Task ShowNoInternetAsync()
        {
            throw new NotImplementedException();
        }

        public void ShowSystemTray(string text, int delay = 2000)
        {
            throw new NotImplementedException();
        }
    }
}