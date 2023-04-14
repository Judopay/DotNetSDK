using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// The information required to complete ThreeDSecure 2.x authorization on a transaction
    /// </summary>
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    [DataContract(Name = "ThreeDRequired", Namespace = "")]
    public class PaymentRequiresThreeDSecureTwoModel : ITransactionResult
    {
        /// <summary>
        /// The Judopay transaction identifier.
        /// </summary>
        [DataMember]
        public string ReceiptId { get; set; }

        /// <summary>
        /// e.g. "Challenge completion is needed for 3D Secure 2"
        /// </summary>
        [DataMember]
        public string Result { get; set; }

        /// <summary>
        /// e.g. "Issuer ACS has responded with a Challenge URL"
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// Base64 encoded string representing the ThreeDSecure transaction details.
        /// </summary>
        [DataMember]
        public string Md { get; set; }

        /// <summary>
        /// When device details are required, this is the URL that device details should be POSTed to.
        /// </summary>
        [DataMember]
        public string MethodUrl { get; set; }

        /// <summary>
        /// When a challenge is required, this is the URL the customer should be directed to to complete the challenge.
        /// </summary>
        [DataMember]
        public string ChallengeUrl { get; set; }

        /// <summary>
        /// Base64 encoded string representing the ThreeDSecure challenge details.
        /// </summary>
        [DataMember]
        public string CReq { get; set; }

        /// <summary>
        /// ThreeDSecure version used by the ACS.
        /// </summary>
        [DataMember]
        public string Version { get; set; }
    }
    // ReSharper restore UnusedAutoPropertyAccessor.Global
}
