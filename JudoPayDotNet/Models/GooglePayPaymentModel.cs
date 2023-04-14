using System.Runtime.Serialization;

// ReSharper disable ClassNeverInstantiated.Global
namespace JudoPayDotNet.Models
{
    [DataContract]
    public class GooglePayPaymentModel : ThreeDSecureTwoPaymentModel
    {
        /// <summary>
        /// Google Pay transaction details.
        /// </summary>
        [DataMember(IsRequired = true)]
        public GooglePayWalletModel GooglePayWallet { get; set; }

        /// <summary>
        /// Card holder address.
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public CardAddressModel CardAddress { get; set; }
    }
}