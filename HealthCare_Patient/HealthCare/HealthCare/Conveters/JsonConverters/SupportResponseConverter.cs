using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Enums;
using HealthCare.Models;
using Newtonsoft.Json;

namespace HealthCare.Conveters.JsonConverters
{
   public class SupportResponseConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var item = (Promotion)value;

            writer.WriteStartArray();
            writer.WriteValue(item);
            writer.WriteEndArray();

            writer.Flush();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                var item = serializer.Deserialize<SupportResponsesModel>(reader);
                return item;
            }
            catch
            {
                return MaritalStatus.Single;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}
