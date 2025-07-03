using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    [DataContract(Name = "NetworkTokenisationDetails", Namespace = "")]
    public class NetworkTokenisationDetailsModel
    {
        /// <summary>
        /// Was a new network token created for this transaction
        /// </summary>
        [DataMember]
        public bool NetworkTokenProvisioned { get; set; }

        /// <summary>
        /// Was a network token used for this transaction
        /// </summary>
        [DataMember]
        public bool NetworkTokenUsed { get; set; }

        /// <summary>
        /// Copy of the exemption flag that was sent by the merchant
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public NetworkTokenisationVirtualPanModel VirtualPan { get; set; }
    }
}
