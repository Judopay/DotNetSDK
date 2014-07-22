using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// ThreeD secure receipt
    /// </summary>
    [DataContract(Name = "ThreeDSecure", Namespace = "")]
    public class ThreeDSecureReceiptModel
    {
        /// <summary>
        /// Did the consumer attempt to authenticate through 3d secure
        /// </summary>
        [DataMember]
        public bool Attempted { get; set; }


        /// <summary>
        /// what was the outcome of their authentication
        /// </summary>
        [DataMember]
        public string Result { get; set; }
    }
}
