using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    ///     Details of the card used in the requested operation (add card/payment)
    /// </summary>
    [DataContract(Name = "CardDetails", Namespace = "")]
    public class CardDetails
    {
        /// <summary>
        /// Gets or sets the card last four digits.
        /// </summary>
        /// <value>
        /// The card lastfour.
        /// </value>
        [DataMember]
        public string CardLastfour { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        [DataMember]
        public string EndDate { get; set; }

        /// <summary>
        /// Gets or sets the card token.
        /// </summary>
        /// <value>
        /// The card token.
        /// </value>
        [DataMember]
        public string CardToken { get; set; }

        /// <summary>
        /// Gets or sets the type of the card.
        /// </summary>
        /// <value>
        /// The type of the card.
        /// </value>
        [DataMember]
        public CardType CardType { get; set; }
    }
}
