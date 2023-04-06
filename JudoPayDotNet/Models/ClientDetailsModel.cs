using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Encrypted device details sent from mobile SDK using DeviceDna
    /// </summary>
    [DataContract]
    public class ClientDetailsModel
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Key { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Value { get; set; }
    }
}