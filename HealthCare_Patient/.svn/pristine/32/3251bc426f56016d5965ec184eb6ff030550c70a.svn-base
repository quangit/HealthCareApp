using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using HealthCare.DependencyServices;
using HealthCare.Enums;
using HealthCare.Exceptions;
using HealthCare.Models;
using HealthCare.Models.ChBaseModel;
using HealthCare.Objects;
using HealthCare.ViewModels;

using Newtonsoft.Json;
using Org.BouncyCastle.Asn1;
using Xamarin.Forms;
using PersonModel = HealthCare.Models.ChBaseModel.PersonModel;

namespace HealthCare.Helpers
{
    public static class ChBaseHelper
    {
        private const string UrlReq = "https://qa-grh-platform-sa.chbase.com/platform/wildcat.ashx";
        private const string UrlUpload = "http://chbase.bacsi24x7.vn/api/UploadImage";
        public static CultureInfo EnCultureInfo = new System.Globalization.CultureInfo("en-US");

        private const string CreateAuthenticatedSessionToken =
            "<wc-request:request xmlns:wc-request=\"urn:com.microsoft.wc.request\"><header><method>CreateAuthenticatedSessionToken</method><method-version>2</method-version><app-id>" +
            "7dba8069-110c-4952-9b1f-9d2859480100</app-id><culture-code>en-US</culture-code>" +
            "<msg-time>{0}.242Z</msg-time><msg-ttl>1800</msg-ttl>" +
            "<version>HV-NET/1.0.0.0 (Microsoft Windows NT 10.0.14342.0; CLR 4.0.30319.42000)</version>" +
            "</header><info><auth-info><app-id>7dba8069-110c-4952-9b1f-9d2859480100</app-id><credential>" +
            "<appserver2><sig digestMethod=\"SHA1\" sigMethod=\"RSA-SHA1\" thumbprint=\"36C8E4F479FB649CA9FD6DB77F3CD40CB32883ED\">HmacData</sig><content>" +
            "<app-id>7dba8069-110c-4952-9b1f-9d2859480100</app-id><hmac>HMACSHA256</hmac>" +
            "<signing-time>{1}.2115431Z</signing-time>" +
            "</content></appserver2></credential></auth-info></info></wc-request:request>";

        private const string GetPersonInfo =
            "<wc-request:request xmlns:wc-request=\"urn:com.microsoft.wc.request\"><auth><hmac-data algName=\"HMACSHA256\">HmacData</hmac-data></auth><header><method>GetPersonInfo</method><method-version>1</method-version><auth-session><auth-token>{0}</auth-token><user-auth-token>{1}</user-auth-token></auth-session><culture-code>en-US</culture-code><msg-time>{2}.641Z</msg-time><msg-ttl>1800</msg-ttl><version>HV-NET/1.0.0.0 (Microsoft Windows NT 10.0.14342.0; CLR 4.0.30319.42000)</version><info-hash><hash-data algName=\"SHA256\">InfoHash</hash-data></info-hash></header><info/></wc-request:request>";

        private const string GetThings =
            "<wc-request:request xmlns:wc-request=\"urn:com.microsoft.wc.request\"><auth><hmac-data algName=\"HMACSHA256\">HmacData</hmac-data></auth><header><method>GetThings</method><method-version>3</method-version><record-id>{0}</record-id><auth-session><auth-token>{1}</auth-token><user-auth-token>{2}</user-auth-token></auth-session><culture-code>en-US</culture-code><msg-time>{3}.242Z</msg-time><msg-ttl>1800</msg-ttl><version>HV-NET/1.0.0.0 (Microsoft Windows NT 10.0.14342.0; CLR 4.0.30319.42000)</version><info-hash><hash-data algName=\"SHA256\">InfoHash</hash-data></info-hash></header><info><group><filter><type-id>{4}</type-id><thing-state>Active</thing-state></filter><format><section>core</section><xml /><type-version-format>{5}</type-version-format></format></group></info></wc-request:request>";

