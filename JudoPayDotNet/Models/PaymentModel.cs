using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using JudoPayDotNet.Enums;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Generic information about a payment
    /// </summary>
// ReSharper disable UnusedMember.Global
    [DataContract]
    public abstract class PaymentModel : IModelWithHttpHeaders
    {
        protected PaymentModel()
        {
            Currency = "GBP";
            YourPaymentReference = Guid.NewGuid().ToString();
            HttpHeaders = new Dictionary<string, string>();
        }

        /// <summary>
        /// The merchant reference to anonymously and uniquely identify a consumer.   GUIDs are recommended.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string YourConsumerReference { get; set; }

        /// <summary>
        /// The merchant reference to anonymously and uniquely identify a consumer.  GUIDs are recommended.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string YourPaymentReference { get; set; }

        /// <summary>
        /// The merchant payment meta data.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public IDictionary<string, object> YourPaymentMetaData { get; set; }

        /// <summary>
        /// The judo account identifier.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string JudoId { get; set; }

        /// <summary>
        /// The transaction amount.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public decimal Amount { get; set; }

        /// <summary>
        /// The ISO currency code of the transaction (e.g. GBP, EUR, USD)
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Currency { get; set; }

        /// <summary>
        /// Encrypted mobile device information sent from the mobile SDKs
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public ClientDetailsModel ClientDetails { get; set; }

        /// <summary>
        /// Details needed for MCC 6012 transactions
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public PrimaryAccountDetailsModel PrimaryAccountDetails { get; set; }

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
        /// Allows a custom Dynamic Descriptor instead of the auto-generated one
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string DynamicDescriptor { get; set; }

        /// <summary>
        /// Enum for Regular Payments (recurring)
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public RecurringPaymentType? RecurringPaymentType { get; set; }

        /// <summary>
        /// Receipt ID of original authenticated transaction (for recurring payments)
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string RelatedReceiptId { get; set; }

        /// <summary>
        /// Reference of associated web payment session
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string WebPaymentReference { get; set; }

        /// <summary>
        /// Allows you to set HTTP headers on requests
        /// </summary>
        [IgnoreDataMember]
        public Dictionary<string, string> HttpHeaders { get; private set; }
    }
// ReSharper restore UnusedMember.Global
}
