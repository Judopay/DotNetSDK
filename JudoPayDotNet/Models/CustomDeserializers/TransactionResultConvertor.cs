using System;
using Newtonsoft.Json.Linq;

namespace JudoPayDotNet.Models.CustomDeserializers
{
    public class TransactionResultConvertor : JsonCreationConverter<ITransactionResult>
    {
        protected override ITransactionResult Create(Type objectType, JObject jObject)
        {
            if (jObject.Value<string>("md") == null)
            {
                return new PaymentReceiptModel();
            }
            return new PaymentRequiresThreeDSecureModel();
        }
    }
}
