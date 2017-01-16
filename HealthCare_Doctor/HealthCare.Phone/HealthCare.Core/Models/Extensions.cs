using System;
using HealthCare.Core.Models.Enums;
using Newtonsoft.Json;
using HealthCare.Core.Resources;

namespace HealthCare.Core.Models
{
    public static class Extensions
    {
        public static T Clone<T>(this T obj)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
        }

        public static string ToResourceString(this Enum obj)
        {
            var r = AppResources.ResourceManager.GetString(obj.GetType().Name + "_" + obj.ToString());
            if (string.IsNullOrEmpty(r))
                return obj.ToString();
            return r;

        }

        public static void test()
        {
            DoctorDayOfWeek.Friday.ToResourceString();
        }
    }
}