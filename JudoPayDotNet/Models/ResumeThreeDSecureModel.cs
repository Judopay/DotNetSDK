using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Fields to resume a ThreeDSecure transaction
    /// </summary>
    [DataContract(Name = "ResumeThreeDSecureModel", Namespace = "")]
    public class ResumeThreeDSecureModel
    {
        /// <summary>
        /// Information needed for ThreeDSecure2 payments
        /// </summary>
        [DataMember]
        public ThreeDSecureModel ThreeDSecure { get; set; }

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