        private const string RemoveThings =
          "<wc-request:request xmlns:wc-request=\"urn:com.microsoft.wc.request\"><auth><hmac-data algName=\"HMACSHA256\">HmacData</hmac-data></auth><header><method>RemoveThings</method><method-version>1</method-version><record-id>{0}</record-id><auth-session><auth-token>{1}</auth-token><user-auth-token>{2}</user-auth-token></auth-session><culture-code>en-US</culture-code><msg-time>{3}.242Z</msg-time><msg-ttl>1800</msg-ttl><version>HV-NET/1.0.0.0 (Microsoft Windows NT 10.0.14342.0; CLR 4.0.30319.42000)</version><info-hash><hash-data algName=\"SHA256\">InfoHash</hash-data></info-hash></header><info>{4}</info></wc-request:request>";

        private const string PutThings =
            "<wc-request:request xmlns:wc-request=\"urn:com.microsoft.wc.request\"><auth><hmac-data algName=\"HMACSHA256\">HmacData</hmac-data></auth><header><method>PutThings</method><method-version>2</method-version><record-id>{0}</record-id><auth-session><auth-token>{1}</auth-token><user-auth-token>{2}</user-auth-token></auth-session><culture-code>en-US</culture-code><msg-time>{3}.16Z</msg-time><msg-ttl>1800</msg-ttl><version>HV-NET/1.0.0.0 (Microsoft Windows NT 10.0.14342.0; CLR 4.0.30319.42000)</version><info-hash><hash-data algName=\"SHA256\">InfoHash</hash-data></info-hash></header><info><thing><type-id>{4}</type-id><thing-state>Active</thing-state><flags>0</flags><data-xml>XMLData<common /></data-xml></thing></info></wc-request:request>";

        private const string AddHeightXml =
            "<height><when><date><y>{0}</y><m>{1}</m><d>{2}</d></date><time><h>{3}</h><m>{4}</m><s>{5}</s><f>{6}</f></time></when><value><m>{7}</m></value></height>";

        private const string AddWeightXml =
            "<weight><when><date><y>{0}</y><m>{1}</m><d>{2}</d></date><time><h>{3}</h><m>{4}</m><s>{5}</s><f>{6}</f></time></when><value><kg>{7}</kg></value></weight>";

        private const string AddProcedureXml =
            "<procedure><when><structured><date><y>{0}</y><m>{1}</m><d>{2}</d></date><time><h>{3}</h><m>{4}</m><s>{5}</s><f>{6}</f></time></structured></when><name><text>{7}</text></name><anatomic-location><text>{8}</text></anatomic-location><primary-provider><name><full>{9}</full></name><type><text>{10}</text></type></primary-provider><secondary-provider><name><full>{11}</full></name><type><text>note</text></type></secondary-provider></procedure>";

        private const string AddMedicationXml =
            "<medication><name><text>{0}</text></name><generic-name><text>{1}</text></generic-name><dose><display>{2}</display></dose><strength><display>{3}</display></strength><frequency><display>{4}</display></frequency><route><text>{5}</text></route><indication><text>{6}</text></indication><date-started><structured><date><y>{7}</y><m>{8}</m><d>{9}</d></date><time><h>0</h><m>0</m><s>0</s><f>0</f></time></structured></date-started><date-discontinued><structured><date><y>{10}</y><m>{11}</m><d>{12}</d></date><time><h>0</h><m>0</m><s>0</s><f>0</f></time></structured></date-discontinued><prescribed><text>{13}</text></prescribed><prescription><prescribed-by><name><full>{14}</full></name></prescribed-by></prescription></medication>";

