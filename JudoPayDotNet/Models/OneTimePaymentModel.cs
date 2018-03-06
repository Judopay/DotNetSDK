using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// A payment made by a one use token
    /// </summary>
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    [DataContract]
    public class OneTimePaymentModel : PaymentModel
    {
        /// <summary>
        /// Gets or sets the one use token.
        /// </summary>
        /// <value>
        /// The card number.
        /// </value>
        [DataMember(IsRequired = true)]
        public string OneUseToken { get; set; }

        /// <summary>
        /// Gets or sets the card address.
        /// </summary>
        /// <value>
        /// The card address.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public CardAddressModel CardAddress { get; set; }
    }
    // ReSharper restore UnusedAutoPropertyAccessor.Global
}
