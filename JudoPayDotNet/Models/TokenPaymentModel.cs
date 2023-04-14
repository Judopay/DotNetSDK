using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// A token payment request
    /// </summary>
    /// <remarks>Used to process additional transactions with a saved card. Requires both the CardToken and YourConsumerReference</remarks>
    [DataContract]
    public class TokenPaymentModel : ThreeDSecureTwoPaymentModel
    {
        /// <summary>
        /// The card token for a previously registered card, as returned in the Judopay receipt.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string CardToken { get; set; }
    }
}