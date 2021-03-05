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
