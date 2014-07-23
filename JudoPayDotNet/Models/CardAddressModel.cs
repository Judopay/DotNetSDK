using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// A card address information
    /// </summary>
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    [DataContract]
    public class CardAddressModel
    {
        /// <summary>
        /// Gets or sets the address line1.
        /// </summary>
        /// <value>
        /// The line1.
        /// </value>
        [DataMember(IsRequired = false)]
        public string Line1 { get; set; }

        /// <summary>
        /// Gets or sets the address line2.
        /// </summary>
        /// <value>
        /// The line2.
        /// </value>
        [DataMember(IsRequired = false)]
        public string Line2 { get; set; }

        /// <summary>
        /// Gets or sets the address line3.
        /// </summary>
        /// <value>
        /// The line3.
        /// </value>
        [DataMember(IsRequired = false)]
        public string Line3 { get; set; }

        /// <summary>
        /// Gets or sets the town.
        /// </summary>
        /// <value>
        /// The town.
        /// </value>
        [DataMember(IsRequired = false)]
        public string Town { get; set; }

        /// <summary>
        /// Gets or sets the post code.
        /// </summary>
        /// <value>
        /// The post code.
        /// </value>
        [DataMember(IsRequired = true)]
        public string PostCode { get; set; }
    }
    // ReSharper restore UnusedAutoPropertyAccessor.Global
}
