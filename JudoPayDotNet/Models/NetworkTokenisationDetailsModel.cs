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
        /// Details of the Virtual PAN used for this transaction, if applicable.
        /// Will be null if no network token was used for this transaction.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public NetworkTokenisationVirtualPanModel VirtualPan { get; set; }

        /// <summary>
        /// When true, indicates that the underlying payment card details have been updated as part of this request.
        /// This is usually caused by an expired or cancelled card.
        /// </summary>
        [DataMember]
        public bool? AccountDetailsUpdated { get; set; }
    }
}
