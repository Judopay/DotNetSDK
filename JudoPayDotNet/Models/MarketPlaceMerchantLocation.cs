using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// A market place merchange location
    /// </summary>
    [DataContract(Name = "Location", Namespace = "")]
    public class MarketPlaceMerchantLocation
    {
        /// <summary>
        /// Gets or sets the partner reference.
        /// </summary>
        /// <value>
        /// The partner reference.
        /// </value>
        [DataMember]
        public string PartnerReference { get; set; }

        /// <summary>
        /// Gets or sets the name of the trading.
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
}
