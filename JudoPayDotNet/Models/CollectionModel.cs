using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Collect all or part of a previously authorised (PreAuth) transaction
    /// </summary>
    // ReSharper disable UnusedMember.Global
    [DataContract]
    public class CollectionModel : ReferencingTransactionBase
    {
        /// <summary>
        /// Gets or sets the partner service fee.
        /// </summary>
        /// <value>
        /// The partner service fee.
        /// </value>
        [DataMember]
        public decimal PartnerServiceFee { get; set; }

        /// <summary>
        /// Gets or sets your payment meta data.
        /// </summary>
        /// <value>
        /// Your payment meta data.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public IDictionary<string, string> YourPaymentMetaData { get; set; }

    }
    // ReSharper restore UnusedMember.Global
}
