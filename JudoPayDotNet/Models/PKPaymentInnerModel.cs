using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
   public class PKPaymentInnerModel
    {
        /// <summary>
        /// Gets or sets the apple pay token.
        /// </summary>
        /// <value>
        /// The apple pay token.
        /// </value>
        [DataMember(IsRequired = true)]
        public PKPaymentTokenModel Token { get; set; }


        /// <summary>
        /// Gets or sets the Issue Number.
        /// </summary>
        /// <value>
        /// The billing adress.
        /// </value>
        [DataMember(EmitDefaultValue = true)]
        public CardAddressModel BillingAddress { get; set; }

        /// <summary>
        /// Gets or sets the card address.
        /// </summary>
        /// <value>
        /// The shipping address.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public CardAddressModel ShippingAddress { get; set; }
    }
}
