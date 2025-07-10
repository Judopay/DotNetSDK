using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Request attribute to save and tokenise a card.
    /// </summary>
    [DataContract]
    // ReSharper disable UnusedMember.Global
    public class SaveCardModel : IModelWithHttpHeaders
    {
        public SaveCardModel()
        {
            HttpHeaders = new Dictionary<string, string>();
        }
        
        /// <summary>
        /// The card PAN (13 to 19 digits depending on card type).   Submit without spaces or dashes.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string CardNumber { get; set; }

        /// <summary>
        /// The card expiry date in format MM/YY, e.g. 07/27.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string ExpiryDate { get; set; }

        /// <summary>
        /// The billing address associated with the card.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public CardAddressModel CardAddress { get; set; }

        /// <summary>
        /// Your unique reference to anonymously identify this consumer.   It is advisable to use GUIDs.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string YourConsumerReference { get; set; }

        /// <summary>
        /// Allows you to set HTTP headers on requests
        /// </summary>
        [IgnoreDataMember]
        public Dictionary<string, string> HttpHeaders { get; private set; }

        /// <summary>
        /// The Judopay account identifier that you can get from our portal.   Typically 9 digits (specify without
        /// spaces or dashes).   If not specified the your default JudoId will be used.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string JudoId { get; set; }

        /// <summary>
        /// The ISO 3 character currency to be used, defaults to GBP if not specified.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Currency { get; set; }

        /// <summary>
        /// The full name of the card holder.  Note: When testing 3D Secure in the sandbox environment, it is the
        /// CardHolderName that is used to determine the test card for 3D Secure 2 authentication.
        /// </summary>
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string CardHolderName { get; set; }

        /// <summary>
        /// Set true to specify that network tokenisation should not be used for this transaction even if network
        /// token registration has been enabled on the account
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public bool? DisableNetworkTokenisation { get; set; }
    }
    // ReSharper restore UnusedMember.Global
}
