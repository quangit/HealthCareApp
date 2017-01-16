using System;
using System.Threading.Tasks;
using HealthCare.Services.Interfaces;
using Xamarin.Forms;

namespace HealthCare.Services
{
    public class HcDialogService : IHcDialogService
    {
        private readonly string _title = "HealthCare";
        private Page _dialogPage;
        public bool IsInitialized => _dialogPage != null;

        public async Task ShowError(string message,
            string buttonText,
            Action afterHideCallback)
        {
            if (message.Contains(" ?"))
                message = message.Replace(" ?", "?");
            await _dialogPage.DisplayAlert(
                _title,
                message,
                buttonText);

            if (afterHideCallback != null)
            {
                afterHideCallback();
            }
        }

        public async Task ShowError(
            Exception error,
            string buttonText,
            Action afterHideCallback)
        {
            await _dialogPage.DisplayAlert(
                _title,
                error.Message,
                buttonText);

            if (afterHideCallback != null)
            {
                afterHideCallback();
            }
        }

        public async Task ShowError(Exception error)
        {
            await ShowMessage(error.Message);
        }

        public async Task ShowMessage(
            string message)
        {
            if (message.Contains(" ?"))
                message = message.Replace(" ?", "?");
            await _dialogPage.DisplayAlert(
                _title,
                message,
                "OK");
        }

        public async Task ShowMessage(
            string message,
            string buttonText,
            Action afterHideCallback)
        {
            if (message.Contains(" ?"))
                message = message.Replace(" ?", "?");
            await _dialogPage.DisplayAlert(
                _title,
                message,
                buttonText);

            if (afterHideCallback != null)
            {
                afterHideCallback();
            }
        }

        public async Task<bool> ShowMessage(
            string message,
            string buttonConfirmText,
            string buttonCancelText,
            Action<bool> afterHideCallback)
        {
            if (message.Contains(" ?"))
                message = message.Replace(" ?", "?");

            var result = await _dialogPage.DisplayAlert(
                _title,
                message,
                buttonConfirmText,
                buttonCancelText);

            if (afterHideCallback != null)
            {
                afterHideCallback(result);
            }

            return result;
        }

        public async Task ShowMessageBox(
            string message)
        {
            if (message.Contains(" ?"))
                message = message.Replace(" ?", "?");

            await _dialogPage.DisplayAlert(
                _title,
                message,
                "OK");
        }

        public void Initialize(Page dialogPage)
        {
            _dialogPage = dialogPage;
        }
    }
}