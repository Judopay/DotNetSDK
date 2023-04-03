using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
	/// <summary>
    /// A payment receipt
    /// </summary>
    /// <remarks>This receipt model contains all the information about the transaction processed, including the outcome (see <see cref="Result"/>)</remarks>
    // ReSharper disable UnusedMember.Global
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    [DataContract(Name = "Receipt", Namespace = "")]
    public class PaymentReceiptModel : ITransactionResult
    {
        /// <summary>
        /// Judopay transaction identifier.
        /// </summary>
        [DataMember]
        public string ReceiptId { get; set; }

        /// <summary>
        /// The Judopay receipt id of the original payment, if this is a refund, collection or void.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
// ReSharper disable once UnusedMember.Global
        public string OriginalReceiptId { get; set; }

        /// <summary>
        /// The merchant reference to uniquely identify a transaction.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string YourPaymentReference { get; set; }

        /// <summary>
        /// The transaction type.  Payment, Refund, PreAuth, Collection, VOID, CheckCard, Register, Save
        /// </summary>
        [DataMember]
        public string Type { get; set; }

        /// <summary>
        /// The time the receipt was created at.
        /// </summary>
        [DataMember]
// ReSharper disable once UnusedMember.Global
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Gets the result of this transaction (e.g. Success, DeclineD)
        /// </summary>
        [DataMember]
        public string Result { get; set; }

        /// <summary>
        /// The message giving more context about the result (E.g. 'Card declined: CV2 policy', 'AuthCode: 123456'
        /// for successful payments)
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// The Judopay account identifier.
        /// </summary>
        [DataMember]
        public long JudoId { get; set; }

        /// <summary>
        /// The trading name associated with the Juodpay merchant account.
        /// </summary>
        [DataMember]
// ReSharper disable once UnusedMember.Global
        public string MerchantName { get; set; }

        /// <summary>
        /// Merchant description requested to be shown on the consumer's statement.
        /// </summary>
        [DataMember]
        public string AppearsOnStatementAs { get; set; }

        /// <summary>
        /// Amount of original transaction (not affected by refunds or collections).
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public decimal? OriginalAmount { get; set; }

        /// <summary>
        /// If this transaction is a PreAuth then this is the amount of that has already been collected.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public decimal? AmountCollected { get; set; }

        /// <summary>
        /// For payments, this is the original amount that has not been refunded. For preauths, the original
        /// amount that has not been collected.
        /// </summary>
        [DataMember]
        public decimal NetAmount { get; set; }

        /// <summary>
        /// The amount of this transaction, for refunds and collections the amount that has been refunded or collected
        /// in this transaction..
        /// </summary>
        [DataMember]
        public decimal Amount { get; set; }

        /// <summary>
        /// The ISO-217 alphabetic code of the currency of the transaction
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
// ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string Currency { get; set; }

        /// <summary>
        /// The unique ID of the transaction set by the acquirer.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string AcquirerTransactionId { get; set; }

        /// <summary>
        /// Response code set by the acquirer.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string ExternalBankResponseCode { get; set; }

        /// <summary>
        /// Authorisation code set by acquirer.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string AuthCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public WalletType? WalletType { get; set; }

        /// <summary>
        /// The transaction risk score (0-100).
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        // ReSharper disable once UnusedMember.Global
        public int? RiskScore { get; set; }

        /// <summary>
        /// The acquirer name.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Acquirer { get; set; }

        /// <summary>
        /// Judopay identifier for associated web payment reference (if the transaction was associated with a
        /// payment session).   Only populated for historic receipts.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string WebPaymentReference { get; set; }

        /// <summary>
        /// Number of times the WebPaymentReference was used to authenticate for a Session
        /// </summary>
        [DataMember(EmitDefaultValue = true)]
        public int? NoOfAuthAttempts { get; set; }

        /// <summary>
        /// Transaction ID set by the card scheme.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string PaymentNetworkTransactionId { get; set; }

        /// <summary>
        /// The type of recurring payment
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string RecurringPaymentType { get; set; }

        /// <summary>
        /// The card details.
        /// </summary>
        [DataMember]
        public CardDetails CardDetails { get; set; }

        /// <summary>
        /// The card billing address.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public CardAddressModel BillingAddress { get; set; }

        /// <summary>
        /// Details about the consumer.
        /// The consumer details.
        /// </summary>
        [DataMember]
        public Consumer Consumer { get; set; }

        /// <summary>
        /// Details about the consumers device.
        /// </summary>
        [DataMember]
        public Device Device { get; set; }

        /// <summary>
        /// Merchant payment meta data.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
// ReSharper disable once UnusedMember.Global
        public IDictionary<string, object> YourPaymentMetaData { get; set; }

        /// <summary>
        /// If the payment requested 3d secure, we need to include the result of that authentication process
        /// </summary>
        /// <value>
        /// The three d secure.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public ThreeDSecureReceiptModel ThreeDSecure { get; set; }

        /// <summary>
        /// Information about the risks associated with the transaction
        /// </summary>
        [DataMember]
        public RiskModel Risks { get; set; }
    }

    // ReSharper restore UnusedMember.Global
    // ReSharper restore UnusedAutoPropertyAccessor.Global
}