        private const string AddImmunizationXml =
            "<immunization><name><text>{0}</text></name><administration-date><structured><date><y>{1}</y><m>{2}</m><d>{3}</d></date><time><h>0</h><m>0</m><s>0</s><f>0</f></time></structured></administration-date><administrator><name><full>{4}</full></name></administrator><manufacturer><text>{5}</text></manufacturer><lot>{6}</lot><route><text>{7}</text></route><expiration-date><y>{8}</y><m>{9}</m><d>{10}</d></expiration-date><sequence>{11}</sequence><anatomic-surface><text>{12}</text></anatomic-surface><adverse-event>{13}</adverse-event><consent>{14}</consent></immunization>";

        private const string AddBloodGlucoseXml =
            "<blood-glucose><when><date><y>{0}</y><m>{1}</m><d>{2}</d></date><time><h>0</h><m>0</m><s>0</s><f>0</f></time></when><value><mmolPerL>{3}</mmolPerL></value><glucose-measurement-type><text>{4}</text></glucose-measurement-type><outside-operating-temp>{5}</outside-operating-temp><is-control-test>{6}</is-control-test><normalcy>{7}</normalcy><measurement-context><text>{8}</text></measurement-context></blood-glucose>";

        private const string AddBloodPressureXml =
            "<blood-pressure><when><date><y>{0}</y><m>{1}</m><d>{2}</d></date><time><h>0</h><m>0</m><s>0</s><f>0</f></time></when><systolic>{3}</systolic><diastolic>{4}</diastolic><pulse>{5}</pulse><irregular-heartbeat>{6}</irregular-heartbeat></blood-pressure>";

        public const string SignupUrl = "signup.aspx";

        public static string AutoFillDataJs
        {
            get
            {
                string fillFistName = $"document.getElementById('ctl00_ContentPlaceHolder1_SignupContainerCtrl1_SignupCtrl1_TxtFirstName').value = '{UserViewModel.Instance.CurrentUser.FirstName}';"; 
                string fillLastName = $"document.getElementById('ctl00_ContentPlaceHolder1_SignupContainerCtrl1_SignupCtrl1_TxtLastName').value = '{UserViewModel.Instance.CurrentUser.LastName}';";
                string fillPersonFirstName = $"document.getElementById('ctl00_ContentPlaceHolder1_SignupContainerCtrl1_SignupCtrl1_TxtPersonFirstName').value = '{UserViewModel.Instance.CurrentUser.FirstName}';";
                string fillPersonLastName = $"document.getElementById('ctl00_ContentPlaceHolder1_SignupContainerCtrl1_SignupCtrl1_TxtPersonLastName').value = '{UserViewModel.Instance.CurrentUser.LastName}';";
                string fillEmail = $"document.getElementById('ctl00_ContentPlaceHolder1_SignupContainerCtrl1_SignupCtrl1_TxtEmail').value = '{UserViewModel.Instance.CurrentUser.Email}';";
                string fillGender = UserViewModel.Instance.CurrentUser.Gender == Gender.Female ? "document.getElementById('ctl00_ContentPlaceHolder1_SignupContainerCtrl1_SignupCtrl1_radFemale').checked = true;" : "document.getElementById('ctl00_ContentPlaceHolder1_SignupContainerCtrl1_SignupCtrl1_radMale').checked = true;";

                string fillDoB = "";
                switch (Common.OS)
                {
                    case TargetPlatform.WinPhone:
                        fillDoB = $"document.getElementById('ctl00_ContentPlaceHolder1_SignupContainerCtrl1_SignupCtrl1_dtDOB_CBTextBoxID').value ='{UserViewModel.Instance.CurrentUser.BirthDay.ToString("yyyy-MM-dd")}';";
                        break;
                    case TargetPlatform.Android:
                        
                        fillDoB = $"document.getElementsByClassName('html5-date-ctrl')[0].value ='{UserViewModel.Instance.CurrentUser.BirthDay.ToString("yyyy-MM-dd")}';" +
                                  $"document.getElementsByClassName('html5-date-ctrl')[0].blur();";

                        break;
                    case TargetPlatform.iOS:
                        fillDoB = $"document.getElementsByClassName('html5-date-ctrl')[0].value ='{UserViewModel.Instance.CurrentUser.BirthDay.ToString("yyyy-MM-dd")}';";
                        break;
                }
                string js = fillFistName + fillLastName + fillPersonFirstName + fillPersonLastName + fillEmail + fillGender + fillDoB;
                return js;
            }
        }
        private const string format = "yyyy-MM-ddThh:mm:ss";

