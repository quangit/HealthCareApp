using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.Content;
using Android.Net;
using Android.Provider;
using Android.Util;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid.Platform;
using HealthCare.Core.Resources;
using HealthCare.Core.Services.Interfaces;
using Java.Net;
using Newtonsoft.Json.Linq;
using Android.OS;

namespace HealthCare.Droid.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private CancellationTokenSource _cts = new CancellationTokenSource();

        public HttpService()
        {


            var handler = new ModernHttpClient.NativeMessageHandler
            {
                AllowAutoRedirect = true,
                UseCookies = true
            };
            var cookieManager = new CookieManager();
            CookieHandler.Default = cookieManager;

            if (handler.SupportsAutomaticDecompression)
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;


            _httpClient = new HttpClient(handler);
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:35.0) Gecko/20100101 Firefox/35.0");
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.5");
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-Requested-With", "XMLHttpRequest");
        }

        public async Task<bool> NetCheck()
        {
            var top = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
            if (top != null && top.Activity != null)
            {
                var connectivityManager = (ConnectivityManager)top.Activity.GetSystemService(Context.ConnectivityService);
                var activeConnection = connectivityManager.ActiveNetworkInfo;
                if ((activeConnection != null) && activeConnection.IsConnected)
                {
                    return true;
                }
                else
                {
                    var m = Mvx.Resolve<IMessageService>();
                    var r = await m.ShowConfirmMessageAsync(
                            AppResources.Message_InternetSettings, AppResources.ApplicationTitle, AppResources.Messsage_Yes, AppResources.Messsage_No);
                    if (r)
                    {
                        var i = new Intent(Settings.ActionWifiSettings);
                        top.Activity.StartActivity(i);
                    }

                    return false;
                }
            }
            Log.Warn("NET", "Could not check network state");
            return true;
        }

        public void SetHeader(string key, string value)
        {
            _httpClient.DefaultRequestHeaders.Remove(key);
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, value);
        }

        public event EventHandler ConnectionFailed;
        public async Task<string> GetAsync(string url)
        {
            try
            {

                if (!await NetCheck())
                    throw new Exception("NoInternetConnection");

                var response = await _httpClient.GetAsync(url, _cts.Token);



                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (Exception e)
            {
                if (ConnectionFailed != null)
                    ConnectionFailed(e, EventArgs.Empty);
                throw;
            }
        }


        public async Task<string> GetAsync(string url, CancellationToken token)
        {
            try
            {
                if (!await NetCheck())
                    throw new Exception("NoInternetConnection");

                var response = await _httpClient.GetAsync(url, token);



                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (Exception e)
            {
                if (ConnectionFailed != null)
                    ConnectionFailed(e, EventArgs.Empty);
                throw;
            }
        }

        public async Task<string> GetAsync(string p, Dictionary<string, string> data)
        {
            var dataContent = new FormUrlEncodedContent(data);
            var param = await dataContent.ReadAsStringAsync();
            return await GetAsync(p + "?" + param);
        }

        public async Task<string> PostAsync(string url, string data)
        {
            try
            {
                if (!await NetCheck())
                    throw new Exception("NoInternetConnection");
                {
                }
                _cts = new CancellationTokenSource();
                var response = await _httpClient.PostAsync(url, new StringContent(data), _cts.Token);



                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (Exception e)
            {
                if (ConnectionFailed != null)
                    ConnectionFailed(e, EventArgs.Empty);
                throw;
            }
        }

        // ReSharper disable once MethodOverloadWithOptionalParameter
        public async Task<string> PostAsync(string url, string data, string dataType)
        {
            try
            {
                if (!await NetCheck())
                    throw new Exception("NoInternetConnection");


                {
                }
                _cts = new CancellationTokenSource();
                var postData = new StringContent(data);
                postData.Headers.ContentType = new MediaTypeHeaderValue(dataType);
                var response = await _httpClient.PostAsync(url, postData, _cts.Token);



                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (Exception e)
            {
                if (ConnectionFailed != null)
                    ConnectionFailed(e, EventArgs.Empty);
                throw;
            }
        }
        public async Task<string> PostFormAync(string url, Dictionary<string, object> data)
        {
            try
            {
                using (var content = new MultipartFormDataContent())
                {
                    foreach (var item in data)
                    {
                        if (item.Value is byte[])
                        {
                            var imageContent = new ByteArrayContent(item.Value as byte[]);
                            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                            content.Add(imageContent, item.Key, "image.jpg");
                        }
                        else if (item.Value is JArray)
                        {
                            var array = item.Value as JArray;
                            foreach (var jToken in array)
                            {
                                var v = (JObject)jToken;
                                JToken code = null;
                                v.TryGetValue("code", out code);
                                if (code != null)
                                    content.Add(new StringContent(code.Value<string>()), item.Key);

                            }
                        }
                        else
                        {
                            if (item.Value != null)
                                content.Add(new StringContent(item.Value.ToString()), item.Key);
                        }

                    }

                    var response = await _httpClient.PostAsync(url, content);
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception e)
            {
                if (ConnectionFailed != null)
                    ConnectionFailed(e, EventArgs.Empty);
                throw;
            }
        }
        public async Task<string> PostAsync(string url, Dictionary<string, string> data)
        {
            try
            {
                if (!await NetCheck())
                    throw new Exception("NoInternetConnection");

                _cts = new CancellationTokenSource();

                //_httpClient.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded; charset=utf-8");

                var response = await _httpClient.PostAsync(url, new CustomFormUrlEncodedContent(data), _cts.Token);


                //TryGetCookie();
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (Exception e)
            {
                if (ConnectionFailed != null)
                    ConnectionFailed(e, EventArgs.Empty);
                throw;
            }
        }
        public async Task<string> PostDefaultAsync(string url, Dictionary<string, string> data, string referrer = "")
        {
            try
            {
                //if (!string.IsNullOrEmpty(referrer))
                //{
                //    _httpClient.DefaultRequestHeaders.Referrer = new Uri(referrer);
                //}
                _cts = new CancellationTokenSource();
                var response = await _httpClient.PostAsync(url, new FormUrlEncodedContent(data), _cts.Token);


                //TryGetCookie();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                if (ConnectionFailed != null)
                    ConnectionFailed(e, EventArgs.Empty);
                throw;
            }
        }
        //private void TryGetCookie()
        //{
        //    //return;
        //    try
        //    {
        //        var cookies = _cookieContainer.GetCookies(new Uri("https://www.microsoftvirtualacademy.com"));
        //        var fedAuth = cookies["FedAuth"].Value;
        //        var fedAuth1 = cookies["FedAuth1"].Value;
        //        var sessionId = cookies["ASP.NET_SessionId"].Value;


        //    }
        //    catch (Exception)
        //    {
        //        // ignored
        //    }
        //}

        public async Task<string> PutAsync(string url, HttpContent content)
        {
            try
            {
                var response = await _httpClient.PutAsync(url, content);

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                if (ConnectionFailed != null)
                    ConnectionFailed(e, EventArgs.Empty);
                throw;
            }
        }
    }



    /// <summary>
    /// This class for help POST Huge content value. 
    /// </summary>
    public class CustomFormUrlEncodedContent : ByteArrayContent
    {
        public CustomFormUrlEncodedContent(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
            : base(GetContentByteArray(nameValueCollection))
        {
            Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
        }

        private static byte[] GetContentByteArray(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
        {
            if (nameValueCollection == null)
            {
                throw new ArgumentNullException("nameValueCollection");
            }

            var stringBuilder = new StringBuilder();
            foreach (var current in nameValueCollection)
            {
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Append('&');
                }

                stringBuilder.Append(Encode(current.Key));
                stringBuilder.Append('=');
                stringBuilder.Append(Encode(current.Value));
            }
            return Encoding.UTF8.GetBytes(stringBuilder.ToString());
        }

        private static string Encode(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return string.Empty;
            }
            return WebUtility.UrlEncode(data).Replace("%20", "+");
        }
    }
}