using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Information about a ThreeDSecure 2.x payment
    /// </summary>
    [DataContract]
    public abstract class ThreeDSecureTwoPaymentModel : PaymentModel
    {
        // Not explicitly associated with 3DS2, but moved here from PaymentModel to avoid the wallet payment types inheriting it
        /// <summary>
        /// The card CV2/CVV (3-4 digit validation code).
        /// </summary>
        [DataMember(IsRequired = true)]
        // ReSharper disable InconsistentNaming
        public string CV2 { get; set; }
        // ReSharper restore InconsistentNaming

        /// <summary>
        /// The full card holder name.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string CardHolderName { get; set; }

        /// <summary>
        /// The card holders mobile number.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string MobileNumber { get; set; }

        /// <summary>
        /// Phone country code of mobile number.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string PhoneCountryCode { get; set; }

        /// <summary>
        /// Card holders email address.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string EmailAddress { get; set; }
        
        /// <summary>
        /// Information needed for ThreeDSecure2 payments
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public ThreeDSecureTwoModel ThreeDSecure { get; set; }

        /// <summary>
        /// Details needed for passing in 3DS2 authentication details performed outside Judopay
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public ThreeDSecureMpiModel ThreeDSecureMpi { get; set; }

        /// <summary>
        /// The end consumers browser useragent, used to define challenge screens
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string UserAgent { get; set; }

        /// <summary>
        /// The end consumers browser accept headers, used to define challenge screens
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string AcceptHeaders { get; set; }
    }
}
