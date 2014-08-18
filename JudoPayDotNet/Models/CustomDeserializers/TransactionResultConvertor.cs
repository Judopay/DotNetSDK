using Newtonsoft.Json.Linq;

namespace JudoPayDotNet.Models.CustomDeserializers
{
	/// <summary>
	/// Creates either the <see cref="PaymentReceiptModel"/> or <see cref="PaymentRequiresThreeDSecureModel"/> as needed
	/// </summary>
	/// <remarks>If you have 3D secure enabled, the API may return a 3D secure result. So we need to create an instance of the correct type.</remarks>
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
