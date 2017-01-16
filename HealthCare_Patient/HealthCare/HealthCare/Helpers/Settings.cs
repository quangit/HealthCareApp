using System;
using HealthCare.DependencyServices;
using HealthCare.Models;
using Refractored.Xam.Settings;
using Refractored.Xam.Settings.Abstractions;
using Xamarin.Forms;

namespace HealthCare.Helpers
{
    public static class Settings
    {

        private const string ULang = "language";
        private const string USessionId = "SESSION_ID";
        private const string USessionExpired = "session_expired";
        private const string UId = "USER_ID";
        private const string UCode = "USER_CODE";
        private const string UEmail = "u_email";
        private const string UPhone = "u_phone";
        private const string UPassword = "u_password";
        private const string UFacebookid = "u_facebook_id";
        private const string UFbaccesstoken = "u_facebook_accesstoken";
        private const string UFbaccesstokenLogout = "u_facebook_accesstoken_logout";
        private const string KRegisterSuccessCount = "register_success_count";
        private const string UAddCheckupLastTime = "add_checkup_last_time";
        private const string UDeviceChannel = "u_device_channel";
        private const string URegistrationId = "registration_id";
        private const string UPromotionCount = "u_promotion_count";
        private const string URegistrationLimitPerDevice = "u_registration_limit_per_device";
        private const string UCheckupLimitPerAccountPerDay = "u_checkup_limit_peracc_perday";
        private const string UCanReceivePushNotify = "u_can_receive_push_notify";
        private const string UChBaseEmail = "u_chbase_email";
        private const string UChBasePass = "u_chbase_pass";


        private static readonly string StringDefault = string.Empty;
        private static ISettings AppSettings => CrossSettings.Current;

        public static bool CanReceivePushNotify
        {
            get { return AppSettings.GetValueOrDefault(UCanReceivePushNotify, true); }
            set { AppSettings.AddOrUpdateValue(UCanReceivePushNotify, value); }
        }

        public static string DeviceChannel
        {
            get { return AppSettings.GetValueOrDefault(UDeviceChannel, ""); }
            set { AppSettings.AddOrUpdateValue(UDeviceChannel, value); }
        }

        public static string RegistrationId
        {
            get { return AppSettings.GetValueOrDefault(URegistrationId, ""); }
            set { AppSettings.AddOrUpdateValue(URegistrationId, value); }
        }

        public static long SessionExpired
        {
            get { return AppSettings.GetValueOrDefault<long>(USessionExpired, 0); }
            set { AppSettings.AddOrUpdateValue(USessionExpired, value); }
        }

        public static string SessionId
        {
            get { return AppSettings.GetValueOrDefault(USessionId, StringDefault); }
            set { AppSettings.AddOrUpdateValue(USessionId, value); }
        }

        public static int RegisterSuccessCount
        {
            get { return AppSettings.GetValueOrDefault(KRegisterSuccessCount, 0); }
            set { AppSettings.AddOrUpdateValue(KRegisterSuccessCount, value); }
        }

        public static long AddCheckupLastTime
        {
            get { return Convert.ToInt64(AppSettings.GetValueOrDefault(UAddCheckupLastTime, "0")); }
            set { AppSettings.AddOrUpdateValue(UAddCheckupLastTime, value.ToString()); }
        }

        public static string UserId
        {
            get { return AppSettings.GetValueOrDefault(UId, StringDefault); }
            set { AppSettings.AddOrUpdateValue(UId, value); }
        }

        public static string Language
        {
            get { return AppSettings.GetValueOrDefault(ULang, "vi"); }
            set { AppSettings.AddOrUpdateValue(ULang, value); }
        }

        public static UserModel CurrentUser
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(AppSettings.GetValueOrDefault(UCode, StringDefault)))
                    return new UserModel
                    {
                        Id = AppSettings.GetValueOrDefault(UId, StringDefault),
                        UserCode = AppSettings.GetValueOrDefault(UCode, StringDefault),
                        Email = AppSettings.GetValueOrDefault(UEmail, StringDefault),
                        FacebookId = AppSettings.GetValueOrDefault(UFacebookid, StringDefault),
                        Password = AppSettings.GetValueOrDefault(UPassword, StringDefault),
                        PhoneNo = AppSettings.GetValueOrDefault(UPhone, StringDefault)
                    };
                return new UserModel();
            }
            set
            {
                AppSettings.AddOrUpdateValue(UId, value.Id);
                AppSettings.AddOrUpdateValue(UCode, value.UserCode);
                AppSettings.AddOrUpdateValue(UEmail, value.Email);
                AppSettings.AddOrUpdateValue(UFacebookid, value.FacebookId);
                if (!string.IsNullOrWhiteSpace(value.Password))
                    AppSettings.AddOrUpdateValue(UPassword, value.Password);
                AppSettings.AddOrUpdateValue(UPhone, value.PhoneNo);
            }
        }

        public static string FacebookAccessToken
        {
            get { return AppSettings.GetValueOrDefault(UFbaccesstoken, StringDefault); }
            set { AppSettings.AddOrUpdateValue(UFbaccesstoken, value); }
        }

        public static string FacebookAccessTokenLogout
        {
            get { return AppSettings.GetValueOrDefault(UFbaccesstokenLogout, StringDefault); }
            set { AppSettings.AddOrUpdateValue(UFbaccesstokenLogout, value); }
        }

        public static void Reset()
        {
            CurrentUser = new UserModel();
            SessionExpired = 0;
            ChBaseToken =   RegistrationId = DeviceChannel = FacebookAccessToken = SessionId = UserId = ChBasePass = ChBaseEmail = string.Empty;
            // Clear cache
            ClearCacheChBase();
        }

        public static void ClearCacheChBase()
        {
            ChBasePass = ChBaseEmail = string.Empty;
            var service = DependencyService.Get<IClearCacheWebView>();
            service.ClearWebViewCache();
        }
        public static HealthCare.Objects.SystemConfig SystemConfig
        {
            get
            {
                return new HealthCare.Objects.SystemConfig
                {
                    MaxRegistrationLimitPerDevice =
                        AppSettings.GetValueOrDefault(URegistrationLimitPerDevice, default(int)),
                    MaxCheckupPerAccountPerDay =
                        AppSettings.GetValueOrDefault(UCheckupLimitPerAccountPerDay, default(int)),
                    PromotionCount = AppSettings.GetValueOrDefault(UPromotionCount, default(int))
                };
            }
            set
            {
                AppSettings.AddOrUpdateValue(URegistrationLimitPerDevice, value.MaxRegistrationLimitPerDevice);
                AppSettings.AddOrUpdateValue(UCheckupLimitPerAccountPerDay, value.MaxCheckupPerAccountPerDay);
                AppSettings.AddOrUpdateValue(UPromotionCount, value.PromotionCount);
            }
        }

        public static string ChBaseEmail
        {
            get { return AppSettings.GetValueOrDefault(UChBaseEmail, StringDefault); }
            set { AppSettings.AddOrUpdateValue(UChBaseEmail, value); }
        }
        public static string ChBasePass
        {
            get { return AppSettings.GetValueOrDefault(UChBasePass, StringDefault); }
            set { AppSettings.AddOrUpdateValue(UChBasePass, value); }
        }

        public static string ChBaseToken
        {
            get { return AppSettings.GetValueOrDefault(UChBasePass, StringDefault); }
            set { AppSettings.AddOrUpdateValue(UChBasePass, value); }
        }
    }
}