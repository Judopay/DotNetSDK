using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// A judo location
    /// </summary>
    [DataContract(Name = "JudoLocation", Namespace = "")]
    public class JudoLocationModel
    {
        /// <summary>
        /// Gets or sets the partner reference.
        /// </summary>
        /// <value>
        /// The partner reference.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string PartnerReference { get; set; }

        /// <summary>
        /// Gets or sets the judo identifier.
        /// </summary>
        /// <value>
        /// The judo identifier.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public int? JudoId { get; set; }

        /// <summary>
        /// Gets or sets the name of the trading.
        /// </summary>
        /// <value>
        /// The name of the trading.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string TradingName { get; set; }
    }
}
