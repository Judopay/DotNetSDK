using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    [DataContract]
    public class ApplePayPaymentMethodModel
    {
        /// <summary>
        /// Card description suitable for display, provided by the ApplePay session. e.g. Visa 1234
        /// </summary>
        [DataMember(IsRequired = false)]
        public string DisplayName { get; set; }

        /// <summary>
        /// Name of the payment network backing the card, provided by the ApplePay session. e.g. Visa, MasterCard, AmEx
        /// </summary>
        [DataMember(IsRequired = false)]
        public string Network { get; set; }

        /// <summary>
        /// Card's type of payment, provided by the ApplePay session. e.g. debit, credit
        /// </summary>
        [DataMember(IsRequired = false)]
        public string Type { get; set; }
    }
}