using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.DependencyServices
{
    public interface IChBaseHash
    {
        string Hmac(string key, string header);
        string InfoHash(string info);
        Task<string> SigHash(string content);
    }
}
