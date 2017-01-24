using HealthCare.Helpers;

namespace HealthCare
{
    public static class AppConstant
    {
        public static double ScreenWidth;
        public static string SymptomIOS;
        public static bool IsChangePin;
        public static double Height9X16 => ScreenWidth * 9 / 16;
        
        public const string VnPayCreaditCardId = "VNPAY";
        public const string VtcPayCreaditCardId = "VTCPAY";

        public const string IdAllItems = "Id_AllItems_HealthCare_04112015";
        public const int DelayBinding = 300; // in milliseconds
        public const int DelaySearchSuggestions = 300; // in milliseconds
        public static readonly int ITEMS_PER_PAGE = 10;

        public struct TypeOS
        {
            public const string Android = "gcm";
            public const string Ios = "apns";
            public const string Wp = "wns";
        }


        #region Url used for call API
        public const string AboutBlank = "about:blank";
        public const string AvatarPatientMale = "http://i1073.photobucket.com/albums/w399/thanh_quang5/avatar_patient_male_zpsf7ki6qwk.jpg";
        public const string AvatarPatientFeMale = "http://i1073.photobucket.com/albums/w399/thanh_quang5/avatar_patient_female_zpsy9kwbizh.jpg";
        public const string AvatarDoctorMale = "http://i1073.photobucket.com/albums/w399/thanh_quang5/avatar_doctor_male_zpstojtjcco.jpg";
        public const string AvatarDoctorFeMale = "http://i1073.photobucket.com/albums/w399/thanh_quang5/avatar_doctor_female_zpsvgo7pcvt.jpg";

        public static string ChBaseApiUrl
        {
            get
            {
                int culId = 1033;
                if (Common.GetDeviceLanguage() == "vi")
                    culId = 1066;
                return "https://qa-grh-shell-sa.chbase.com/signin.aspx?appid=7dba8069-110c-4952-9b1f-9d2859480100&redirect=http%3a%2f%2fgoogle.com&actionqs=%2fMedicalPage.aspx&aib=true&trm=post&lcid=" + culId;
            }
        }


        public const string HttpContentType = "application/json";
        public const string RootUrl = "https://healthcare.clas.mobi:8445/clas-healthcare-v2/";
        public const string SkypeValidateUrl = "https://login.skype.com/json/validator?new_username=";
        public const string SacombankHost = "https://card.sacombank.com.vn/the";
        public const string VnPayHost = "http://ubet.edu.vn/";
        public const string ChBaseUrl = "http://chbase.bacsi24x7.vn/";
        public const string ChBaseRegUrl = "https://qa-grh-shell-sa.chbase.com/signup.aspx";
        public const string ChBaseErrorLogin = "ctl00_ContentPlaceHolder1_LoginContainerCtrl1_LoginCtrl2_LitError";
        public const string ChBaseNeedLogin = "signin-btn";
        public const string ChBaseLinkToPatient = "patient/linkCHBase/";
        public const string ChBasePatientInfo = "patient/packageFee/get";
        public const string ChBasePackageFeeInfo = "packageFees?isValid=true&status={0}&start={1}&length={2}";
        public const string ChBasePackageDetail = "packageFee/{0}/getDetailAmount";
        public const string ChBasePurchasePackageFee = "patient/packageFee/{0}/purchase";

        public const string VnPayProcessUrl = "users/accounts/connect-vnpay.html";
        public const string GetAmountWithPromotion = "checkup/{0}/getAmountWithPromotion";
        public const string Paymentstatus = "checkup/{0}/paymentstatus?totalAmount={1}";

