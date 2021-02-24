using System.Runtime.Serialization;
using JudoPayDotNet.Enums;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// 3D verification result
    /// </summary>
    [DataContract(Name = "ThreeDSecureTwo", Namespace = "")]
    public class ThreeDSecureTwoModel
    {
        [DataMember(EmitDefaultValue = false)]
        public ThreeDSecureTwoAuthenticationSource AuthenticationSource { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string MethodNotificationUrl { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ChallengeNotificationUrl { get; set; }
    }
}
