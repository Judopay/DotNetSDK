using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Web payment card address information
    /// </summary>
    [DataContract]
    public class WebPaymentCardAddress
    {
        /// <summary>
        /// Gets or sets the name of the card holder.
        /// </summary>
        /// <value>
        /// The name of the card holder.
        /// </value>
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string CardHolderName { get; set; }

        /// <summary>
        /// Gets or sets the line1.
        /// </summary>
        /// <value>
        /// The line1.
        /// </value>
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string Line1 { get; set; }

        /// <summary>
        /// Gets or sets the line2.
        /// </summary>
        /// <value>
        /// The line2.
        /// </value>
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string Line2 { get; set; }

        /// <summary>
        /// Gets or sets the line3.
        /// </summary>
        /// <value>
        /// The line3.
        /// </value>
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string Line3 { get; set; }

        /// <summary>
        /// Gets or sets the town.
        /// </summary>
        /// <value>
        /// The town.
        /// </value>
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string Town { get; set; }

        /// <summary>
        /// Gets or sets the post code.
        /// </summary>
        /// <value>
        /// The post code.
        /// </value>
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string PostCode { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string Country { get; set; }
    }
}
