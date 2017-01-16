using System;
using HealthCare.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HealthCare.Helpers
{
    public static class JsonUtils
    {
        public static T ParseData<T>(JObject jObject, string key = null)
        {
            var result = key == null ? jObject : jObject[key];
            if (result != null)
                return result.ToObject<T>();
            return default(T);
        }

        public static bool ValidJsonString(string jsonString)
        {
            try
            {
                JToken.Parse(jsonString);
                return true;
            }
            catch (JsonReaderException)
            {
                return false;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}