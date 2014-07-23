using Newtonsoft.Json.Linq;

namespace JudoPayDotNet.Models.CustomDeserializers
{
    public class TransactionResultConvertor : JsonCreationConverter<ITransactionResult>
    {
        protected override ITransactionResult Create(JObject jObject)
        {
            if (jObject.Value<string>("md") == null)
            {
                return new PaymentReceiptModel();
            }
            return new PaymentRequiresThreeDSecureModel();
        }
    }
}
