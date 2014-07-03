using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Models
{
    public abstract class PaymentModel
    {
        public string YourConsumerReference { get; set; }
        public string YourPaymentReference { get; set; }
        public IDictionary<string, string> YourPaymentMetaData { get; set; }
        public string JudoId { get; set; }
        public decimal Amount { get; set; }
        public string CV2 { get; set; }
        public ConsumerLocationModel ConsumerLocation { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}
