using System;
using System.Reflection;

namespace HealthCare.Enums
{
    public class Description : Attribute
    {
        public Description(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static string ToString(Enum value)
        {
            string output = null;
            var type = value.GetType();

            var fi = type.GetRuntimeField(value.ToString());
            var attrs =
                fi.GetCustomAttributes(typeof (Description),
                    false) as Description[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }

            return output;
        }
    }
}