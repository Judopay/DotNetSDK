using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
        [DataMember(IsRequired = false)]
        public string Data { get; set; }

        [DataMember(IsRequired = false)]
        public string Signature { get; set; }
   
    }
}
