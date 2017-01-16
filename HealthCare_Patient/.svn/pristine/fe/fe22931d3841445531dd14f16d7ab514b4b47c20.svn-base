using System;
using HealthCare.Helpers;
using Newtonsoft.Json;

namespace HealthCare.Conveters.JsonConverters
{
    public class DateTimeConverterAttribute : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var item = (DateTime)value;

            writer.WriteValue(Common.ConvertToTimestamp(item));

            writer.Flush();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                                        JsonSerializer serializer)
        {
            var item = serializer.Deserialize<long?>(reader);

            //return item != null ? Common.UnixTimeStampToDateTime(item) : null;
            return Common.UnixTimeStampToDateTime(item ?? Common.ConvertToTimestamp(DateTime.MinValue));
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}