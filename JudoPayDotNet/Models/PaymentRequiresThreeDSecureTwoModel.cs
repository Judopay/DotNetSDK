using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// The information required to complete ThreeDSecure Two authorization on a transaction (payment or preauth)
    /// </summary>
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    [DataContract(Name = "ThreeDRequired", Namespace = "")]
    public class PaymentRequiresThreeDSecureTwoModel : PaymentRequiresThreeDSecureModel
    {
        [DataMember]
        public string MethodUrl { get; set; }

        [DataMember]
        public string ChallengeUrl { get; set; }

        // Base64 encoded value from result.authentication.encodedChallengeRequestMessage in Authentication response
        [DataMember]
        public string CReq { get; set; }

        [DataMember]
        public string Version { get; set; }
    }
    // ReSharper restore UnusedAutoPropertyAccessor.Global
}
