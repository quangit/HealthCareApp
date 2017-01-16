using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.DependencyServices
{
   public interface ISHA256
    {
        string GenerateSha256(string txt);
    }
}
