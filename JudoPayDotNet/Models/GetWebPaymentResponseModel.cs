using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
using JudoPayDotNet.Models.CustomDeserializers;

namespace JudoPayDotNet.Models
{
    // The response model to a call to WebPayments.Transactions.Get
    public class GetWebPaymentResponseModel : WebPaymentRequestModel
    {
        /// <summary>
        /// The name of the company.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets the value set with CancelUrl. If not set, this is the default cancel url specified on your account
        /// </summary>
        /// <value>
        /// The account payment cancel URL.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string PaymentCancelUrl { get; set; }

        /// <summary>
        /// Gets the value set with SuccessUrl. If not set, this is the default success url specified on your account
        /// </summary>
        /// <value>
        /// The account payment success URL.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string PaymentSuccessUrl { get; set; }

        /// <summary>
        /// Judopay generated reference for the web payment.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Reference { get; set; }

        /// <summary>
        /// Card Types that can be used for this web payment.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public List<CardType> AllowedCardTypes { get; set; }

        /// <summary>
        /// The response to the initial creation of this web payment.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public WebPaymentResponseModel Response { get; set; }

        /// <summary>
        /// Current status of the web payment.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        [JsonConverter(typeof(StringEnumConverter))]
        public WebPaymentStatus Status { get; set; }

        /// <summary>
        /// The type of the transaction.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        [JsonConverter(typeof(TransactionTypeConvertor))]
        public TransactionType TransactionType { get; set; }

        /// <summary>
        /// Details of a successful transaction against this payment session.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public PaymentReceiptModel Receipt { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool? IsThreeDSecureTwo { get; set; }

        /// <summary>
        /// Number of times the WebPaymentReference was used to authenticate for a Session
        /// </summary>
        [DataMember(EmitDefaultValue = true)]
        public int? NoOfAuthAttempts { get; set; }
    }
}