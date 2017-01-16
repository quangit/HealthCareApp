using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using HealthCare.Core.Models;
using HealthCare.Core.Resources;
using HealthCare.Core.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HealthCare.Core.Services
{
    public interface ICmeService
    {
        Task<List<string>> GetCategories();
        Task<string> GetClassDetail(CmeClass item);
        Task<CmeCategory> GetClasses(string cat);
        Task<List<CmeClass>> SearchClasses(string searchTerm);
    }

    public class CmeService : ICmeService
    {
        private const string CME_HOST = "http://api.dfc.datafirst.vn:8081/api/";
        private const string HOST = CME_HOST;
        private readonly IHttpService _httpService;
        private readonly IReporterService _reporterService;

        public CmeService(IHttpService httpService, IReporterService reporterService)
        {
            _httpService = httpService;
            _reporterService = reporterService;
        }

        public async Task<List<string>> GetCategories()
        {
            _reporterService.ShowProgress();
            try
            {
                var url = HOST + "cme/category/0/0";
                var resp = await _httpService.GetAsync(url);
                Debug.WriteLine("GET CME: " + url);
                Debug.WriteLine("GET RESP CME: " + resp);
                return JsonConvert.DeserializeObject<List<string>>(resp);
            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                _reporterService.StopProgress();
            }
            return new List<string>();
        }

        public async Task<CmeCategory> GetClasses(string cat)
        {
            _reporterService.ShowProgress();
            try
            {
                var url = HOST + "cme-get-class-by-cate";
                var resp = await _httpService.PostAsync(url, "[\"" + cat + "\"]");

                Debug.WriteLine("Post CME: " + url);
                Debug.WriteLine("Post Data CME: " + "[\"" + cat + "\"]");
                Debug.WriteLine("Post RESP CME: " + resp);
                var r = JsonConvert.DeserializeObject<JToken>(resp);
                var ret = r[cat].ToObject<List<CmeClass>>();
                return new CmeCategory {Name = cat, CmeClasses = ret};
            }
            catch (HttpRequestException ex)
            {
                _reporterService.ReportError(AppResources.Error_NoInternet, ErrorType.Message);
            }
            catch (NoInternetConnection)
            {
                _reporterService.ReportError(AppResources.Error_NoInternet, ErrorType.Message);
            }
            catch (Exception ex)
            {
                _reporterService.ReportError("Exception: " + ex.Message, ErrorType.Message);
            }
            finally
            {
                _reporterService.StopProgress();
            }
            return new CmeCategory();
        }

        public async Task<string> GetClassDetail(CmeClass item)
        {
            _reporterService.ShowProgress();
            try
            {
                var url = HOST + "cme-full-desc/" + item.id;
                var resp = await _httpService.GetAsync(url);
                var r = JsonConvert.DeserializeObject<JToken>(resp);
                return r["full_description"].ToObject<string>();
            }
            catch (HttpRequestException ex)
            {
                _reporterService.ReportError(AppResources.Error_NoInternet, ErrorType.Message);
            }
            catch (NoInternetConnection)
            {
                _reporterService.ReportError(AppResources.Error_NoInternet, ErrorType.Message);
            }
            catch (Exception ex)
            {
                _reporterService.ReportError("Exception: " + ex.Message, ErrorType.Message);
            }
            finally
            {
                _reporterService.StopProgress();

            }
            return string.Empty;
        }

        public async Task<List<CmeClass>> SearchClasses(string searchTerm)
        {
//http://api.dfc.datafirst.vn:8081/api/cme/class/0/
            try
            {
                _reporterService.ShowProgress();
                var param = "0";
                if (!string.IsNullOrWhiteSpace(searchTerm))
                    param = Uri.EscapeUriString(searchTerm);
                var url = HOST + "cme/class/0/" + param;
                var resp = await _httpService.GetAsync(url);

                var ret = JsonConvert.DeserializeObject<List<CmeClass>>(resp);
                _reporterService.StopProgress();
                if (ret == null)
                    return null;
                return ret;
            }
            catch (HttpRequestException ex)
            {
                _reporterService.ReportError(AppResources.Error_NoInternet, ErrorType.Message);
            }
            catch (NoInternetConnection)
            {
                _reporterService.ReportError(AppResources.Error_NoInternet, ErrorType.Message);
            }
            catch (Exception ex)
            {
                _reporterService.ReportError("Exception: " + ex.Message, ErrorType.Message);
            }
            finally
            {
                _reporterService.StopProgress();
            }
            return null;
        }
    }
}