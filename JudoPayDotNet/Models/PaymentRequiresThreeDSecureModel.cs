using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// A payment that requires threeD secure
    /// </summary>
    [DataContract(Name = "ThreeDRequired", Namespace = "")]
    public class PaymentRequiresThreeDSecureModel : ITransactionResult
    {
        /// <summary>
        /// Gets or sets the receipt identifier.
        /// </summary>
        /// <value>
        /// The receipt identifier.
        /// </value>
        [DataMember]
        public string ReceiptId { get; set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        [DataMember]
        public string Result { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the AcsUrl.
        /// </summary>
        /// <value>
        /// The acs URL.
        /// </value>
        [DataMember]
        public string AcsUrl { get; set; }

        /// <summary>
        /// Gets or sets the Md.
        /// </summary>
        /// <value>
        /// The md.
        /// </value>
        [DataMember]
        public string Md { get; set; }

        /// <summary>
        /// Gets or sets the PaReq.
        /// </summary>
        /// <value>
        /// The pa req.
        /// </value>
        [DataMember]
        public string PaReq { get; set; }
    }
}
