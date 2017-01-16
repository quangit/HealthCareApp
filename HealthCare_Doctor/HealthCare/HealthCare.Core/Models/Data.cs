using System;
using System.Collections.Generic;

namespace HealthCare.Core.Models
{
    public class Data
    {
        public enum Os
        {
            Android,
            iOS,
            WinPhone,
        }
#if MVVMCROSS
        public static HealthCare.Core.ViewModels.MyMvxViewModel CurrentVm;
        public static HealthCare.Core.ViewModels.MyMvxViewModel PreviousVm;
#endif
        public static DateTime day;
        public static Os Platform;

        private static readonly Dictionary<string, object> PageParameter = new Dictionary<string, object>();
            //PageName, Object

        public static LoginResult User { get; set; }
        public static string PushPlatform { get; set; }

        public const string SupportPhone = "+84862747451";
		public const int LoadLength = 10;

        public static Setting Setting = null;
		public static BandSetting BandSetting;
        public static string PushChannelUri { get; set; }
        public static string UserName { get; set; }
        public static string Password { get; set; }
        public static bool Remember { get; set; }
        public static bool PushSync { get; set; }
        public static bool WinHello { get; set; }

        static Data()
        {
            PushSync = true;
        }
        public static void PageParamAdd(string page, object param)
        {
            if (!PageParameter.ContainsKey(page))
                PageParameter.Add(page, param);
            else
                PageParameter[page] = param;
        }

        public static object PageParamGet(string page)
        {
            object param;
            PageParameter.TryGetValue(page, out param);
            return param;
        }
    }
}