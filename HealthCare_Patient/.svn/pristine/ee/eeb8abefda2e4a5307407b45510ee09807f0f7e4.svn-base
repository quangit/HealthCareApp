using System;
using System.Collections;
using System.Linq;
using HealthCare.Enums;
using HealthCare.Models;
using Newtonsoft.Json;

namespace HealthCare.Conveters.JsonConverters
{
    /// <summary>
    ///     Database do not follow any data integrity constraint
    ///     So, every pieces of data can be null
    ///     In case of data is null, we convert null to default value
    ///     that enable mobile application to use it without engage crash
    /// </summary>
    public class NullConverterAttribute : JsonConverter
    {
        private const string EnumNamespace = "HealthCare.Enums";
        private const string ModelNamespace = "HealthCare.Models";
        private const string ObjectNamespace = "HealthCare.Objects";
        private const string ModelApiNamespace = "HealthCare.ModelApis";

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is IList)
            {
                var arrayValue = (IList)value;
                writer.WriteStartArray();
                if (IsPrimitiveType(arrayValue.GetType().GetElementType()))
                {
                    foreach (var item in arrayValue)
                        writer.WriteValue(item);
                }
                else
                {
                    foreach (var item in arrayValue)
                        serializer.Serialize(writer, item);
                }
                writer.WriteEndArray();
            }
            else if (value.GetType().IsArray && value.GetType().GetElementType() == typeof(string))
            {
                var strArr = ToStringArray(value);
                writer.WriteStartArray();
                foreach (var str in strArr)
                    writer.WriteValue(str);
                writer.WriteEndArray();
            }
            else if (IsPrimitiveType(value))
                writer.WriteValue(value);
            else
                serializer.Serialize(writer, value);
            writer.Flush();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            if (objectType == typeof(string))
            {
                var item = serializer.Deserialize<string>(reader);
                if (string.IsNullOrWhiteSpace(item))
                    item = string.Empty;
                return item;
            }
            if (objectType == typeof(string[]))
            {
                var array = serializer.Deserialize<string[]>(reader) ?? new string[0];
                foreach (var item in array)
                    if (string.IsNullOrWhiteSpace(item))
                        item.Replace(item, string.Empty);
                return array;
            }
            if (objectType.Namespace.Equals(EnumNamespace))
                return ReadEnum(reader, serializer, objectType);
            if (objectType.Namespace.Equals(ModelNamespace))
                return ReadModel(reader, serializer, objectType);
            if (objectType == typeof(int))
                return ReadPrimitive<int, int?>(reader, serializer);
            if (objectType == typeof(double))
                return ReadPrimitive<double, double?>(reader, serializer);
            if (objectType == typeof(float))
                return ReadPrimitive<float, float?>(reader, serializer);
            if (objectType == typeof(long))
                return ReadPrimitive<long, long?>(reader, serializer);
            if (objectType == typeof(bool))
                return ReadPrimitive<bool, bool?>(reader, serializer);
           
            var obj = serializer.Deserialize(reader, objectType);
            if (obj == null)
                return Activator.CreateInstance(objectType);
            return obj;
        }

        public override bool CanConvert(Type objectType)
        {
            return
                (IsPrimitiveType(objectType) ||
                 objectType.Namespace.Equals(EnumNamespace) ||
                 objectType == typeof(IList) ||
                 objectType.Namespace.Equals(ModelNamespace) ||
                 objectType.Namespace.Equals(ModelApiNamespace) ||
                 objectType.Namespace.Equals(ObjectNamespace))
                &&
                !(objectType.IsArray && objectType.GetElementType() != typeof(string));
        }

        private object ReadPrimitive<TPrimitive, TNullable>(JsonReader reader, JsonSerializer serializer)
            where TPrimitive : struct
        {
            var item = serializer.Deserialize<TNullable>(reader);
            if (item == null)
                return default(TPrimitive);
            return item;
        }

        private object ReadEnum(JsonReader reader, JsonSerializer serializer, Type type)
        {
            object item = null;
            if (reader.Value == null)
            {
                item = Activator.CreateInstance(type);
                return item;
            }
               
            if (type.Name.Equals("CheckupStatus"))
                item = serializer.Deserialize<CheckupStatus>(reader);
            else if (type.Name.Equals("FeeType"))
                item = serializer.Deserialize<FeeType>(reader);
            else if (type.Name.Equals("Gender"))
                item = serializer.Deserialize<Gender>(reader);
            else if (type.Name.Equals("KeywordType"))
                item = serializer.Deserialize<KeywordType>(reader);
            else if (type.Name.Equals("MaritalStatus"))
                item = serializer.Deserialize<MaritalStatus>(reader);
            else if (type.Name.Equals("PatientType"))
                item = serializer.Deserialize<PatientType>(reader);
            else if (type.Name.Equals("PersonStatus"))
                item = serializer.Deserialize<PersonStatus>(reader);
            else if (type.Name.Equals("Role"))
                item = serializer.Deserialize<Role>(reader);
            else if (type.Name.Equals("ActiveType"))
                item = serializer.Deserialize<ActiveType>(reader);

            if (item == null)
                item = Activator.CreateInstance(type);
            return item;
        }

        private object ReadModel(JsonReader reader, JsonSerializer serializer, Type type)
        {
            object item = null;
            if (type.Name.Equals("CheckupModel"))
                item = serializer.Deserialize<CheckupModel>(reader);
            else if (type.Name.Equals("CheckupTypeModel"))
                item = serializer.Deserialize<CheckupTypeModel>(reader);
            else if (type.Name.Equals("CityModel"))
                item = serializer.Deserialize<CityModel>(reader);
            else if (type.Name.Equals("CreditCardModel"))
                item = serializer.Deserialize<CreditCardModel>(reader);
            else if (type.Name.Equals("DistrictModel"))
                item = serializer.Deserialize<DistrictModel>(reader);
            else if (type.Name.Equals("DoctorModel"))
                item = serializer.Deserialize<DoctorModel>(reader);
            else if (type.Name.Equals("HospitalModel"))
                item = serializer.Deserialize<HospitalModel>(reader);
            else if (type.Name.Equals("PersonModel"))
                item = serializer.Deserialize<PersonModel>(reader);
            else if (type.Name.Equals("PromotionModel"))
                item = serializer.Deserialize<PromotionModel>(reader);
            else if (type.Name.Equals("ScheduleModel"))
                item = serializer.Deserialize<ScheduleModel>(reader);
            else if (type.Name.Equals("Suggestion"))
                item = serializer.Deserialize<Suggestion>(reader);
            else if (type.Name.Equals("UserModel"))
                item = serializer.Deserialize<UserModel>(reader);
            else if (type.Name.Equals("Room"))
                item = serializer.Deserialize<Room>(reader);

            if (item == null)
                item = Activator.CreateInstance(type);
            return item;
        }

        private static string[] ToStringArray(object arg)
        {
            var collection = arg as IEnumerable;
            if (collection != null)
            {
                return collection
                    .Cast<object>()
                    .Select(x => x.ToString())
                    .ToArray();
            }

            if (arg == null)
            {
                return new string[] { };
            }

            return new[] { arg.ToString() };
        }

        private bool IsPrimitiveType(object value)
        {
            return value is int ||
                   value is double ||
                   value is float ||
                   value is long ||
                   value is string ||
                   value is bool;
        }

        private bool IsPrimitiveType(Type type)
        {
            return type == typeof(int) ||
                   type == typeof(double) ||
                   type == typeof(float) ||
                   type == typeof(long) ||
                   type == typeof(string) ||
                   type == typeof(bool);
        }
    }
}