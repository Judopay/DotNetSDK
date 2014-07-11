using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// ThreeD verification result
    /// </summary>
    [DataContract(Name = "ThreeDResult", Namespace = "")]
    public class ThreeDResultModel
    {
        /// <summary>
        /// Gets or sets the pa resource.
        /// </summary>
        /// <value>
        /// The pa resource.
        /// </value>
        [DataMember]
        public string PaRes { get; set; }
    }
}
