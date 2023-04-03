using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using JudoPayDotNet.Enums;
using JudoPayDotNet.Models.CustomDeserializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// WebPayments request
    /// </summary>
    // ReSharper disable UnusedMember.Global
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    [DataContract]
    public class WebPaymentRequestModel : IModelWithHttpHeaders
    {

        public WebPaymentRequestModel()
        {
            YourPaymentReference = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the card address.
        /// </summary>
        /// <value>
        /// The card address.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public WebPaymentCardAddress CardAddress { get; set; }

        /// <summary>
        /// Gets or sets the client ip address.
        /// </summary>
        /// <value>
        /// The client ip address.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string ClientIpAddress { get; set; }

        /// <summary>
        /// Gets or sets the client user agent.
        /// </summary>
        /// <value>
        /// The client user agent.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string ClientUserAgent { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string CompanyName { get; set; }

        /// <summary>
        /// The currency to process (GBP, USD, EUR)
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Currency { get; set; }

        /// <summary>
        ///     The date and time this webpayment expires
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public DateTimeOffset ExpiryDate { get; set; }

        /// <summary>
        ///     The judo id of the location you wish to pay
        /// </summary>
        /// <remarks>
        /// You can have multiple locations in your account.
        /// </remarks>
        [DataMember(EmitDefaultValue = false)]
        public string JudoId { get; set; }

        /// <summary>
        /// The value of the partner service fee
        /// </summary>
        /// <remarks>Unless your using our marketplace product, this will always be zero</remarks>
        [DataMember]
        public decimal PartnerServiceFee { get; set; }

        /// <summary>
        /// Gets the value set with CancelUrl. If not set, this is the default cancel url specified on your account
        /// </summary>
        /// <value>
        /// The account payment cancel URL.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string PaymentCancelUrl { get; }

        /// <summary>
        /// Sets the custom URL to which the customer is redirected if they cancel the transaction or if the transaction fails
        /// </summary>
        /// <value>
        /// The override payment cancel URL.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string CancelUrl { get; set; }

        /// <summary>
        /// Gets the value set with SuccessUrl. If not set, this is the default success url specified on your account
        /// </summary>
        /// <value>
        /// The account payment success URL.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string PaymentSuccessUrl { get;  }

        /// <summary>
        /// Sets the custom URL to which the customer is redirected if their transaction is successful
        /// </summary>
        /// <value>
        /// The override payment success URL.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string SuccessUrl { get; set; }

        /// <summary>
        ///     This is the random reference for the payment
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        /// <value>
        /// The response.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public WebPaymentResponseModel Response { get; set; }

        /// <summary>
        ///     This should be the last state the webpayment was in (i.e. failed payments can retry)
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        [JsonConverter(typeof(StringEnumConverter))]
        public WebPaymentStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the type of the transaction.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        [JsonConverter(typeof(TransactionTypeConvertor))]
        public TransactionType TransactionType { get; set; }

        /// <summary>
        /// Gets or sets your consumer reference.
        /// </summary>
        /// <value>
        /// Your consumer reference.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string YourConsumerReference { get; set; }

        /// <summary>
        /// Gets or sets your payment meta data.
        /// </summary>
        /// <value>
        /// Your payment meta data.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public IDictionary<string, string> YourPaymentMetaData { get; set; }

        /// <summary>
        /// Gets or sets your payment reference.
        /// </summary>
        /// <value>
        /// Your payment reference.
        ///PLEASE NOTE!!!! there is a reflection call within JudoPayClient.cs that gets this property via a string call. update in both places
        /// including  other model instances of yourPaymentReference ********************
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string YourPaymentReference { get; }

        /// <summary>
        /// Gets or sets the receipt.
        /// </summary>
        /// <value>
        /// The receipt.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public PaymentReceiptModel Receipt { get; set; }

        [DataMember(EmitDefaultValue = false)]
        [JsonConverter(typeof(StringEnumConverter))]
        public WebPaymentOperation? WebPaymentOperation { get; set; }

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

        private Dictionary<string, string> _httpHeaders;

        /// <summary>
        /// Allows you to set HTTP headers on requests
        /// </summary>
        [IgnoreDataMember]
        public Dictionary<string, string> HttpHeaders
        {
            get { return _httpHeaders ?? (_httpHeaders = new Dictionary<string, string>()); }
        }
    }
    // ReSharper restore UnusedAutoPropertyAccessor.Global
    // ReSharper restore UnusedMember.Global
}
