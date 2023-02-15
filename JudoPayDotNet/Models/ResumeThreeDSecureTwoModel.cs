using System.Runtime.Serialization;
using JudoPayDotNet.Enums;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Fields to resume a ThreeDSecure transaction after a requested device details check has been performed.
    /// </summary>
    [DataContract(Name = "ResumeThreeDSecureTwoModel", Namespace = "")]
    public class ResumeThreeDSecureTwoModel
    {
        /// <summary>
        /// The card CV2 from the initial transaction.   This is not persisted by Judopay.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string CV2 { get; set; }

        /// <summary>
        /// Set to Yes to indicate that the device details were sent to the MethodUrl, and a notification
        /// received on the MethodNotificationUrl.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public MethodCompletion MethodCompletion { get; set; }

        /// <summary>
        /// Details needed for MCC 6012 transactions
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public PrimaryAccountDetailsModel PrimaryAccountDetails { get; set; }
    }
}