        public static string UserToken { get; set; }

        public static string ShareViaChbaseUrl
        {
            get
            {
                int culId = 1033;
                if (Common.GetDeviceLanguage() == "vi")
                    culId = 1066;
                return "https://qa-grh-shell-sa.chbase.com/ShareRecord.aspx?appid=7dba8069-110c-4952-9b1f-9d2859480100&extrecordid=" + RecordId + "&redirect=http%3a%2f%2fchbase.bacsi24x7.vn%2fRedirect.aspx&aib=true&trm=post&lcid="+ culId;
            }
        }
        public static string RecordId { get; set; }
        public static string SharedSecret { get; set; }

        public static string AppToken { get; set; }

        public static async Task<string> GetAppToken()
        {
            var parser = new ChBaseParser();
            var temp = string.Format(CreateAuthenticatedSessionToken, DateTime.UtcNow.ToString(format),
                DateTime.UtcNow.ToString(format));

             var content = GetInfo(temp, "content");
            var hash = await DependencyService.Get<IChBaseHash>().SigHash(content);
            temp = temp.Replace("HmacData", hash);
            return parser.GetAuthToken(await PostData(temp));
        }

        public static async Task<ObservableCollection<HeightModel>> GetHeights()
        {
            var typeId = "40750a6a-89b2-455c-bd8d-b420a4cb500b";
            var data = await GetData(typeId, RecordId);
            var parser = new ChBaseParser();
            return parser.ParseHeights(data);
        }

        public static async Task<bool> AddHeight(double value)
        {
            var typeId = "40750a6a-89b2-455c-bd8d-b420a4cb500b";
            var xmlData = string.Format(EnCultureInfo, AddHeightXml, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond, value);
            var data = await PutData(xmlData, typeId, RecordId);
            var parser = new ChBaseParser();
            return parser.IsSuccess(data);
        }

        public static async Task<bool> RemoveData(string info)
        {
            var data = await RemoveData(RecordId, info);
            var parser = new ChBaseParser();
            return parser.IsSuccess(data);
        }


        public static async Task<ObservableCollection<WeightModel>> GetWeights()
        {
            var typeId = "3d34d87e-7fc1-4153-800f-f56592cb0d17";
            var data = await GetData(typeId, RecordId);
            var parser = new ChBaseParser();
            return parser.ParseWeights(data);
        }

        public static async Task<bool> AddWeight(double value)
        {
            var typeId = "3d34d87e-7fc1-4153-800f-f56592cb0d17";
            var xmlData = string.Format(EnCultureInfo, AddWeightXml, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond, value);
            var data = await PutData(xmlData, typeId, RecordId);
            var parser = new ChBaseParser();
            return parser.IsSuccess(data);
        }

        public static async Task<ObservableCollection<ProcedureModel>> GetProcedure()
        {
            var typeId = "df4db479-a1ba-42a2-8714-2b083b88150f";
            var data = await GetData(typeId, RecordId);
            var parser = new ChBaseParser();
            return parser.ParseProcedure(data);
        }
        public static async Task<bool> AddProcedure(ProcedureModel model)
        {
            var typeId = "df4db479-a1ba-42a2-8714-2b083b88150f";
            var xmlData = string.Format(AddProcedureXml, model.When.Year, model.When.Month, model.When.Day,
                DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond, model.Name, model.Location, model.PrimaryProvider.Name, model.PrimaryProvider.Type, model.SecondaryProvider.Name);
            var data = await PutData(xmlData, typeId, RecordId);
            var parser = new ChBaseParser();
            return parser.IsSuccess(data);
        }


