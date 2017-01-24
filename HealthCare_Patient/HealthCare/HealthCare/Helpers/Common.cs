using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Connectivity.Plugin;
using HealthCare.DependencyServices;
using HealthCare.Enums;
using HealthCare.Exceptions;
using HealthCare.ModelApis;
using HealthCare.Models;
using HealthCare.Objects;
using HealthCare.Resx;
using HealthCare.Validators;
using HealthCare.ViewModels;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Globalization;
using System.Net.Http;
using System.Threading;

namespace HealthCare.Helpers
{
    public static class Common
    {
        public static TargetPlatform OS => Device.OS == TargetPlatform.Windows ? TargetPlatform.WinPhone : Device.OS;
        public static string GetDeviceLanguage()
        {
            var language = DependencyService.Get<ILocalize>().GetCurrentCultureInfo().TwoLetterISOLanguageName;
            if (language != null && language.Contains("vi"))
            {
                return "vi";
            }
            else return "en";
        }

        public static void ShowLoading()
        {
            UserDialogs.Instance.ShowLoading(AppResources.loading);
        }
        public static void HideLoading()
        {
            UserDialogs.Instance.HideLoading();
        }
        public static void ShowErrorNetWork()
        {
            UserDialogs.Instance.Alert(AppResources.network_not_available);
        }
        public static T MapIdToList<T>(this IList<T> list, string id) where T : Entity
        {
            return list.FirstOrDefault(x => x.Id.Equals(id));
        }

        public static string GetTimeZone()
        {
            var timeZoneSpan = TimeZoneInfo.Local.BaseUtcOffset;
            var time = Math.Abs(timeZoneSpan.Hours).ToString("00") + ":" + timeZoneSpan.Minutes.ToString("00");
            time = time.Replace("-", "").Replace("+", "");
            if (timeZoneSpan.TotalSeconds < 0)
                return "-" + time;
            else
                return "+" + time;
        }

