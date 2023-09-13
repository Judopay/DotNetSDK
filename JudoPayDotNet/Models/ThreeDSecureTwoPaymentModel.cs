using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Information about a ThreeDSecure 2.x payment
    /// </summary>
    [DataContract]
    public abstract class ThreeDSecureTwoPaymentModel : PaymentModel
    {
        // Not explicitly associated with 3DS2, but moved here from PaymentModel to avoid the wallet payment types
        // inheriting it.   This public accessor keeps the existing case but the DataMember is now the private Cv2
        /// <summary>
        /// The card CV2/CVV (3-4 digit validation code).
        /// </summary>
        // ReSharper disable InconsistentNaming
        public string CV2
        {
            get => Cv2;
            set => Cv2 = value;
        }
        // ReSharper restore InconsistentNaming

        // This is marked as the DataMember to ensure the expected case is sent to the server API in Json - cv2
        [DataMember(IsRequired = false)]
        private string Cv2 { get; set; }

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

        /// <summary>
        /// Gets or sets the card address.
        /// </summary>
        /// <value>
        /// The card address.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public CardAddressModel CardAddress { get; set; }
    }
}
