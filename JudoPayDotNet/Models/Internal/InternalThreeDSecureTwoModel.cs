using System.Runtime.Serialization;
using JudoPayDotNet.Enums;

namespace JudoPayDotNet.Models.Internal
{
    // Only used for internal call to PartnerApi 6.x on resume
    [DataContract(Name = "ThreeDSecureTwo", Namespace = "")]
    public class InternalThreeDSecureTwoModel : ThreeDSecureTwoModel
    {
        [DataMember(EmitDefaultValue = false)]
        public MethodCompletion MethodCompletion { get; set; }
    }
}
