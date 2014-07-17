using System.Collections.Generic;
using System.Linq;
using JudoPayDotNet.Enums;
using JudoPayDotNet.Errors;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;

namespace JudoPayDotNet.Models.CustomDeserializers
{
    public class JudoApiErrorModelConverter : JsonConverter
    {
        private T GetProperty<T>(JsonSerializer serializer, IEnumerable<JProperty> properties, string propertyName)
        {
            var property = properties.FirstOrDefault(p => p.Name.ToLowerInvariant() == propertyName);

            if (property != null)
            {
                return serializer.Deserialize<T>(property.Value.CreateReader());
            }

            return default(T);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            var properties = jsonObject.Properties().ToList();

            var errorType = GetProperty<int>(serializer, properties, "errortype");

            return new JudoApiErrorModel
            {
                ErrorMessage = GetProperty<string>(serializer, properties, "errormessage"),
                ErrorType = (JudoApiError)errorType,
                ModelErrors = GetProperty<List<JudoModelError>>(serializer, properties, "modelerrors")
            };

        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
        
        public override bool CanConvert(Type objectType)
        {
            return typeof(JudoApiErrorModel) == objectType;
        }

        
    }
}