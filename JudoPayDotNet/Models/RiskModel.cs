using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    [DataContract(Name = "Risks", Namespace = "")]
    public class RiskModel
    {
        /// <summary>
        /// The merchant suggestion from the amount of risk associated with the transaction 
        /// </summary>
        /// <value>
        /// Possible values include Allow, Block, Review
        /// </value>
        [DataMember]
        public string MerchantSuggestion { get; set; }

        /// <summary>
        /// If AVS is submitted, this will contain the postcode check result
        /// </summary>
        /// <value>
        /// Possible values include PASSED, NOTCHECKED  and FAILED
        /// </value>
        [DataMember]
        public string PostcodeCheck { get; set; }
    }
}