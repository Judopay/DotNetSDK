using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// A collection request
    /// </summary>
    [DataContract]
    public class CollectionModel
    {
        /// <summary>
        /// Gets or sets the receipt identifier.
        /// </summary>
        /// <value>
        /// The receipt identifier.
        /// </value>
        [DataMember(IsRequired = true)]
        public int ReceiptId { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        [DataMember(IsRequired = true)]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets your payment reference.
        /// </summary>
        /// <value>
        /// Your payment reference.
        /// </value>
        [DataMember(IsRequired = true)]
        public string YourPaymentReference { get; set; }

        /// <summary>
        /// Gets or sets the partner service fee.
        /// </summary>
        /// <value>
        /// The partner service fee.
        /// </value>
        [DataMember]
        public decimal PartnerServiceFee { get; set; }

        /// <summary>
        /// This is a set of fraud signals sent by the mobile SDKs
        /// </summary>
        /// <value>
        /// The client details.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public JObject ClientDetails { get; set; }
    }
}
