using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Web payment card address information 
    /// </summary>
    [DataContract]
    public class WebPaymentCardAddress : CardAddressModel
    {
        /// <summary>
        /// The name of the card holder.
        /// </summary>
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string CardHolderName { get; set; }
    }
}
