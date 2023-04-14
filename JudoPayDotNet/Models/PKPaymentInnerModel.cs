using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
   public class PKPaymentInnerModel
    {
        /// <summary>
        /// Apple Pay token from the wallet payload.
        /// </summary>
        [DataMember(IsRequired = true)]
        public ApplePayTokenModel Token { get; set; }

        /// <summary>
        /// The billing address.
        /// </summary>
        [DataMember(EmitDefaultValue = true)]
        public ApplePayCardAddressModel BillingContact { get; set; }
    }
}
