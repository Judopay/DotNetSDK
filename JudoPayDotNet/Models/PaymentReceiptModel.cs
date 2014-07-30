using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// A payment receipt
    /// </summary>
    // ReSharper disable UnusedMember.Global
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    [DataContract(Name = "Receipt", Namespace = "")]
    public class PaymentReceiptModel : ITransactionResult
    {
        /// <summary>
        /// Gets or sets the receipt identifier.
        /// </summary>
        /// <value>
        /// The receipt identifier.
        /// </value>
        [DataMember]
        public string ReceiptId { get; set; }

        /// <summary>
        /// The receipt id of the original payment, if this is a refund or collection
        /// </summary>
        /// <value>
        /// The original receipt identifier.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
// ReSharper disable once UnusedMember.Global
        public long? OriginalReceiptId { get; set; }

        /// <summary>
        /// Payment, Refund, PreAuth, or Collection
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [DataMember]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        [DataMember]
// ReSharper disable once UnusedMember.Global
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        [DataMember]
        public string Result { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the judo identifier.
        /// </summary>
        /// <value>
        /// The judo identifier.
        /// </value>
        [DataMember]
        public long JudoId { get; set; }

        /// <summary>
        /// Gets or sets the name of the merchant.
        /// </summary>
        /// <value>
        /// The name of the merchant.
        /// </value>
        [DataMember]
// ReSharper disable once UnusedMember.Global
        public string MerchantName { get; set; }

        /// <summary>
        /// Gets or sets the appears on statement as.
        /// </summary>
        /// <value>
        /// The appears on statement as.
        /// </value>
        [DataMember]
        public string AppearsOnStatementAs { get; set; }

        /// <summary>
        /// Gets or sets the original amount
        /// </summary>
        /// <value>
        /// The original amount.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public decimal? OriginalAmount { get; set; }

        /// <summary>
        /// Refunds and PreAuths will not have this value
        /// </summary>
        /// <value>
        /// The refunds.
        /// </value>
        [DataMember(EmitDefaultValue = false)]

        public decimal Refunds { get; set; }


        /// <summary>
        /// Gets or sets the net amount.
        /// </summary>
        /// <value>
        /// The net amount.
        /// </value>
        [DataMember]
        public decimal NetAmount { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        [DataMember]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
// ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the partner service fee.
        /// </summary>
        /// <value>
        /// The partner service fee.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
// ReSharper disable once UnusedMember.Global
        public decimal PartnerServiceFee { get; set; }

        /// <summary>
        /// Gets or sets the card details.
        /// </summary>
        /// <value>
        /// The card details.
        /// </value>
        [DataMember]
        public CardDetails CardDetails { get; set; }

        /// <summary>
        /// Gets or sets the consumer.
        /// </summary>
        /// <value>
        /// The consumer.
        /// </value>
        [DataMember]
        public Consumer Consumer { get; set; }

        /// <summary>
        /// Gets or sets the risk score.
        /// </summary>
        /// <value>
        /// The risk score.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
// ReSharper disable once UnusedMember.Global
        public int? RiskScore { get; set; }

        /// <summary>
        /// Gets or sets your payment meta data.
        /// </summary>
        /// <value>
        /// Your payment meta data.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
// ReSharper disable once UnusedMember.Global
        public IDictionary<string, string> YourPaymentMetaData { get; set; }

        /// <summary>
        /// If the payment requested 3d secure, we need to include the result of that authentication process
        /// </summary>
        /// <value>
        /// The three d secure.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public ThreeDSecureReceiptModel ThreeDSecure { get; set; }
    }
    // ReSharper restore UnusedMember.Global
    // ReSharper restore UnusedAutoPropertyAccessor.Global
}
