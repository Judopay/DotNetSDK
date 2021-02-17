using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// A token payment request
    /// </summary>
    /// <remarks>Used to process additional transactions with a saved card. Requires both the Card and Consumer Tokens</remarks>
    [DataContract]
    public class TokenPaymentModel : ThreeDSecureTwoPaymentModel
    {
        /// <summary>
        /// Gets or sets the consumer token.
        /// </summary>
        /// <value>
        /// The consumer token.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string ConsumerToken { get; set; }

        /// <summary>
        /// The card token for a previously registered card.
        /// </summary>
        /// <value>
        /// The card token.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string CardToken { get; set; }
    }
}