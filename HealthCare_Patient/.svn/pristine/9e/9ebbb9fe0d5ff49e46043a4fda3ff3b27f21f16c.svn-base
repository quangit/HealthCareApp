using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Models;
using HealthCare.Models.ChBaseModel;
using PersonModel = HealthCare.Models.ChBaseModel.PersonModel;

namespace HealthCare.WebServices.Interfaces
{
    public interface IChBaseWS
    {
        Task<ObservableCollection<HeightModel>> GetHeights();
        Task<ObservableCollection<WeightModel>> GetWeights();
        Task<ObservableCollection<ProcedureModel>> GetProcedure();
        Task<ObservableCollection<MedicationModel>> GetMedication();
        Task<ObservableCollection<ImmunizationModel>> GetImmunization();
        Task<ObservableCollection<BloodGlucoseModel>> GetBloodGlucose();
        Task<ObservableCollection<BloodPressureModel>> GetBloodPressure();
        Task<string> GetAppToken();
        Task<PersonModel> GetUserInfo();
        Task<bool> AddHeight(double value);
        Task<bool> AddWeight(double value);
        Task<bool> AddProcedure(ProcedureModel value);
        Task<bool> AddMedication(MedicationModel value);
        Task<bool> AddImmunization(ImmunizationModel value);
        Task<bool> AddBloodGlucose(BloodGlucoseModel value);
        Task<bool> AddBloodPressure(BloodPressureModel value);
        Task<bool> RemoveData(string id);
        Task<string> UploadFile(byte[] data, string fileName);
        Task ShareHealthInformationViaEmail(ChBaseHealthInfoModel model);
        Task<string> GetPatientInfo(string id);
        Task Register(UserModel newAccount);
    }
}
