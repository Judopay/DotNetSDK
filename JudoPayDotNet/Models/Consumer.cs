using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    ///     Details of the consumer used in the requested operation (add card/payment)
    /// </summary>
    [DataContract(Name = "Consumer", Namespace = "")]
    public class Consumer
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
        /// Gets or sets your consumer reference.
        /// </summary>
        /// <value>
        /// Your consumer reference.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string YourConsumerReference { get; set; }
    }
}
