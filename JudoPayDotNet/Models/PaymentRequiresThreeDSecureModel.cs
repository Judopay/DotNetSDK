using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Models
{
    public class PaymentRequiresThreeDSecureModel : ITransactionResult
    {
        public string ReceiptId { get; set; }

        public string Result { get; set; }

        public string Message { get; set; }

        public string AcsUrl { get; set; }

        public string Md { get; set; }

        public string PaReq { get; set; }
    }
}
