using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Connectivity.Plugin;
using HealthCare.DependencyServices;
using HealthCare.Exceptions;
using HealthCare.Helpers;
using HealthCare.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace HealthCare.WebServices
{
    public class BaseWebService
    {
        protected static readonly AsyncLock MLock = new AsyncLock();
        protected static readonly int TIME_OUT_IN_SECOND = 60;

        /// <summary>
        ///     Sends the http request with contentType is application/json and method Get/Post
        /// </summary>
        /// <param name="httpMethod">Get/Post</param>
        /// <param name="bodyParam">Json String</param>
        protected async Task<JObject> SendHttpRequest(HttpMethod httpMethod, string url, string bodyParam = null)
        {
            if (!Common.CheckNetworkConnected())
                throw new NetworkException();
            return await SendHttpMessage(() => CreateJsonContentMessage(httpMethod, url, bodyParam));
        }
        protected async Task<HttpResponseMessage> GetHttpRequest(string url)
        {
            if (!Common.CheckNetworkConnected())
                throw new NetworkException();
            HttpClient client = new HttpClient();
            return await client.GetAsync(url);
        }
        /// <summary>
        ///     Sends the http request with contentType is multipart/form-data, method is Post
        /// </summary>
        /// <param name="file">Not null</param>
        protected async Task<JObject> SendMultipartRequest(string url, string keyFile, byte[] dataFile,
            Dictionary<string, string> formsData)
        {
            if (!Common.CheckNetworkConnected())
                throw new NetworkException();
            return await SendHttpMessage(() => CreateMultipartMessage(url, keyFile, dataFile, formsData));
        }
        public async Task<HttpResponseMessage> SendFormUrlMessage(string url, Dictionary<string, string> formsData)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, url);
            message.Content = new FormUrlEncodedContent(formsData);
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.AllowAutoRedirect = false;

            using (var httpClient = new HttpClient(httpClientHandler))
            {
                var result = await httpClient.SendAsync(message);
                return result;
            }
        }
        private HttpRequestMessage CreateMultipartMessage(string url, string keyFile, byte[] dataFile,
            Dictionary<string, string> formsData)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, url);
            var multiPartContent = new MultipartFormDataContent("----HcBoundary");
            if (dataFile != null)
            {
                var byteArrayContent = new ByteArrayContent(dataFile);
                byteArrayContent.Headers.Add("Content-Type", "application/octet-stream");
                var avatarFileName = string.Format("HC_{0}.jpg", DateTime.Now.ToString("MMddyy_Hmmss"));
                multiPartContent.Add(byteArrayContent, keyFile, avatarFileName);
            }
            if (formsData != null)
            {
                var keys = new List<string>(formsData.Keys);
                foreach (var key in keys)
                {
                    if (formsData[key] != null)
                        multiPartContent.Add(new StringContent(formsData[key]), key);
                }
            }
            message.Content = multiPartContent;
            return message;
        }

        private HttpRequestMessage CreateJsonContentMessage(HttpMethod method, string url, string bodyParam)
        {
            var message = new HttpRequestMessage(method, url);
            if (!string.IsNullOrWhiteSpace(bodyParam))
                message.Content = new StringContent(bodyParam, Encoding.UTF8, "application/json");

            if (Common.OS == TargetPlatform.WinPhone && method == HttpMethod.Get)
                // This's for fix cache data bug on winphone 
                message.Headers.IfModifiedSince = new DateTimeOffset(DateTime.Now);
            return message;
        }

        public HttpClient CreateWithRetries()
        {
            return new HttpClient(new RetryDelegatingHandler(new HttpClientHandler()), false)
            {
                BaseAddress = new Uri(AppConstant.RootUrl),
                Timeout = TimeSpan.FromSeconds(TIME_OUT_IN_SECOND),
            };
        }

        private async Task<JObject> SendHttpMessage(Func<HttpRequestMessage> requestMsgFunc)
        {
            using (var httpClient = Common.OnPlatform<HttpClient>(new HttpClientImp(), CreateWithRetries(), new HttpClientImp()))
            {
                httpClient.DefaultRequestHeaders.Clear();
                //httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident / 6.0)");
                httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                httpClient.DefaultRequestHeaders.CacheControl.NoCache = true;
                httpClient.DefaultRequestHeaders.CacheControl.NoStore = true;

                if (!string.IsNullOrWhiteSpace(Settings.SessionId) &&
                    !string.IsNullOrWhiteSpace(Settings.UserId))
                {   
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("sessionId", Settings.SessionId);
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("userId", Settings.UserId);
                }

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("lang", Common.GetDeviceLanguage());
                try
                {
                    var requestMsg = requestMsgFunc();
                    Debug.WriteLine("Real API: " + requestMsg.RequestUri);

                    var response = await Task.Run(() =>
                    {
                        var cancelSource = new CancellationTokenSource();
                        var reqTask = httpClient.SendAsync(requestMsg, cancelSource.Token);
                        if (Task.WaitAny(new Task[] { reqTask }, TimeSpan.FromSeconds(TIME_OUT_IN_SECOND)) < 0)
                        {
                            cancelSource.Cancel(); // attempt to cancel the HTTP request
                            throw new NetworkException();
                        }
                        return reqTask.GetAwaiter().GetResult();
                    }).ConfigureAwait(false);

                    var responseString = await response.Content.ReadAsStringAsync();
                    requestMsg.Dispose();

                    if (string.IsNullOrWhiteSpace(responseString))
                        throw new NetworkException();
                    if (!JsonUtils.ValidJsonString(responseString))
                        throw new ApiException("");

                    //var birth = "540259200000";
                    //var birthReplace = "null";
                    //responseString = responseString.Replace(birth, birthReplace);
                    var parseResponse = JsonConvert.DeserializeObject<ApiResponse>(responseString);

                    if (!parseResponse.IsSuccessful)
                    {
                        if (parseResponse.ErrorCode == ResponseCode.FAILURE_SESSION_INVALID)
                            return await RefreshSessionAndResendMsg(requestMsgFunc);
                        else
                            throw new ApiException(parseResponse.ErrorDescription, parseResponse.ErrorCode);
                    }
                    return parseResponse.Data;
                }
                catch (TaskCanceledException)
                {
                    throw new NetworkException();
                }
                catch (OperationCanceledException)
                {
                    throw new NetworkException();
                }
                catch (Exception ex)
                {
                    //httpClient.Dispose();
                    throw ex;
                }
            }
        }

        protected async Task<JObject> RefreshSessionAndResendMsg(Func<HttpRequestMessage> message)
        {
            // refresh session
            if (!string.IsNullOrWhiteSpace(Settings.CurrentUser?.FacebookId))
            {
                var url = AppConstant.RootUrl + AppConstant.LoginFacebookUrl;
                var data = new { Settings.CurrentUser?.FacebookId, fbAccessToken = Settings.FacebookAccessToken };
                var newSession = await SendHttpRequest(HttpMethod.Post, url, bodyParam: JsonConvert.SerializeObject(data));
                Settings.SessionId = JsonUtils.ParseData<string>(newSession, AppConstant.KeySessionId);
                Settings.SessionExpired = Convert.ToInt64(JsonUtils.ParseData<string>(newSession,
                    AppConstant.KeySessionExpired));
            }
            else if (!string.IsNullOrWhiteSpace(Settings.CurrentUser?.UserCode)
                     && !string.IsNullOrWhiteSpace(Settings.CurrentUser?.Password))
            {
                var url = AppConstant.RootUrl + AppConstant.LoginUrl;
                var data = new
                {
                    id = Settings.CurrentUser?.UserCode,
                    password = Settings.CurrentUser?.Password,
                    timezone = Common.GetTimeZone()
                };
                var newSession = await SendHttpRequest(HttpMethod.Post, url, bodyParam: JsonConvert.SerializeObject(data));
                Settings.SessionId = JsonUtils.ParseData<string>(newSession, AppConstant.KeySessionId);
                Settings.SessionExpired = Convert.ToInt64(JsonUtils.ParseData<string>(newSession,
                    AppConstant.KeySessionExpired));
            }
            // resend http message
            return await SendHttpMessage(message);
        }

        internal class HttpClientImp : HttpClient
        {
            public HttpClientImp()
            {
                BaseAddress = new Uri(AppConstant.RootUrl);
                Timeout = TimeSpan.FromSeconds(TIME_OUT_IN_SECOND);
            }
        }

        protected sealed class AsyncLock
        {
            private readonly Task<IDisposable> _mReleaser;
            private readonly SemaphoreSlim _mSemaphore = new SemaphoreSlim(1, 1);

            public AsyncLock()
            {
                _mReleaser = Task.FromResult((IDisposable)new Releaser(this));
            }

            public Task<IDisposable> LockAsync()
            {
                var wait = _mSemaphore.WaitAsync();
                return wait.IsCompleted
                    ? _mReleaser
                    : wait.ContinueWith((_, state) => (IDisposable)state,
                        _mReleaser.Result, CancellationToken.None,
                        TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
            }

            private sealed class Releaser : IDisposable
            {
                private readonly AsyncLock _mToRelease;

                internal Releaser(AsyncLock toRelease)
                {
                    _mToRelease = toRelease;
                }

                public void Dispose()
                {
                    _mToRelease._mSemaphore.Release();
                }
            }
        }
    }
}