        public static void DoTaskWithDelay(Action action, int delay = AppConstant.DelayBinding)
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(delay), () =>
            {
                action.Invoke();
                return false;
            });
        }

        public static async Task<bool> CheckNetwordStatus()
        {
            if (CheckNetworkConnected())
                return true;
            await AlertAsync(AppResources.network_not_available);
            return false;
        }

        public static bool CheckNetworkConnected()
        {
            if (OS == TargetPlatform.Android)
            {
                return DependencyService.Get<INetworkInfo>().IsConnected();
            }
            else
            {
                return CrossConnectivity.Current.IsConnected;
            }

        }

        public static void IsPatientUserType(int userType)
        {
            if (userType != 1)
                throw new Exception(AppResources.login_access_denied);
        }

        public static bool compareLocation(Position a, Position b)
        {
            double latA = Math.Round(a.Latitude, 3);
            double lngA = Math.Round(a.Longitude, 3);
            double latB = Math.Round(b.Latitude, 3);
            double lngB = Math.Round(b.Longitude, 3);
            if (latA == latB && lngA == lngB)
                return true;
            else
                return false;
        }

        public static async Task<bool> IsLimitAddCheckup()
        {
            var limit = CommonViewModel.Instance.SystemConfig.MaxCheckupPerAccountPerDay;
            var checkupToday = CheckupViewModel.Instance.CheckupAddedTodaySuccessCount;

            if (checkupToday >= limit)
            {
                await AlertAsync(string.Format(AppResources.limit_account_checkup_add_message, limit));
                return true;
            }
            return false;
        }

        public static TModel VerifyDataIntegrity<TModel>(this TModel data)
            where TModel : Entity
        {
            var result = new ValidationResult();
            if (data is CheckupModel)
                result = new CheckupValidator().Validate(data as CheckupModel);
            if (data is CheckupTypeModel)
                result = new CheckupTypeValidator().Validate(data as CheckupTypeModel);
            if (data is CreditCardModel)
                result = new CreditCardValidator().Validate(data as CreditCardModel);
            if (data is PersonModel)
                result = new PersonValidator().Validate(data as PersonModel);
            if (data is PromotionModel)
                result = new PromotionValidator().Validate(data as PromotionModel);
            if (data is ScheduleModel)
                result = new ScheduleValidator().Validate(data as ScheduleModel);
            if (!result.IsValid)
                throw new DataIntegrityException(result.Errors[0]);
            return data;
        }

        public static TModelList VerifyDataIntegrityOnList<TModelList>(this TModelList dataList)
            where TModelList : IList
        {
            foreach (var data in dataList)
                VerifyDataIntegrity(data as Entity);
            return dataList;
        }

        public static async Task AlertAsync(string msg)
        {
            UserDialogs.Instance.HideLoading();
            if (!string.IsNullOrWhiteSpace(msg))
                await UserDialogs.Instance.AlertAsync(msg,null,AppResources.ok);
        }

        public static async void ShowMessage(string msg)
        {
            UserDialogs.Instance.HideLoading();
            if (!string.IsNullOrWhiteSpace(msg))
                await UserDialogs.Instance.AlertAsync(msg);
        }

        public static async Task<bool> ConfirmAsync(string msg, string ok = null, string cancel = null)
        {
            if (ok == null) ok = AppResources.ok;
            if (cancel == null) cancel = AppResources.cancel;
            UserDialogs.Instance.HideLoading();
            if (!string.IsNullOrWhiteSpace(msg))
                return await UserDialogs.Instance.ConfirmAsync(msg, null, ok, cancel);
            return false;
        }

        public static Dictionary<string, string> UserModelToFormData(this UserModel model)
        {
            var formData = new Dictionary<string, string>();
            formData.Add("phone", model.PhoneNo?.Trim());

            if (!string.IsNullOrWhiteSpace(model.Password))
                formData.Add("password", model.Password);

            formData.Add("firstName", model.FirstName);
            formData.Add("lastName", model.LastName);

            if (model.Gender != Gender.None)
                formData.Add("gender", model.Gender == Gender.Male ? "M" : "F");

            if (!string.IsNullOrWhiteSpace(model.IdNo))
                formData.Add("idNo", model.IdNo);

            if (!string.IsNullOrWhiteSpace(model.Address))
                formData.Add("address", model.Address);
            if (model.IsBirthDaySet)
                formData.Add("birthDay", ConvertToTimestamp(model.BirthDay).ToString());

            if (!string.IsNullOrWhiteSpace(model.Email))
                formData.Add("email", model.Email?.Trim());

            formData.Add("cityId", model.CityId);
            formData.Add("districtId", model.DistrictId);
            //formData.Add("cityId", null);
            //formData.Add("districtId", null);
            formData.Add("skypeId", model.SkypeId);

            if (!string.IsNullOrWhiteSpace(model.Occupation))
                formData.Add("occupation", model.Occupation);
            formData.Add("maritalStatus", (model.MaritalStatus == MaritalStatus.Married).ToString());
            if (!string.IsNullOrWhiteSpace(model.FacebookId))
                formData.Add("facebookId", model.FacebookId);
            return formData;
        }

        public static byte[] ReadByteArrayFromStream(this Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static ObservableCollection<TModel> ToBaseModel<TModel, KApiModel>
            (this ObservableCollection<KApiModel> array) where KApiModel : IApiModel<TModel> where TModel : Entity
        {
            var baseArray = new ObservableCollection<TModel>();
            foreach (var item in array)
                baseArray.Add(item.ToBaseModel());
            return baseArray;
        }

        private static int GenericIdForNotifycation(CheckupModel checkup)
        {
            // Generate unique ID which have 9 numeral from checkup
            var id = 0;
            var arrChar = Encoding.UTF8.GetBytes(checkup.Symptom);
            foreach (var c in arrChar)
                id += c;
            if (checkup != null)
                id = (id % 10000) * 100000 + (int)ConvertToTimestamp(checkup.Schedule.StartDateTime) / 1000 / 60 % 100000;

            return id;
        }

        public static void CancelNotifyForCheckup(CheckupModel checkup)
        {
            var id = GenericIdForNotifycation(checkup);
            for (var i = 0; i < 5; i++)
            {
                DependencyService.Get<ILocalNotifier>().Cancel(id + i);
            }
        }

        public static void SetNotificationForCheckup(CheckupModel checkup)
        {
            if (checkup.Schedule.StartDateTime == DateTime.Now)
                return;

            var id = GenericIdForNotifycation(checkup);

            // Notify at before 1 day, before 2h 1h 30mins, 8am at appointment's day,
            var t = checkup.Schedule.StartDateTime.ToLocalTimeZone();
            //var t = Common.UnixTimeStampToDateTime(1448525820000).ToLocalTimeZone();
            var at8HOfDay = new DateTime(t.Year, t.Month, t.Day).AddHours(8);
            DateTime[] listTime =
            {
                t.CloneJson().AddDays(-1), t.CloneJson().AddHours(-2),
                t.CloneJson().AddHours(-1), t.CloneJson().AddMinutes(-30), at8HOfDay
            };
            for (var i = 0; i < listTime.Length; i++)
            {
                if (listTime[i].CompareTo(DateTime.Now) > 0)
                {
                    var notification = new LocalNotification
                    {
                        Text = string.Format(AppResources.local_notification, checkup.Schedule.StartDateTime.ToLocalTimeZone().ToString("HH:mm dd/MM/yy")),
                        //Title = checkup.Schedule.Doctor.FirstName + checkup.Schedule.Doctor.LastName,
                        Title = "",
                        Id = id + i,
                        NotifyTime = listTime[i]
                    };
                    Debug.WriteLine(string.Format("PCL_Notify_{0} {1}: ", notification.Title, notification.Text) +
                                    notification.NotifyTime.ToString("G"));
                    DependencyService.Get<ILocalNotifier>().Cancel(notification.Id);
                    DependencyService.Get<ILocalNotifier>().Notify(notification);
                }
            }
        }

        public static bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;
            //if (Common.OS == TargetPlatform.iOS)
            //    return true;

            var patternPass = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,30}$";

            var myRegexDienThoai = new Regex(patternPass);

            return myRegexDienThoai.IsMatch(password);
        }

        public static bool IsPhoneNumberValid(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile))
            {
                return false;
            }
            mobile = mobile.Trim();
            const string patternDienThoai = @"^-*[0-9,\.?\-?\(?\)?\ ]+$";

            var myRegexDienThoai = new Regex(patternDienThoai);

            return myRegexDienThoai.IsMatch(mobile);
        }

        public static bool IsEmailValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            if (Device.OS == TargetPlatform.iOS)
                return true;

            email = email.Trim();

            var regex = new Regex(@"[a-zA-Z0-9_\.\+-]+[^.]@[a-zA-Z0-9-]+\.[a-zA-Z0-9-\.]+");

            return regex.IsMatch(email);
        }

        public static bool Mod10Check(string ccValue)
        {
            if (ccValue == null)
                return false;
            ccValue = ccValue.Replace("-", "");
            ccValue = ccValue.Replace(" ", "");

            var checksum = 0;
            var evenDigit = false;
            var charArray = ccValue.ToCharArray();
            Array.Reverse(charArray);
            foreach (var digit in charArray)
            {
                if (digit < '0' || digit > '9')
                {
                    return false;
                }

                var digitValue = (digit - '0') * (evenDigit ? 2 : 1);
                evenDigit = !evenDigit;

                while (digitValue > 0)
                {
                    checksum += digitValue % 10;
                    digitValue /= 10;
                }
            }
            return (checksum % 10) == 0;
        }

        /// <summary>
        ///     Perform a deep Copy of the object, using Json as a serialisation method.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T CloneJson<T>(this T source)
        {
            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(source, null))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));
        }

        public static T OnPlatform<T>(T iOS, T Android, T WinPhone, T Windows)
        {
            switch (Device.OS)
            {
                case TargetPlatform.iOS:
                    return iOS;
                case TargetPlatform.Android:
                    return Android;
                case TargetPlatform.WinPhone:
                    return WinPhone;
                case TargetPlatform.Windows:
                    return Windows;
                default:
                    return iOS;
            }
        }

        public static T OnPlatform<T>(T iOS, T Android, T WinPhone)
        {
            switch (Device.OS)
            {
                case TargetPlatform.iOS:
                    return iOS;
                case TargetPlatform.Android:
                    return Android;
                case TargetPlatform.WinPhone:
                    return WinPhone;
                case TargetPlatform.Windows:
                    return WinPhone;
                default:
                    return iOS;
            }
        }

        public static DateTime ToLocalTimeZone(this DateTime dateTime)
        {
            if (dateTime.Equals(DateTime.MinValue)) return dateTime;
            return dateTime.AddMilliseconds(TimeZoneInfo.Local.BaseUtcOffset.TotalMilliseconds);
        }

        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            try
            {
                // Unix timestamp is seconds past epoch
                double seconds = unixTimeStamp / 1000;
                return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(seconds);
            }
            catch
            {
                return DateTime.Now.Date.AddDays(-1);
            }
        }

        public static long ConvertToTimestamp(DateTime value)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var diff = value - origin;
            return Convert.ToInt64(diff.TotalMilliseconds);
        }

        public static TimeSpan GetTimeSpanOfADay(long millisecond)
        {
            var curDate = UnixTimeStampToDateTime(millisecond);
            var baseDate = new DateTime(curDate.Year, curDate.Month, curDate.Day);
            return curDate - baseDate;
        }

        public static void DeepCopy<T>(this T obj1, T obj2)
        {
            var listField1 = obj1.GetType().GetRuntimeFields();
            var listField2 = obj2.GetType().GetRuntimeFields();
            foreach (var field in listField1)
            {
                field.SetValue(obj1,
                    listField2.FirstOrDefault(x => x.Name.Equals(field.Name)).GetValue(obj2));
            }
        }

        public static string getImageSource(Image objImage)
        {
            if (objImage.Source is FileImageSource)
            {
                var objFileImageSource =
                    (FileImageSource)objImage.Source;
                // Access the file that was specified:-
                return objFileImageSource.File;
            }
            return null;
        }

        public static T MapClass<T, K>(this K baseObject, T derivedObject)
        {
            var fieldsOfBaseClass = baseObject.GetType().GetRuntimeProperties();
            var a = typeof(T);
            var fieldsOfDerivedClass = typeof(T).GetRuntimeProperties().Intersect(fieldsOfBaseClass);
            foreach (var fieldOfBaseClass in fieldsOfBaseClass)
            {
                foreach (var fieldOfDerivedClass in fieldsOfDerivedClass)
                {
                    fieldOfDerivedClass.SetValue(derivedObject, fieldOfBaseClass.GetValue(baseObject));
                }
            }

            return derivedObject;
        }

        public static async Task<bool> IsReachable(string host, double msTimeout)
        {
            if (await Common.CheckNetwordStatus())
            {
                try
                {
                    using (var client = new HttpClient() { BaseAddress = new Uri(host), Timeout = TimeSpan.FromMilliseconds(msTimeout) })
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                        client.DefaultRequestHeaders.CacheControl.NoCache = true;
                        client.DefaultRequestHeaders.CacheControl.NoStore = true;

                        var response = await Task.Run(() =>
                        {
                            var cancelSource = new CancellationTokenSource();
                            var reqTask = client.GetAsync(host, HttpCompletionOption.ResponseHeadersRead,
                                cancelSource.Token);
                            if (Task.WaitAny(new Task[] { reqTask }, TimeSpan.FromMilliseconds(msTimeout)) < 0)
                            {
                                cancelSource.Cancel(); // attempt to cancel the HTTP request
                                throw new NetworkException();
                            }
                            return reqTask.GetAwaiter().GetResult();
                        }).ConfigureAwait(false);

                        return response.IsSuccessStatusCode;
                    }

                }
                catch
                {
                    return false;
                }
            }

            return false;
        }
    }
}