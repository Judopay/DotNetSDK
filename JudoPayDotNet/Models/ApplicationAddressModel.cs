using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Application Address
    /// </summary>
    [DataContract(Name = "ApplicationAddress", Namespace = "")]
    public class ApplicationAddressModel
    {
        /// <summary>
        /// Gets or sets the name or number of the building.
        /// </summary>
        /// <value>
        /// The name of the building number or.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string BuildingNumberOrName { get; set; }

        /// <summary>
        /// Gets or sets the flat number.
        /// </summary>
        /// <value>
        /// The flat number.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string FlatNumber { get; set; }

        /// <summary>
        /// Gets or sets the address line 1.
        /// </summary>
        /// <value>
        /// The address line1.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the address line 2.
        /// </summary>
        /// <value>
        /// The address line2.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the post code.
        /// </summary>
        /// <value>
        /// The post code.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string PostCode { get; set; }
    }
}
