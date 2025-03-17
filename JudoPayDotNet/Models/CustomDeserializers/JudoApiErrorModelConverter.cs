using System.Collections.Generic;
using System.Linq;
using JudoPayDotNet.Errors;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;

namespace JudoPayDotNet.Models.CustomDeserializers
{
    internal class JudoApiErrorModelConverter : JsonConverter
    {
        private static T GetProperty<T>(JsonSerializer serializer, IEnumerable<JProperty> properties, string propertyName)
        {
            var property = properties.FirstOrDefault(p => p.Name.ToLowerInvariant() == propertyName);

            return property != null ? serializer.Deserialize<T>(property.Value.CreateReader()) : default;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var properties = jsonObject.Properties().ToList();
            var judoApiErrorModelPropertiesNames = new[] { "details", "messages", "code", "category" };

            if (judoApiErrorModelPropertiesNames.Any(p => properties.Select(t => t.Name.ToLower()).Contains(p.ToLower())))
            {
                var modelError = new ModelError
                {
                    ModelErrors = GetProperty<List<FieldError>>(serializer, properties, "details"),
                    Code = GetProperty<int>(serializer, properties, "code"),
                    Category = GetProperty<string>(serializer, properties, "category"),
                    Message = GetProperty<string>(serializer, properties, "message"),
                    RequestId = GetProperty<string>(serializer, properties, "requestid")

                };
                return modelError;
            }

            if (properties.Any(t => t.Name == "message"))
            {
                return new ModelError
                {
                    Message = GetProperty<string>(serializer, properties, "message"),
                    RequestId = GetProperty<string>(serializer, properties, "requestid"),
                    Code = 0
                };
            }
            return new ModelError
            {
                Message = "Unknown Return type",
                Code = 0
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(ModelError) == objectType;
        }
    }
}