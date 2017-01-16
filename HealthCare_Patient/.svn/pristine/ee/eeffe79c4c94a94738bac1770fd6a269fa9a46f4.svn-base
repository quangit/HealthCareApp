using System;
using System.Reflection;
using HealthCare.Resx;

namespace HealthCare.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string errorDesc) : base(errorDesc)
        {
            ErrorCode = ResponseCode.HC_MOBILE_EXCEPTION;
            ErrorDesc = errorDesc;
        }

        public ApiException(string errorDesc, ResponseCode errorCode) : base(errorDesc)
        {
            ErrorCode = errorCode;
            ErrorDesc = errorDesc;
        }

        public ResponseCode ErrorCode { get; }
        private string ErrorDesc { get; }
        public override string Message => GetMessage(ErrorCode, ErrorDesc);

        private string GetMessage(ResponseCode v, string errDesc)
        {
            var key = "rs_" + v.ToString().ToLower();
            if (!string.IsNullOrWhiteSpace(key) && GetString(key) != null)
                return GetString(key) ?? errDesc;
            return errDesc;
        }

        private static string GetString(string key)
        {
            try
            {
                var propertyInfo = typeof(AppResources).GetTypeInfo().GetDeclaredProperty(key);
                var r = propertyInfo.GetValue(null, null);
                return r != null ? (string)r : "";
            }
            catch
            {
                return null;
            }
        }
    }


    public enum ResponseCode
    {
        HC_MOBILE_EXCEPTION = -98,
        SUCCESS = 0,
        FAILURE_DISABLE_USER = -3,
        FAILURE_UNKNOWN = -2,
        FAILURE_EXCEPTION = -1,
        FAILURE_INVALID_PARAMS = 1,
        FAILURE_SESSION_INVALID = 2,
        FAILURE_LOGIN_FAIL = 3,
        FAILURE_OBJECT_NOT_FOUND = 4,
        FAILURE_PERMISSION_DENY = 5,
        FAILURE_INVALID_PHONE = 7,
        FAILURE_INVALID_EMAIL = 8,
        FAILURE_USER_NOT_FOUND = 9,
        FAILURE_PHONE_EXISTED = 11,
        FAILURE_EMAIL_EXISTED = 12,
        FAILURE_USER_EXISTED = 13,
        FAILURE_FB_ACCESS_TOKEN_INVALID = 14,
        FAILURE_INVALID_LOCATION = 15,
        FAILURE_HOSPITAL_NOT_FOUND = 16,
        FAILURE_USERNAME_EXISTED = 17,
        FAILURE_IDNO_EXISTED = 18,
        FAILURE_FACEBOOKID_EXISTED = 19,
        FAILURE_NAME_EMPTY = 20,
        FAILURE_CITY_EMPTY = 21,
        FAILURE_ADDRESS_EMPTY = 22,
        FAILURE_PHOTO_EMPTY = 23,
        FAILURE_FIRSTNAME_EMPTY = 24,
        FAILURE_LASTNAME_EMPTY = 25,
        FAILURE_GENDER_EMPTY = 26,
        FAILURE_BIRTHDAY_EMPTY = 27,
        FAILURE_USERNAME_EMPTY = 28,
        FAILURE_PASSWORD_EMPTY = 29,
        FAILURE_HOSPITALID_EMPTY = 30,
        FAILURE_IDNO_EMPTY = 31,
        FAILURE_CONTENT_EMPTY = 32,
        FAILURE_INVALID_STARTDATE = 33,
        FAILURE_INVALID_ENDDATE = 34,
        FAILURE_INVALID_USERNAME = 35,
        FAILURE_INVALID_USER_TYPE = 36,
        FAILURE_INVALID_ROLES = 37,
        FAILURE_PROMOTION_NOT_FOUND = 38,
        FAILURE_CHECKUP_TYPE_NOT_FOUND = 39,
        FAILURE_SYMPTOM_EMPTY = 40,
        FAILURE_CHECKUP_TYPE_EMPTY = 41,
        FAILURE_PATIENT_TYPE_EMPTY = 42,
        FAILURE_INVALID_PATIENT_TYPE = 43,
        FAILURE_DOCTOR_NOT_FOUND = 44,
        FAILURE_SERVICE_TYPE_EMPTY = 45,
        FAILURE_INVALID_SERVICE_TYPE = 46,
        FAILURE_INVALID_CHECKUP_TYPE = 47,
        FAILURE_HOSPITAL_REGISTERED = 48,
        FAILURE_DOCTOR_ID_EMPTY = 49,
        FAILURE_INVALID_APPOINTMENT_DATE = 50,
        FAILURE_INVALID_GENDER = 51,
        FAILURE_ID_EMPTY = 52,
        FAILURE_CHECKUP_NOT_FOUND = 53,
        FAILURE_INVALID_DOCTOR = 54,
        FAILURE_CARD_ID_EMPTY = 55,
        FAILURE_PATIENT_ID_EMPTY = 56,
        FAILURE_FIRST_NAME_EMPTY = 57,
        FAILURE_LAST_NAME_EMPTY = 58,
        FAILURE_EMAIL_EMPTY = 59,
        FAILURE_PHONE_EMPTY = 60,
        FAILURE_INVALID_IDNO = 61,
        FAILURE_CARD_EXISTED = 62,
        FAILURE_CARD_ENTER_PIN = 63,
        FAILURE_CARD_ENTER_OTP = 64,
        FAILURE_CARD_NOT_FOUND = 65,
        FAILURE_PIN_OR_OTP_EMPTY = 66,
        FAILURE_CARD_INCORRECT_PIN = 67,
        FAILURE_INVALID_START_MORNING_HOUR = 68,
        FAILURE_INVALID_START_MORNING_MINUTE = 69,
        FAILURE_INVALID_END_MORNING_HOUR = 70,
        FAILURE_INVALID_END_MORNING_MINUTE = 71,
        FAILURE_INVALID_START_AFTERNOON_HOUR = 72,
        FAILURE_INVALID_START_AFTERNOON_MINUTE = 73,
        FAILURE_INVALID_END_AFTERNOON_HOUR = 74,
        FAILURE_INVALID_END_AFTERNOON_MINUTE = 75,
        FAILURE_INVALID_CHECKUP_INTERVAL = 76,
        FAILURE_INVALID_REGULAR_PATIENT_THRESHOLD = 77,
        FAILURE_NO_DOCTOR_FOR_CHECKUP_TYPE = 78,
        FAILURE_INVALID_PRICE = 79,
        FAILURE_INVALID_HOSPITAL = 80,
        FAILURE_BUSY_DOCTOR = 81,
        FAILURE_INVALID_PASSWORD_LENGTH = 82,
        FAILURE_INVALID_OLD_PASSWORD = 83,
        FAILURE_CHECKUP_ID_EMPTY = 84,
        FAILURE_INVALID_PAYMENT_PASSWORD = 85,
        FAILURE_PAYMENT_PASSWORD_NOT_SET = 86,
        FAILURE_HOSPITAL_NOT_REGISTERED_PAYMENT = 87,
        FAILURE_CHECKUP_PAYMENTED = 88,
        FAILURE_INVALID_CARD = 89,
        FAILURE_NOT_ENOUGH_MONEY_FOR_PAYMENT = 90,
        FAILURE_INVALID_ZERO_MONEY = 91,

        FAILURE_INVALID_PATIENT = 92,
        FAILURE_CHECKUP_FINISHED = 93,
        FAILURE_SCB_MID_EMPTY = 94,
        FAILURE_SCB_TID_EMPTY = 95,
        FAILURE_STORE_MID_NOT_FOUND = 96,
        FAILURE_STORE_REGISTERED = 97,
        FAILURE_FACEBOOK_ID_EMPTY = 98,
        FAILURE_TYPE_EMPTY = 99,
        FAILURE_DEACTIVE_REFERENCE = 100,

        FAILURE_HASH_EXPIRED = 102,

        FAILURE_NAME_EXISTED = 109,
        FAILURE_OVERLAP_TIME = 114,
        FAILURE_CHECKUP_TYPE_IN_USE = 126,
        FAILURE_CANNOT_DELETE_REF_DATA = 129,
        FAILURE_DATA_IN_USE = 136,
        FAILURE_GTM_TIME_NOT_EMPTY = 139,
        FAILURE_GTM_TIME_NOT_VALID = 140,
        FAILURE_SMALL_MONEY = 146,

        FAILURE_INVALID_OTP = 158,
        FAILURE_PIN_TRIES_EXCEEDED = 160,

    }
}