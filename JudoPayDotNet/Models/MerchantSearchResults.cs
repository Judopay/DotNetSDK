using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Results of a Merchant Search
    /// </summary>
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    // ReSharper disable UnusedMember.Global
    [DataContract(Name = "SearchResults", Namespace = "")]
// ReSharper disable ClassNeverInstantiated.Global
    public class MerchantSearchResults
// ReSharper restore ClassNeverInstantiated.Global
    {
        /// <summary>
        /// Gets or sets the result count.
        /// </summary>
        /// <value>
        /// The result count.
        /// </value>
        [DataMember]
        public long ResultCount { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        [DataMember]
        public long PageSize { get; set; }

        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        /// <value>
        /// The offset.
        /// </value>
        [DataMember]
        public long Offset { get; set; }

        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>
        /// The results.
        /// </value>
        [DataMember]
        public IEnumerable<MarketPlaceMerchant> Results { get; set; }

        /// <summary>
        /// Gets or sets the sort.
        /// </summary>
        /// <value>
        /// The sort.
        /// </value>
        [DataMember]
        public string Sort { get; set; }
    }
    // ReSharper restore UnusedMember.Global
    // ReSharper restore UnusedAutoPropertyAccessor.Global
}
