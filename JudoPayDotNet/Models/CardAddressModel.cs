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
        /// The first line of the card holder address.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string Address1 { get; set; }

        /// <summary>
        /// The second line of the card holder address.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string Address2 { get; set; }

        /// <summary>
        /// The third line of the card holder address.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string Address3 { get; set; }

        /// <summary>
        /// The town of the card holder.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string Town { get; set; }

        /// <summary>
        /// The post code of the card holder.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string PostCode { get; set; }

        /// <summary> 
        /// The optional country code (ISO 3166-1) for this address. 
        /// </summary> 
        /// <remarks>UK is 826</remarks>
        [DataMember(IsRequired = false)]
        public int? CountryCode { get; set; }

        /// <summary>
        /// The state (for cards from USA or Canada)
        /// </summary>
        [DataMember(IsRequired = false)]
        public string State { get; set; }
    }
    // ReSharper restore UnusedAutoPropertyAccessor.Global
}
