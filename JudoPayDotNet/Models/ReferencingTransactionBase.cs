using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// The base class for transactions that reference other transactions (like voids, collections and refunds)
    /// </summary>
    [DataContract]
    public abstract class ReferencingTransactionBase :  IModelWithHttpHeaders
    {
        private Dictionary<string, string> _httpHeaders;

        protected ReferencingTransactionBase()
        {
            YourPaymentReference = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Allows you to set HTTP headers on requests
        /// </summary>
        public Dictionary<string, string> HttpHeaders => _httpHeaders ?? (_httpHeaders = new Dictionary<string, string>());

        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>
        /// The transaction identifier.
        /// </value>
        [DataMember(IsRequired = true)]
        public long ReceiptId { get; set; }

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
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        [DataMember(IsRequired = true)]
        public decimal Amount { get; set; }

        /// <summary>
        /// This is a set of fraud signals sent by the mobile SDKs
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        // ReSharper disable once UnusedMember.Global
        public JObject ClientDetails { get; set; }
    }
}