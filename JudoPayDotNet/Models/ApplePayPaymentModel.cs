using System.Runtime.Serialization;

// ReSharper disable ClassNeverInstantiated.Global
namespace JudoPayDotNet.Models
{
    [DataContract]
    public class ApplePayPaymentModel : PaymentModel
    {
        /// <summary>
        /// Apple Pay transaction details.
        /// </summary>
        [DataMember(IsRequired = true)]
        public PKPaymentInnerModel PkPayment { get; set; }
    }
}
