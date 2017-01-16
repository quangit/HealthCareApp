using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Helpers;
using HealthCare.Models;
using HealthCare.Models.ChBaseModel;
using HealthCare.WebServices.Interfaces;
using PersonModel = HealthCare.Models.ChBaseModel.PersonModel;

namespace HealthCare.WebServices
{
    public class ChBaseWS : IChBaseWS
    {
        public Task<ObservableCollection<HeightModel>> GetHeights()
        {
            return ChBaseHelper.GetHeights();
        }

        public Task<ObservableCollection<WeightModel>> GetWeights()
        {
            return ChBaseHelper.GetWeights();

        }

        public Task<ObservableCollection<ProcedureModel>> GetProcedure()
        {
            return ChBaseHelper.GetProcedure();
        }

        public Task<ObservableCollection<MedicationModel>> GetMedication()
        {
            return ChBaseHelper.GetMedication();
        }

        public Task<ObservableCollection<ImmunizationModel>> GetImmunization()
        {
            return ChBaseHelper.GetImmunization();

        }

        public Task<ObservableCollection<BloodGlucoseModel>> GetBloodGlucose()
        {
            return ChBaseHelper.GetBloodGlucose();

        }

        public Task<ObservableCollection<BloodPressureModel>> GetBloodPressure()
        {
            return ChBaseHelper.GetBloodPressure();

        }

        public Task<string> GetAppToken()
        {
            return ChBaseHelper.GetAppToken();
        }

        public Task<PersonModel> GetUserInfo()
        {
            return ChBaseHelper.GetUserInfo();
        }

        public Task<string> GetPatientInfo(string id)
        {
            return ChBaseHelper.GetPatientInfo(id);
        }

        public Task Register(UserModel newAccount)
        {
            return ChBaseHelper.CreateAccount(newAccount);
        }

        public Task<bool> AddHeight(double value)
        {
            return  ChBaseHelper.AddHeight(value);
        }

        public Task<bool> AddWeight(double value)
        {
            return ChBaseHelper.AddWeight(value);
        }

        public Task<bool> AddProcedure(ProcedureModel value)
        {
            return ChBaseHelper.AddProcedure(value);
        }

        public Task<bool> AddMedication(MedicationModel value)
        {
            return ChBaseHelper.AddMedication(value);
        }

        public Task<bool> AddImmunization(ImmunizationModel value)
        {
            return ChBaseHelper.AddImmunization(value);
        }

        public Task<bool> AddBloodGlucose(BloodGlucoseModel value)
        {
            return ChBaseHelper.AddBloodGlucose(value);
        }

        public Task<bool> AddBloodPressure(BloodPressureModel value)
        {
            return ChBaseHelper.AddBloodPressure(value);
        }

        public Task<bool> RemoveData(string id)
        {
            return ChBaseHelper.RemoveData(id);
        }

        public Task<string> UploadFile(byte[] data, string fileName)
        {
            return ChBaseHelper.UploadImage(data, fileName);
        }
        public Task ShareHealthInformationViaEmail(ChBaseHealthInfoModel model)
        {
            return ChBaseHelper.ShareHealthInformationViaEmail(model);
        }

        
    }
}
