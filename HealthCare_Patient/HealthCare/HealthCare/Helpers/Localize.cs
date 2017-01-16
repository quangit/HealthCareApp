using System.Globalization;
using System.Reflection;
using System.Resources;
using HealthCare.DependencyServices;
using Xamarin.Forms;

namespace HealthCare.Helpers
{
    public class Localize
    {
        private static readonly CultureInfo ci;

        static Localize()
        {
            ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
        }

        public static string GetString(string key, string comment)
        {
            var temp = new ResourceManager("HealthCare.Resx.AppResources", typeof (Localize).GetTypeInfo().Assembly);

            var result = temp.GetString(key, ci);

            return result;
        }
    }
}