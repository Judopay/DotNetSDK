using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    public class VisaCheckoutWalletModel
    {
        [DataMember(IsRequired = true)]
        public string EncryptedKey { get; set; }
        [DataMember(IsRequired = true)]
        public string EncryptedPaymentData { get; set; }

        /// <summary>
        /// Visa's identifier for this wallet, usually a long numeric string
        /// </summary>
        [DataMember(IsRequired = true)]
        public string CallId { get; set; }
    }
}