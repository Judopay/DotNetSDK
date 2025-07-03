using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    [DataContract(Name = "VirtualPan", Namespace = "")]
    public class NetworkTokenisationVirtualPanModel
    {
        /// <summary>
        /// Last four digits of the virtual PAN
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string LastFour { get; set; }

        /// <summary>
        /// Expiry date of the virtual PAN
        /// </summary>
        [DataMember]
        public string ExpiryDate { get; set; }
    }
}
