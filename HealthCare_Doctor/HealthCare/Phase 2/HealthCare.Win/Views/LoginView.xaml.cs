using System;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using Windows.Security.Cryptography;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using HealthCare.Core.Models;
using HealthCare.Core.Services.Interfaces;
using HealthCare.Core.ViewModels;
using Windows.UI.ViewManagement;
using HealthCare.Core;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HealthCare.Win.Views
{



    public interface IRoot
    {

    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginView : Page, IRoot
    {
        private string userId;
        private string publicKeyHint;
        private string keyId;

        public LoginViewModel ViewModel => DataContext as LoginViewModel;
        public LoginView()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (!ViewModel.Remember)
            {
                userId = ApplicationData.Current.LocalSettings.Values["userId"] as string ?? string.Empty;
                keyId = ApplicationData.Current.LocalSettings.Values["keyId"] as string ?? string.Empty;

                publicKeyHint = ApplicationData.Current.LocalSettings.Values["publicKeyHint"] as string ?? string.Empty;
                await App.AccountManager.RestoreAsync();
                if (!string.IsNullOrEmpty(keyId) && !string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(publicKeyHint))
                {
                    Data.WinHello = true;
                    RegisteredUserName.Text = userId;
                    SignInWithHelloContent.Visibility = Visibility.Visible;
                    LoginWithPasswordContent.Visibility = Visibility.Visible;
                    return;
                }
            }
            SignInWithHelloContent.Visibility = Visibility.Collapsed;
            LoginWithPasswordContent.Visibility = Visibility.Visible;
        }

        private async void SignInWithHello()
        {
            // Core.IoC.Resolve<IReporterService>().ShowProgress();
            LoadingScreen.Visibility = Visibility.Visible;
            //// Don't let the user try a new operation while we are busy with this one.
            SignInWithHelloButton.IsEnabled = false;
            bool result = false;
            try
            {
                 result = await SignInWithHelloAsync();

                InputPane.GetForCurrentView()?.TryHide();
            }
            catch (Exception ex)
            {
                await IoC.Resolve<IMessageService>().ShowMessageAsync(ex.Message, "WinHello ERROR");
            }

            if (result)
            {
                await ViewModel.Login(userId, keyId);
                LoadingScreen.Visibility = Visibility.Collapsed;
            }
            else
            {
                SignInWithHelloContent.Visibility = Visibility;
                LoadingScreen.Visibility = Visibility.Collapsed;
            }
            SignInWithHelloButton.IsEnabled = true;
           
            //      Core.IoC.Resolve<IReporterService>().StopProgress();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            LoadingScreen.Visibility = Visibility.Collapsed;
            base.OnNavigatedFrom(e);
        }

        private async Task<bool> SignInWithHelloAsync()
        {
            // Open the existing user key credential.
            KeyCredentialRetrievalResult retrieveResult = await KeyCredentialManager.OpenAsync(userId);

            if (retrieveResult.Status != KeyCredentialStatus.Success)
            {
                return false;
            }

            KeyCredential userCredential = retrieveResult.Credential;


            // Sign the challenge using the user's KeyCredential.
            IBuffer challengeBuffer = CryptographicBuffer.DecodeFromBase64String(App.AccountManager.Token);
            KeyCredentialOperationResult opResult = await userCredential.RequestSignAsync(challengeBuffer);

            if (opResult.Status != KeyCredentialStatus.Success)
            {
                return false;
            }

            return true;
        }
    }



}
