using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HealthCare.Core.Services.Interfaces;
using Newtonsoft.Json.Linq;

#if MVVMCROSS
using ModernHttpClient;
#endif


namespace HealthCare.Core.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private CancellationTokenSource _cts = new CancellationTokenSource();

        public HttpService()
        {
            var cookieContainer = new CookieContainer();
#if !MVVMCROSS
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = true,
                UseCookies = true,
                CookieContainer = cookieContainer,
            };
            //if (handler.SupportsAutomaticDecompression)
            //    handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
#else
           

            var handler = new NativeMessageHandler
            {
                AllowAutoRedirect = true,
                UseCookies = true,
                CookieContainer = cookieContainer,
            };
#endif
            _httpClient = new HttpClient(handler);
        }

        public void CancelCurrentRequest()
        {
            if (_cts != null)
                _cts.Cancel();
        }

        public event EventHandler ConnectionFailed;


        public async Task<string> GetNoRedirectAsync(string url)
        {
            //_handler.AllowAutoRedirect = false;
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            // _handler.AllowAutoRedirect = true;
            return content;
        }

        public async Task<string> GetAsync(string url)
        {
            try
            {
                if (!NetworkInterface.GetIsNetworkAvailable())
                    throw new NoInternetConnection();
                if (url.Contains("ignore="))
                {
                    _httpClient.DefaultRequestHeaders.IfModifiedSince = DateTimeOffset.Now;
                }
                var response = await _httpClient.GetAsync(url, _cts.Token);

                //response.EnsureSuccessStatusCode();



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
                if (!NetworkInterface.GetIsNetworkAvailable())
                    throw new NoInternetConnection();

                var response = await _httpClient.GetAsync(url, token);

                response.EnsureSuccessStatusCode();

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
                if (!NetworkInterface.GetIsNetworkAvailable())
                    throw new NoInternetConnection();


                {
                }
                _cts = new CancellationTokenSource();
                var response = await _httpClient.PostAsync(url, new StringContent(data), _cts.Token);

                response.EnsureSuccessStatusCode();

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
                if (!NetworkInterface.GetIsNetworkAvailable())
                    throw new NoInternetConnection();


                {
                }
                _cts = new CancellationTokenSource();
                var postData = new StringContent(data);
                postData.Headers.ContentType = new MediaTypeHeaderValue(dataType);
                var response = await _httpClient.PostAsync(url, postData, _cts.Token);

                response.EnsureSuccessStatusCode();

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

        public async Task<string> PostAsync(string url, Dictionary<string, string> data)
        {
            try
            {
                if (!NetworkInterface.GetIsNetworkAvailable())
                    throw new NoInternetConnection();

                _cts = new CancellationTokenSource();

                //_httpClient.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded; charset=utf-8");

                var response = await _httpClient.PostAsync(url, new CustomFormUrlEncodedContent(data), _cts.Token);

                response.EnsureSuccessStatusCode();

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

        public async Task<string> PutAsync(string url, HttpContent content)
        {
            try
            {
                var response = await _httpClient.PutAsync(url, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                if (ConnectionFailed != null)
                    ConnectionFailed(e, EventArgs.Empty);
                throw;
            }
        }

        public Task<bool> NetCheck()
        {
            return Task.FromResult(NetworkInterface.GetIsNetworkAvailable());
        }

        public void SetHeader(string key, string value)
        {
            _httpClient.DefaultRequestHeaders.Remove(key);
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, value);
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


    }

    public class NoInternetConnection : Exception
    {
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