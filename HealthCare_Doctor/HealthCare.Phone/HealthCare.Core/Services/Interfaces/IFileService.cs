using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCare.Core.Services.Interfaces
{
     public interface IFileService
	{
		Task Save(string file, object data);
		Task<T> Load<T>(string file);

        Task Delete(string file);
        Task<Dictionary<string, object>> LoadLocal();
        Task SaveLocal(bool remember, string userName, string password);
        Task SaveSetting();
    }
}