        public static async Task<ObservableCollection<MedicationModel>> GetMedication()
        {
            var typeId = "30cafccc-047d-4288-94ef-643571f7919d";
            var data = await GetData(typeId, RecordId);
            var parser = new ChBaseParser();
            return parser.ParseMedication(data);
        }

        public static async Task<bool> AddMedication(MedicationModel model)
        {
            var typeId = "30cafccc-047d-4288-94ef-643571f7919d";
            var xmlData = string.Format(AddMedicationXml, model.Name, model.GenericName, model.Dose, model.Strength, model.HowOftenTaken, model.HowTaken, model.Indication, model.DateStarted.Year, model.DateStarted.Month, model.DateStarted.Day, model.DateDiscontinued.Year
                , model.DateDiscontinued.Month, model.DateDiscontinued.Day, model.Prescribed, model.PrescribedBy);
            var data = await PutData(xmlData, typeId, RecordId);
            var parser = new ChBaseParser();
            return parser.IsSuccess(data);
        }
        public static async Task<ObservableCollection<ImmunizationModel>> GetImmunization()
        {
            var typeId = "cd3587b5-b6e1-4565-ab3b-1c3ad45eb04f";
            var data = await GetData(typeId, RecordId);
            var parser = new ChBaseParser();
            return parser.ParseImmunization(data);
        }


        public static async Task<bool> AddImmunization(ImmunizationModel model)
        {
            var typeId = "cd3587b5-b6e1-4565-ab3b-1c3ad45eb04f";
            var xmlData = string.Format(AddImmunizationXml, model.Name, model.DateAdministrated.Year, model.DateAdministrated.Month, model.DateAdministrated.Day, model.Administrator, model.Manufacturer, model.Lot, model.Route, model.ExpirationDate.Year,
                model.ExpirationDate.Month, model.ExpirationDate.Day, model.Sequence, model.AnatomicSurface, model.AdverseEvent, model.Consent);
            var data = await PutData(xmlData, typeId, RecordId);
            var parser = new ChBaseParser();
            return parser.IsSuccess(data);
        }


        public static async Task<ObservableCollection<BloodGlucoseModel>> GetBloodGlucose()
        {
            var typeId = "879e7c04-4e8a-4707-9ad3-b054df467ce4";
            var data = await GetData(typeId, RecordId);
            var parser = new ChBaseParser();
            return parser.ParseBloodGlucose(data);
        }

        public static async Task<bool> AddBloodGlucose(BloodGlucoseModel model)
        {
            var typeId = "879e7c04-4e8a-4707-9ad3-b054df467ce4";
            var xmlData = string.Format(AddBloodGlucoseXml, model.When.Year, model.When.Month, model.When.Day, model.Value, model.Type, model.OutsideOperatingTemp.ToString().ToLower(), model.IsControlTest.ToString().ToLower(), model.Normalcy, model.MeasurementContext);
            var data = await PutData(xmlData, typeId, RecordId);
            var parser = new ChBaseParser();
            return parser.IsSuccess(data);
        }

        public static async Task<ObservableCollection<BloodPressureModel>> GetBloodPressure()
        {
            var typeId = "ca3c57f4-f4c1-4e15-be67-0a3caf5414ed";
            var data = await GetData(typeId, RecordId);
            var parser = new ChBaseParser();
            return parser.ParseBloodPressure(data);
        }

        public static async Task<bool> AddBloodPressure(BloodPressureModel model)
        {
            var typeId = "ca3c57f4-f4c1-4e15-be67-0a3caf5414ed";
            var xmlData = string.Format(AddBloodPressureXml, model.When.Year, model.When.Month, model.When.Day, model.Systolic, model.Diastolic, model.Pulse, model.IrregularHeartbeat.ToString().ToLower());
            var data = await PutData(xmlData, typeId, RecordId);
            var parser = new ChBaseParser();
            return parser.IsSuccess(data);
        }

