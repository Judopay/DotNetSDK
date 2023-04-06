using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// The response model to a call to IPreAuths/IPayments/ICheckCard.Create
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
        /// Judopay generated reference for the web payment.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Reference { get; set; }
    }
    // ReSharper restore UnusedAutoPropertyAccessor.Global
}