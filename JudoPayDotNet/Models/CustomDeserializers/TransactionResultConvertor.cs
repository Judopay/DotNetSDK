using Newtonsoft.Json.Linq;

namespace JudoPayDotNet.Models.CustomDeserializers
{
    /// <summary>
    /// Creates either the <see cref="PaymentReceiptModel"/>, <see cref="PaymentRequiresThreeDSecureModel"/> or <see cref="PaymentRequiresThreeDSecureTwoModel"/> as needed
    /// </summary>
    /// <remarks>If you have 3D secure enabled, the API may return a 3D secure result. So we need to create an instance of the correct type.</remarks>
    internal class TransactionResultConvertor : JsonCreationConverter<ITransactionResult>
    {
        protected override ITransactionResult Create(JObject jObject)
        {
            // Device Details or Challenge Required 
            if (jObject.Value<string>("methodUrl") != null || jObject.Value<string>("challengeUrl") != null)
            {
                return new PaymentRequiresThreeDSecureTwoModel();
            }

            return new PaymentReceiptModel();
        }
    }
}