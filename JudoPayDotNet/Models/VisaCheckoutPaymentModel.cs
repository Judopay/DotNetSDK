using System.Runtime.Serialization;

// ReSharper disable ClassNeverInstantiated.Global

namespace JudoPayDotNet.Models
{
    [DataContract]
    public class VisaCheckoutPaymentModel : PaymentModel
    {
        [DataMember(IsRequired = true)]
        public VisaCheckoutWalletModel Wallet { get; set; }

    }
}