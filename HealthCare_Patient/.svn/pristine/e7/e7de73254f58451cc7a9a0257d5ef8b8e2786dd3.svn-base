using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Models.ChBaseModel;
using HealthCare.ViewModels.CHBases;
using Newtonsoft.Json.Linq;

namespace HealthCare.WebServices.Interfaces
{
    public interface IPatientCHBaseWS
    {
        Task<bool> LinkToPatient(string id);
        Task<CHBasePatientInfoModel> GetCHBaseInfo();

        Task<PaymentViewModel.ListFees> GetPackageInfo(int status =1, int start =0, int length =10);
        Task<DetailPackageFeeModel> GetPackageFeeDetail(string idPackageFee);
        Task<ResultPurchaseModel> PurchaseCHBasePackage(string idPackageFee);
    }
}
