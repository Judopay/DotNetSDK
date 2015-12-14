using System.Collections.Generic;
using System.Linq;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Logging;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;

namespace JudoPayDotNet.Models.CustomDeserializers
{
    internal class JudoApiErrorModelConverter : JsonConverter
    {
        private readonly ILog _log;

        public JudoApiErrorModelConverter(ILog log)
        {
            _log = log;
        }

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
            var jsonObject = JObject.Load(reader);
            var properties = jsonObject.Properties().ToList();
            var oldNames = new []{"errormessage", "errortype", "modelerrors"};
            var judoApiErrorModelPropertiesNames = new[] { "details", "messages", "code", "category" };



            if (judoApiErrorModelPropertiesNames.Any(p => properties.Select(t => t.Name).Contains(p.ToLower())))
            {

                var modelError = new ModelError()
                {
                    ModelErrors = GetProperty<List<FieldError>>(serializer, properties, "details"),
                    Code = GetProperty<int>(serializer, properties, "code"),
                    Category = GetProperty<string>(serializer, properties, "category"),
                    Message = GetProperty<string>(serializer, properties, "message"),

                };
                return modelError;
            }

            if (oldNames.Any(p => properties.Select(t => t.Name).Contains(p.ToLower())))
            {
                var errorType = GetProperty<int>(serializer, properties, "errortype");
                var error  = JudoApiError.General_Error;
                
                if (Enum.IsDefined(typeof(JudoApiError), errorType))
                {
                    error = (JudoApiError)errorType;
                }
                else
                {
                    _log.InfoFormat("The JudoApiError {0} was sent by the server and it is not recognized by the SDK", errorType);
                  
                }

                var modelError = new JudoApiErrorModel()
                {
                    ErrorMessage = GetProperty<string>(serializer, properties, "errormessage"),
                    ErrorType = error,
                    ModelErrors = GetProperty<List<JudoModelError>>(serializer, properties, "modelerrors")


                };
                return modelError;
             
            }
            if (properties.Any(t => t.Name == "message"))
            {
                return new ModelError()
                {
                    Message = GetProperty<string>(serializer, properties, "message"),
                    Code = 0

                };
            }
            return new ModelError()
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