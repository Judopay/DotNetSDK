using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Information about a ThreeDSecureTwo Two payment 
    /// </summary>
    [DataContract]
    public abstract class ThreeDSecureTwoPaymentModel : PaymentModel
    {
        /// <summary>
        /// Gets or sets the full card holder name.
        /// </summary>
        /// <value>
        /// The card holder name.
        /// </value>
        [DataMember(IsRequired = false)]
        public string CardHolderName { get; set; }

        /// <summary>
        /// Gets or sets the mobile number.
        /// </summary>
        /// <value>
        /// The mobile number.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the phone country code.
        /// </summary>
        /// <value>
        /// The phone country code.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string PhoneCountryCode { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string EmailAddress { get; set; }
        
        /// <summary>
        /// Information needed for ThreeDSecure2 payments
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public ThreeDSecureTwoModel ThreeDSecure { get; set; }
    }
}
