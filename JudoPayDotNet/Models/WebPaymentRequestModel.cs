using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using JudoPayDotNet.Enums;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Request model for creation of web payment session.
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
        /// The transaction amount.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public decimal Amount { get; set; }

        /// <summary>
        /// Card holder address.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public WebPaymentCardAddress CardAddress { get; set; }

        /// <summary>
        /// The currency to process (GBP, USD, EUR)
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Currency { get; set; }

        /// <summary>
        /// Date and time this web payment expires
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public DateTimeOffset ExpiryDate { get; set; }

        /// <summary>
        /// The Judopay account identifier.
        /// </summary>
        /// <remarks>
        /// You can have multiple locations in your account.
        /// </remarks>
        [DataMember(EmitDefaultValue = false)]
        public string JudoId { get; set; }

        /// <summary>
        /// Sets the custom URL to which the customer is redirected if they cancel the transaction or if the transaction fails
        /// </summary>
        /// <value>
        /// The override payment cancel URL.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string CancelUrl { get; set; }

        /// <summary>
        /// Sets the custom URL to which the customer is redirected if their transaction is successful
        /// </summary>
        /// <value>
        /// The override payment success URL.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string SuccessUrl { get; set; }

        /// <summary>
        /// Your merchant consumer reference.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string YourConsumerReference { get; set; }

        /// <summary>
        /// Your merchant payment meta data.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public IDictionary<string, object> YourPaymentMetaData { get; set; }

        /// <summary>
        /// Your merchant payment reference.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string YourPaymentReference { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public WebPaymentOperation? WebPaymentOperation { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool IsPayByLink { get; set; }

        /// <summary>
        /// Card holder mobile number (used for 3DS2 authentication).
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string MobileNumber { get; set; }

        /// <summary>
        /// Card holder phone country code (used for 3DS2 authentication).
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string PhoneCountryCode { get; set; }

        /// <summary>
        /// Card holder email address (used for 3DS2 authentication).
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Used to customise WebPayments v2 form.   Not populated in GetWebPaymentResponseModel
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public bool? HideBillingInfo { get; set; }

        /// <summary>
        /// Used to customise WebPayments v2 form.   Not populated in GetWebPaymentResponseModel
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public bool? HideReviewInfo { get; set; }

        /// <summary>
        /// Information needed for ThreeDSecure2 payments. Not returned in
        /// GetWebPaymentResponseModel
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public ThreeDSecureTwoModel ThreeDSecure { get; set; }

        /// <summary>
        /// Financial Beneficiary Information for MCC 6012 transactions. Not returned in
        /// GetWebPaymentResponseModel
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public PrimaryAccountDetailsModel PrimaryAccountDetails { get; set; }

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
