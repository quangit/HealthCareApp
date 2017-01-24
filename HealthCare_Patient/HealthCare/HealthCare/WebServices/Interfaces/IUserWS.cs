using System.Threading.Tasks;
using HealthCare.Models;
using Newtonsoft.Json.Linq;

namespace HealthCare.WebServices.Interfaces
{
    public interface IUserWS
    {
        Task<JObject> Login(string userName, string password, string timeZone);
        Task<JObject> LoginWithFacebook(string facebookId);
        Task<JObject> RefreshData();
        Task ResetPassword(string email);
        Task ChangeUserPassword(string userId, string oldPassword, string newPassword);
        Task<UserModel> EditProfile(UserModel model, byte[] avatar);
        Task<bool> ValidateSkype(string skypeId);
        Task<int> CheckExistEmail(string email);
        Task<UserModel> Register(UserModel newAccount, byte[] avatar);
        Task<UserModel> RegisterWithFb(UserModel newAccount, byte[] avatar);
    }
}