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
    }
    // ReSharper restore UnusedMember.Global
    // ReSharper restore UnusedAutoPropertyAccessor.Global
}
