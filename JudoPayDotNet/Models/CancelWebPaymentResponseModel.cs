using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Response model for calls to IPayments/IPreAuths/ICheckCard Cancel
    /// </summary>
    [DataContract]
    public class CancelWebPaymentResponseModel
    {
        /// <summary>
        /// Judopay reference for the web payment session.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Reference { get; set; }

        /// <summary>
        /// The payment session status.  e.g Cancelled
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        [JsonConverter(typeof(StringEnumConverter))]
        public WebPaymentStatus Status { get; set; }
    }
}
