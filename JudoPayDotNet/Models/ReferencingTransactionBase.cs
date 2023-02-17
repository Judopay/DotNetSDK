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
        /// Judopay transaction identifier of the referenced transaction.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string ReceiptId { get; set; }

        /// <summary>
        /// The merchant reference to for this transaction (not the referenced transaction).
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string YourPaymentReference { get; set; }

        /// <summary>
        /// The transaction amount.   If not specified, the amount of the referenced transaction is used.
        /// If specified, for voids, this must match the amount of the referenced transaction, but refunds
        /// and collections can be less than the referenced amount.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public decimal? Amount { get; set; }

        /// <summary>
        /// The ISO currency code of the transaction (e.g. GBP, EUR, USD).   If not specified the currency
        /// of the referenced transaction is assumed.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Currency { get; set; }
    }
}