using System.Runtime.Serialization;

// ReSharper disable ClassNeverInstantiated.Global

namespace JudoPayDotNet.Models
{
    [DataContract]
    public class AndroidPaymentModel : PaymentModel
    {
  
        [DataMember(IsRequired = true)]
        public AndroidWalletModel Wallet { get; set; }

    }
}