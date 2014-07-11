using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// A token payment request
    /// </summary>
    [DataContract]
    public class TokenPaymentModel : PaymentModel
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