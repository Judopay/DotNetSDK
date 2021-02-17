using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Fields to resume a ThreeDSecure Two transaction
    /// </summary>
    [DataContract(Name = "ResumeThreeDSecureTwoModel", Namespace = "")]
    public class ResumeThreeDSecureTwoModel
    {
        /// <summary>
        /// Information needed for ThreeDSecure2 payments
        /// </summary>
        [DataMember]
        public ThreeDSecureTwoModel ThreeDSecure { get; set; }

        /// <summary>
        /// Gets or sets the CV2.
        /// </summary>
        /// <value>
        /// The CV2.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string CV2 { get; set; }
    }
}
