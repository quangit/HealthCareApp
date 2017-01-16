using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Helpers;
using Newtonsoft.Json;

namespace HealthCare.Conveters.JsonConverters
{
    public class BirthDaySetConverterAttribute : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var item = (bool)value;

            writer.WriteValue(item);

            writer.Flush();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                                        JsonSerializer serializer)
        {
            var item = serializer.Deserialize<long?>(reader);

            //return item != null ? Common.UnixTimeStampToDateTime(item) : null;
            //return Common.UnixTimeStampToDateTime(item ?? Common.ConvertToTimestamp(DateTime.Now.AddDays(-1)));
            return item != null;
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}
