using System;
using System.Runtime.Serialization;
using JudoPayDotNet.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Data to register a card (pre-auth with a pre-configured amount)
    /// </summary>
    [DataContract]
    // ReSharper disable UnusedMember.Global
    public class RegisterCardModel : SaveCardModel
    {
        public RegisterCardModel()
        {
            YourPaymentReference = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Gets or sets the CV2. 
        /// </summary>
        /// <value>
        /// The CV2.
        /// </value>
        public string CV2
        {
            get => Cv2;
            set => Cv2 = value;
        }

        // This is marked as the DataMember to ensure the expected case is sent to the server API in Json - cv2
        [DataMember(IsRequired = false)]
        private string Cv2 { get; set; }

        /// <summary>
        /// Gets your payment reference.
        /// </summary>
        /// <value>
        /// Your payment reference.
        /// PLEASE NOTE!!!! there is a reflection call within JudoPayClient.cs that gets this property via a string call. update in both places
        /// including  other model instances of yourPaymentReference ********************
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string YourPaymentReference { get; set; }

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
        /// Is this transaction the first transaction of a series (has continuous authority
        /// been granted to the merchant by the card holder).
        /// </summary>
        /// <remarks>Mastercard requires that when dealing with continuous authority
        /// payments this flag identifies the transaction where the card holder gave permission for
        /// repeat charges.</remarks>
        [DataMember(EmitDefaultValue = false)]
        public bool? InitialRecurringPayment { get; set; }

        /// <summary>
        /// Indicates that the transaction has been given recurring authorization from the consumer
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public bool? RecurringPayment { get; set; }

        /// <summary>
        /// Enum for Regular Payments (recurring)
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        [JsonConverter(typeof(StringEnumConverter))]
        public RecurringPaymentType? RecurringPaymentType { get; set; }

        /// <summary>
        /// Receipt ID of original authenticated transaction (for recurring payments)
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string RelatedReceiptId { get; set; }

        /// <summary>
        /// Information needed for ThreeDSecure2 payments
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public ThreeDSecureTwoModel ThreeDSecure { get; set; }

        /// <summary>
        /// This is a set of fraud signals sent by the mobile SDKs
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        // ReSharper disable once UnusedMember.Global
        public JObject ClientDetails { get; set; }

        /// <summary>
        /// Details needed for MCC 6012 transactions
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        // ReSharper disable once UnusedMember.Global
        public PrimaryAccountDetailsModel PrimaryAccountDetails { get; set; }

        /// <summary>
        /// Details needed for passing in 3DS2 authentication details performed outside Judopay
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public ThreeDSecureMpiModel ThreeDSecureMpi { get; set; }
    }
    // ReSharper restore UnusedMember.Global
}
