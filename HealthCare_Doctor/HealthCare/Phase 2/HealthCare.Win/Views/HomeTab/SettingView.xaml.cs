using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.Security.Credentials;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using HealthCare.Core.Models;
using HealthCare.Core.Resources;
using HealthCare.Core.Services.Interfaces;
using HealthCare.Core.ViewModels;
using Template10.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HealthCare.Win.Views.HomeTab
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingView : Page
    {
        public HomeViewModel ViewModel { get; set; }
        public SettingView()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel = Shell.ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var r = ApplicationData.Current.LocalSettings.Values.ContainsKey("userId")
                        ? ApplicationData.Current.LocalSettings.Values["userId"]
                        : string.Empty;
            AccountTextBlock.Text = $"({r})";
            WinHelloEnableContent.Visibility = Data.WinHello ? Visibility.Visible : Visibility.Collapsed;
            WinHelloDiableContent.Visibility = Data.WinHello ? Visibility.Collapsed : Visibility.Visible;
            BootStrapper.Current.NavigationService.Frame.BackStack.Clear();
            BootStrapper.Current.NavigationService.ClearHistory();
            BootStrapper.Current.NavigationService.ClearCache(true);

        }

        private string userId => Data.UserName;
        private async Task<IBuffer> CreatePassportKeyCredentialAsync()
        {

            // Create a new KeyCredential for the user on the device.
            KeyCredentialRetrievalResult keyCreationResult = await KeyCredentialManager.RequestCreateAsync(userId, KeyCredentialCreationOption.ReplaceExisting);
            if (keyCreationResult.Status == KeyCredentialStatus.Success)
            {
                // User has autheniticated with Windows Hello and the key credential is created.
                KeyCredential userKey = keyCreationResult.Credential;
                return userKey.RetrievePublicKey();
            }
            else if (keyCreationResult.Status == KeyCredentialStatus.NotFound)
            {
                MessageDialog message = new MessageDialog(AppResources.Setting_HelloOptions);
                await message.ShowAsync();

                return null;
            }
            else if (keyCreationResult.Status == KeyCredentialStatus.UnknownError)
            {
                MessageDialog message = new MessageDialog("The key credential could not be created. Please try again.");
                await message.ShowAsync();

                return null;
            }

            return null;
        }

        private async Task<bool> RegisterPassportCredentialWithServerAsync(IBuffer publicKey)
        {
            // Include the name of the current device for the benefit of the user.
            // The server could support a Web interface that shows the user all the devices they
            // have signed in from and revoke access from devices they have lost.

            var hostNames = NetworkInformation.GetHostNames();
            var localName = hostNames.FirstOrDefault(name => name.DisplayName.Contains(".local"));
            string computerName = localName.DisplayName.Replace(".local", "");

            App.AccountManager.UserId = userId;
            App.AccountManager.Username = Data.UserName;
            App.AccountManager.Password = Data.Password;
            App.AccountManager.Token = CryptographicBuffer.EncodeToBase64String(publicKey);

            await App.AccountManager.StoreAsync();

            //TODO
            //JsonValue jsonResult = await JsonRequest.Create()
            //    .AddString("userId", userId)
            //    .AddString("publicKey", CryptographicBuffer.EncodeToBase64String(publicKey))
            //    .AddString("deviceName", computerName)
            //    .PostAsync("api/Authentication/Register");

            //bool result = (jsonResult != null) && jsonResult.GetBoolean();

            return true;
        }

        private async void StartUsingWindowsHello()
        {
            Core.IoC.Resolve<IReporterService>().ShowProgress();

            try
            {
                ToggleSwitchHello.IsOn = true;
                StartUsingWindowsHelloButton.IsEnabled = false;

                // Create the key credential with Passport APIs
                IBuffer publicKey = await CreatePassportKeyCredentialAsync();
                if (publicKey != null)
                {
                    // Register the public key and attestation of the key credential with the server
                    // In a real-world scenario, this would likely also include:
                    // - Certificate chain for attestation endorsement if available
                    // - Status code of the Key Attestation result : Included / retrieved later / retry type
                    if (await RegisterPassportCredentialWithServerAsync(publicKey))
                    {
                        // When communicating with the server in the future, we pass a hash of the
                        // public key in order to identify which key the server should use to verify the challenge.
                        HashAlgorithmProvider hashProvider = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha256);
                        IBuffer publicKeyHash = hashProvider.HashData(publicKey);

                        // Remember that this is the user whose credentials have been registered
                        // with the server.
                        ApplicationData.Current.LocalSettings.Values["userId"] = userId;
                        ApplicationData.Current.LocalSettings.Values["keyId"] = Data.Password;
                        ApplicationData.Current.LocalSettings.Values["publicKeyHint"] = CryptographicBuffer.EncodeToBase64String(publicKeyHash);

                        // Registration successful. Continue to the signed-in state.
                        //Frame.Navigate(typeof(AccountOverviewPage));
                    }
                    else
                    {
                        // Delete the failed credentials from the device.
                        await Util.TryDeleteCredentialAccountAsync(userId);

                        MessageDialog message = new MessageDialog("Failed to register with the server.");
                        await message.ShowAsync();
                    }
                    Data.WinHello = true;
                    var r = ApplicationData.Current.LocalSettings.Values.ContainsKey("userId")
                        ? ApplicationData.Current.LocalSettings.Values["userId"]
                        : string.Empty;
                    AccountTextBlock.Text = $"({r})";
                    WinHelloEnableContent.Visibility = Data.WinHello ? Visibility.Visible : Visibility.Collapsed;
                    WinHelloDiableContent.Visibility = Data.WinHello ? Visibility.Collapsed : Visibility.Visible;
                }
              
            }
            catch (Exception ex)
            {
                await Core.IoC.Resolve<IMessageService>().ShowMessageAsync(ex.Message,"Error");
            }
            Core.IoC.Resolve<IReporterService>().StopProgress();
            StartUsingWindowsHelloButton.IsEnabled = true;
        }

        public async void DisableHello()
        {
            try
            {
                Data.WinHello = false;
                WinHelloEnableContent.Visibility = Data.WinHello ? Visibility.Visible : Visibility.Collapsed;
                WinHelloDiableContent.Visibility = Data.WinHello ? Visibility.Collapsed : Visibility.Visible;
                ApplicationData.Current.LocalSettings.Values.Remove("publicKeyHint");
                ApplicationData.Current.LocalSettings.Values.Remove("userId");
                ToggleSwitchHello.IsOn = true;
            }
            catch (Exception)
            {//throw;
            }
        }

        class Util
        {
            const int NTE_NO_KEY = unchecked((int)0x8009000D);
            const int NTE_PERM = unchecked((int)0x80090010);

            public static async Task<bool> TryDeleteCredentialAccountAsync(string userId)
            {
                bool deleted = false;

                try
                {
                    await KeyCredentialManager.DeleteAsync(userId);
                    deleted = true;
                }
                catch (Exception ex)
                {
                    switch (ex.HResult)
                    {
                        case NTE_NO_KEY:
                            // Key is already deleted. Ignore this error.
                            break;
                        case NTE_PERM:
                            // Access is denied. Ignore this error. We tried our best.
                            break;
                        default:
                            throw;
                    }
                }
                return deleted;
            }
        }

        private void Image_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            ViewModel.BandSetting.SyncCommand.Execute(null);
        }

        private void UIElement_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            DisableHello();
        }
    }
}
