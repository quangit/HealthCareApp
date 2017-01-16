using System.Threading.Tasks;

namespace HealthCare.DependencyServices
{
    public interface IFacebookHelper
    {
        void Login();

        /// <summary>
        ///     Get info of faecbook user and save it
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        Task GetMe(string accessToken);

        bool IsLoginInProgress();
    }
}