        public static async Task<string> PutData(string xmlData, string typeId, string recordId)
        {
            var temp = string.Format(PutThings, recordId, AppToken, UserToken, DateTime.UtcNow.ToString(format), typeId);
            temp = temp.Replace("XMLData", xmlData);
            var info = GetInfo(temp, "info");
            var infoHash = DependencyService.Get<IChBaseHash>().InfoHash(info);
            temp = temp.Replace("InfoHash", infoHash);
            var header = GetHeader(temp);
            var hash = DependencyService.Get<IChBaseHash>().Hmac(SharedSecret, header);
            temp = temp.Replace("HmacData", hash);

            var data = await PostData(temp);
            return data;
        }

        private static async Task<string> GetData(string typeId, string recordId)
        {
            var temp = string.Format(GetThings, recordId, AppToken, UserToken, DateTime.UtcNow.ToString(format), typeId,
                typeId);
            var info = GetInfo(temp, "info");
            var infoHash = DependencyService.Get<IChBaseHash>().InfoHash(info);
            temp = temp.Replace("InfoHash", infoHash);
            var header = GetHeader(temp);
            var hash = DependencyService.Get<IChBaseHash>().Hmac(SharedSecret, header);
            var data = await PostData(temp.Replace("HmacData", hash));
            return data;
        }

        private static async Task<string> RemoveData(string recordId, string xmlData)
        {
            var temp = string.Format(RemoveThings, recordId, AppToken, UserToken, DateTime.UtcNow.ToString(format), xmlData);
            var info = GetInfo(temp, "info");
            var infoHash = DependencyService.Get<IChBaseHash>().InfoHash(info);
            temp = temp.Replace("InfoHash", infoHash);
            var header = GetHeader(temp);
            var hash = DependencyService.Get<IChBaseHash>().Hmac(SharedSecret, header);
            var data = await PostData(temp.Replace("HmacData", hash));
            return data;
        }

        public static async Task<string> UploadImage(byte[] bytes, string fileName)
        {
            try
            {

                HttpClient client = new HttpClient();
                MultipartFormDataContent form = new MultipartFormDataContent();
                HttpContent content = new StringContent("file");
                //form.Add(content, "file");

                var stream = new MemoryStream(bytes);
                content = new StreamContent(stream);
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "file",
                    FileName = fileName
                };  
                form.Add(content);
                var response = await client.PostAsync(UrlUpload, form);
                var temp = response.Content.ReadAsStringAsync().Result;
                var parser = new ChBaseParser();
                var url = parser.ParseUrlImage(temp);
                return url;
            }
            catch
            {
                return null;
            }
        }

        public static async Task<PersonModel> GetUserInfo()
        {
            try
            {
                var parser = new ChBaseParser();
                var temp = string.Format(GetPersonInfo, AppToken, UserToken, DateTime.UtcNow.ToString(format));
                var info = GetInfo(temp, "info");
                var infoHash = DependencyService.Get<IChBaseHash>().InfoHash(info);
                temp = temp.Replace("InfoHash", infoHash);
                var header = GetHeader(temp);
                var hash = DependencyService.Get<IChBaseHash>().Hmac(SharedSecret, header);
                return parser.GetUserInfo(await PostData(temp.Replace("HmacData", hash)));
            }
            catch
            {
                return null;
            }
        }

