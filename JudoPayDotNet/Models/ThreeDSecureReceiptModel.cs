using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// 3D secure receipt
    /// </summary>
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    // ReSharper disable UnusedMember.Global
    [DataContract(Name = "ThreeDSecure", Namespace = "")]
// ReSharper disable ClassNeverInstantiated.Global
    public class ThreeDSecureReceiptModel
// ReSharper restore ClassNeverInstantiated.Global
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

        /// <summary>
        /// Copy of the exemption flag that was sent by the merchant
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string ChallengeRequestIndicator { get; set; }

        /// <summary>
        /// Copy of the exemption flag that was sent by the merchant
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string ScaExemption { get; set; }

        /// <summary>
        /// Electronic Commerce Indicator 
        /// </summary>
        [DataMember]
        public string Eci { get; set; }
    }
    // ReSharper restore UnusedMember.Global
    // ReSharper restore UnusedAutoPropertyAccessor.Global
}
