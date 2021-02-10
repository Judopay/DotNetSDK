using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Data to register a card
    /// </summary>
    [DataContract]
    [KnownType(typeof(RegisterEncryptedCardModel))]
    // ReSharper disable UnusedMember.Global
    public class RegisterCardModel : ThreeDSecureTwoPaymentModel
    {
        public RegisterCardModel()
        {
            YourPaymentReference = Guid.NewGuid().ToString();
            HttpHeaders = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets or sets the card number.
        /// </summary>
        /// <value>
        /// The card number.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string CardNumber { get; set; }

        /// <summary>
        /// Gets or sets the expiry date.
        /// </summary>
        /// <value>
        /// The expiry date.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string StartDate { get; set; }

        /// <summary>
        /// Gets or sets the Issue Number.
        /// </summary>
        /// <value>
        /// The Issue Number date.
        /// </value>
        [DataMember(IsRequired = false)]
        public string IssueNumber { get; set; }

        /// <summary>
        /// Gets or sets the card address.
        /// </summary>
        /// <value>
        /// The card address.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public CardAddressModel CardAddress { get; set; }

        /// <summary>
        /// Allows you to set HTTP headers on requests
        /// </summary>
        [IgnoreDataMember]
        public Dictionary<string, string> HttpHeaders { get; private set; }
    }
    // ReSharper restore UnusedMember.Global
}
