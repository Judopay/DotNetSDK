using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Models
{
    [DataContract]
    public class PKPaymentModel : PaymentModel
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
