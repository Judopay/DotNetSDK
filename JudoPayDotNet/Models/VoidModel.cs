using System;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Void a previously authorised (PreAuth) transaction
    /// </summary>
    // ReSharper disable UnusedMember.Global
    [DataContract]
    public class VoidModel : ReferencingTransactionBase
    {
        public VoidModel()
        {
            YourPaymentReference = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>
		/// The transaction identifier.
        /// </value>
        [DataMember(IsRequired = true)]
        public long ReceiptId { get; set; }

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
        ///PLEASE NOTE!!!! there is a reflection call within JudoPayClient.cs that gets this property via a string call. update in both places
        /// including  other model instances of yourPaymentReference ********************
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string YourPaymentReference { get; }

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
