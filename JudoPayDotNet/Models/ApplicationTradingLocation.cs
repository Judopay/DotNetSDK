using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract(Name = "ApplicationTradingLocation", Namespace = "")]
    public class ApplicationTradingLocation
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
        /// Gets or sets the name of the trading.
        /// </summary>
        /// <value>
        /// The name of the trading.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string TradingName { get; set; }

        /// <summary>
        /// Gets or sets the trading address.
        /// </summary>
        /// <value>
        /// The trading address.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public ApplicationAddressModel TradingAddress { get; set; }

        /// <summary>
        /// Gets or sets the location annual turnover.
        /// </summary>
        /// <value>
        /// The location annual turnover.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string LocationAnnualTurnover { get; set; }

        /// <summary>
        /// Gets or sets the average transaction value.
        /// </summary>
        /// <value>
        /// The average transaction value.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string AverageTransactionValue { get; set; }

        /// <summary>
        /// Gets or sets the MCC code.
        /// </summary>
        /// <value>
        /// The MCC code.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string MccCode { get; set; }

        /// <summary>
        /// Gets or sets the judo identifier.
        /// </summary>
        /// <value>
        /// The judo identifier.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public int JudoId { get; set; }

        /// <summary>
        /// Gets or sets the trading phone number.
        /// </summary>
        /// <value>
        /// The trading phone number.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string TradingPhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the application location LNK record identifier.
        /// </summary>
        /// <value>
        /// The application location LNK record identifier.
        /// </value>
        public long ApplicationLocationLnkRecId { get; set; }

        /// <summary>
        /// Gets or sets the signage type record identifier.
        /// </summary>
        /// <value>
        /// The signage type record identifier.
        /// </value>
        public long? SignageTypeRecId { get; set; }

        /// <summary>
        /// Gets or sets the physical goods leadtime record identifier.
        /// </summary>
        /// <value>
        /// The physical goods leadtime record identifier.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public long? PhysicalGoodsLeadtimeRecId { get; set; }

        /// <summary>
        /// Gets or sets the website address.
        /// </summary>
        /// <value>
        /// The website address.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string WebsiteAddress { get; set; }

        /// <summary>
        /// Gets or sets the product description.
        /// </summary>
        /// <value>
        /// The product description.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string ProductDescription { get; set; }

        /// <summary>
        /// Gets or sets the business support number.
        /// </summary>
        /// <value>
        /// The business support number.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string BusinessSupportNumber { get; set; }

        /// <summary>
        /// Gets or sets the account location LNK record identifier.
        /// </summary>
        /// <value>
        /// The account location LNK record identifier.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public long? AccountLocationLnkRecId { get; set; }
    }
}
