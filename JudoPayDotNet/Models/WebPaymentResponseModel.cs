using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Web payment response
    /// </summary>
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    [DataContract]
// ReSharper disable ClassNeverInstantiated.Global
    public class WebPaymentResponseModel
// ReSharper restore ClassNeverInstantiated.Global
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
    // ReSharper restore UnusedAutoPropertyAccessor.Global
}