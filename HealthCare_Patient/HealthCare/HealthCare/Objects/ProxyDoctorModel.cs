using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using HealthCare.Models;
using Newtonsoft.Json;

namespace HealthCare.Objects
{
    public class ProxyDoctorModel : DoctorModel
    {
        public ProxyDoctorModel()
        {
        }

        public ProxyDoctorModel(DoctorModel baseObject)
        {
            var fieldsOfBaseClass = baseObject.GetType().GetRuntimeProperties();
            var fieldsOfDerivedClass = GetType().GetRuntimeProperties();
            foreach (var fieldOfBaseClass in fieldsOfBaseClass)
            {
                foreach (var fieldOfDerivedClass in fieldsOfDerivedClass)
                {
                    if (fieldOfDerivedClass.Name.Equals(fieldOfBaseClass.Name) && fieldOfDerivedClass.CanWrite)
                        fieldOfDerivedClass.SetValue(this, fieldOfBaseClass.GetValue(baseObject));
                }
            }
        }

        [JsonProperty(PropertyName = "checkupType")]
        public CheckupTypeModel CheckupType { get; set; }

        [JsonProperty(PropertyName = "hospitals")]
        public ObservableCollection<ProxyHospitalModel> Hospitals { get; set; }

        public CheckupTypeModel CurrenCheckupType { get; set; }

        public string AsHospitalString
        {
            get { return Hospitals.Aggregate("", (current, hospital) =>
            current + ("♦ " + hospital.AsString.Trim() + "\n"));
            }
        }
    }
}