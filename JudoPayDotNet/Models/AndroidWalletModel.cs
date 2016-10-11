using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    public class AndroidWalletModel
    {

        [DataMember(IsRequired = true)]
        public string EncryptedMessage{ get; set; }

        [DataMember(IsRequired = true)]
        public string EphemeralPublicKey { get; set; }

        [DataMember(IsRequired = true)]
        public string Tag { get; set; }

        [DataMember(IsRequired = true)]
        public string PublicKey { get; set; }

        [DataMember(IsRequired = true)]
        public string InstrumentDetails { get; set; }

        [DataMember(IsRequired = true)]
        public string InstrumentType { get; set; }

        [DataMember(IsRequired = true)]
        public string GoogleTransactionId { get; set; }

        [DataMember(IsRequired = true)]
        public int Environment { get; set; }

        [DataMember(IsRequired = true)]
        public int Version { get; set; }

        public string PaymentMethodToken
        {
            set
            {
                var json = JObject.Parse(value);
                EncryptedMessage = json.GetValue("encryptedMessage").Value<string>();
                EphemeralPublicKey = json.GetValue("ephemeralPublicKey").Value<string>();
                Tag = json.GetValue("tag").Value<string>();
            }
            private get { return null; }
        }
    }
}