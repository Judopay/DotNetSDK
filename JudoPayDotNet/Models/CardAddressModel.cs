﻿using System;
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
        /// Gets or sets the Address1
        /// </summary>
        /// <value>
        /// The Address1.
        /// </value>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the Address2
        /// </summary>
        /// <value>
        /// The Address2.
        /// </value>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the Address3
        /// </summary>
        /// <value>
        /// The Address1.
        /// </value>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Address3 { get; set; }

        [Obsolete("This property is obsolete. Please use Address1 instead.", false)]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string Line1 { get; set; }

        [Obsolete("This property is obsolete. Please use Address2 instead.", false)]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string Line2 { get; set; }

        [Obsolete("This property is obsolete. Please use Address3 instead.", false)]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string Line3 { get; set; }

        /// <summary>
        /// Gets or sets the town.
        /// </summary>
        /// <value>
        /// The town.
        /// </value>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Town { get; set; }

        /// <summary>
        /// Gets or sets the post code.
        /// </summary>
        /// <value>
        /// The post code.
        /// </value>
        [DataMember(IsRequired = true, EmitDefaultValue = false)]
        public string PostCode { get; set; }

        /// <summary> 
        /// The optional country code (ISO 3166-1) for this address. 
        /// </summary> 
        /// <remarks>UK is 826</remarks>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public int? CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string State { get; set; }

    }
    // ReSharper restore UnusedAutoPropertyAccessor.Global
}
