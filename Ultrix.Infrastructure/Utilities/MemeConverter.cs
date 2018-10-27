using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ultrix.Infrastructure.Utilities
{
    internal class MemeConverter : JsonConverter
    {
        private readonly Dictionary<string, string> _propertyMappings = new Dictionary<string, string>
        {
            {"id", "Id"},
            {"title", "Title"},
            {"imageUrl", "ImageUrl"},
            {"videoUrl", "VideoUrl"},
            {"pageUrl", "PageUrl"}
        };

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.GetTypeInfo().IsClass;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            object instance = Activator.CreateInstance(objectType);
            var props = objectType.GetTypeInfo().DeclaredProperties.ToList();

            JObject jo = JObject.Load(reader);
            foreach (JProperty jp in jo.Properties())
            {
                if (!_propertyMappings.TryGetValue(jp.Name, out var name))
                    name = jp.Name;

                PropertyInfo prop = props.FirstOrDefault(pi =>
                    pi.CanWrite && pi.Name == name);

                prop?.SetValue(instance, jp.Value.ToString());
            }

            return instance;
        }
    }
}