        public static async Task<string> GetPatientInfo(string id)
        {
            try
            {

                using (var client2 = new HttpClient())
                {
                    client2.BaseAddress = new Uri(AppConstant.RootUrl);
                    client2.DefaultRequestHeaders.Accept.Clear();
                    client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // New code:
                    HttpResponseMessage response = await client2.GetAsync(AppConstant.ChBasePatientInfo + id);
                    if (response.IsSuccessStatusCode)
                    {
                        string tam = await response.Content.ReadAsStringAsync();
                        return tam;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }

        }

        public static async Task CreateAccount(UserModel newUser)
        {
            try
            {
                HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30) };

                var raw = new
                {
                    newUser.FirstName,
                    newUser.LastName,
                    Email= newUser.Email,
                    Settings.CurrentUser?.Password,
                    DoB = newUser.BirthDay.ToString("MM/dd/yyyy"),
                    Gender = newUser.Gender == Gender.Male ? "M" : "F",
                    Lang = "vi"
                };
                var bodyParam = JsonConvert.SerializeObject(raw);

                var message = new HttpRequestMessage(HttpMethod.Post, AppConstant.ChBaseUrl + "API/CreateAccount");
                if (!string.IsNullOrWhiteSpace(bodyParam))
                    message.Content = new StringContent(bodyParam, Encoding.UTF8, "application/json");

                var response = await client.SendAsync(message);
                var responseString = await response.Content.ReadAsStringAsync();
                message.Dispose();

                if (string.IsNullOrWhiteSpace(responseString))
                    throw new NetworkException();
                if (!JsonUtils.ValidJsonString(responseString))
                    throw new ApiException("");

                var parseResponse = JsonConvert.DeserializeObject<ApiResponse>(responseString);

                if (!parseResponse.IsSuccessful)
                {
                    throw new ApiException(parseResponse.ErrorDescription, parseResponse.ErrorCode);
                }                
            }
            catch (TaskCanceledException)
            {
                throw new NetworkException();
            }
            catch (OperationCanceledException)
            {
                throw new NetworkException();
            }
        }

        private static async Task<string> PostData(string data)
        {
            var client = new HttpClient();
            var msg = new HttpRequestMessage(HttpMethod.Post, UrlReq);
            msg.Content = new StringContent(data, Encoding.UTF8, "text/xml");
            var rs = await client.SendAsync(msg);
            var str = await rs.Content.ReadAsStringAsync();
            return str;
        }

        private static string GetInfo(string data, string node)
        {
            var doc = XDocument.Parse(data, LoadOptions.PreserveWhitespace);
            var header = doc.Descendants(node).FirstOrDefault().ToString(SaveOptions.DisableFormatting);
            return header;
        }

        private static string GetHeader(string data)
        {
            var doc = XDocument.Parse(data, LoadOptions.PreserveWhitespace);
            var header = doc.Descendants("header").FirstOrDefault().ToString(SaveOptions.DisableFormatting);
            return header;
        }

        public static async Task ShareHealthInformationViaEmail(ChBaseHealthInfoModel model)
        {
            try
            {
                HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30) };

               
                var bodyParam = JsonConvert.SerializeObject(model);

                var message = new HttpRequestMessage(HttpMethod.Post, AppConstant.ChBaseUrl + "API/ShareHealthInfo");
                if (!string.IsNullOrWhiteSpace(bodyParam))
                    message.Content = new StringContent(bodyParam, Encoding.UTF8, "application/json");

                var response = await client.SendAsync(message);
                var responseString = await response.Content.ReadAsStringAsync();
                message.Dispose();

                if (string.IsNullOrWhiteSpace(responseString))
                    throw new NetworkException();
                if (!JsonUtils.ValidJsonString(responseString))
                    throw new ApiException("");

                var parseResponse = JsonConvert.DeserializeObject<ApiResponse>(responseString);

                if (!parseResponse.IsSuccessful)
                {
                    throw new ApiException(parseResponse.ErrorDescription, parseResponse.ErrorCode);
                }
            }
            catch (TaskCanceledException)
            {
                throw new NetworkException();
            }
            catch (OperationCanceledException)
            {
                throw new NetworkException();
            }
          
        }

        public static List<string> GetListImages(string txt)
        {
            return (from Match m in Regex.Matches(txt, "https?://(\\S+?)\\.(jpg|png|gif|jpeg)") select m.Value).ToList();
        }

        public static int GetLanguageCode()
        {
            //1: English
            //2: Svenska
            //3: Vietnam

            return Common.GetDeviceLanguage() == "vi" ? 3 : 1;
        }
    }
}