        public const string SystemConfigUrl = "public/generalconfig";
        public const string LoginUrl = "patient/login";
        public const string LoginFacebookUrl = "patient/loginFB";
        public const string RequestResetPasswordUrl = "public/patient/requestPassword?id={0}&lang={1}";
        public const string ResetPasswordUrl = "public/user/resetPassword?hash={0}";
        public const string GetCityListUrl = "public/cities?isDistricts={0}";
        public const string SignUpUrl = "patient/signUpv2";
        public const string SignUpFbUrl = "patient/signUpFBv2";
        public const string GetPromotionsUrl = "public/promotions?start={0}&length={1}&sortField={2}&order={3}";
        public const string RefreshDataUrl = "patient/refreshData";
        public const string GetCheckupListUrl = "patient/checkups";
        public const string GetSuggestionsUrl = "suggestionkeywords/search?keyword={0}&searchExactly=true";
        public const string CheckExistedEmail = "public/checkExistedOrActivedEmail?email={0}";

        public const string GetDoctorBySearchUrl =
            "doctor/searchwithsuggestionkeyword?id={0}&type={1}&cityId={2}&districtId={3}&lat={4}&lng={5}&start={6}&length={7}";

        //public const string SendAdditionalSupportUrl = "patient/sendsupportrequest";
        public const string SendAdditionalSupportUrl = "patient/sendDoctorRequest";
        public const string AddCardUrl = "patient/card/link";
        public const string GetCardListUrl = "patient/cards";
        public const string GetCardListUrlv2 = "patient/card/v2/list";
        public const string VerifyPinOtp = "patient/card/verify";
        public const string SetPaymentPasswordUrl = "patient/payment/password/set";
        public const string GetHospitalListUrl = "hospitals?getAdminUsers=false&start={0}&length={1}";
        public const string GetDoctorsByHospitalUrl = "hospital/doctors?hospitalId={0}";

        public const string GetSchedulesOfDoctorUrl =
            "doctor/schedule/available?doctorId={0}&startDate={1}&endDate={2}&hospitalId={3}";
        //Bug: check param startTime, endTime...

        public const string AddCheckupUrl = "patient/checkupV2";
        public const string EditCheckupUrl = "doctor/checkup/updateV2";
        public const string DeleteCheckupUrl = "checkup/{0}/changestatus?status={1}";
        public const string GetHospitalByIdUrl = "hospital/{0}/get?getAdminUsers=false";
       // public const string DoPaymentUrl = "patient/payment/make";
       public const string DoPaymentUrl = "patient/card/v2/payment";
        public const string GetHistoricalCheckupsUrl =
            "patient/checkupsHistory?patientId={0}&sortField=appointmentDate&order=desc&start={1}&length={2}";

        public const string EditProfileUrl = "patient/{0}/profile/save";
        public const string ChangeUserPasswordUrl = "/changePassword";
        public const string RegisterDeviceUrl = "notification/register";


        public const string GetSupportQuestionListUrl = "patient/FAQ/getQuestionList?start={0}&length={1}&search={2}&searchExactly={3}";
        public const string AddQuestion = "patient/FAQ/ask";
        public const string GetSupportAnswerListUrl = "patient/FAQ/{0}/getAnswerList?start={1}&length={2}";

        public const string RatingCheckupUrl = "patient/rating/{0}?point={1}";

        public const string SendOtpUrl = "public/signup/verifyOTP?id={0}&otp={1}";

        public const string SearchHospitalUrl = "search/hospitals?start={0}&length={1}&search={2}&searchExactly={3}";

        public const string SetQuestionStatusUrl = "patient/FAQ/question/{0}/share?status={1}";

        public const string JavascripLogin =
            "document.getElementById('TxtUserName').value = '{0}';document.getElementById('TxtPassword').value = '{1}';document.getElementsByClassName('signin-btn')[0].click();";


        //KEY PAYMENT WITH VTCPAY
        public const string VtcPayUrl = "https://pay.vtc.vn/cong-thanh-toan/checkout.html?";
        public const string VtcPayStatusUrl = "https://pay.vtc.vn/cong-thanh-toan/WSCheckTrans.asmx?";

        //VTCPAY SANDBOX
        //public const string VtcPayUrl = "http://sandbox1.vtcebank.vn/pay.vtc.vn/cong-thanh-toan/checkout.html?";
        //public const string VtcPayStatusUrl = "http://sandbox1.vtcebank.vn/pay.vtc.vn/gate/WSCheckTrans.asmx?";

