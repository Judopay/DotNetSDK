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
        /// URL prefix to launch Judo hosted web payments page (follow with ?reference={Reference})
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string PostUrl { get; private set; }

        /// <summary>
        /// Judopay generated reference for the web payment.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Reference { get; private set; }

        [DataMember(EmitDefaultValue = false)]
        private string PayByLinkUrl { get; set; }

        /// <summary>
        /// Short URL to launch Judo hosted web payments page
        /// </summary>
        public string ShortUrl => PayByLinkUrl;
    }
    // ReSharper restore UnusedAutoPropertyAccessor.Global
}