using System;
using System.Reflection;
using JudoPayDotNet.Enums;
using Newtonsoft.Json;

namespace JudoPayDotNet.Models.CustomDeserializers
{
	public class TransactionTypeConvertor : JsonConverter
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
			bool isNullable = (objectType.GetTypeInfo().IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Nullable<>));
			Type t = isNullable ? Nullable.GetUnderlyingType(objectType) : objectType;

			if (reader.TokenType == JsonToken.Null)
			{
				if (isNullable)
				{ 
					return null;
				}
			}

			if (reader.TokenType == JsonToken.Integer)
			{
				return Enum.Parse(t, reader.Value.ToString());
			}

			if (reader.TokenType == JsonToken.String)
			{
				string enumText = reader.Value.ToString();
				if (enumText == string.Empty && isNullable)
					return null;

				return EnumUtils.GetValueFromDescription<TransactionType>(enumText);
			}

			return TransactionType.UNKNOWN;
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof (string) == objectType;
		}
	}
}