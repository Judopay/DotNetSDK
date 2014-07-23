using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JudoPayDotNet.Models.CustomDeserializers
{
    public abstract class JsonCreationConverter<T> : JsonConverter
    {
        /// <summary>
        /// this is very important, otherwise serialization breaks!
        /// </summary>
        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        /// <summary> 
        /// Create an instance of objectType, based properties in the JSON object 
        /// </summary>
        /// <param name="jObject">contents of JSON object that will be 
        /// deserialized</param> 
        /// <returns></returns> 
        protected abstract T Create(JObject jObject);

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }

        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            // Load JObject from stream 
            var jObject = JObject.Load(reader);

            // Create target object based on JObject 
            var target = Create(jObject);

            // Populate the object properties 
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }

        // Because this JsonConverter is configured to not serialize objects, returning false on CanWrite
        public override void WriteJson(JsonWriter writer, object value,
            JsonSerializer serializer)
        {
        }
    }
}