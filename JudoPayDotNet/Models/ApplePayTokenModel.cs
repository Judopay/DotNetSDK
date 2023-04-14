using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;

namespace JudoPayDotNet.Models
{
    [DataContract]
    public class ApplePayTokenModel
    {
        /// <summary>
        /// Information about the associated card
        /// </summary>
        [DataMember(IsRequired = true)]
        public ApplePayPaymentMethodModel PaymentMethod { get; set; }

        /// <summary>
        /// Encrypted payment data from payload (expected keys Version, Data, Signature, Header)
        /// </summary>
        [DataMember(IsRequired = true)]
        public JObject PaymentData { get; set; }
    }
}
