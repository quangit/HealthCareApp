using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.Resources.Core;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Data.Json;
using Windows.Security.Credentials;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HealthCare.Win.VoiceCommands
{
    /// <summary>
    /// The VoiceCommandService implements the entrypoint for all headless voice commands
    /// invoked via Cortana. The individual commands supported are described in the
    /// AdventureworksCommands.xml VCD file in the AdventureWorks project. The service
    /// entrypoint is defined in the Package Manifest (See section uap:Extension in 
    /// AdventureWorks:Package.appxmanifest)
    /// </summary>
    public sealed class KVoiceCommandService : IBackgroundTask
    {
        /// <summary>
        /// the service connection is maintained for the lifetime of a cortana session, once a voice command
        /// has been triggered via Cortana.
        /// </summary>
        VoiceCommandServiceConnection voiceServiceConnection;

        /// <summary>
        /// Lifetime of the background service is controlled via the BackgroundTaskDeferral object, including
        /// registering for cancellation events, signalling end of execution, etc. Cortana may terminate the 
        /// background service task if it loses focus, or the background task takes too long to provide.
        /// 
        /// Background tasks can run for a maximum of 30 seconds.
        /// </summary>
        BackgroundTaskDeferral serviceDeferral;

        /// <summary>
        /// ResourceMap containing localized strings for display in Cortana.
        /// </summary>
        ResourceMap cortanaResourceMap;

        /// <summary>
        /// The context for localized strings.
        /// </summary>
        ResourceContext cortanaContext;

        /// <summary>
        /// Get globalization-aware date formats.
        /// </summary>
        DateTimeFormatInfo dateFormatInfo;

        private HttpClient _httpClient;

        /// <summary>
        /// Background task entrypoint. Voice Commands using the <VoiceCommandService Target="...">
        /// tag will invoke this when they are recognized by Cortana, passing along details of the 
        /// invocation. 
        /// 
        /// Background tasks must respond to activation by Cortana within 0.5 seconds, and must 
        /// report progress to Cortana every 5 seconds (unless Cortana is waiting for user
        /// input). There is no execution time limit on the background task managed by Cortana,
        /// but developers should use plmdebug (https://msdn.microsoft.com/en-us/library/windows/hardware/jj680085%28v=vs.85%29.aspx)
        /// on the Cortana app package in order to prevent Cortana timing out the task during
        /// debugging.
        /// 
        /// Cortana dismisses its UI if it loses focus. This will cause it to terminate the background
        /// task, even if the background task is being debugged. Use of Remote Debugging is recommended
        /// in order to debug background task behaviors. In order to debug background tasks, open the
        /// project properties for the app package (not the background task project), and enable
        /// Debug -> "Do not launch, but debug my code when it starts". Alternatively, add a long
        /// initial progress screen, and attach to the background task process while it executes.
        /// </summary>
        /// <param name="taskInstance">Connection to the hosting background service process.</param>
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            serviceDeferral = taskInstance.GetDeferral();

            // Register to receive an event if Cortana dismisses the background task. This will
            // occur if the task takes too long to respond, or if Cortana's UI is dismissed.
            // Any pending operations should be cancelled or waited on to clean up where possible.
            taskInstance.Canceled += OnTaskCanceled;

            var triggerDetails = taskInstance.TriggerDetails as AppServiceTriggerDetails;

            // Load localized resources for strings sent to Cortana to be displayed to the user.
            cortanaResourceMap = ResourceManager.Current.MainResourceMap.GetSubtree("Resources");

            // Select the system language, which is what Cortana should be running as.
            cortanaContext = ResourceContext.GetForViewIndependentUse();

            // Get the currently used system date format
            dateFormatInfo = CultureInfo.CurrentCulture.DateTimeFormat;

            // This should match the uap:AppService and VoiceCommandService references from the 
            // package manifest and VCD files, respectively. Make sure we've been launched by
            // a Cortana Voice Command.
            if (triggerDetails != null && triggerDetails.Name == "KVoiceCommandService")
            {
                try
                {
                    voiceServiceConnection =
                        VoiceCommandServiceConnection.FromAppServiceTriggerDetails(
                            triggerDetails);

                    voiceServiceConnection.VoiceCommandCompleted += OnVoiceCommandCompleted;

                    // GetVoiceCommandAsync establishes initial connection to Cortana, and must be called prior to any 
                    // messages sent to Cortana. Attempting to use ReportSuccessAsync, ReportProgressAsync, etc
                    // prior to calling this will produce undefined behavior.
                    VoiceCommand voiceCommand = await voiceServiceConnection.GetVoiceCommandAsync();

                    // Depending on the operation (defined in AdventureWorks:AdventureWorksCommands.xml)
                    // perform the appropriate command.
                    switch (voiceCommand.CommandName)
                    {
                        case "findNextCMETopic":
                            await SendCompletionMessageForDestination();
                            break;
                        case "findCMELibrary":
                            {
                                var userMessage = new VoiceCommandUserMessage();
                                userMessage.DisplayMessage = "Show your CME Library";
                                userMessage.SpokenMessage = "Show your CME Library";
                                var response = VoiceCommandResponse.CreateResponse(userMessage);
                                response.AppLaunchArgument = "CmeLibrary";
                                await voiceServiceConnection.ReportSuccessAsync(response);
                                break;
                            }
                        default:
                            // As with app activation VCDs, we need to handle the possibility that
                            // an app update may remove a voice command that is still registered.
                            // This can happen if the user hasn't run an app since an update.
                            LaunchAppInForeground();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Handling Voice Command failed " + ex.ToString());
                }
            }
        }

        /// <summary>
        /// Handle the Trip Cancellation task. This task demonstrates how to prompt a user
        /// for confirmation of an operation, show users a progress screen while performing
        /// a long-running task, and showing a completion screen.
        /// </summary>
        /// <param name="destination">The name of a destination, expected to match the phrase list.</param>
        /// <returns></returns>
        private async Task SendCompletionMessageForDestination()
        {
            var account = new AccountManager();
            account.RestoreAsync();
            TopicData data = null;
            if (!string.IsNullOrEmpty(account.Username) && !string.IsNullOrEmpty(account.Password))
            {

                var ret =
                    await
                        PostApiCall<ApiRootObjectLoginResult>("doctor/login",
                            new { id = account.Username, password = account.Password });
                if (ret != null && ret.Successful)
                {
                    LoginResult = ret.Data;
                    var r = await GetApiCall<ApiRootObjectTopic>(string.Format("weektopics?start={0}&length={1}", 0, 10));
                    if (r != null && r.Successful)
                        data = r.Data;
                }


            }
            var userMessage = new VoiceCommandUserMessage();
            var topicsContentTiles = new List<VoiceCommandContentTile>();
            if (data?.Data == null || data.Data.Length == 0)
            {

                userMessage.DisplayMessage = "Sorry, you don't have any CME Topics";
                userMessage.SpokenMessage = "Sorry, you don't have any CME Topics";
            }
            else
            {
                if (data?.Data != null && data.Data.Length > 0)
                {
                    var topics = data.Data.Where(x => LongtoDateTime(x.StartDateTime) >= DateTime.Now);
                    foreach (var topic in topics)
                    {
                        var tile = new VoiceCommandContentTile
                        {
                            ContentTileType = VoiceCommandContentTileType.TitleWithText,
                            Title = Trim(topic.Title, 100),
                            TextLine1 = Trim(topic.Address, 100),
                            TextLine2 = Trim(topic.Time, 100),
                            AppLaunchArgument = "topics:" + topic.Id,
                        };
                        topicsContentTiles.Add(tile);
                    }


                }
            }
            if (topicsContentTiles.Any())
            {
                userMessage.DisplayMessage = "Here your next CME Topics";
                userMessage.SpokenMessage = "Here your next CME Topics";
            }
            var response = VoiceCommandResponse.CreateResponse(userMessage, topicsContentTiles);

            if (topicsContentTiles.Any())
            {
                response.AppLaunchArgument = "topics";
            }

            await voiceServiceConnection.ReportSuccessAsync(response);

        }

        /// <summary>
        /// Show a progress screen. These should be posted at least every 5 seconds for a 
        /// long-running operation, such as accessing network resources over a mobile 
        /// carrier network.
        /// </summary>
        /// <param name="message">The message to display, relating to the task being performed.</param>
        /// <returns></returns>
        private async Task ShowProgressScreen(string message)
        {
            var userProgressMessage = new VoiceCommandUserMessage();
            userProgressMessage.DisplayMessage = userProgressMessage.SpokenMessage = message;

            VoiceCommandResponse response = VoiceCommandResponse.CreateResponse(userProgressMessage);
            await voiceServiceConnection.ReportProgressAsync(response);
        }

        private string Trim(string s, int n)
        {
            if (s.Length > n)
            {
                return new string(s.Take(n - 4).ToArray()) + "...";
            }
            return s;
        }

        /// <summary>
        /// Provide a simple response that launches the app. Expected to be used in the
        /// case where the voice command could not be recognized (eg, a VCD/code mismatch.)
        /// </summary>
        private async void LaunchAppInForeground()
        {
            var userMessage = new VoiceCommandUserMessage();
            userMessage.SpokenMessage = "Start Health Care Doctor";

            var response = VoiceCommandResponse.CreateResponse(userMessage);

            response.AppLaunchArgument = "";

            await voiceServiceConnection.RequestAppLaunchAsync(response);
        }

        /// <summary>
        /// Handle the completion of the voice command. Your app may be cancelled
        /// for a variety of reasons, such as user cancellation or not providing 
        /// progress to Cortana in a timely fashion. Clean up any pending long-running
        /// operations (eg, network requests).
        /// </summary>
        /// <param name="sender">The voice connection associated with the command.</param>
        /// <param name="args">Contains an Enumeration indicating why the command was terminated.</param>
        private void OnVoiceCommandCompleted(VoiceCommandServiceConnection sender, VoiceCommandCompletedEventArgs args)
        {
            if (this.serviceDeferral != null)
            {
                this.serviceDeferral.Complete();
            }
        }
        private DateTime LongtoDateTime(long value, bool utc = false)
        {
            double result = value;
            if (!utc)
                result += TimeZoneInfo.Local.BaseUtcOffset.TotalMilliseconds;
            var dateTime = TimeSpan.FromMilliseconds(result);
            var refDate = new DateTime(1970, 1, 1);
            refDate = refDate + dateTime;
            return refDate;
        }
        /// <summary>
        /// When the background task is cancelled, clean up/cancel any ongoing long-running operations.
        /// This cancellation notice may not be due to Cortana directly. The voice command connection will
        /// typically already be destroyed by this point and should not be expected to be active.
        /// </summary>
        /// <param name="sender">This background task instance</param>
        /// <param name="reason">Contains an enumeration with the reason for task cancellation</param>
        private void OnTaskCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            System.Diagnostics.Debug.WriteLine("Task cancelled, clean up");
            if (this.serviceDeferral != null)
            {
                //Complete the service deferral
                this.serviceDeferral.Complete();
            }
        }


        private const string OLD_HOST = "http://clas-healthcare.cloudapp.net:7181/clas-healthcare/";
        private const string NEW_HOST = "https://healthcare.clas.mobi:8445/clas-healthcare/";
        private const string V2_HOST = "https://healthcare.clas.mobi:8445/clas-healthcare-v2/";
        private const string HOST = V2_HOST;
        private LoginResult LoginResult;
        private async Task<T> PostApiCall<T>(string api, object data)
        {
            var cookieContainer = new CookieContainer();

            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = true,
                UseCookies = true,
                CookieContainer = cookieContainer,
            };

            _httpClient = new HttpClient(handler);

            try
            {
                TrySetHeader();
                var postData = (data is string) ? data.ToString() : JsonConvert.SerializeObject(data);
                Debug.WriteLine("POST: " + HOST + api);
                Debug.WriteLine("DATA: " + postData);


                var request = new StringContent(postData);
                request.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await _httpClient.PostAsync(HOST + api, request);

                response.EnsureSuccessStatusCode();

                var source = await response.Content.ReadAsStringAsync();

                Debug.WriteLine("RESPONSE: " + source);


                var r = JsonConvert.DeserializeObject<T>(source);
                return r;
            }
            catch
            {

            }
            return default(T);
        }


        private async Task<T> GetApiCall<T>(string api)
        {
            if (_httpClient == null)
            {
                var cookieContainer = new CookieContainer();

                var handler = new HttpClientHandler
                {
                    AllowAutoRedirect = true,
                    UseCookies = true,
                    CookieContainer = cookieContainer,
                };

                _httpClient = new HttpClient(handler);
            }
            try
            {
                TrySetHeader();

                var response = await _httpClient.GetAsync(HOST + api);

                response.EnsureSuccessStatusCode();

                var source = await response.Content.ReadAsStringAsync();

                Debug.WriteLine("RESPONSE: " + source);

                return JsonConvert.DeserializeObject<T>(source);

            }
            catch
            {

            }
            return default(T);
        }




        private void TrySetHeader()
        {
            if (_httpClient != null && LoginResult != null)
            {
                SetHeader("userId", LoginResult.UserId);
                SetHeader("sessionId", LoginResult.SessionId);
            }

        }
        public void SetHeader(string key, string value)
        {
            _httpClient.DefaultRequestHeaders.Remove(key);
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, value);
        }
    }

    public sealed class Topic
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }
        [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }
        [JsonProperty("districtId", NullValueHandling = NullValueHandling.Ignore)]
        public string DistrictId { get; set; }
        [JsonProperty("cityId", NullValueHandling = NullValueHandling.Ignore)]
        public string CityId { get; set; }
        [JsonProperty("skypeForBusinessUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string SkypeForBusinessUrl { get; set; }
        [JsonProperty("startDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public long StartDateTime { get; set; }
        [JsonProperty("endDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public long EndDateTime { get; set; }
        [JsonProperty("isOnline", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsOnline { get; set; }
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public bool Status { get; set; }
        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public double[] Location { get; set; }

        [JsonProperty("owner", NullValueHandling = NullValueHandling.Ignore)]
        public string Owner { get; set; }

        [JsonIgnore]
        public bool isFiles { get; set; }




        [JsonIgnore]
        public string Time
        {
            get { return LongtoDateTime(StartDateTime) + "-" + LongtoDateTime(EndDateTime); }
        }
        [JsonIgnore]
        public string StartDateTimeString
        {
            get { return LongtoDateTime(StartDateTime).ToString(); }
        }
        [JsonIgnore]
        public string EndDateTimeString
        {
            get { return LongtoDateTime(EndDateTime).ToString(); }
        }
        private DateTime LongtoDateTime(long value, bool utc = false)
        {
            double result = value;
            if (!utc)
                result += TimeZoneInfo.Local.BaseUtcOffset.TotalMilliseconds;
            var dateTime = TimeSpan.FromMilliseconds(result);
            var refDate = new DateTime(1970, 1, 1);
            refDate = refDate + dateTime;
            return refDate;
        }


    }
    public sealed class TopicData
    {
        [JsonProperty("topics", NullValueHandling = NullValueHandling.Ignore)]
        public Topic[] Data { get; set; }

    }
    public sealed class ApiRootObjectTopic
    {
        [JsonProperty("successful", NullValueHandling = NullValueHandling.Ignore)]
        public bool Successful { get; set; }
        [JsonProperty("errorCode", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorCode { get; set; }
        [JsonProperty("errorDesc", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorDesc { get; set; }
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public TopicData Data { get; set; }
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
    }
    public sealed class ApiRootObjectLoginResult
    {
        [JsonProperty("successful", NullValueHandling = NullValueHandling.Ignore)]
        public bool Successful { get; set; }
        [JsonProperty("errorCode", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorCode { get; set; }
        [JsonProperty("errorDesc", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorDesc { get; set; }
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public LoginResult Data { get; set; }
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
    }



    public sealed class LoginResult
    {

        [JsonProperty("sessionId", NullValueHandling = NullValueHandling.Ignore)]
        public string SessionId { get; set; }
        [JsonProperty("userId", NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }
        [JsonProperty("sessionExpired", NullValueHandling = NullValueHandling.Ignore)]
        public long SessionExpired { get; set; }




    }

    public sealed class AccountManager
    {
        private HttpClient _httpClient;
        private const string RESOURCE_NAME_USER = "HealthCareCredentialUser";
        private const string RESOURCE_NAME_EMAIL = "HealthCareCredentialEmail";
        public int AuthenticationProvider { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public AccountManager()
        {
            AuthenticationProvider = 99;
        }

        public void RestoreAsync()
        {

            var vault = new PasswordVault();
            try
            {

                foreach (var credential in vault.RetrieveAll())
                {
                    credential.RetrievePassword();
                    if (credential.Resource.Equals(RESOURCE_NAME_EMAIL))
                    {
                        Username = credential.UserName;
                        Password = credential.Password;
                    }
                    else if (credential.Resource.Equals(RESOURCE_NAME_USER))
                    {
                        AuthenticationProvider = int.Parse(credential.UserName.Split(':')[0]);
                        UserId = credential.UserName.Split(':')[1];
                        Token = vault.Retrieve(RESOURCE_NAME_USER, credential.UserName).Password;
                    }

                }



            }
            catch (Exception)
            {
                // If no credentials have been stored with the given RESOURCE_NAME, an exception
                // is thrown.
            }
        }


    }
}
