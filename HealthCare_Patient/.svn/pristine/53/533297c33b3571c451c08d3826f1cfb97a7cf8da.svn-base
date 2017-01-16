using System;
using Newtonsoft.Json;
using Xamarin.Forms.Maps;

namespace HealthCare.Conveters.JsonConverters
{
    public class LocationConverterAttribute : JsonConverter
    {
        #region implemented abstract members of JsonConverter

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var position = (Position) value;
            //double[] data = {position.Latitude, position.Longitude};
            writer.WriteStartArray();
            writer.WriteValue(position.Latitude);
            writer.WriteValue(position.Longitude);
            writer.WriteEndArray();
            writer.Flush();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var item = serializer.Deserialize<double[]>(reader);
            if (item == null)
                return new Position(0, 0);
            return new Position(item[1], item[0]);
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        #endregion
    }
}