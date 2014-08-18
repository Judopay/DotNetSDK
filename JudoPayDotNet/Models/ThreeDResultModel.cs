using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// 3D verification result
    /// </summary>
    [DataContract(Name = "ThreeDResult", Namespace = "")]
    public class ThreeDResultModel
    {
        /// <summary>
        /// Gets or sets the Payment authorization response.
        /// </summary>
        /// <value>
        /// The Payment authorization.
        /// </value>
        [DataMember]
        public string PaRes { get; set; }
    }
}
