using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Data to save a card using a OneUseToken
    /// </summary>
    [DataContract]
    // ReSharper disable UnusedMember.Global
    public class SaveEncryptedCardModel : SaveCardModel
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
