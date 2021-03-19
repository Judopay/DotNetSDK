using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using JudoPayDotNet.Enums;
using Newtonsoft.Json.Linq;

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
        /// Gets or sets your consumer reference.
        /// </summary>
        /// <value>
        /// Your consumer reference.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string YourConsumerReference { get; set; }

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
        /// Gets or sets your payment meta data.
        /// </summary>
        /// <value>
        /// Your payment meta data.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public IDictionary<string, string> YourPaymentMetaData { get; set; }

        /// <summary>
        /// Gets or sets the judo identifier.
        /// </summary>
        /// <value>
        /// The judo identifier.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string JudoId { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <remarks>Valid values GBP, EUR or USD.</remarks>
        /// <value>
        /// The currency.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
// ReSharper disable MemberCanBePrivate.Global
        public string Currency { get; set; }
// ReSharper restore MemberCanBePrivate.Global

        /// <summary>
        /// Gets or sets the partner service fee.
        /// </summary>
        /// <value>
        /// The partner service fee.
        /// </value>
        [DataMember]
// ReSharper disable UnusedAutoPropertyAccessor.Global
        public decimal PartnerServiceFee { get; set; }
// ReSharper restore UnusedAutoPropertyAccessor.Global

        /// <summary>
        /// Gets or sets the consumer location.
        /// </summary>
        /// <value>
        /// The consumer location.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
// ReSharper disable UnusedAutoPropertyAccessor.Global
        public ConsumerLocationModel ConsumerLocation { get; set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Global

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
        /// The end consumers browser useragent for 3D secure
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
// ReSharper disable UnusedAutoPropertyAccessor.Global
        public string UserAgent { get; set; }
// ReSharper restore UnusedAutoPropertyAccessor.Global

        /// <summary>
        /// The end consumers browser DeviceCategory for 3D secure
        /// </summary>
        /// <remarks>This should either be Desktop or Mobile</remarks>
        [DataMember(EmitDefaultValue = false)]
        public string DeviceCategory { get; set; }

        /// <summary>
		/// The end consumers browser accept headers, used for 3D secure
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
// ReSharper disable UnusedAutoPropertyAccessor.Global
        public string AcceptHeaders { get; set; }
// ReSharper restore UnusedAutoPropertyAccessor.Global

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
