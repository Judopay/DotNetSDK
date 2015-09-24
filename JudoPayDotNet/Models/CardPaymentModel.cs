using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// A payment made by card
    /// </summary>
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    [DataContract]
    public class CardPaymentModel : PaymentModel
    {
        /// <summary>
        /// Gets or sets the card number.
        /// </summary>
        /// <value>
        /// The card number.
        /// </value>
        [DataMember(IsRequired = true)]
        public string CardNumber { get; set; }

        /// <summary>
        /// Gets or sets the expiry date.
        /// </summary>
        /// <value>
        /// The expiry date.
        /// </value>
        [DataMember(IsRequired = true)]
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
    }
    // ReSharper restore UnusedAutoPropertyAccessor.Global
}