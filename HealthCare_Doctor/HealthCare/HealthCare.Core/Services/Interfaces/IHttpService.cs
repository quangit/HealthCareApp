using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HealthCare.Core.Services.Interfaces
{
    public interface IHttpService
    {
        Task<string> GetAsync(string p, Dictionary<string, string> data);
        Task<string> GetAsync(string url);
        Task<string> GetAsync(string url, CancellationToken token);        
        Task<string> PostAsync(string url, Dictionary<string, string> data);
        Task<string> PostAsync(string url, string data);
        Task<string> PostAsync(string url, string data, string dataType);        
        Task<string> PutAsync(string url, HttpContent content);
        Task<bool> NetCheck();
        void SetHeader(string key, string value);
        Task<string> PostFormAync(string url, Dictionary<string, object> data);
    }
}
