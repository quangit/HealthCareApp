using System.Collections.ObjectModel;
using System.Linq;
using HealthCare.Conveters.JsonConverters;
using HealthCare.Models;
using HealthCare.Objects;
using Newtonsoft.Json;

namespace HealthCare.ModelApis
{
    public class DoctorApiModel : ProxyDoctorModel, IApiModel<ProxyDoctorModel>
    {
        [JsonProperty(PropertyName = "doctorInfos")]
        [JsonConverter(typeof (NullConverterAttribute))]
        public ObservableCollection<DoctorInfoApiModel> DoctorInfos { get; set; }

        public ProxyDoctorModel ToBaseModel()
        {
            Hospitals = new ObservableCollection<ProxyHospitalModel>(
                DoctorInfos.Select(x => new ProxyHospitalModel(x.Hospital)
                {
                    CheckupType = x.CheckupType
                }));
            return this;
        }
    }

    public class DoctorInfoApiModel
    {
        [JsonProperty(PropertyName = "hospital")]
        public HospitalModel Hospital { get; set; }

        [JsonProperty(PropertyName = "checkupType")]
        public CheckupTypeModel CheckupType { get; set; }
    }
}