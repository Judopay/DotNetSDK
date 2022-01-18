using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// This is the whole message from ApplePay, after decryption
    /// </summary>
    /// <remarks>
    /// "version": "EC_v1",
    /// "data": ".....",
    /// "signature": "...", 
    /// "header": { }
    /// </remarks>
    public class EncryptedPaymentMessage
    {
        [DataMember(IsRequired = true)]
        public string Data { get; set; }

        [DataMember(IsRequired = true)]
        public string Signature { get; set; }


   
    }
}
