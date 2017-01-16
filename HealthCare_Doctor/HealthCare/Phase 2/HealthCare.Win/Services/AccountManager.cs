using System;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace HealthCare.Win.Services
{
    public class AccountManager
    {
        private const string RESOURCE_NAME_USER = "HealthCareCredentialUser";
        private const string RESOURCE_NAME_EMAIL = "HealthCareCredentialEmail";
        public int AuthenticationProvider { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public AccountManager()
        {
            AuthenticationProvider = 99;
        }
        public Task<bool> RestoreAsync()
        {
            
            var vault = new PasswordVault();
            try
            {

                foreach (var credential in vault.RetrieveAll())
                {
                    credential.RetrievePassword();
                    if (credential.Resource.Equals(RESOURCE_NAME_EMAIL))
                    {
                        Username = credential.UserName;
                        Password = credential.Password;
                    }else if (credential.Resource.Equals(RESOURCE_NAME_USER))
                    {
                        AuthenticationProvider = int.Parse(credential.UserName.Split(':')[0]);
                        UserId = credential.UserName.Split(':')[1];
                        Token = vault.Retrieve(RESOURCE_NAME_USER, credential.UserName).Password;
                    }

                }
                
                return Task.FromResult(true);

            }
            catch (Exception)
            {
                // If no credentials have been stored with the given RESOURCE_NAME, an exception
                // is thrown.
            }
            return Task.FromResult(false);
        }

        public async Task<bool> StoreAsync()
        {
          
            try
            {
                var vault = new PasswordVault();
                await RemoveAsync();

                if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
                    vault.Add(new PasswordCredential(RESOURCE_NAME_EMAIL, Username, Password));
                if (!string.IsNullOrEmpty(UserId) && !string.IsNullOrEmpty(Token))
                    vault.Add(new PasswordCredential(RESOURCE_NAME_USER, AuthenticationProvider + ":" + UserId, Token));

                return true;
            }
            catch (Exception)
            {
                //throw;
            }
            return false;
        }

        public Task<bool> RemoveAsync()
        {
          
            var vault = new PasswordVault();
            try
            {
                // Removes the credential from the password vault.
                foreach (var credential in vault.RetrieveAll())
                {
                    vault.Remove(credential);
                }

                return Task.FromResult(true);
            }
            catch (Exception)
            {
                // If no credentials have been stored with the given RESOURCE_NAME, an exception
                // is thrown.
            }
            return Task.FromResult(false);
        }
    }
}