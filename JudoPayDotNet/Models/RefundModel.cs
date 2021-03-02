using System;
using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// A refund request
    /// </summary>
    /// <remarks>You can refund all or part of a collection or payment</remarks>
    // ReSharper disable UnusedMember.Global
    [DataContract]
    public class RefundModel : ReferencingTransactionBase
    {
        public RefundModel()
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
        /// Gets or sets the partner service fee.
        /// </summary>
        /// <value>
        /// The partner service fee.
        /// </value>
        [DataMember]
        public decimal PartnerServiceFee { get; set; }
    }
    // ReSharper restore UnusedMember.Global
}
