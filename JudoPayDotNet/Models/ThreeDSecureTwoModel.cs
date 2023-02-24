using System.Runtime.Serialization;
using JudoPayDotNet.Enums;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// ThreeDSecure specific transaction request attributes
    /// </summary>
    [DataContract(Name = "ThreeDSecureTwo", Namespace = "")]
    public class ThreeDSecureTwoModel
    {
        /// <summary>
        /// Type of device that will be used for challenge authentication. Only Browser is supported currently.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public ThreeDSecureTwoAuthenticationSource AuthenticationSource { get; set; }

        /// <summary>
        /// URL of the page listening for a POST event once the supplied device details have been processed.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string MethodNotificationUrl { get; set; }

        /// <summary>
        /// URL of the page listening for a POST event once the consumer challenge has been processed.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string ChallengeNotificationUrl { get; set; }

        /// <summary>
        /// Indicates whether a challenge is requested for this transaction (this may be over-ruled by the issuer).
        /// Should not be specified in the same request as ScaExemption
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public ThreeDSecureTwoChallengeRequestIndicator? ChallengeRequestIndicator { get; set; }

        /// <summary>
        /// Indicates reason why challenge may not be necessary (this may be over-ruled by the issuer).
        /// Should not be specified in the same request as ChallengeRequestIndicator
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public ThreeDSecureTwoScaExemption? ScaExemption { get; set; }
    }
}
