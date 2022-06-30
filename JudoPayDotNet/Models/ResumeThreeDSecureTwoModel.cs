using System.Runtime.Serialization;
using JudoPayDotNet.Enums;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Fields to resume a ThreeDSecure Two transaction
    /// </summary>
    [DataContract(Name = "ResumeThreeDSecureTwoModel", Namespace = "")]
    public class ResumeThreeDSecureTwoModel
    {
        /// <summary>
        /// Gets or sets the CV2.
        /// </summary>
        /// <value>
        /// The CV2.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string CV2 { get; set; }


        [DataMember(EmitDefaultValue = false)]
        public MethodCompletion MethodCompletion { get; set; }

        /// <summary>
        /// Details needed for MCC 6012 transactions
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public PrimaryAccountDetailsModel PrimaryAccountDetails { get; set; }
    }
}
