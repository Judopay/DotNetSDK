using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Data to check a card
    /// </summary>
    [DataContract]
    [KnownType(typeof(CheckEncryptedCardModel))]
    // ReSharper disable UnusedMember.Global
    public class CheckCardModel : IModelWithHttpHeaders
    {
        public CheckCardModel()
        {
            YourPaymentReference = Guid.NewGuid().ToString();
            HttpHeaders = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets your payment reference.
        /// </summary>
        /// <value>
        /// Your payment reference.
        /// PLEASE NOTE!!!! there is a reflection call within JudoPayClient.cs that gets this property via a string call. update in both places
        /// including  other model instances of yourPaymentReference ********************
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string YourPaymentReference { get; set; }

        /// <summary>
        /// Gets or sets the CV2.
        /// </summary>
        /// <value>
        /// The CV2.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string CV2 { get; set; }

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
        /// Gets or sets your consumer reference.
        /// </summary>
        /// <value>
        /// Your consumer reference.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string YourConsumerReference { get; set; }

        /// <summary>
        /// Allows you to set HTTP headers on requests
        /// </summary>
        [IgnoreDataMember]
        public Dictionary<string, string> HttpHeaders { get; private set; }

        /// <summary>
        /// Gets or sets the judo identifier. This is the identifier that you can get from our portal
        /// </summary>
        /// <value>
        /// This will be 6 or 9 digits
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string JudoId { get; set; }

        /// <summary>
        /// Gets or sets the iso 3 character currency to be used in the transaction.
        /// </summary>
        /// <value>
        /// GBP, EUR, USD
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets your payment meta data.
        /// </summary>
        /// <value>
        /// Your payment meta data.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public IDictionary<string, string> YourPaymentMetaData { get; set; }
    }
    // ReSharper restore UnusedMember.Global
}
