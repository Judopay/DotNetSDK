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
        /// Gets or sets the Payment authorisation response.
        /// </summary>
        /// <value>
        /// The Payment authorisation.
        /// </value>
        [DataMember]
        public string PaRes { get; set; }
    }
}
