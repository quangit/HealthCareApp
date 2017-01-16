using System;
using System.Collections.Generic;
using HealthCare.Models;
using Newtonsoft.Json;

namespace HealthCare.Conveters.JsonConverters
{
    public class PromotionHospitalConverterAttribute : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var item = (HospitalModel) value;

            writer.WriteStartArray();
            writer.WriteValue(item);
            writer.WriteEndArray();

            writer.Flush();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var item = serializer.Deserialize<List<HospitalModel>>(reader);

            return item?[0];
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}