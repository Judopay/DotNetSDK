using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Properties used for passing in 3DS2 authentication details performed outside Judopay
    /// </summary>
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    public class ThreeDSecureMpiModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the authentication assigned by the Directory Server (Card Scheme).
        /// </summary>
        [DataMember]
        public string DsTransId { get; set; }

        /// <summary>
        /// Gets or sets the unique value for the authentication provided by the ACS. May be used to provide proof of authentication.
        /// </summary>
        [DataMember]
        public string Cavv { get; set; }

        /// <summary>
        /// Gets or sets the Electronic Commerce Indicator (ECI) is the value returned by Directory Servers (namely Visa, MasterCard,
        /// JCB, and American Express) indicating the outcome of authentication attempted on transactions enforced by 3DS.
        /// </summary>
        [DataMember]
        public string Eci { get; set; }

        /// <summary>
        /// Gets or sets the Version of 3DSecure that the card is enrolled into.
        /// </summary>
        [DataMember]
        public string ThreeDSecureVersion { get; set; }
    }
}
