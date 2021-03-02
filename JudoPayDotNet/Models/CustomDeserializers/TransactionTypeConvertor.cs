using System;
using System.Reflection;
using JudoPayDotNet.Enums;
using Newtonsoft.Json;

namespace JudoPayDotNet.Models.CustomDeserializers
{
	internal class TransactionTypeConvertor : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var e = (TransactionType)value;

			writer.WriteValue(EnumUtils.GetEnumDescription(e));
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var isNullable = (objectType.GetTypeInfo().IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Nullable<>));
			var t = isNullable ? Nullable.GetUnderlyingType(objectType) : objectType;

			if (reader.TokenType == JsonToken.Null && isNullable)
			{ 
                return null;
            }

			if (reader.TokenType == JsonToken.Integer)
			{
				return Enum.Parse(t, reader.Value.ToString());
			}

            if (reader.TokenType != JsonToken.String)
                throw new JsonReaderException("The object does not represent a valid TransactionType");

            var enumText = reader.Value.ToString();
            if (enumText == string.Empty && isNullable)
                return null;

            return EnumUtils.GetValueFromDescription<TransactionType>(enumText);

        }

		public override bool CanConvert(Type objectType)
		{
			return typeof (string) == objectType;
		}
	}
}