using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.DependencyServices;
using HealthCare.WinPhone.DependencyServices;

[assembly: Xamarin.Forms.Dependency(typeof(Localize))]
namespace HealthCare.WinPhone.DependencyServices
{
    public class Localize : ILocalize
    {
        public void SetLocale()
        {
            //
        }

        public System.Globalization.CultureInfo GetCurrentCultureInfo()
        {
            return System.Threading.Thread.CurrentThread.CurrentUICulture;
        }
    }
}
