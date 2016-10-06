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

        public void SetPaymentMethodToken(string json)
        {     
            var jsonObject = JObject.Parse(json);
            EncryptedMessage = jsonObject.GetValue("encryptedMessage").Value<string>();
            EphemeralPublicKey = jsonObject.GetValue("ephemeralPublicKey").Value<string>();
            Tag = jsonObject.GetValue("tag").Value<string>();   
        }
    }
}