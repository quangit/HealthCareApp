using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HealthCare.Enums;
using HealthCare.Models;
using HealthCare.Objects;

namespace HealthCare.WebServices.Interfaces
{
    public interface ICommonWS
    {
        Task<SystemConfig> GetSystemConfig();
        Task<ObservableCollection<CityModel>> GetCityList();

        Task SendAdditionalSupport(string firstName, string lastName, string age, Gender gender,
            string email, string doctorName, string hospitalName, string symptom);
        Task SendAdditionalSupport(string symptom, byte[] arrayImg, bool shared);

        Task<string> RegisterDevice(string channel, string platform, string registrationId);

        Task SendOtp(string phoneNumber, string otp);
    }
}