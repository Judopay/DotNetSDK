using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Collect all or part of a previously authorised (PreAuth) transaction
    /// </summary>
    // ReSharper disable UnusedMember.Global
    [DataContract]
    public class CollectionModel
    {
        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>
		/// The transaction identifier.
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
        /// This is a set of fraud signals sent by the SDKs as part of our judoShield product
        /// </summary>
        /// <value>
        /// The client details.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public JObject ClientDetails { get; set; }
    }
    // ReSharper restore UnusedMember.Global
}
