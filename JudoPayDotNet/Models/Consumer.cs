using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Details of your consumer as used in the requested operation (add card/payment)
    /// </summary>
    // ReSharper disable UnusedAutoPropertyAccessor.Global
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
    // ReSharper restore UnusedAutoPropertyAccessor.Global
}
