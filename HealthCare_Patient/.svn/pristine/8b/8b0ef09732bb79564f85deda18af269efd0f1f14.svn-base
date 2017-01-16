using System;
using HealthCare.Enums;
using Newtonsoft.Json;

namespace HealthCare.Conveters.JsonConverters
{
    public class GenderConverterAttribute : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var item = (Gender)value;
            if (item == Gender.Male)
                writer.WriteValue("M");
            else if (item == Gender.Female)
                writer.WriteValue("F");
            else if (item == Gender.None)
                writer.WriteValue("");

            writer.Flush();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var item = serializer.Deserialize<string>(reader);

            if (!string.IsNullOrWhiteSpace(item))
            {
                if (item.Equals("M"))
                    return Gender.Male;
                if (item.Equals("F"))
                    return Gender.Female;
            }
            else return Gender.None;

            return Gender.None;
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}