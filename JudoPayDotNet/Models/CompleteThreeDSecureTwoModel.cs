using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Fields to complete a ThreeDSecure Two transaction
    /// </summary>
    [DataContract(Name = "CompleteThreeDSecureTwoModel", Namespace = "")]
    public class CompleteThreeDSecureTwoModel
    {
        /// <summary>
        /// The card CV2 from the initial transaction.   This is not persisted by Judopay.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string CV2 { get; set; }

        /// <summary>
        /// Details needed for MCC 6012 transactions
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public PrimaryAccountDetailsModel PrimaryAccountDetails { get; set; }
    }
}
