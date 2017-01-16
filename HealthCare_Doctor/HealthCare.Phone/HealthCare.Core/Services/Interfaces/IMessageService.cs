using System.Threading.Tasks;

namespace HealthCare.Core.Services.Interfaces
{
    public interface IMessageService
    {
        void ShowSystemTray(string text, int delay = 2000);
        void HideSystemTray();
        Task ShowMessageAsync(string content, string title);
        Task<bool> ShowConfirmMessageAsync(string content, string title, string yes = "yes", string no = "no");
        Task ShowNoInternetAsync();
    }
}
