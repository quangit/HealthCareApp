using System.Reflection;
using HealthCare.Models;
using Newtonsoft.Json;

namespace HealthCare.Objects
{
    public class ProxyHospitalModel : HospitalModel
    {
        public ProxyHospitalModel()
        {
        }

        public ProxyHospitalModel(HospitalModel baseObject)
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
    }
}