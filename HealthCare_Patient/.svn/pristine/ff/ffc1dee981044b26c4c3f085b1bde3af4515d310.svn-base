using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HealthCare.Helpers
{
    public class IgnorePropertyResolver : DefaultContractResolver
    {
        private readonly string[] _ignoreList;

        public IgnorePropertyResolver(params string[] ignorePropertyList)
        {
            foreach (var str in ignorePropertyList)
                str.Replace(str, str.ToLower());
            _ignoreList = ignorePropertyList;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var properties = base.CreateProperties(type, memberSerialization);
            return properties.Where(p => !_ignoreList.Contains(p.PropertyName.ToLower())).ToList();
        }
    }
}