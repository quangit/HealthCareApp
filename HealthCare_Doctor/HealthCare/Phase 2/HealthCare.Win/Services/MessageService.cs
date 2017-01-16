using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using HealthCare.Core;
using HealthCare.Core.Resources;
using HealthCare.Core.Services;
using HealthCare.Core.Services.Interfaces;
using Template10.Controls;

namespace HealthCare.Win.Services
{
    public class ReporterService : IReporterService
    {
        private IMessageService _messageService = IoC.Resolve<IMessageService>();
        public ReporterService()
        {

        }
        public async void ReportError(string error, ErrorType errorType)
        {
            await _messageService.ShowMessageAsync(error, AppResources.Warning);
        }

        public async void ReportMessage(string title, string message)
        {
            await _messageService.ShowMessageAsync(message, title);
        }

        public void ShowProgress()
        {
            var md = Window.Current.Content as ModalDialog;
            if (md != null)
                md.IsModal = true;
        }

        public void StopProgress()
        {
            var md = Window.Current.Content as ModalDialog;
            if (md != null)
                md.IsModal = false;
        }

        public Task<bool> RequestRetry()
        {
            throw new NotImplementedException();
        }

        public event EventHandler<ErrorEventArgs> ErrorReported;
    }
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
            try
            {

                var messageDialog = new MessageDialog(content, title);

                await messageDialog.ShowAsync();

            }
            finally
            {

            }

        }

        public async Task<bool> ShowConfirmMessageAsync(string content, string title, string yes = "yes", string no = "no")
        {
            try
            {

                var messageDialog = new MessageDialog(content, title);
                messageDialog.Commands.Add(new UICommand(yes) { Id = 0 });
                messageDialog.Commands.Add(new UICommand(no) { Id = 1 });
                messageDialog.DefaultCommandIndex = 0;

                var r = await messageDialog.ShowAsync();
                return r != null && (int)r.Id == 0;
            }
            finally
            {

            }
        }

        public Task ShowNoInternetAsync()
        {
            throw new NotImplementedException();
        }
    }
}
