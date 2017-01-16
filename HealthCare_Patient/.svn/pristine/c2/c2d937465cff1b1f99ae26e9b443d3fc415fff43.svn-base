using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Connectivity.Plugin;
using HealthCare.Exceptions;
using HealthCare.Helpers;
using HealthCare.Objects;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace HealthCare.WebServices
{
    public class BaseFakeWebService : BaseWebService
    {
        protected async Task<JObject> SendHttpRequest(HttpMethod httpMethod, string url, string fakeApiFn,
            string bodyParam = null)
        {
            url = "http://fakeapi-healthcare.azurewebsites.net/api/FakeAPI/";
            if (!Common.CheckNetworkConnected())
            {
                throw new NetworkException();   
            }
            using (await MLock.LockAsync())
            {
                using (var httpClient = new HttpClientImp())
                {
                    Debug.WriteLine("Fake Api Service: " + url);

                    var request = new HttpRequestMessage(httpMethod, url);

                    if (!string.IsNullOrWhiteSpace(bodyParam))
                        request.Content = new StringContent(bodyParam, Encoding.UTF8, AppConstant.HttpContentType);
                    if (Common.OS == TargetPlatform.WinPhone && httpMethod == HttpMethod.Get)
                    {
                        // This's for fix cache data bug on winphone 
                        request.Headers.IfModifiedSince = new DateTimeOffset(DateTime.Now);
                    }
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    httpClient.DefaultRequestHeaders.CacheControl.NoCache = true;
                    httpClient.DefaultRequestHeaders.CacheControl.NoStore = true;

                    if (!string.IsNullOrWhiteSpace(Settings.SessionId) &&
                        !string.IsNullOrWhiteSpace(Settings.UserId))
                    {
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("sessionId", Settings.SessionId);
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("userId", Settings.UserId);
                    }
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("lang", Settings.Language);
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("filename", fakeApiFn);

                    try
                    {
                        var response = await httpClient.SendAsync(request);
                        var responseString = await response.Content.ReadAsStringAsync();
                        request.Dispose();

                        if (string.IsNullOrWhiteSpace(responseString))
                            throw new NetworkException();
                        if (!JsonUtils.ValidJsonString(responseString))
                            throw new ApiException("Json is wrong format!");
                        var parseResponse = JObject.Parse(responseString).ToObject<ApiResponse>();
                        //Bug: this line with register API 
                        //JsonConvert.DeserializeObject<ApiResponse>(responseString);

                        if (!parseResponse.IsSuccessful)
                        {
                            if (parseResponse.ErrorCode == ResponseCode.SUCCESS)
                            {
                                parseResponse.ErrorCode = ResponseCode.FAILURE_UNKNOWN;
                                parseResponse.ErrorDescription = "Unknown Error";
                            }
                            throw new ApiException(parseResponse.ErrorDescription, parseResponse.ErrorCode);
                        }
                        return parseResponse.Data;
                    }
                    catch (TaskCanceledException) //Time out
                    {
                        throw new NetworkException();
                    }
                }
            }
        }
    }
}