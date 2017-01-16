using System.Globalization;

namespace HealthCare.DependencyServices
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();
        void SetLocale();
    }
}