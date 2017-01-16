using System;
using HealthCare.Enums;
using Newtonsoft.Json;

namespace HealthCare.Conveters.JsonConverters
{
    /// <summary>
    ///     This Converter is only used for namespace Healthcare.Enums
    /// </summary>
    public class MaritalStatusConverterAttribute : JsonConverter
    {
        #region implemented abstract members of JsonConverter

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var item = (MaritalStatus) value;
            writer.WriteValue(item == MaritalStatus.Married);
            writer.Flush();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            try
            {
                var item = serializer.Deserialize<bool?>(reader);
                if (item == null || !item.Value)
                    return MaritalStatus.Single;
                return MaritalStatus.Married;
            }
            catch
            {
                return MaritalStatus.Single;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.Namespace.Equals("HealthCare.Enums");
        }

        #endregion
    }
}