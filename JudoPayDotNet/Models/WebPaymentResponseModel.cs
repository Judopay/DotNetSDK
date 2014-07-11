using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Web payment response
    /// </summary>
    [DataContract]
    public class WebPaymentResponseModel
    {
        /// <summary>
        /// Gets or sets the post URL.
        /// </summary>
        /// <value>
        /// The post URL.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string PostUrl { get; set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        /// <value>
        /// The reference.
        /// </value>
        [DataMember(EmitDefaultValue = false)]
        public string Reference { get; set; }
    }
}