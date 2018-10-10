using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Data to register a credit card
    /// </summary>
    [DataContract]
    // ReSharper disable UnusedMember.Global
    public class CheckEncryptedCardModel : CheckCardModel
    {
        /// <summary>
        /// Gets or sets the one use token.
        /// </summary>
        /// <value>
        /// The one use token.
        /// </value>
        [DataMember(IsRequired = true)]
        public string OneUseToken { get; set; }
    }
    // ReSharper restore UnusedMember.Global
}
