using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Application information
    /// </summary>
    [DataContract(Name = "Application", Namespace = "")]   
    public class ApplicationModel
    {
        /// <summary>
        /// Gets or sets the application reference.
        /// </summary>
        /// <value>
        /// The application reference.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string ApplicationReference { get; set; }

        /// <summary>
        /// Gets or sets the application submitted date.
        /// </summary>
        /// <value>
        /// The application submitted date.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public DateTimeOffset? ApplicationSubmittedDate { get; set; }

        /// <summary>
        /// Gets or sets the abandoned date.
        /// </summary>
        /// <value>
        /// The abandoned date.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public DateTimeOffset? AbandonedDate { get; set; }

        /// <summary>
        /// Gets or sets the partner application reference.
        /// </summary>
        /// <value>
        /// The partner application reference.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string PartnerApplicationReference { get; set; }

        /// <summary>
        /// Gets or sets your application meta data.
        /// </summary>
        /// <value>
        /// Your application meta data.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public IDictionary<string, string> YourApplicationMetaData { get; set; }

        /// <summary>
        /// Gets or sets the principle.
        /// </summary>
        /// <value>
        /// The principle.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public List<ApplicationPrincipleModel> Principle { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public ApplicationBusinessModel Business { get; set; }

        /// <summary>
        /// Gets or sets the locations.
        /// </summary>
        /// <value>
        /// The locations.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public List<ApplicationTradingLocation> Locations { get; set; }

        /// <summary>
        /// Gets or sets the deposit account.
        /// </summary>
        /// <value>
        /// The deposit account.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public DepositAcccount DepositAccount { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string Product { get; set; }

        /// <summary>
        /// Gets or sets the signage.
        /// </summary>
        /// <value>
        /// The signage.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string Signage { get; set; }

        /// <summary>
        /// Gets or sets the account record identifier.
        /// </summary>
        /// <value>
        /// The account record identifier.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public long? AccountRecId { get; set; }

        /// <summary>
        /// Gets or sets the product record identifier.
        /// </summary>
        /// <value>
        /// The product record identifier.
        /// </value>
        public long? ProductRecId { get; set; }

        /// <summary>
        /// Gets or sets the seller user identifier.
        /// </summary>
        /// <value>
        /// The seller user identifier.
        /// </value>
        public long? SellerUserId { get; set; }

        /// <summary>
        /// Gets or sets the partner record identifier.
        /// </summary>
        /// <value>
        /// The partner record identifier.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public long? PartnerRecId { get; set; }

        /// <summary>
        /// if this is a market place signup
        /// </summary>
        /// <value>
        /// The oauth client identifier.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string OAuthClientId { get; set; }

        /// <summary>
        /// Gets or sets the short name of the partner.
        /// </summary>
        /// <value>
        /// The short name of the partner.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string PartnerShortName { get; set; }
    }
}
