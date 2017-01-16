using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Helpers;
using HealthCare.Models.ChBaseModel;
using HealthCare.ViewModels.CHBases;
using HealthCare.WebServices.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HealthCare.WebServices
{
    class PatientCHBaseWS : BaseFakeWebService, IPatientCHBaseWS
    {
        public async Task<bool> LinkToPatient(string id)
        {
            try
            {

                var url = AppConstant.RootUrl + AppConstant.ChBaseLinkToPatient + id;
                var data = await SendHttpRequest(HttpMethod.Get, url);
                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }

        public async Task<CHBasePatientInfoModel> GetCHBaseInfo()
        {
            var url = AppConstant.RootUrl + AppConstant.ChBasePatientInfo;
            var data = await SendHttpRequest(HttpMethod.Get, url);
            
            return JsonUtils.ParseData<CHBasePatientInfoModel>(data);
        }

        public async Task<PaymentViewModel.ListFees> GetPackageInfo(int status = 1, int start = 0, int length=20)
        {
            var url = AppConstant.RootUrl+AppConstant.ChBasePackageFeeInfo;
            url = string.Format(url, status, start, length);
            var data = await SendHttpRequest(HttpMethod.Get, url);
            return JsonUtils.ParseData<PaymentViewModel.ListFees>(data);
        }

        public async Task<DetailPackageFeeModel> GetPackageFeeDetail(string idPackageFee)
        {
            var url = AppConstant.RootUrl+AppConstant.ChBasePackageDetail;
            url = string.Format(url, idPackageFee);
            var data = await SendHttpRequest(HttpMethod.Get, url);
            return JsonUtils.ParseData<DetailPackageFeeModel>(data);
        }

        public async Task<ResultPurchaseModel> PurchaseCHBasePackage(string idPackageFee)
        {
            try
            {
                var url = AppConstant.RootUrl+AppConstant.ChBasePurchasePackageFee;
                url = string.Format(url, idPackageFee);
                var data = await SendHttpRequest(HttpMethod.Get, url);
                return JsonUtils.ParseData<ResultPurchaseModel>(data);
            }
            catch (Exception)
            {
                return null;
            }
            
        }
    }     

}
