using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// A market place merchant location
    /// </summary>
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    // ReSharper disable UnusedMember.Global
    [DataContract(Name = "Location", Namespace = "")]
// ReSharper disable ClassNeverInstantiated.Global
    public class MarketPlaceMerchantLocation
// ReSharper restore ClassNeverInstantiated.Global
    {
        /// <summary>
        /// Gets or sets your reference for this merchant
        /// </summary>
        /// <value>
        /// The partner reference.
        /// </value>
        [DataMember]
        public string PartnerReference { get; set; }

        /// <summary>
        /// Gets or sets the name of this trading location.
        /// </summary>
        /// <value>
        /// The name of the trading.
        /// </value>
        [DataMember]
        public string TradingName { get; set; }

        /// <summary>
        /// Gets or sets the judo identifier.
        /// </summary>
        /// <value>
        /// The judo identifier.
        /// </value>
        [DataMember]
        public string JudoId { get; set; }
    }
    // ReSharper restore UnusedMember.Global
    // ReSharper restore UnusedAutoPropertyAccessor.Global
}