        public const string VtcPayWebId = "1395";
        public const string VtcPayKey =
            "Clas#Health@Care_2016";
        public const string VtcPayUrlReturn = "http://bacsi24x7.vn/result";
        public const string VtcPayReceiver = "0962919804";

        public const string VtcPayUrlFormat =
            "{0}website_id={1}&payment_method=1&order_code={2}&amount={3}&receiver_acc={4}&urlreturn={5}&customer_first_name=&customer_last_name=&customer_mobile=&bill_to_address_line1=&bill_to_address_line2=&city_name=&address_country=&customer_email=&order_des={6}&param_extend=&sign={7}";
        #endregion

        #region File name used for fakeAPI

        public const string SystemConfigFn = "config_general";
        public const string LoginFn = "login_regular";
        public const string LoginFacebookFn = "login_facebook";
        public const string LoginFacebookFailureFn = "login_facebook_failure";
        public const string ResetPasswordFn = "requestResetPassword";
        public const string GetCityListFn = "get_list_city";
        public const string SignUpFn = "register";
        public const string GetPromotionsFn = "get_promotion_list";
        public const string RefreshDataFn = "refresh_data";
        public const string GetCheckupListFn = "get_list_checkups";
        public const string GetSuggestionsFn = "get_suggetions";
        public const string GetDoctorBySearchFn = "get_list_search_doctors";
        public const string SendAdditionalSupportFn = "additional_support";
        public const string AddCardFn = "add_card";
        public const string GetCardListFn = "get_list_cards";
        public const string SetPaymentPasswordFn = "set_password_payment";
        public const string GetHospitalListFn = "get_list_hospitals";
        public const string GetDoctorsByHospitalFn = "get_doctors_by_hospital";
        public const string GetSchedulesOfDoctorFn = "get_schedule_of_doctor";
        public const string GetSchedulesOfDoctorFnEmpty = "get_schedule_of_doctor_empty";
        public const string GetSchedulesOfDoctorFn2 = "get_schedule_of_doctor_1";
        public const string AddCheckupFn = "add_checkup";
        public const string EditCheckupFn = "edit_datetime_checkup";
        public const string DeleteCheckupFn = "delete_checkup";
        public const string GetHospitalByIdFn = "get_hospital_by_id";
        public const string DoPaymentFn = "do_payment";
        public const string GetHistoricalCheckupsFn = "get_historical_checkups_by_user_id";
        public const string EditProfileFn = "edit_profile";
        public const string ChangeUserPasswordFn = "change_passworduser";
        public const string RegisterDeviceFn = "send_pushregisterID";

        #endregion

        #region Key object JSON used for API Responses

        public const string KeySessionId = "sessionId";
        public const string KeyChBaseId = "chbaseId";
        public const string KeySessionExpired = "sessionExpired";
        public const string KeyProfile = "profile";
        public const string KeyUser = "user";
        public const string KeyActiveType = "activateType";
        public const string KeyRelatedAccount = "relatedAccounts";
        public const string KeyCheckups = "checkups";
        public const string KeyCheckup = "checkup";
        public const string KeyCards = "cards";
        public const string KeyCitiesForSearch = "cities";
        public const string KeyCities = "cities";
        public const string KeyDoctors = "doctors";
        public const string KeySuggestions = "keywords";
        public const string KeySchedules = "schedules";
        public const string KeyPromotions = "promotions";
        public const string KeyHospital = "hospital";
        public const string KeyHospitals = "hospitals";
        public const string KeyId = "Id";
        public const string KeyConfig = "config";
        public const string KeyUserType = "userType";
        public const string KeySupportQuestionListType = "requests";
        public const string KeyCHBasePatientInfo = "patientinfo";

        #endregion

        #region Facebook config

        public static string FacebookClientId = "996566490388529";
        public static string FacebookScope = "email,public_profile,user_birthday";
        public static string FacebookAuthorizeUrl = "https://graph.facebook.com/oauth/authorize";
        public static string FacebookRedirectUrl = "http://www.google.com";

        #endregion
    }
}