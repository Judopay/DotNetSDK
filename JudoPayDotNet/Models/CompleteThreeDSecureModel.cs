using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Fields to complete a ThreeDSecure Two transaction
    /// </summary>
    [DataContract(Name = "CompleteThreeDSecureModel", Namespace = "")]
    public class CompleteThreeDSecureModel
    {
        /// <summary>
        /// Gets or sets the CV2.
        /// </summary>
        /// <value>
        /// The CV2.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string CV2 { get; set; }
        
        /// <summary>
        /// Gets or sets the Version.
        /// </summary>
        /// <value>
        /// The CV2.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string Version { get; set; }
    }
}
